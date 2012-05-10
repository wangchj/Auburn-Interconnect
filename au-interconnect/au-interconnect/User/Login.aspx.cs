//#define BYPASS_AU_AUTH

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using FormsAuthAD;
using AUInterconnect.DataModels;
using AUInterconnect.Configuration;

namespace AUInterconnect.Views.User
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //RegisterHyperLink.NavigateUrl = "Register.aspx?ReturnUrl=" + HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);

            try
            {
                //If user already logged in, return to previous page
                DataModels.User user = (DataModels.User)Session[Const.User];
                if (user != null && !AuthenticateAuStudent ||
                    user != null && AuthenticateAuStudent && user.IsAuStudent)
                {
                    if (!AuthenticateAuStudent || user.IsAuStudent)
                        Nav.ReturnToPrevPage(this);
                }

                //Configure UI
                if (!AuthenticateSysUser)
                    Panel1.Visible = false;
                if (!AuthenticateAuStudent)
                    Panel2.Visible = false;

            }
            catch (Exception) { }
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            try
            {
                int uid = -1;
                bool isStudent = false;

                //Authenticate 
                if (AuthenticateSysUser)
                {
                    //We need to authenticate system user
                    //Authenticate system user
                    string email = emailTxb.Text.Trim();
                    string pwd = pwdTxb.Text;
                    uid = Authenticate(email, pwd);

                }

                //Authenticate AU user
                if (AuthenticateAuStudent)
                {
                    isStudent = AuthAuUser(auusrTxb.Text.Trim(),
                        aupwdTxb.Text);
                }

                //
                //Check and process result for both authentications.
                //
                if (AuthenticateSysUser && AuthenticateAuStudent)
                {
                    //Need authenticate for both system user and au user.

                    if (uid != -1)
                    {
                        //Sys auth succeeded. Create user and hide sys login
                        //box.
                        Panel1.Visible = false;
                        DataModels.User user = new DataModels.User(uid);
                        Session[Const.User] = user;
                        if (isStudent)
                        {
                            //Both auth succeed, redirect.
                            user.IsAuStudent = true;
                            Nav.ReturnToPrevPage(this);
                        }
                        else
                        {
                            //Sys succeed, AU fail
                            AuLoginLit.Text =
                                "Auburn credential is incorrect.";
                        }

                    }
                    else
                    {
                        FailureText.Text = "Email and password is incorrect.";
                    }
                }
                else if (AuthenticateSysUser)
                {
                    //Only need authentication for sys user.

                    if (uid != -1)
                    {
                        //Sys auth succeeded.
                        DataModels.User user = new DataModels.User(uid);
                        Session[Const.User] = user;
                        Nav.ReturnToPrevPage(this);
                    }
                    else
                    {
                        //Sys auth fail.
                        FailureText.Text = "Email and password is incorrect.";
                    }
                }
                else if (AuthenticateAuStudent)
                {
                    //Only need authenticatation for AU user.

                    DataModels.User user = (DataModels.User)Session[Const.User];
                    if (isStudent)
                    {
                        user.IsAuStudent = true;
                        Nav.ReturnToPrevPage(this);
                    }
                    else
                    {
                        AuLoginLit.Text =
                                "Auburn credential is incorrect.";
                    }
                }

            }
            catch (Exception ex)
            {
                FailureText.Text = "Sorry! There was a system error!";
                FailureText.Text += ex.Message;
                FailureText.Text += ex.StackTrace;
            }
        }

        /// <summary>
        /// Authenticates and gets the user id from the email and password
        /// pair.
        /// </summary>
        /// <param name="email">email of the user</param>
        /// <param name="pwd">password of the user</param>
        /// <returns>The user's uid if the credential is correct; -1
        /// otherwise.</returns>
        protected int Authenticate(string email, string pwd)
        {
            string queryStr = "SELECT uid FROM Users WHERE email=@email " +
                " AND pwd=@pwd";

            using (SqlConnection con = new SqlConnection(Config.SqlConStr))
            {
                SqlCommand command = new SqlCommand(queryStr, con);
                command.Parameters.Add(new SqlParameter("email", email));
                command.Parameters.Add(new SqlParameter("pwd",
                    DataModels.User.HashPassword(pwd)));
                con.Open();
                Object obj = command.ExecuteScalar();
                if (obj == null)
                    return -1;
                else
                    return (int)obj;
            }
        }

        private bool AuthAuUser(string username, string pwd)
        {
#if BYPASS_AU_AUTH
            return true;
#endif
            if (string.IsNullOrEmpty(username) ||
                string.IsNullOrEmpty(pwd))
                return false;

            String adPath = "LDAP://auburn.edu/DC=auburn,DC=edu";
            LdapAuthentication adAuth;
            adAuth = new LdapAuthentication(adPath);
            try
            {
                if (adAuth.IsAuthenticated("auburn", username, pwd) == true)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private bool AuthenticateSysUser
        {
            get
            {
                return Session[Const.User] == null;
            }
        }

        private bool AuthenticateAuStudent
        {
            get
            {
                string flag = Request[Const.AuthAuStud];
                if (flag == null)
                    return false;
                return flag == "1";
            }
        }
    }
}
