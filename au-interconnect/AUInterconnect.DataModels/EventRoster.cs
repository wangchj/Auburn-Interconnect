using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using AUInterconnect.Configuration;

namespace AUInterconnect.DataModels
{
    public class EventRoster
    {
        public static SqlDataReader GetReaderEventRoster(int eventId)
        {
            string queryStr =
                "SELECT (fname + ' ' + lname) AS name, headCount, vehicleCap " +
                "FROM Users INNER JOIN EventRegs ON Users.uid=EventRegs.userId " +
                "WHERE eventId=@eventId";

            SqlConnection con = new SqlConnection(Config.SqlConStr);
            SqlCommand command = new SqlCommand(queryStr, con);
            command.Parameters.Add(new SqlParameter("eventId", eventId));
            con.Open();
            return command.ExecuteReader();
        }

        public static int GetEventTotalParticipant(int eventId)
        {
            string queryStr =
                "SELECT SUM(headCount) FROM EventRegs WHERE eventId=@eventId";
            using (SqlConnection con = new SqlConnection(Config.SqlConStr))
            {
                SqlCommand command = new SqlCommand(queryStr, con);
                command.Parameters.Add(new SqlParameter("eventId", eventId));
                con.Open();
                object result = command.ExecuteScalar();
                return result == DBNull.Value ? 0 : (int)result;
            }
        }

        public static int GetEventTotalCarpoolCapacity(int eventId)
        {
            string queryStr =
                "SELECT SUM(vehicleCap) FROM EventRegs WHERE eventId=@eventId";
            using (SqlConnection con = new SqlConnection(Config.SqlConStr))
            {
                SqlCommand command = new SqlCommand(queryStr, con);
                command.Parameters.Add(new SqlParameter("eventId", eventId));
                con.Open();
                object result = command.ExecuteScalar();
                return result == DBNull.Value ? 0 : (int)result;
            }
        }
    }
}