using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using AUInterconnect.Configuration;

namespace AUInterconnect.Views.User
{
    public partial class Reg : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void submitBtn_Click(object sender, EventArgs e)
        {
            try
            {
                //Input validation
                if (!Page.IsValid)
                    return;

                //Check captcha
                if (!CaptchaControl1.UserValidated)
                {
                    ErrorLbl.Text = "Challenge code value not match.";
                    return;
                }

                //Check if user with this email already exist in the system.
                if (UserAlreadyExist(emailTxb.Text))
                {
                    ErrorLbl.Text = "Email already exist!";
                    Response.Redirect("../Default.aspx", true);
                }
                else
                {
                    DataModels.User.AddNewUser(fnTxb.Text, lnTxb.Text,
                        emailTxb.Text, 
                        FormatHelper.ParsePhoneNum(phoneTxb.Text), pwdTxb.Text);
                    Response.Redirect("../Default.aspx", true);
                }
            }
            catch (Exception ex)
            {
                ErrorLbl.Text = "Sorry! There was a system error.";
                //ErrorLbl.Text = ex.Message;
                //ErrorLbl.Text += ex.StackTrace;
            }
        }

        /// <summary>
        /// Checks if the email address exists in the system.
        /// </summary>
        /// <param name="email">The email address to check</param>
        /// <returns>true if address already exist; false otherwise</returns>
        /// <exception cref="System.SqlClient.SqlException"></exception>
        private bool UserAlreadyExist(string email)
        {
            if (email == null)
                throw new ArgumentNullException();

            string queryStr = "SELECT COUNT(*) FROM Users WHERE email=@email";

            using (SqlConnection con = new SqlConnection(Config.SqlConStr))
            {
                SqlCommand command = new SqlCommand(queryStr, con);
                command.Parameters.Add(new SqlParameter("email", email));
                con.Open();
                Object obj = command.ExecuteScalar();
                return obj != null && ((int)obj) >= 1;
            }
        }

        /// <summary>
        /// Gets user id
        /// </summary>
        /// <param name="email">email of user</param>
        /// <returns>-1 if email does not exist</returns>
        //private int GetUserId(string email)
        //{
        //    string queryStr =
        //        "SELECT uid FROM Users WHERE email=@email";

        //    using (SqlConnection con = new SqlConnection(Config.SqlConStr))
        //    {
        //        SqlCommand command = new SqlCommand(queryStr, con);
        //        command.Parameters.Add(new SqlParameter("email", email));
        //        con.Open();
        //        Object obj = command.ExecuteScalar();
        //        if (obj == null)
        //            return -1;
        //        return (int)obj;
        //    }
        //}
    }
}
