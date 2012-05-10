using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using AUInterconnect.UserControls;
using AUInterconnect.Configuration;
using AUInterconnect.Configuration;

namespace AUInterconnect.Views.User
{
    public partial class UserRegisteredEvents : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

#if DEBUG
            if (Session[Const.User] == null)
                Session[Const.User] = new DataModels.User(1, true);
#endif

            if (Session[Const.User] == null)
            {
                string returnUrl = HttpUtility.UrlEncode(
                    Request.Url.ToString());
                string url = "~/User/Login.aspx?ReturnUrl=" + returnUrl;
                Response.Redirect(url, true);
            }

            DataModels.User user = (DataModels.User)Session[Const.User];
            UserID.Value = user.Uid.ToString();

            //Load eventes from database
            try
            {
                using (SqlDataReader reader = GetUserRegisteredEvents())
                {
                    while (reader.Read())
                    {

                        ShortEventInfo eventInfo =
                            (ShortEventInfo)LoadControl("~/UserControls/ShortEventInfo.ascx");
                        eventInfo.SetEventName((int)reader["eventId"],
                            reader["eventName"].ToString());
                        eventInfo.StartTime = (DateTime)reader["startTime"];
                        eventInfo.Desc = reader["descr"].ToString();

                        //string eventUrl = "Events/EventDetails.aspx?" +
                        //    Const.EventId + "=" + reader["eventId"];
                        //EventEntryCntrl entry =
                        //    (EventEntryCntrl)LoadControl("EventEntryCntrl.ascx");
                        //entry.EventTitle = reader["eventName"].ToString();
                        //entry.EventUrl = eventUrl;
                        //entry.StartTime = (DateTime)reader["startTime"];

                        TableCell cell = new TableCell();
                        TableRow row = new TableRow();
                        cell.Controls.Add(eventInfo);
                        cell.CssClass = "eventCell";
                        row.Cells.Add(cell);
                        EventListTable.Rows.Add(row);

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //msgLbl.Text = ex.Message;
            }
        }

        private SqlDataReader GetUserRegisteredEvents()
        {
            string queryStr = "SELECT [Events].eventId, eventName, startTime, descr " +
                "FROM [Events] INNER JOIN EventRegs " +
                "ON [Events].eventId = EventRegs.eventId " +
                "WHERE userId=@userId AND endTime>@now " +
                "ORDER BY [Events].startTime";

            DataModels.User user = (DataModels.User)Session[Const.User];

            SqlConnection con = new SqlConnection(Config.SqlConStr);
            SqlCommand command = new SqlCommand(queryStr, con);
            command.Parameters.Add(new SqlParameter("userId", user.Uid));
            command.Parameters.Add(new SqlParameter("now", DateTime.Now));
            con.Open();
            return command.ExecuteReader();
        }

        [System.Web.Services.WebMethod()]
        public static bool RemoveRegistration(int eventId, int userId)
        {
            //TODO: can anyone delete an event for another person?

            string queryStr = "DELETE FROM EventRegs WHERE eventId=@eid AND userId=@uid";
            SqlConnection con = new SqlConnection(Config.SqlConStr);
            SqlCommand command = new SqlCommand(queryStr, con);
            command.Parameters.Add(new SqlParameter("uid", userId));
            command.Parameters.Add(new SqlParameter("eid", eventId));

            try
            {
                con.Open();
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }
    }
}