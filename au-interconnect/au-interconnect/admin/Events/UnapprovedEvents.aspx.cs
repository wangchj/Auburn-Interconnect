using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AUInterconnect.admin.Events
{
    public partial class UnapprovedEvents : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                PageHelper.LoginAsAdmin(this, false);
            }
            catch (Exception)
            {
            }
        }

        protected void ApproveRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                RadioButton rbtn = (RadioButton)sender;
                int eventId = int.Parse(rbtn.GroupName);
                Event.ApproveEvent(eventId);
                GridView1.DataBind();
            }
            catch (Exception)
            {

            }
        }

        protected void DeclineRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                RadioButton rbtn = (RadioButton)sender;
                int eventId = int.Parse(rbtn.GroupName);
                Event.DeclineEvent(eventId);
                GridView1.DataBind();
            }
            catch (Exception)
            {

            }
        }
    }
}