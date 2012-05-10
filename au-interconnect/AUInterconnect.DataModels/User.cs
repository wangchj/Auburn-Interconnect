using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Security.Cryptography;
using AUInterconnect.Configuration;

namespace AUInterconnect.DataModels
{
    public class User
    {
        public int Uid { get; private set; }
        public bool IsAuStudent { get; set; }
        public bool IsAdmin { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Phone { get; private set; }
        public string Email { get; private set; }

        public User(int uid)
            : this(uid, false) { }

        public User(int uid, bool auStud)
        {
            Uid = uid;
            IsAuStudent = auStud;
            Fill();
        }

        /// <summary>
        /// Fills this object from the database.
        /// </summary>
        public void Fill()
        {
            string queryStr = "SELECT fname, lname, email, phone, admin FROM Users " +
                "WHERE uid=@id";
            using (SqlConnection con = new SqlConnection(Config.SqlConStr))
            {
                SqlCommand command = new SqlCommand(queryStr, con);
                command.Parameters.Add(new SqlParameter("id", Uid));
                con.Open();
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);
                reader.Read();
                IsAdmin = (bool)reader["admin"];
                FirstName = reader["fname"].ToString();
                LastName = reader["lname"].ToString();
                Email = reader["email"].ToString();
                Phone = reader["phone"].ToString();
                reader.Close();
            }
        }

        public static void AddNewUser(string firstName, string lastName,
            string email, long phone, string password)
        {
            string queryStr =
                "INSERT INTO Users (fname, lname, email, phone, pwd) " +
                "VALUES (@fname, @lname, @email, @phone, @pwd)";

            using (SqlConnection con = new SqlConnection(Config.SqlConStr))
            {
                SqlCommand command = new SqlCommand(queryStr, con);
                command.Parameters.Add(new SqlParameter("fname",
                    firstName.Trim()));
                command.Parameters.Add(new SqlParameter("lname",
                    lastName.Trim()));
                command.Parameters.Add(new SqlParameter("email",
                    email.Trim()));
                command.Parameters.Add(phone == 0 ?
                    new SqlParameter("phone", DBNull.Value) :
                    new SqlParameter("phone", phone));
                command.Parameters.Add(new SqlParameter("pwd",
                    HashPassword(password)));

                con.Open();
                command.ExecuteNonQuery();
            }
        }

        public static byte[] HashPassword(string pwd)
        {
            UTF8Encoding encoder = new UTF8Encoding();
            byte[] bytes = encoder.GetBytes(pwd.ToCharArray());
            SHA256Managed sha = new SHA256Managed();
            return sha.ComputeHash(bytes);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldPwd"></param>
        /// <param name="newPwd"></param>
        /// <returns>
        /// 0 - success
        /// 1 - old password is invalid
        /// 2 - SQL Exception
        /// </returns>
        public int ChangePassword(string oldPwd, string newPwd)
        {
            if (!IsValidCredential(Uid, oldPwd))
                return 1;

            string queryStr = "UPDATE Users SET pwd=@pwd WHERE uid=@uid";

            using (SqlConnection con = new SqlConnection(Config.SqlConStr))
            {
                SqlCommand command = new SqlCommand(queryStr, con);
                command.Parameters.Add(new SqlParameter("uid", Uid));
                command.Parameters.Add(new SqlParameter("pwd", 
                    HashPassword(newPwd)));

                try
                {
                    con.Open();
                    command.ExecuteNonQuery();
                    return 0;
                }
                catch (Exception)
                {
                    return 2;
                }
            }
        }

        public static bool IsValidCredential(int userId, string pwd)
        {
            string queryStr = "SELECT COUNT(*) FROM Users WHERE uid=@uid AND pwd=@pwd";
            using (SqlConnection con = new SqlConnection(Config.SqlConStr))
            {
                SqlCommand command = new SqlCommand(queryStr, con);
                command.Parameters.Add(new SqlParameter("uid", userId));
                command.Parameters.Add(new SqlParameter("pwd",
                    HashPassword(pwd)));
                con.Open();
                int count = (int)command.ExecuteScalar();
                return count == 1;
            }
        }

        /// <summary>
        /// Updates a user in the database.
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="email"></param>
        /// <param name="phone"></param>
        /// <returns>0 successul; -1 otherwise</returns>
        public int Update(string firstName, string lastName, string email, long phone)
        {
            string queryStr =
                "UPDATE Users SET fname=@fname, lname=@lname, email=@email, phone=@phone " +
                "WHERE uid=@uid";
            using (SqlConnection con = new SqlConnection(Config.SqlConStr))
            {
                SqlCommand command = new SqlCommand(queryStr, con);
                command.Parameters.Add(new SqlParameter("fname", firstName));
                command.Parameters.Add(new SqlParameter("lname", lastName));
                command.Parameters.Add(new SqlParameter("email", email));
                command.Parameters.Add(phone == 0 ?
                    new SqlParameter("phone", DBNull.Value) :
                    new SqlParameter("phone", phone));
                command.Parameters.Add(new SqlParameter("uid", Uid));

                try
                {
                    con.Open();
                    command.ExecuteNonQuery();
                    return 0;
                }
                catch (Exception)
                {
                    return -1;
                }
            }
        }

        //public static bool IsAdmin(int userId)
        //{
        //    string queryStr = "SELECT admin FROM Users " +
        //        "WHERE uid=@id";
        //    using (SqlConnection con = new SqlConnection(Config.SqlConStr))
        //    {
        //        SqlCommand command = new SqlCommand(queryStr, con);
        //        command.Parameters.Add(new SqlParameter("id", userId));
        //        con.Open();
        //        object obj = command.ExecuteScalar();
        //        return (bool)obj;
        //    }
        //}

        /// <summary>
        /// Gets the first name of a user from the database.
        /// </summary>
        /// <param name="userId">The user id of the user</param>
        /// <returns>null if user is not found.</returns>
        /// <exception cref="SqlException"></exception>
        private string GetLastName(int userId)
        {
            string queryStr = "SELECT lname FROM Users " +
                "WHERE uid=@id";

            using (SqlConnection con = new SqlConnection(Config.SqlConStr))
            {
                SqlCommand command = new SqlCommand(queryStr, con);
                command.Parameters.Add(new SqlParameter("id", userId));
                con.Open();
                object obj = command.ExecuteScalar();
                if (obj == null || obj == DBNull.Value)
                    return null;
                return (string)obj;
            }
        }

        /// <summary>
        /// Gets the first name of a user from the database.
        /// </summary>
        /// <param name="userId">The user id of the user</param>
        /// <returns>null if user is not found.</returns>
        /// <exception cref="SqlException"></exception>
        public static string GetFirstName(int userId)
        {
            string queryStr = "SELECT fname FROM Users " +
                "WHERE uid=@id";

            using (SqlConnection con = new SqlConnection(Config.SqlConStr))
            {
                SqlCommand command = new SqlCommand(queryStr, con);
                command.Parameters.Add(new SqlParameter("id", userId));
                con.Open();
                object obj = command.ExecuteScalar();
                if (obj == null || obj == DBNull.Value)
                    return null;
                return (string)obj;
            }
        }

        public static int GetAllUserCount()
        {
            string queryStr = "SELECT COUNT(*) FROM Users";
            using (SqlConnection con = new SqlConnection(Config.SqlConStr))
            {
                SqlCommand command = new SqlCommand(queryStr, con);
                con.Open();
                object obj = command.ExecuteScalar();
                return (int)obj;
            }
        }

        public static void UpdateAdmin(int userId, bool isAdmin)
        {
            string queryStr = "UPDATE Users SET admin=@admin WHERE uid=@uid";
            using (SqlConnection con = new SqlConnection(Config.SqlConStr))
            {
                SqlCommand command = new SqlCommand(queryStr, con);
                command.Parameters.Add(new SqlParameter("admin", isAdmin));
                command.Parameters.Add(new SqlParameter("uid", userId));
                con.Open();
                command.ExecuteNonQuery();
            }
        }

    }
}