using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AUInterconnect.Configuration;

namespace AUInterconnect.UserControls
{
    public partial class HostEvent : System.Web.UI.UserControl
    {
        protected EventNameDate NameDate;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public DateTime StartTime
        {
            set
            {
                NameDate.StartTime = value;
            }
        }

        public void SetEventName(int eventId, string eventName)
        {
            NameDate.SetEventName(eventId, eventName);
            RosterLink.NavigateUrl = "~/Host/EventRoster.aspx?" +
                Const.EventId + "=" + eventId;
        }
    }
}