using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Text;
using AUInterconnect.Configuration;
using AUInterconnect.Utilities;

namespace AUInterconnect.DataModels
{
    public class PasswordReset
    {
        public static bool HasUser(string firstName, string email)
        {
            string queryStr = "SELECT COUNT(*) FROM Users WHERE fname=@f AND email=@e";
            using (SqlConnection con = new SqlConnection(Config.SqlConStr))
            {
                SqlCommand command = new SqlCommand(queryStr, con);
                command.Parameters.Add(new SqlParameter("f", firstName));
                command.Parameters.Add(new SqlParameter("e", email));
                con.Open();
                int count = (int)command.ExecuteScalar();
                return count == 1;
            }
        }

        public static void SendResetCode(string email)
        {
            string code = MakeRandomResetCode(10);
            RecordResetRequest(email, code);
            EmailResetCode(email, code);
        }

        private static void EmailResetCode(string email, string code)
        {
            //Message content.
            StringBuilder message = new StringBuilder(
                "We received a request to reset the password associated with this e-mail address. ");
            message.Append("Your password reset code is ").Append(code);
            message.Append(Environment.NewLine).Append(Environment.NewLine);
            message.Append("You may reset your password by entering the reset code at this URL:");
            message.Append("https://fp.auburn.edu/interconnect/user/PwdReset.aspx");
            Mailer.Send(email, "Auburn Interconnect Password Reset", message.ToString());
        }

        private static void RecordResetRequest(string email, string code)
        {
            string queryStr =
                "INSERT INTO PwdResetRequests (email, resetcode, requestDate) " +
                "VALUES(@email, @code, @date)";

            using (SqlConnection con = new SqlConnection(Config.SqlConStr))
            {
                SqlCommand command = new SqlCommand(queryStr, con);
                command.Parameters.Add(new SqlParameter("email", email));
                command.Parameters.Add(new SqlParameter("code", code));
                command.Parameters.Add(new SqlParameter("date", DateTime.Now.Date));
                con.Open();
                command.ExecuteNonQuery();
            }
        }

        private static string MakeRandomResetCode(int length)
        {
            Random rand = new Random();
            StringBuilder result = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                result.Append(Convert.ToChar(rand.Next(65, 122)));
            }
            return result.ToString();
        }


        public static bool ResetPassword(string email, string code)
        {
            if (RedeemCode(email, code))
            {
                string newPwd = MakeRandomResetCode(7);
                UpdatePassword(email, newPwd);
                EmailPassword(email, newPwd);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Sends the new temporary password to user.
        /// </summary>
        /// <param name="email">Email address of receiver.</param>
        /// <param name="newPwd">User's new password.</param>
        private static void EmailPassword(string email, string newPwd)
        {
            //Message content.
            StringBuilder message = new StringBuilder(
                "Your new temporary password is ").Append(newPwd);
            Mailer.Send(email, "Auburn Interconnect", message.ToString());
        }

        private static void UpdatePassword(string email, string newPwd)
        {
            string queryStr = "UPDATE Users SET pwd=@pwd WHERE email=@email";

            using (SqlConnection con = new SqlConnection(Config.SqlConStr))
            {
                SqlCommand command = new SqlCommand(queryStr, con);
                command.Parameters.Add(new SqlParameter("pwd", User.HashPassword(newPwd)));
                command.Parameters.Add(new SqlParameter("email", email));
                con.Open();
                command.ExecuteNonQuery();
            }
        }

        private static bool RedeemCode(string email, string code)
        {
            string queryStr =
                "DELETE FROM PwdResetRequests WHERE email=@email AND resetCode=@code";
            using (SqlConnection con = new SqlConnection(Config.SqlConStr))
            {
                SqlCommand command = new SqlCommand(queryStr, con);
                command.Parameters.Add(new SqlParameter("email", email));
                command.Parameters.Add(new SqlParameter("code", code));
                con.Open();
                int i = command.ExecuteNonQuery();
                return (i >= 1);
            }
        }

    }
}