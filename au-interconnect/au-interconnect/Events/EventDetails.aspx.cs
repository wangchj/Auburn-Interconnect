using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using AUInterconnect.DataModels;
//using log4net;
using AUInterconnect.Configuration;

namespace AUInterconnect.Views.Events
{
    public partial class EventDetails : System.Web.UI.Page
    {
        //private ILog log = LogManager.GetLogger(typeof(_Default));

        protected void Page_Load(object sender, EventArgs e)
        {
#if DEBUG
            //If in debug mode and the current user is null, randomly generate
            //a user so we don't have to manually login every time.
            if (Session[Const.User] == null)
                Session[Const.User] = new DataModels.User(DevConf.DebugUserId);
#endif

            //Get the user.
            DataModels.User user = (DataModels.User)Session[Const.User];
            //Get the event ID from GET.
            string eidStr = Request[Const.EventId];

#if DEBUG
            //If in debug mode and we have no request event ID, randomly set
            //an event ID, so we can see some info on this page.
            if (eidStr == null) eidStr = DevConf.DebugEventIdStr;
#endif

            try
            {
                int eventId = 0;
                if (eidStr == null || !int.TryParse(eidStr, out eventId))
                    Response.Redirect("~/Default.aspx", true);

                //Fill event info
                if (!PopulateEventInfo(eventId))
                    Response.Redirect("~/Default.aspx", true);

                //Hide registration button if user is already registered.
                if (user!=null && EventRegistration.HasRegistration(user.Uid, eventId))
                {
                    RegPanel.Visible = false;
                }
                //Show hidden event full message if event is full.
                if(Event.IsFull(eventId))
                {
                    RegPanel.Visible = false;
                    EventFullPanel.Visible = true;
                    AddToWL.NavigateUrl = "EventFull.aspx?" + Const.EventIdEquals + eventId;
                }
            }
            catch (Exception ex)
            {
                ErrorLit.Text = ex.Message;
            }
        }

        private bool PopulateEventInfo(int eventId)
        {
            string queryStr = "SELECT eventName, startTime, endTime, location, " +
                "descr, hostName, meetTime, meetLocation, transportation, guestLimit, " +
                "costs, equipment FROM [Events] WHERE eventId=@eventId";

            using (SqlConnection con = new SqlConnection(Config.SqlConStr))
            {

                //Command to get event info.
                SqlCommand command = new SqlCommand(queryStr, con);
                command.Parameters.Add(new SqlParameter("eventId", eventId));
                con.Open();
                SqlDataReader reader = command.ExecuteReader(
                    CommandBehavior.SingleRow);

                if (reader.Read())
                {
                    HostName.Text = PageHelper.TextToHtmlEncode(reader["hostName"].ToString());
                    EventCap.Text = EventRegistration.GetEventRegHeadCount(eventId).ToString() + "/" +
                        (reader["guestLimit"] == DBNull.Value ?
                        "No Limit" : reader["guestLimit"].ToString());
                    EventName.Text = PageHelper.TextToHtmlEncode(reader["eventName"].ToString());
                    BigEventTime.Text = ((DateTime)reader["startTime"]).ToString("MMM d, yyyy");
                    StartTime.Text = ((DateTime)reader["startTime"]).ToString("MMM d, yyyy h:mm tt");
                    EndTime.Text = ((DateTime)reader["endTime"]).ToString("MMM d, yyyy h:mm tt");
                    Location.Text = PageHelper.TextToHtmlEncode(reader["location"].ToString());
                    Desc.Text = PageHelper.TextToHtmlEncode(reader["descr"].ToString());
                    MeetTime.Text = ((DateTime)reader["startTime"]).ToString("MMM d, yyyy h:mm tt");
                    MeetLocation.Text = PageHelper.TextToHtmlEncode(reader["meetLocation"].ToString());
                    Transportation.Text = PageHelper.TextToHtmlEncode(reader["transportation"].ToString());
                    Costs.Text = PageHelper.TextToHtmlEncode(reader["costs"].ToString());
                    Equipments.Text = PageHelper.TextToHtmlEncode(reader["equipment"].ToString());

                    //Space
                    //if (reader["maxReg"] == DBNull.Value)
                    //{
                    //    spaceLit.Text = "This event has no limit on the " +
                    //        "number of participants.";
                    //}
                    //else
                    //{
                    //    int maxReg = (int)reader["maxReg"];
                    //    spaceLit.Text = "There are total " + maxReg +
                    //        "spaces for this event.";
                    //}

                    return true;
                }
                else { return false; }
            }
        }

        protected void regBtn_Click(object sender, EventArgs e)
        {
            try
            {
                int eventId = RequestUtil.GetEventId(Request);
                string url;

                if (Event.IsFull(eventId))
                    //If event is full, redirect to event full page.
                    url = "EventFull.aspx?" + Const.EventId + '=' + eventId;
                else
                    //if event is not full, redirect to sign up page.
                    url = "~/Events/Signup.aspx?" + Const.EventId + '=' + eventId;
                
                Response.Redirect(url, true);
            }
            catch (Exception ex)
            {
                //log.Error(ex);
            }
        }
    }
}