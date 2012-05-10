using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AUInterconnect.Views.User
{
    public partial class UserAccountInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataModels.User user = PageHelper.Login(this, false);
            NameLabel.Text = HttpUtility.HtmlEncode(user.FirstName + " " + user.LastName);
            EmailLabel.Text = user.Email;
            if(!string.IsNullOrEmpty(user.Phone))
                PhoneLabel.Text = string.Format("{0:(###) ###-####}",
                    long.Parse(user.Phone));
        }


    }
}