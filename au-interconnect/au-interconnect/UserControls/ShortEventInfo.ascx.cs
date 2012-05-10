using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AUInterconnect.Configuration;

namespace AUInterconnect.UserControls
{
    public partial class ShortEventInfo : System.Web.UI.UserControl
    {
        protected DateIcon Cal;
        private int eventId;
        private string eventName;
        private string desc;
        private DateTime startTime;

        public ShortEventInfo() { }

        public ShortEventInfo(int eventId, string eventName,
            DateTime startTime, string desc)
        {
            this.eventId = eventId;
            this.eventName = eventName;
            this.desc = desc;
            this.startTime = startTime;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Cal.Date = startTime;
            SetEventName(eventId, eventName);
            EventTime.Text = HttpUtility.HtmlEncode(startTime.ToString("hh:mm tt"));
            EventDesc.Text = PageHelper.TextToHtmlEncode(desc);
        }

        public DateTime StartTime
        {
            get { return startTime; }
            set
            {
                startTime = value;
                Cal.Date = startTime;
                EventTime.Text = HttpUtility.HtmlEncode(startTime.ToString("hh:mm tt"));
            }
        }

        public int EventID
        {
            get { return eventId; }
        }

        public string Desc
        {
            get { return desc; }
            set
            {
                desc = value;
                EventDesc.Text = HttpUtility.HtmlEncode(value);
            }
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