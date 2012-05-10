using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AUInterconnect.Views.User
{
    public partial class UserPasswordChange : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PageHelper.Login(this, false);
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            DataModels.User user = PageHelper.GetCurrentUser(this);
            int r = user.ChangePassword(OldPwd.Text, NewPwd.Text);

            if (r == 0)
                Response.Redirect("UserAccountInfo.aspx", false);
            else if (r == 1)
                ErrorLiteral.Text = "Invalid credential.";
            else
                ErrorLiteral.Text = "Error occurred. Please contact admin.";
            
        }
    }
}