using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AUInterconnect.DataModels;

namespace AUInterconnect.Views.User
{
    public partial class PwdReset : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (PasswordReset.ResetPassword(Email.Text.Trim(),
                    ResetCode.Text.Trim()))
                {
                    Response.Redirect("PwdResetConfirm.aspx", true);
                }
                else
                {
                    ErrorLit.Text = "Reset code is incorrect.";
                }
            }
            catch (Exception)
            {
                ErrorLit.Text = "An error has occurred. " +
                    "Please contact system administrator.";
            }
        }
    }
}