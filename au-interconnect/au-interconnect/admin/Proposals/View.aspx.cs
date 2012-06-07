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
            Content.Text = DataModels.Proposal.GetContent(propId);
        }

        /// <summary>
        /// Event handler when save button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

                //Push the update to the database.
                DataModels.Proposal.UpdateContent(propId, Content.Text);
                
                //Email proposer of the change.
                DataModels.Proposal.SendUpdateEmail(propId);

            }
            catch (Exception ex)
            {
                ErrorLit.Text = ex.Message;
            }
        }

        /// <summary>
        /// Event handler for when approve button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ApproveButton_Click(object sender, EventArgs e)
        {
            DataModels.Proposal.Approve(RequestUtil.GetProposalId(Request));
            Response.Redirect("Default.aspx");
        }
    }
}