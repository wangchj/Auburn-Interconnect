using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace AUInterconnect.DataModels
{
    public class ProposalParser
    {
        private static string phoneRex = @"\(\d{3}\)\d{3}-\d{4}";
        private static string datetimeRex = @"\d{1,2}/\d{1,2}/\d{4} \d{1,2}:\d{1,2} (AM|PM)";
        
        public static Event CreateEvent(Proposal proposal)
        {
            Event e = new Event();
            e.CreatorId = proposal.CreatorId;
            e.ProposalId = proposal.Id;
            e.CreateTime = DateTime.Now;
            e.GuestLimit = GetEventCapacity(proposal.Content);
            e.HostOrg = GetHostOrg(proposal.Content);
            e.HostName = GetContactName(proposal.Content);
            e.HostEmail = GetContactEmail(proposal.Content);
            e.HostPhone = GetContactPhone(proposal.Content);
            e.EventName = GetEventName(proposal.Content);
            e.StartTime = GetEventStartTime(proposal.Content);
            e.EndTime = GetEventEndTime(proposal.Content);
            e.RegDeadline = GetRegDeadline(proposal.Content);
            e.Location = GetEventLocation(proposal.Content);
            e.Descr = GetEventDesc(proposal.Content);
            e.MeetLocation = GetMeetingLocation(proposal.Content);
            e.MeetTime = GetMeetingTime(proposal.Content);
            e.Transportation = GetTrasportation(proposal.Content);
            e.RequestDrivers = GetRequestDrivers(proposal.Content);
            e.Costs = GetCost(proposal.Content);
            e.Equipment = GetEquipment(proposal.Content);
            e.Food = GetFood(proposal.Content);
            e.Other = GetOther(proposal.Content);
            return e;
        }

        public static string GetEventName(string content)
        {
            string rex = @"<h1>(.+?)</h1>";
            Match match = Regex.Match(content, rex, RegexOptions.Singleline);
            if (!match.Success)
                throw new ApplicationException(
                    "Unable to get event name from proposal.");
            return RemoveStrikeViaStyle(RemoveHtmlTags(match.Groups[1].Value)).Trim();
        }

        public static string GetHostOrg(string content)
        {
            string sec = GetSectionClean("Host Organization", content);
            if (string.IsNullOrEmpty(sec))
                return null;
            return sec;
        }

        /// <summary>
        /// Gets the phone number.
        /// </summary>
        /// <param name="content">Content of the proposal</param>
        /// <returns>phone number</returns>
        public static long GetContactPhone(string content)
        {
            string sec = GetContactSection(content);
            Match m = Regex.Match(sec, phoneRex);
            if (!m.Success)
                throw new ApplicationException("Unable to get phone number from proposal.");
            string str = m.Value.Replace("(", "").Replace(")", "").Replace("-", "");
            return long.Parse(str);
        }

        /// <summary>
        /// Gets contact name.
        /// </summary>
        /// <param name="content">Content of the proposal</param>
        /// <returns>Contact name.</returns>
        public static string GetContactName(string content)
        {
            string sec = GetContactSection(content);
            Match m = Regex.Match(sec, phoneRex);
            if (!m.Success)
                throw new ApplicationException("Unable to get contact name from proposal.");
            return sec.Substring(0, m.Index).Trim();
        }

        public static string GetContactEmail(string content)
        {
            string emailRex = @"\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,6}";
            string sec = GetContactSection(content);
            Match m = Regex.Match(sec, emailRex);
            if (!m.Success)
                throw new ApplicationException("Unable to get contact email from proposal.");
            return m.Value;
        }

        /// <summary>
        /// Returns the plain text of contact section.
        /// </summary>
        /// <param name="content">proposal content</param>
        /// <returns>contact section.</returns>
        private static string GetContactSection(string content)
        {
            string sec = GetSectionClean("Contact", content);
            if (string.IsNullOrEmpty(sec))
                throw new ApplicationException("Unable to get contact section from proposal.");
            return sec;
        }

        /// <summary>
        /// Gets the event description.
        /// </summary>
        /// <param name="content">proposal content</param>
        /// <returns>Event description.</returns>
        public static string GetEventDesc(string content)
        {
            string sec = GetSectionClean("Description", content);
            if (sec == null)
                throw new ApplicationException("Unable to get event description from proposal.");
            string result = RemoveHtmlTags(sec).Trim();
            if (result == "")
                throw new ApplicationException("Proposal description is empty.");
            return result;
        }

        /// <summary>
        /// Gets the start date time of the event.
        /// </summary>
        /// <param name="content">proposal content</param>
        /// <returns>start datetime</returns>
        public static DateTime GetEventStartTime(string content)
        {
            string sec = GetSectionClean("Date and Time", content);
            if (sec == null)
                throw new ApplicationException("Unable to get event start time from proposal.");
            Match m = Regex.Match(sec, datetimeRex);
            if (!m.Success)
                throw new ApplicationException("Unable to get event start time from proposal.");
            return DateTime.Parse(m.Value);
        }

        /// <summary>
        /// Gets the end date time of the event.
        /// </summary>
        /// <param name="content">proposal content</param>
        /// <returns>end datetime</returns>
        public static DateTime GetEventEndTime(string content)
        {
            string sec = GetSectionClean("Date and Time", content);
            if (sec == null)
                throw new ApplicationException("Unable to get event end time from proposal.");
            MatchCollection matches = Regex.Matches(sec, datetimeRex);
            if(matches.Count < 2)
                throw new ApplicationException("Unable to get event end time from proposal.");
            return DateTime.Parse(matches[1].Value);
        }

        /// <summary>
        /// Gets registration deadline.
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static DateTime GetRegDeadline(string content)
        {
            string sec = GetSectionClean("Registration Deadline", content);
            if (sec == null)
                throw new ApplicationException("Unable to get registration deadline section from proposal.");
            Match match = Regex.Match(sec, datetimeRex);
            if (!match.Success)
                throw new ApplicationException("Unable to get registration deadline from proposal.");
            return DateTime.Parse(match.Value);
        }

        /// <summary>
        /// Gets meeting time.
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static DateTime GetMeetingTime(string content)
        {
            string sec = GetSectionClean("Meeting Time and Location", content);
            if (sec == null)
                throw new ApplicationException("Unable to get meeting time and location section from proposal.");
            Match match = Regex.Match(sec, datetimeRex);
            if (!match.Success)
                throw new ApplicationException("Unable to get meeting time from proposal.");
            return DateTime.Parse(match.Value);
        }

        /// <summary>
        /// Gets meeting location.
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string GetMeetingLocation(string content)
        {
            string sec = GetSectionClean("Meeting Time and Location", content);
            if (sec == null)
                throw new ApplicationException("Unable to get meeting time and location section from proposal.");
            Match match = Regex.Match(sec, datetimeRex);
            if (!match.Success)
                throw new ApplicationException("Unable to get meeting location from proposal.");
            string loc = sec.Substring(match.Index + match.Length).Trim();
            if (loc.StartsWith("at "))
                loc = loc.Substring(3);
            return char.ToUpper(loc[0]) + loc.Substring(1);
        }

        /// <summary>
        /// Gets the event location.
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string GetEventLocation(string content)
        {
            string sec = GetSectionClean("Event Location", content);
            if (sec == null)
                throw new ApplicationException("Unable to get event location section from proposal.");
            return sec;
        }

        public static string GetTrasportation(string content)
        {
            string sec = GetSectionClean("Transportation", content);
            if (sec == null)
                throw new ApplicationException("Unable to get transportation section from proposal.");
            return sec.Substring(0, sec.IndexOf("Requesting drivers"));
        }

        /// <summary>
        /// Gets the request drivers status.
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static bool GetRequestDrivers(string content)
        {
            string rex = @"Requesting drivers: (Yes|No)";
            Match match = Regex.Match(content, rex);
            if (!match.Success)
                throw new ApplicationException("Unable to get request drivers status.");
            if (match.Groups[1].Value == "Yes")
                return true;
            return false;
        }

        public static string GetEquipment(string content)
        {
            string sec = GetSectionClean("Equipment", content);
            if (string.IsNullOrEmpty(sec))
                return null;
            return sec;
        }

        public static string GetFood(string content)
        {
            string sec = GetSectionClean("Food", content);
            if (string.IsNullOrEmpty(sec))
                return null;
            return sec;
        }

        public static string GetCost(string content)
        {
            string sec = GetSectionClean("Cost", content);
            if (string.IsNullOrEmpty(sec))
                return null;
            return sec;
        }

        public static string GetOther(string content)
        {
            string sec = GetSectionClean("Other Notes", content);
            if (string.IsNullOrEmpty(sec))
                return null;
            return sec;
        }

        public static int GetEventCapacity(string content)
        {
            string sec = GetSectionClean("Event Capacity", content);
            if (string.IsNullOrEmpty(sec))
                return Event.EventCapacityUnlimited;
            int result = 0;
            if(!int.TryParse(sec, out result))
                return Event.EventCapacityUnlimited;
            return result;
        }

        /// <summary>
        /// Gets the body of an h2 section with the following removed:
        /// 1. Leading and trailing whitespaces
        /// 2. HTML tags
        /// 3. Stikethrough text
        /// 4. Decode HTML entities
        /// </summary>
        /// <param name="sectionTitle">The title of the section.</param>
        /// <param name="content">HTML content of the proposal.</param>
        /// <returns>null if section is not found. Empty string if section contains no
        /// no content.</returns>
        public static string GetSectionClean(string sectionTitle, string content)
        {
            string rex = @"<h2>" + sectionTitle + @"</h2>(.+?)(<h2>|$)";
            Match m = Regex.Match(content, rex, RegexOptions.Singleline);
            if (!m.Success)
                return null;
            string result = RemoveStrikeViaStyle(m.Groups[1].Value);
            result = RemoveHtmlTags(result).Trim();
            return HttpUtility.HtmlDecode(result).Trim();
        }

        /// <summary>
        /// Removes HTML tags from a string.
        /// </summary>
        /// <param name="str">Original string.</param>
        /// <returns>Clean string.</returns>
        public static string RemoveHtmlTags(string str)
        {
            string rex = @"<[^>]+>";
            return Regex.Replace(str, rex, "");
        }

        /// <summary>
        /// Remove text that are enclosed by span with style: text-decoration:line;through. 
        /// </summary>
        /// <param name="str">Original string.</param>
        /// <returns>String with strikethrough removed. If the entire string is enclosed within
        /// strikethrough, empty string is returned.</returns>
        /// <exception cref="System.NullReferenceException">When parameter is null.</exception>
        public static string RemoveStrikeViaStyle(string str)
        {
            string rex = @"<span [^>]*?style=""[^""]*?line-through;[^>]+?>.*?</span>";
            return Regex.Replace(str, rex, "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
        }
    }
}
