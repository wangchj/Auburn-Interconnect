using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using AUInterconnect.DataModels;
//using log4net;
using AUInterconnect.Configuration;

namespace AUInterconnect.Views.Events
{
    public partial class Signup : System.Web.UI.Page
    {
        /// <summary>
        /// The query string name for update action (value = "Update").
        /// </summary>
        public const string QSTRUpdate = "Update";

        //private ILog log = LogManager.GetLogger(typeof(_Default));

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
#if DEBUG
            //If in debug mode and user is null, instantiate a debug user
            //object, so we don't have to login every time we debug.
                DataModels.User debUser = (DataModels.User)Session[Const.User];
            if (debUser == null)
            {
                debUser = new DataModels.User(DevConf.DebugUserId, true);
                Session[Const.User] = debUser;
            }
            debUser.IsAuStudent = true;
#endif
            DataModels.User user = (DataModels.User)Session[Const.User];
                if (user == null || !user.IsAuStudent)
                {
                    string returnUrl = HttpUtility.UrlEncode(
                        Request.Url.ToString());
                    string queryStr = "?" +
                        Const.AuthAuStud + "=1" +
                        "&ReturnUrl=" + returnUrl;
                    string url = "~/User/Login.aspx" + queryStr;
                    Response.Redirect(url, true);
                }

                //Get the event ID
                string eidStr = Request[Const.EventId];
#if DEBUG
            //If in debug mode and request event ID is undefined, assign an
            //event id.
            if (eidStr == null) eidStr = DevConf.DebugEventIdStr;
#endif
                int eventId = 0;
                if (eidStr == null || !int.TryParse(eidStr, out eventId))
                    Response.Redirect("~/Default.aspx", true);

                if (!IsPostBack)
                {
                    PopulateForm(eventId);

                    if (IsNewRegistration)
                    {
                        //Not an update so it's a new registration.

                        regBtn.Visible = true;
                        UpdateButton.Visible = false;

                        //Check if event is full
                        if (Event.IsFull(eventId))
                        {
                            ErrorLit.Text = "We're sorry the event is full.";
                            regBtn.Enabled = false;
                        }
                    }
                    else
                    {
                        //This is an update of existing registraion.

                        regBtn.Visible = false;
                        UpdateButton.Visible = true;
                        FillRegistrationInfo(user.Uid, eventId);
                    }
                }
            }
            catch (Exception ex)
            {
                //ErrorLit.Text = ex.Message;
                //ErrorLit.Text = ex.StackTrace;
            }
        }

        private void FillRegistrationInfo(int userId, int eventId)
        {
            EventRegistration reg = new EventRegistration(userId, eventId);
            HeadCount.Text = reg.HeadCount.ToString();
            VehicleCap.Text = reg.VehicleCapacity.ToString();
            CanDrive.Checked = (reg.VehicleCapacity > 0);
        }

        private void PopulateForm(int eventId)
        {
            string queryStr = "SELECT eventName, hostOrg, hostName, startTime, " +
                "requestDrivers FROM [Events] WHERE eventId=@eventId";

            using (SqlConnection con = new SqlConnection(Config.SqlConStr))
            {
                SqlCommand command = new SqlCommand(queryStr, con);
                command.Parameters.Add(new SqlParameter("eventId", eventId));
                con.Open();
                SqlDataReader reader = command.ExecuteReader(
                    CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    EventName.Text = HttpUtility.HtmlEncode(reader["eventName"].ToString());
                    EventHost.Text = ((reader["hostOrg"] == DBNull.Value) ?
                        HttpUtility.HtmlEncode(reader["hostName"].ToString()) :
                        HttpUtility.HtmlEncode(reader["hostOrg"].ToString()));
                    Date.Text = ((DateTime)reader["startTime"]).ToString("MMM d, yyyy");
                    Time.Text = ((DateTime)reader["startTime"]).ToString("h:mm tt");
                }
            }
        }

        protected void regBtn_Click(object sender, EventArgs e)
        {
            //TODO: validate head count and vehicle cap (not negative).
            //TODO: make sure the user has not registered for this event.
            if (!Page.IsValid)
                return;

            int eventId = RequestUtil.GetEventId(Request);

#if DEBUG
            if (eventId == 0)
                eventId = DevConf.DebugEventId;
#endif

            int headCount = 0;
            int vehicleCap = 0;
            int.TryParse(HeadCount.Text.Trim(), out headCount);
            int.TryParse(VehicleCap.Text.Trim(), out vehicleCap);

            int userId = ((DataModels.User)Session[Const.User]).Uid;

            try
            {
                RegisterEvent(eventId, userId, headCount, vehicleCap);
                //Nav.ReturnToPrevPage(this);
                string url = "EventSignupComplete.aspx";
                Response.Redirect(url, true);
            }
            catch (Exception ex)
            {
                ErrorLit.Text = ex.Message;
            }
        }

        private void RegisterEvent(int eventId, int userId, int headCount,
            int vehicleCap)
        {
            //Check if event is full
            if (Event.IsFull(eventId))
            {
                ErrorLit.Text = "We're sorry the event is full.";
                regBtn.Enabled = false;
                return;
            }

            if (!EventRegistration.EventCanAddCount(eventId, headCount))
            {
                ErrorLit.Text = "We're sorry the number of participants " +
                    "exceeds event capacity.";
                regBtn.Enabled = false;
            }

            string queryStr = "INSERT INTO EventRegs " +
                "(eventId, userId, headCount, vehicleCap) VALUES " +
                "(@eid, @uid, @headCount, @vehicleCap)";

            using (SqlConnection con = new SqlConnection(Config.SqlConStr))
            {
                SqlCommand command = new SqlCommand(queryStr, con);
                command.Parameters.Add(new SqlParameter("eid",
                    eventId));
                command.Parameters.Add(new SqlParameter("uid",
                    userId));
                command.Parameters.Add(new SqlParameter("headCount",
                    headCount));
                command.Parameters.Add(new SqlParameter("vehicleCap",
                    vehicleCap));

                con.Open();
                int r = command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Handles the event when user click on the "Update" button.
        /// </summary>
        /// <param name="sender">Not used.</param>
        /// <param name="e">Not used.</param>
        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            try
            {
                int userId = ((DataModels.User)Session[Const.User]).Uid;

                //Get Event ID
                int eventId = 0;
                bool p = int.TryParse(Request[Const.EventId], out eventId);
                if (!p)
                    ErrorLit.Text = "Invalid event ID";

                int headCount;
                int.TryParse(HeadCount.Text, out headCount);
                if (!p)
                    ErrorLit.Text = "Invalid Head Count";

                int vehCap;
                int.TryParse(VehicleCap.Text, out vehCap);
                if (!p)
                    ErrorLit.Text = "Invalid Vehicle Capacity";

                int updateResult = EventRegistration.UpdateRegistration(userId,
                    eventId, headCount, vehCap);

                switch (updateResult)
                {
                    case 0:
                        Response.Redirect("EventSignupComplete.aspx?" + QSTRUpdate + "=1", true);
                        break;
                    case 1:
                        ErrorLit.Text = "You have no previously registered event.";
                        break;
                    case 2:
                        ErrorLit.Text = "Party size exceed event capacity.";
                        break;
                    default:
                        ErrorLit.Text = "System Error has occurred.";
                        break;
                }
            }
            catch (Exception) { }
        }

        private bool IsNewRegistration
        {
            get
            {
                return !IsUpdate;
            }
        }

        private bool IsUpdate
        {
            get
            {
                return !string.IsNullOrEmpty(Request[QSTRUpdate]) &&
                    Request[QSTRUpdate].Equals("1");
            }
        }

    }
}