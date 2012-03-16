using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AUInterconnect.admin.Events
{
    public partial class EventDetails : System.Web.UI.Page
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
    }
}