using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using AUInterconnect.Configuration;

namespace AUInterconnect.DataModels
{
    public class Event
    {
        public const int EventCapacityUnlimited = -2;

        public int EventId { get; set; }
        public int CreatorId { get; set; }
        public int ProposalId { get; set; }
        public DateTime CreateTime { get; set; }
        public int GuestLimit { get; set; }
        public string HostOrg { get; set; }
        public string HostName { get; set; }
        public string HostEmail { get; set; }
        public long HostPhone { get; set; }
        public string EventName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime RegDeadline { get; set; }
        public string Location { get; set; }
        public string Descr { get; set; }
        public string MeetLocation { get; set; }
        public DateTime MeetTime { get; set; }
        public string Transportation { get; set; }
        public bool RequestDrivers { get; set; }
        public string Costs { get; set; }
        public string Equipment { get; set; }
        public string Food { get; set; }
        public string Other { get; set; }


        /// <summary>
        /// Checks if an event is full.
        /// </summary>
        /// <param name="eventId">Event ID</param>
        /// <returns>true if event is full; false otherwise.
        /// </returns>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="ApplicationException"></exception>
        public static bool IsFull(int eventId)
        {
            int maxRegCount = GetGuestLimit(eventId);
            if (maxRegCount == -1)
                throw new ApplicationException(
                    "Event does not exist");
            if (maxRegCount == -2)
                return false;

            return GetRegCount(eventId) >= maxRegCount;
        }

        /// <summary>
        /// Gets the maximum number of registrations for an event.
        /// </summary>
        /// <param name="eventId">The id of the event</param>
        /// <returns>
        /// -1 if event is not found;
        /// -2 if maxreg is null (EventCapacityUnlimited);
        /// event capacity otherwise</returns>
        /// <exception cref="SqlException"></exception>
        public static int GetGuestLimit(int eventId)
        {
            if (!Exists(eventId))
                return -1;

            string queryStr = "SELECT guestLimit FROM [Events] WHERE eventId=@eventId";
            using(SqlConnection con = new SqlConnection(Config.SqlConStr))
            {
                SqlCommand command = new SqlCommand(queryStr, con);
                command.Parameters.Add(new SqlParameter("eventId", eventId));
                con.Open();
                object obj = command.ExecuteScalar();
                if (obj == null || obj == DBNull.Value)
                    return EventCapacityUnlimited;
                return (int)obj;
            }
        }

        /// <summary>
        /// Gets the number of registered of this event.
        /// </summary>
        /// <param name="eventId">The event ID</param>
        /// <returns>The number of registrations</returns>
        /// <remarks>The number of registration is not the number of
        /// participants of this event since each registration could have
        /// multiple participants.
        /// 
        /// This method does not check if the event exist in
        /// the database.</remarks>
        public static int GetRegCount(int eventId)
        {
            //TODO: What if event does not exist

            string queryStr = "SELECT COUNT(*) FROM EventRegs " +
                "WHERE eventId=@eventId";
            using (SqlConnection con = new SqlConnection(Config.SqlConStr))
            {
                SqlCommand command = new SqlCommand(queryStr, con);
                command.Parameters.Add(new SqlParameter("eventId", eventId));
                con.Open();
                object obj = command.ExecuteScalar();
                if (obj == null)
                    return -1;
                return (int)obj;
            }
        }


        public static int GetHeadCount(int eventId)
        {
            //TODO: consider moving this method to a new Registration class.
            //TODO: what if event does not exist?

            string queryStr = "SELECT SUM(headCount) FROM EventRegs " +
                "WHERE eventId=@eventId";
            using (SqlConnection con = new SqlConnection(Config.SqlConStr))
            {
                SqlCommand command = new SqlCommand(queryStr, con);
                command.Parameters.Add(new SqlParameter("eventId", eventId));
                con.Open();
                object obj = command.ExecuteScalar();
                if (obj == null)
                    return -1;
                return (int)obj;
            }
        }

        public static bool Exists(int eventId)
        {
            string queryStr = "SELECT COUNT(*) FROM [Events] " +
                "WHERE eventId=@eventId";
            using (SqlConnection con = new SqlConnection(Config.SqlConStr))
            {
                SqlCommand command = new SqlCommand(queryStr, con);
                command.Parameters.Add(new SqlParameter("eventId", eventId));
                con.Open();
                object obj = command.ExecuteScalar();
                return ((int)obj > 0);
            }
        }

        /// <summary>
        /// Check if a user has created an event. This is a rough check of
        /// if the user is a host.
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns>
        /// true if user created an event; false otherwise.
        /// </returns>
        public static bool UserProposedEvent(int userId)
        {
            string queryStr = "SELECT COUNT(*) FROM [Events] " +
                "WHERE creatorId=@userId";
            using (SqlConnection con = new SqlConnection(Config.SqlConStr))
            {
                SqlCommand command = new SqlCommand(queryStr, con);
                command.Parameters.Add(new SqlParameter("userId", userId));
                con.Open();
                object obj = command.ExecuteScalar();
                return ((int)obj >= 1);
            }
        }

        /// <summary>
        /// Set the approved to true and declined to false for an event.
        /// </summary>
        /// <param name="eventId">The ID of the event.</param>
        public static void ApproveEvent(int eventId)
        {
            string queryStr = "UPDATE [Events] SET approved=1, declined=0 " +
                "WHERE eventId=@eventId";
            using (SqlConnection con = new SqlConnection(Config.SqlConStr))
            {
                SqlCommand command = new SqlCommand(queryStr, con);
                command.Parameters.Add(new SqlParameter("eventId", eventId));
                con.Open();
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Set the approved to false and declined to true for an event.
        /// </summary>
        /// <param name="eventId">The ID of the event.</param>
        public static void DeclineEvent(int eventId)
        {
            string queryStr = "UPDATE [Events] SET approved=0, declined=1 " +
                "WHERE eventId=@eventId";
            using (SqlConnection con = new SqlConnection(Config.SqlConStr))
            {
                SqlCommand command = new SqlCommand(queryStr, con);
                command.Parameters.Add(new SqlParameter("eventId", eventId));
                con.Open();
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Get the total number of events in the Events table.
        /// </summary>
        /// <returns>Count</returns>
        public static int GetTotalEventCount()
        {
            string queryStr = "SELECT COUNT(*) FROM [Events]";
            using (SqlConnection con = new SqlConnection(Config.SqlConStr))
            {
                SqlCommand command = new SqlCommand(queryStr, con);
                con.Open();
                object obj = command.ExecuteScalar();
                return (int)obj;
            }
        }

        /// <summary>
        /// Get the total number of events that are happening now.
        /// </summary>
        /// <returns>Event count</returns>
        public static int GetActiveEventCount()
        {
            string queryStr = "SELECT COUNT(*) FROM [Events] " +
                "WHERE startTime < {fn now()} AND endTime >= {fn now()} AND " +
                "approved=1 AND declined=0";
            using (SqlConnection con = new SqlConnection(Config.SqlConStr))
            {
                SqlCommand command = new SqlCommand(queryStr, con);
                con.Open();
                object obj = command.ExecuteScalar();
                return (int)obj;
            }
        }

        /// <summary>
        /// Get the number of approved upcoming events.
        /// </summary>
        /// <returns>Event count</returns>
        public static int GetUpcomingEventCount()
        {
            string queryStr = "SELECT COUNT(*) FROM [Events] " +
                "WHERE startTime > {fn now()} AND approved=1 AND declined=0";
            using (SqlConnection con = new SqlConnection(Config.SqlConStr))
            {
                SqlCommand command = new SqlCommand(queryStr, con);
                con.Open();
                object obj = command.ExecuteScalar();
                return (int)obj;
            }
        }

        public static int GetUpcomingUnapprovedEventCount()
        {
            string queryStr = "SELECT COUNT(*) FROM [Events] " +
                "WHERE startTime > {fn now()} AND approved=0 AND declined=0";
            using (SqlConnection con = new SqlConnection(Config.SqlConStr))
            {
                SqlCommand command = new SqlCommand(queryStr, con);
                con.Open();
                object obj = command.ExecuteScalar();
                return (int)obj;
            }
        }

        public static int GetAllDeclinedEventCount()
        {
            string queryStr = "SELECT COUNT(*) FROM [Events] " +
                "WHERE approved=0 AND declined=1";
            using (SqlConnection con = new SqlConnection(Config.SqlConStr))
            {
                SqlCommand command = new SqlCommand(queryStr, con);
                con.Open();
                object obj = command.ExecuteScalar();
                return (int)obj;
            }
        }

        /// <summary>
        /// Inserts a new Event into the database.
        /// </summary>
        /// <param name="e">The event to be inserted.</param>
        /// <returns>The number of rows affected from this operation.</returns>
        public static int Insert(Event e)
        {
            string queryStr =
                "INSERT INTO Events (creatorId, proposalId, createTime, guestLimit, hostOrg, " +
                "hostName, hostEmail, hostPhone, eventName, startTime, endTime, regDeadline, " +
                "location, descr, meetLocation, meetTime, transportation, requestDrivers, " +
                "costs, equipment, food, other, approved) " +
                "VALUES (@creatorId, @proposalId, @createTime, @guestLimit, @hostOrg, @hostName, " +
                "@hostEmail, @hostPhone, @eventName, @startTime, @endTime, @regDeadline, " +
                "@location, @descr, @meetLocation, @meetTime, @transportation, " +
                "@requestDrivers, @costs, @equipment, @food, @other, @approved)";

            using (SqlConnection con = new SqlConnection(Config.SqlConStr))
            {
                SqlCommand command = new SqlCommand(queryStr, con);
                command.Parameters.Add(new SqlParameter("creatorId", e.CreatorId));
                command.Parameters.Add(new SqlParameter("proposalId", e.ProposalId));
                command.Parameters.Add(new SqlParameter("createTime", DateTime.Now));
                command.Parameters.Add(e.GuestLimit == Event.EventCapacityUnlimited ?
                   new SqlParameter("guestLimit", DBNull.Value) :
                   new SqlParameter("guestLimit", e.GuestLimit));
                command.Parameters.Add(new SqlParameter("hostOrg", GetNullableValue(e.HostOrg)));
                command.Parameters.Add(new SqlParameter("hostName", e.HostName));
                command.Parameters.Add(new SqlParameter("hostEmail", e.HostEmail));
                command.Parameters.Add(new SqlParameter("hostPhone", e.HostPhone));
                command.Parameters.Add(new SqlParameter("eventName", e.EventName));
                command.Parameters.Add(new SqlParameter("startTime", e.StartTime));
                command.Parameters.Add(new SqlParameter("endTime", e.EndTime));
                command.Parameters.Add(new SqlParameter("regDeadline", e.RegDeadline));
                command.Parameters.Add(new SqlParameter("location", e.Location));
                command.Parameters.Add(new SqlParameter("descr", e.Descr));
                command.Parameters.Add(new SqlParameter("meetLocation", e.MeetLocation));
                command.Parameters.Add(new SqlParameter("meetTime", e.MeetTime));
                command.Parameters.Add(new SqlParameter("transportation", e.Transportation));
                command.Parameters.Add(new SqlParameter("requestDrivers", e.RequestDrivers));
                command.Parameters.Add(new SqlParameter("costs", GetNullableValue(e.Costs)));
                command.Parameters.Add(new SqlParameter("equipment", GetNullableValue(e.Equipment)));
                command.Parameters.Add(new SqlParameter("food", GetNullableValue(e.Food)));
                command.Parameters.Add(new SqlParameter("other", GetNullableValue(e.Other)));
                command.Parameters.Add(new SqlParameter("approved", true));
                con.Open();
                return command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Returns DBNull.Value if o is null; else o.
        /// </summary>
        /// <param name="o">object to be checked.</param>
        /// <returns>DBNull.Value or o</returns>
        private static object GetNullableValue(object o)
        {
            return o == null ? DBNull.Value : o;
        }
    }
}