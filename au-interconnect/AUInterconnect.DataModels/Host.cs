using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using AUInterconnect.Configuration;

namespace AUInterconnect.DataModels
{
    public class Host
    {
        public static SqlDataReader GetDataReaderActiveEvents(int userId)
        {
            string queryStr = "SELECT eventId, eventName, startTime FROM [Events] " +
                "WHERE creatorId=@cid AND approved=1 AND endTime>@now " +
                "ORDER BY [Events].startTime";

            SqlConnection con = new SqlConnection(Config.SqlConStr);
            SqlCommand command = new SqlCommand(queryStr, con);
            command.Parameters.Add(new SqlParameter("cid", userId));
            command.Parameters.Add(new SqlParameter("now", DateTime.Now));
            con.Open();
            return command.ExecuteReader();
        }
    }
}