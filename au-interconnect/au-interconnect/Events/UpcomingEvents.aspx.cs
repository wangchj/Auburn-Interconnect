using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using AUInterconnect.UserControls;
using AUInterconnect.Configuration;

namespace AUInterconnect.Views.Events
{
    public partial class UpcomingEvents : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            MakeEventsTable();
        }

        private void MakeEventsTable()
        {
            //Load eventes from database
            try
            {
                using (SqlDataReader reader = GetApprovedFutureEvents())
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

        private SqlDataReader GetApprovedFutureEvents()
        {
            string queryStr = "SELECT eventId, eventName, startTime, descr " + 
                "FROM [Events] " +
                "WHERE approved=1 AND endTime>@now " +
                "ORDER BY [Events].startTime";

            SqlConnection con = new SqlConnection(Config.SqlConStr);
            SqlCommand command = new SqlCommand(queryStr, con);
            command.Parameters.Add(new SqlParameter("now", DateTime.Now));
            con.Open();
            return command.ExecuteReader();
        }
    }
}