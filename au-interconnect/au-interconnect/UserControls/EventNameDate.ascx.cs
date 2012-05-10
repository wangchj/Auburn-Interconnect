using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AUInterconnect.Configuration;

namespace AUInterconnect.UserControls
{
    public partial class EventNameDate : System.Web.UI.UserControl
    {
        private int eventId;
        private string eventName;
        private DateTime startTime;

        public EventNameDate() { }

        public EventNameDate(int eventId, string eventName, DateTime startTime)
        {
            this.eventId = eventId;
            this.eventName = eventName;
            this.startTime = startTime;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SetEventName(eventId, eventName);
            EventTime.Text = startTime.ToString("MMM d yyyy - h:mm tt");
        }

        public DateTime StartTime
        {
            get { return startTime; }
            set
            {
                startTime = value;
                EventTime.Text = startTime.ToString("MMM d yyyy - h:mm tt");
            }
        }

        public int EventID
        {
            get { return eventId; }
        }

        public void SetEventName(int eventId, string eventName)
        {
            this.eventId = eventId;
            this.eventName = eventName;
            EventNameLink.Text = HttpUtility.HtmlEncode(eventName);
            EventNameLink.NavigateUrl = GetEventDetailsUrl(eventId);
        }
        
        private string GetEventDetailsUrl(int eventId)
        {
            return "~/Events/EventDetails.aspx?" + Const.EventId + "=" + eventId;
        }
    }
}