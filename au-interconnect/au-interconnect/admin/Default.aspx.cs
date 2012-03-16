using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AUInterconnect.admin
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ActiveEventsCount.Text = Event.GetActiveEventCount().ToString() + ActiveEventsCount.Text;
            UpcomingEventsCount.Text = Event.GetUpcomingEventCount().ToString() + UpcomingEventsCount.Text;
            UnapprovedEventsCount.Text = Event.GetUpcomingUnapprovedEventCount().ToString() +
                UnapprovedEventsCount.Text;
            DeclinedEventsCount.Text = Event.GetAllDeclinedEventCount().ToString() +
                DeclinedEventsCount.Text;
            TotalEventsCount.Text = Event.GetTotalEventCount().ToString() + TotalEventsCount.Text;

            TotalUserCount.Text = AUInterconnect.User.GetAllUserCount().ToString() + TotalUserCount.Text;
        }
    }
}