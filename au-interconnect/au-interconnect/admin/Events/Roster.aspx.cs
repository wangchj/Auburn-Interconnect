using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AUInterconnect.DataModels;

namespace AUInterconnect.Views.admin.Events
{
    public partial class Roster : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //Set Title
                foreach (DataRowView rowView in SqlDataSource2.Select(DataSourceSelectArguments.Empty))
                {
                    EventNameLabel.Text = rowView[0].ToString() + " Roster";
                }

                //Footer
                GridView1.Columns[0].FooterText = "Total";

                //Get total head count
                int eventId = RequestUtil.GetEventId(Request);
                GridView1.Columns[1].FooterText = EventRegistration.GetEventRegHeadCount(eventId).ToString();
            
                //Total carpool capacity
                GridView1.Columns[2].FooterText = EventRoster.GetEventTotalCarpoolCapacity(eventId).ToString();
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Click event of Attend button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                string[] array = btn.ToolTip.Split('-');
                int eventId = int.Parse(array[0].Trim());
                int userId = int.Parse(array[1].Trim());
                EventRegistration.ForceInsert(eventId, userId, 1, false, 0);
                WaitingList.Remove(userId, eventId);
                GridView1.DataBind();
                GridView2.DataBind();
            }
            catch (Exception)
            {

            }
        }

        protected void RemoveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                string[] array = btn.ToolTip.Split('-');
                int eventId = int.Parse(array[0].Trim());
                int userId = int.Parse(array[1].Trim());
                WaitingList.Remove(userId, eventId);
                GridView2.DataBind();
            }
            catch (Exception)
            {

            }
        }
    }
}