using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AUInterconnect.DataModels;

namespace AUInterconnect.Views.admin.Proposals
{
    public partial class View : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;

            int propId = RequestUtil.GetProposalId(Request);
            if (propId == -1)
                Response.Redirect("../Default.aspx");
            Content.Text = Proposal.GetContent(propId);
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                int propId = RequestUtil.GetProposalId(Request);
                if(propId == -1)
                {
                    ErrorLit.Text = "Unable to get proposal ID.";
                    return;
                }

                Proposal.UpdateContent(propId, Content.Text);

                //TODO: Email host

            }
            catch (Exception ex)
            {
                ErrorLit.Text = ex.Message;
            }
        }
    }
}