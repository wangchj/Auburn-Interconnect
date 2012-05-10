using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace AUInterconnect.Configuration
{
    public class Config
    {
        public const string SqlConStrName = "SqlServer";

        /// <summary>
        /// Returns the SQL database connection string.
        /// </summary>
        /// <exception cref="ConfigurationErrorsException"></exception>
        public static string SqlConStr
        {
            get
            {
                return
                    ConfigurationManager.ConnectionStrings[
                    SqlConStrName].ConnectionString;
            }
        }
    }
}