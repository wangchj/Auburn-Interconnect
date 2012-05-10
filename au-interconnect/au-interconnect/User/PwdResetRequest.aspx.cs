using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AUInterconnect.DataModels;

namespace AUInterconnect.Views.User
{
    public partial class PwdResetRequest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsValid)
                    return;

                if (!PasswordReset.HasUser(FirstName.Text.Trim(),
                    Email.Text.Trim()))
                {
                    ErrorLit.Text = "Name and email is invalid.";
                    return;
                }

                PasswordReset.SendResetCode(Email.Text.Trim());
                Response.Redirect("PwdResetRequestComplete.aspx", true);
                //ErrorLit.Text = "The password reset code has been sent to you.";
            }
            catch (Exception)
            {
                ErrorLit.Text = "An error has occurred. Please try again later.";
            }
        }
    }
}