using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data.SqlClient;
using AUInterconnect.Configuration;
using AUInterconnect.Utilities;

namespace AUInterconnect.DataModels
{
    public class Proposal
    {
        public int Id { get; private set; }
        public DateTime CreateTime { get; set; }
        public int CreatorId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool Approved { get; set; }
        public bool Declined { get; set; }


        public Proposal(int id, DateTime createTime, int creatorId, string title,
            string content, bool approved, bool declined)
        {
            Id = id;
            CreateTime = createTime;
            CreatorId = creatorId;
            Title = title.Trim();
            Content = content.Trim();
            Approved = approved;
            Declined = declined;
        }

        //public void Save()
        //{
        //    string queryStr = "UPDATE Proposals SET createTime=@time, creatorId=@creator " +
        //        "title=@title, content=@content, approved=@app, declined=@dec " +
        //        "WHERE proposalId=@proposalId";
        //    using (SqlConnection con = new SqlConnection(Config.SqlConStr))
        //    {
        //        SqlCommand command = new SqlCommand(queryStr, con);
        //        command.Parameters.Add(new SqlParameter("proposalId", Id));
        //        command.Parameters.Add(new SqlParameter("time", CreateTime));
        //        command.Parameters.Add(new SqlParameter("creator", CreatorId));
        //        command.Parameters.Add(new SqlParameter("title", Title));
        //        command.Parameters.Add(new SqlParameter("content", Content));
        //        command.Parameters.Add(new SqlParameter("app", Approved));
        //        command.Parameters.Add(new SqlParameter("dec", Declined));
        //        con.Open();
        //        command.ExecuteNonQuery();
        //    }
        //}

        public static void ValidateFields(Proposal proposal)
        {
            if (string.IsNullOrEmpty(proposal.Title))
                throw new ApplicationException("Proposal title is null or empty.");
            if (string.IsNullOrEmpty(proposal.Content))
                throw new ApplicationException("Proposal content is null or empty.");
        }

        public static int Insert(int creatorId, Event _event, string tempate)
        {
            return Insert(creatorId, _event.EventName,
                MakeProposalHtmlContent(_event, tempate));
        }

        public static string MakeProposalHtmlContent(Event ev, string template)
        {
            StringBuilder result = new StringBuilder(template);
            result.Replace("[EventName]", ev.EventName);
            result.Replace("[HostOrg]", GetString(ev.HostOrg));
            result.Replace("[ContactName]", ev.HostName);
            result.Replace("[ContactPhone]", FormatPhoneNum(ev.HostPhone));
            result.Replace("[ContactEmail]", ev.HostEmail);
            result.Replace("[EventDescription]", ev.Descr);
            result.Replace("[StartTime]", ev.StartTime.ToString("g"));
            result.Replace("[EndTime]", ev.EndTime.ToString("g"));
            result.Replace("[MeetTime]", ev.MeetTime.ToString("g"));
            result.Replace("[MeetingLocation]", ev.MeetLocation);
            result.Replace("[EventLocation]", ev.Location);
            result.Replace("[Transporation]", ev.Transportation);
            result.Replace("[RequestDrivers]", ev.RequestDrivers ? "Yes" : "No");
            result.Replace("[Equipment]", GetString(ev.Equipment));
            result.Replace("[Food]", GetString(ev.Food));
            result.Replace("[Cost]", GetString(ev.Costs));
            result.Replace("[Other]", GetString(ev.Other));
            result.Replace("[EventCapacity]", GetEventCapacityString(ev.GuestLimit));
            result.Replace("[RegDeadline]", ev.RegDeadline.ToString("g"));
            return result.ToString();
        }

        /// <summary>
        /// Return emtpy string is object is null.
        /// </summary>
        /// <param name="o">object</param>
        /// <returns>ToString() if object is not null; or empty string.</returns>
        public static string GetString(object o)
        {
            if (o == null)
                return string.Empty;
            return o.ToString();
        }

        private static string GetEventCapacityString(int capacity)
        {
            if (capacity == Event.EventCapacityUnlimited)
                return "No limit";
            return capacity.ToString();
        }

        /// <summary>
        /// Inserts a new record into proposals table.
        /// </summary>
        /// <param name="creatorId">The ID of the user who create the proposal.</param>
        /// <param name="title">The title of the proposal.</param>
        /// <param name="content">The HTML content of the proposal</param>
        /// <returns>The number of recrds affected; if not 1 then error.</returns>
        /// <exception cref="ArgumentException">If title or content is null.</exception>
        public static int Insert(int creatorId, string title, string content)
        {
            if (string.IsNullOrEmpty(title))
                throw new ArgumentException("Proposal title is null or empty.");
            if (string.IsNullOrEmpty(content))
                throw new ArgumentException("Proposal content is null or empty.");

            string query = "INSERT INTO Proposals(createTime,creatorId,title," +
                "content,approved,declined)VALUES(@createTime,@creatorId,@title," +
                "@content,@approved,@declined)";
            using (SqlConnection con = new SqlConnection(Config.SqlConStr))
            {
                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.Add(new SqlParameter("createTime", DateTime.Now));
                command.Parameters.Add(new SqlParameter("creatorId", creatorId));
                command.Parameters.Add(new SqlParameter("title", title));
                command.Parameters.Add(new SqlParameter("content", content));
                command.Parameters.Add(new SqlParameter("approved", false));
                command.Parameters.Add(new SqlParameter("declined", false));
                con.Open();
                return command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Get the number of proposals that are pending for review.
        /// </summary>
        /// <remarks>
        /// Pending proposals are not accepted and not declined.
        /// </remarks>
        public static int GetPendingProposalCount()
        {
            string queryStr = "SELECT COUNT(*) FROM Proposals WHERE approved=0 AND declined=0";
            using (SqlConnection con = new SqlConnection(Config.SqlConStr))
            {
                SqlCommand command = new SqlCommand(queryStr, con);
                con.Open();
                return (int)command.ExecuteScalar();
            }
        }

        /// <summary>
        /// Gets the HTML content of a proposal.
        /// </summary>
        /// <param name="proposalId">Proposal ID</param>
        /// <returns>null if proposal does not exist</returns>
        public static string GetContent(int proposalId)
        {
            string queryStr = "SELECT content FROM Proposals WHERE proposalId=@id";
            using (SqlConnection con = new SqlConnection(Config.SqlConStr))
            {
                SqlCommand command = new SqlCommand(queryStr, con);
                command.Parameters.Add(new SqlParameter("id", proposalId));
                con.Open();
                object obj = command.ExecuteScalar();
                if (obj == DBNull.Value)
                    return null;
                return (string)obj;
            }
        }

        /// <summary>
        /// Updates the HTML content of a proposal.
        /// </summary>
        /// <param name="proposalId">Proposal ID.</param>
        /// <param name="content">HTML content</param>
        /// <returns>true if update successful; false if the number of rows updated
        /// is not 1.</returns>
        /// <exception cref="ArgumentException">content is null or empty.</exception>
        public static bool UpdateContent(int proposalId, string content)
        {
            if (string.IsNullOrEmpty(content))
                throw new ArgumentException("Proposal content is null or empty.");

            string queryStr = "UPDATE Proposals SET content=@content " +
                "WHERE proposalId=@id";
            using (SqlConnection con = new SqlConnection(Config.SqlConStr))
            {
                SqlCommand command = new SqlCommand(queryStr, con);
                command.Parameters.Add(new SqlParameter("id", proposalId));
                command.Parameters.Add(new SqlParameter("content", content));
                con.Open();
                if (command.ExecuteNonQuery() == 1)
                    return true;
                return false;
            }
        }

        /// <summary>
        /// Gets an instance of Proposal from the database.
        /// </summary>
        /// <param name="proposalId">Proposal ID</param>
        /// <returns>null if proposal does not exist.</returns>
        public static Proposal GetProposal(int proposalId)
        {
            string queryStr = "SELECT * FROM Proposals WHERE proposalId=@id";
            using (SqlConnection con = new SqlConnection(Config.SqlConStr))
            {
                SqlCommand command = new SqlCommand(queryStr, con);
                command.Parameters.Add(new SqlParameter("id", proposalId));
                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return new Proposal(
                        (int)reader["proposalId"],
                        (DateTime)reader["createTime"],
                        (int)reader["creatorId"],
                        (string)reader["title"],
                        (string)reader["content"],
                        (bool)reader["approved"],
                        (bool)reader["declined"]);
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Formats a phone number.
        /// </summary>
        /// <param name="phone">The phone number in long format.</param>
        /// <returns>Formatted phone number string.</returns>
        public static string FormatPhoneNum(long phone)
        {
            StringBuilder s = new StringBuilder(phone.ToString());
            s.Insert(0, '(').Insert(4, ')').Insert(8, '-');
            return s.ToString();
        }

        /// <summary>
        /// Sends the proposal updated email to the event proposer.
        /// </summary>
        /// <param name="proposalId">Proposal ID</param>
        public static void SendUpdateEmail(int proposalId)
        {
            int creatorId = GetProposalCreatorId(proposalId);
            string emailAddr = User.GetEmailAddr(creatorId);
            string subject = "InterConnect Event Proposal Modified";
            string message = "One of your event proposal has been modified. " +
                "Please review and contact Len Vining (vininlj@auburn.edu).";
            Mailer.Send(emailAddr, subject, message);
        }

        /// <summary>
        /// Gets the proposer user ID.
        /// </summary>
        /// <param name="proposalId">Proposal ID</param>
        /// <returns>Proposal creator ID.</returns>
        /// <exception cref="System.ApplicationException">If proposal does not exist.</exception>
        public static int GetProposalCreatorId(int proposalId)
        {
            string queryStr = "SELECT creatorId FROM Proposals WHERE proposalId=@id";
            using (SqlConnection con = new SqlConnection(Config.SqlConStr))
            {
                SqlCommand command = new SqlCommand(queryStr, con);
                command.Parameters.Add(new SqlParameter("id", proposalId));
                con.Open();
                object result = command.ExecuteScalar();
                if (result == null)
                    throw new ApplicationException("Proposal with id " + proposalId +
                        " does not exist.");
                return (int)result;
            }
        }

        /// <summary>
        /// Approves a proposal and creates an event.
        /// </summary>
        /// <param name="proposalId">The proposal to be approved.</param>
        public static void Approve(int proposalId)
        {
            Event e = ProposalParser.CreateEvent(Proposal.GetProposal(proposalId));
            Event.Insert(e);

            string queryStr = "UPDATE Proposals SET approved=@approved " +
                "WHERE proposalId=@id";
            using (SqlConnection con = new SqlConnection(Config.SqlConStr))
            {
                SqlCommand command = new SqlCommand(queryStr, con);
                command.Parameters.Add(new SqlParameter("id", proposalId));
                command.Parameters.Add(new SqlParameter("approved", true));
                con.Open();
                command.ExecuteNonQuery();
            }

            //SendApprovalEmail(proposalId);
        }

        private static void SendApprovalEmail(int proposalId)
        {
            string addr = User.GetEmailAddr(Proposal.GetProposalCreatorId(proposalId));
            string sub = "Auburn InterConnect Proposal Approved";
            string msg = "Your event proposal has been approved and an event has been created.";
            Mailer.Send(addr, sub, msg);
        }
    }
}