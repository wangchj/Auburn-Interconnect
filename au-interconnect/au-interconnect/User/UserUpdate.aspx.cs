using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using AUInterconnect.Views.Events;
using AUInterconnect.DataModels;

namespace AUInterconnect.Views.User
{
    public partial class UserUpdate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataModels.User user = PageHelper.Login(this, false);
                fnTxb.Text = user.FirstName;
                lnTxb.Text = user.LastName;
                phoneTxb.Text = user.Phone;
                emailTxb.Text = user.Email;
            }
        }

        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            DataModels.User user = PageHelper.GetCurrentUser(this);
            if (user.Update(fnTxb.Text, lnTxb.Text, emailTxb.Text,
                FormatHelper.ParsePhoneNum(phoneTxb.Text)) == 0)
            {
                PageHelper.GetCurrentUser(this).Fill();
                Response.Redirect("UserAccountInfo.aspx", false);
            }
            else
            {
                ErrorLbl.Text = "Error updating info. Please contact admin.";
            }
        }
    }
}
