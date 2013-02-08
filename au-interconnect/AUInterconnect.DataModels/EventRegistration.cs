using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using AUInterconnect;
using AUInterconnect.Configuration;

namespace AUInterconnect.DataModels
{
    public class EventRegistration
    {
        private int userId;
        private int eventId;
        private int headCount;
        private int vehicleCap;

        public EventRegistration(int userId, int eventId)
        {
            this.userId = userId;
            this.eventId = eventId;
            SqlDataReader reader = GetRegistrationReader(userId, eventId);
            if (reader.Read())
            {
                headCount = (int)reader["headCount"];
                vehicleCap = (int)reader["vehicleCap"];
            }
            else
                throw new ApplicationException("Registration invalid.");

            reader.Close();
        }

        public static SqlDataReader GetRegistrationReader(int userId,
            int eventId)
        {
            string queryStr = "SELECT * FROM EventRegs WHERE userId=@uid AND eventId=@eid";

            SqlConnection con = new SqlConnection(Config.SqlConStr);
            
            SqlCommand command = new SqlCommand(queryStr, con);
            command.Parameters.Add(new SqlParameter("eid", eventId));
            command.Parameters.Add(new SqlParameter("uid", userId));
            con.Open();
            SqlDataReader reader = command.ExecuteReader(
                CommandBehavior.SingleRow);
            return reader;
        }

        public int UserID
        {
            get { return userId; }
        }

        public int EventID
        {
            get { return eventId; }
        }

        public int HeadCount
        {
            get { return headCount; }
        }

        public int VehicleCapacity
        {
            get { return vehicleCap; }
        }

        public static bool HasRegistration(int userId, int eventId)
        {
            string queryStr =
                "SELECT count(*) FROM EventRegs WHERE eventId=@eid AND userId=@uid";

            using (SqlConnection con = new SqlConnection(Config.SqlConStr))
            {
                SqlCommand command = new SqlCommand(queryStr, con);
                command.Parameters.Add(new SqlParameter("eid", eventId));
                command.Parameters.Add(new SqlParameter("uid", userId));
                con.Open();
                int count = (int)command.ExecuteScalar();
                if (count < 1)
                    return false;
                return true;
            }
        }

        /// <summary>
        /// Updates an event registration.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="eventId"></param>
        /// <param name="headCount"></param>
        /// <param name="vehCap"></param>
        /// <returns>
        /// 0 - Update successful
        /// 1 - User is not registered for the event.
        /// 2 - Party size exceed event capacity.
        /// </returns>
        public static int UpdateRegistration(int userId, int eventId,
            int headCount, int vehCap)
        {
            if (!HasRegistration(userId, eventId))
                return 1;

            //Check if this change exceed event capacity.
            int prev = GetEventRegistrationPartySize(userId, eventId);
            int cap = Event.GetGuestLimit(eventId);
            int total = GetEventRegCount(eventId);
            if (cap != Event.EventCapacityUnlimited && total - prev + headCount > cap)
                return 2;

            string queryStr =
                "UPDATE EventRegs SET headCount=@headCount, vehicleCap=@vehCap " +
                "WHERE eventId=@eid AND userId=@uid";
            using (SqlConnection con = new SqlConnection(Config.SqlConStr))
            {
                SqlCommand command = new SqlCommand(queryStr, con);
                command.Parameters.Add(new SqlParameter("headCount", headCount));
                command.Parameters.Add(new SqlParameter("vehCap", vehCap));
                command.Parameters.Add(new SqlParameter("eid", eventId));
                command.Parameters.Add(new SqlParameter("uid", userId));

                try
                {
                    con.Open();
                    command.ExecuteNonQuery();
                    return 0;
                }
                catch (Exception)
                {
                    return 3;
                }
            }

        }

        /// <summary>
        /// Gets the registration count of an event.
        /// </summary>
        /// <remarks>
        /// Currently does not check if event exist.
        /// </remarks>
        /// <param name="eventId">ID of the event.</param>
        /// <returns>Count of registration.</returns>
        public static int GetEventRegCount(int eventId)
        {
            string queryStr = "SELECT COUNT(*) FROM EventRegs WHERE eventId=@eventId";
            using (SqlConnection con = new SqlConnection(Config.SqlConStr))
            {
                SqlCommand command = new SqlCommand(queryStr, con);
                command.Parameters.Add(new SqlParameter("eventId", eventId));
                con.Open();
                return (int)command.ExecuteScalar();
            }
        }

        /// <summary>
        /// Gets the total head count for an event. This is the sum of party sizes
        /// for all registrations.
        /// </summary>
        /// <param name="eventId">The event ID</param>
        /// <returns>The total head count</returns>
        public static int GetEventRegHeadCount(int eventId)
        {
            string queryStr = "SELECT SUM(headCount) FROM EventRegs WHERE eventId=@eventId";
            using (SqlConnection con = new SqlConnection(Config.SqlConStr))
            {
                SqlCommand command = new SqlCommand(queryStr, con);
                command.Parameters.Add(new SqlParameter("eventId", eventId));
                con.Open();
                object result = command.ExecuteScalar();
                if (result == DBNull.Value)
                    return 0;
                return (int)command.ExecuteScalar();
            }
        }

        /// <summary>
        /// Gets the number of participants for an event registration.
        /// </summary>
        /// <param name="userId">The user who made the registration.</param>
        /// <param name="eventId">The event ID</param>
        /// <returns>The number of participants.</returns>
        public static int GetEventRegistrationPartySize(int userId, int eventId)
        {
            string queryStr = "SELECT headCount FROM EventRegs " +
                "WHERE userId=@userId AND eventId=@eventId";
            using (SqlConnection con = new SqlConnection(Config.SqlConStr))
            {
                SqlCommand command = new SqlCommand(queryStr, con);
                command.Parameters.Add(new SqlParameter("userId", userId));
                command.Parameters.Add(new SqlParameter("eventId", eventId));
                con.Open();
                return (int)command.ExecuteScalar();
            }
        }

        /// <summary>
        /// Checks if an event can accommodate more participants by headCount.
        /// </summary>
        /// <param name="eventId">Event ID</param>
        /// <param name="headCount">Number of additional participants.</param>
        /// <returns>true if adding count does not exceed event capacity;
        /// false otherwise.</returns>
        public static bool EventCanAddCount(int eventId, int headCount)
        {
            int cap = Event.GetGuestLimit(eventId);
            int count = GetEventRegCount(eventId);
            return cap == Event.EventCapacityUnlimited ||
                (count + headCount) <= cap;
        }

        /// <summary>
        /// Puts an user into an event without checking the capacity of the
        /// event.
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="userId"></param>
        /// <param name="groupSize"></param>
        /// <param name="canDrive"></param>
        /// <param name="vehicleCap"></param>
        public static void ForceInsert(int eventId, int userId, int groupSize,
            bool canDrive, int vehicleCap)
        {
            string str = "INSERT INTO EventRegs (eventId, userId, headCount, " +
                "canDrive, vehicleCap) VALUES(@eventId, @userId, @headCount, " +
                "@canDrive, @vehicleCap)";
            using (SqlConnection con = new SqlConnection(Config.SqlConStr))
            {
                SqlCommand command = new SqlCommand(str, con);
                command.Parameters.Add(new SqlParameter("eventId", eventId));
                command.Parameters.Add(new SqlParameter("userId", userId));
                command.Parameters.Add(new SqlParameter("headCount", groupSize));
                command.Parameters.Add(new SqlParameter("canDrive", canDrive));
                command.Parameters.Add(new SqlParameter("vehicleCap", vehicleCap));
                con.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}