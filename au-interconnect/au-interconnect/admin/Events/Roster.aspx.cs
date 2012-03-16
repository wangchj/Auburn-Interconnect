using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AUInterconnect.DataModel;

namespace AUInterconnect.admin.Events
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
    }
}