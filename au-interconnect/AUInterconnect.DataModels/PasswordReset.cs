using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Text;
using System.Net.Mail;
using AUInterconnect.Configuration;

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
            SmtpClient client = new SmtpClient("tigerout.auburn.edu");
            MailAddress from = new MailAddress("vininlj@auburn.edu");
            MailAddress to = new MailAddress(email);
            
            //Message content.
            StringBuilder message = new StringBuilder(
                "We received a request to reset the password associated with this e-mail address. ");
            message.Append("Your password reset code is ").Append(code);
            message.Append(Environment.NewLine).Append(Environment.NewLine);
            message.Append("You may reset your password by entering the reset code at this URL:");
            message.Append("https://fp.auburn.edu/interconnect/user/PwdReset.aspx");

            MailMessage mail = new MailMessage(from, to);
            mail.Body = message.ToString();
            mail.BodyEncoding = System.Text.Encoding.UTF8;

            //Email subject
            mail.Subject = "Auburn Interconnect Password Reset";
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            client.Send(mail);
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

        private static void EmailPassword(string email, string newPwd)
        {
            SmtpClient client = new SmtpClient("tigerout.auburn.edu");
            MailAddress from = new MailAddress("vininlj@auburn.edu");
            MailAddress to = new MailAddress(email);

            //Message content.
            StringBuilder message = new StringBuilder(
                "Your new temporary password is ").Append(newPwd);

            MailMessage mail = new MailMessage(from, to);
            mail.Body = message.ToString();
            mail.BodyEncoding = System.Text.Encoding.UTF8;

            //Email subject
            mail.Subject = "Auburn Interconnect";
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            client.Send(mail);
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