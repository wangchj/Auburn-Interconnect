using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using AUInterconnect.Configuration;

namespace AUInterconnect.DataModels
{
    public class WaitingList
    {
        /// <summary>
        /// Checks if an user is on the waiting list for an event.
        /// </summary>
        /// <param name="userId">ID of the user.</param>
        /// <param name="eventId">ID of an event.</param>
        /// <returns>true if user is on the waiting list; false otherwise.</returns>
        public static bool IsOnWaitingList(int userId, int eventId)
        {
            string queryStr = "SELECT COUNT(*) FROM WaitingList " +
                "WHERE userId=@userId AND eventId=@eventId";
            using (SqlConnection con = new SqlConnection(Config.SqlConStr))
            {
                SqlCommand command = new SqlCommand(queryStr, con);
                command.Parameters.Add(new SqlParameter("userId", userId));
                command.Parameters.Add(new SqlParameter("eventId", eventId));
                con.Open();
                object obj = command.ExecuteScalar();
                if (obj == null)
                    return false;
                return (int)obj > 0;
            }
        }

        /// <summary>
        /// Add an user to the waiting list of an event.
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <param name="eventId">Event ID</param>
        /// <returns>The number of rows affected on the database.</returns>
        public static int Insert(int userId, int eventId)
        {
            string queryStr = "INSERT INTO WaitingList VALUES (@userId, @eventId, @time)";

            using (SqlConnection con = new SqlConnection(Config.SqlConStr))
            {
                SqlCommand command = new SqlCommand(queryStr, con);
                command.Parameters.Add(new SqlParameter("userId", userId));
                command.Parameters.Add(new SqlParameter("eventId", eventId));
                command.Parameters.Add(new SqlParameter("time", DateTime.Now));
                con.Open();
                return command.ExecuteNonQuery();
            }
        }
    }
}
