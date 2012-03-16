using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AUInterconnect.admin.Users
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox checkbox = (CheckBox)sender;
                int userId = int.Parse(checkbox.ToolTip);
                AUInterconnect.User.UpdateAdmin(userId, checkbox.Checked);
                GridView1.DataBind();
            }
            catch (Exception)
            {
                ErrorMessage.Text = "An error has occurred.";
            }
        }
    }
}