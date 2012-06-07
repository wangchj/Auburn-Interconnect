using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AUInterconnect.Views.Proposal
{
    public partial class View : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PageHelper.Login(this, false);

            if (IsPostBack)
                return;

            int propId = RequestUtil.GetProposalId(Request);
            if (propId == -1)
                Response.Redirect("../Default.aspx");
            Content.Text = DataModels.Proposal.GetContent(propId);
        }
    }
}