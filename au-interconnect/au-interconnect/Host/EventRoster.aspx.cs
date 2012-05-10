using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using AUInterconnect.DataModels;

namespace AUInterconnect.Views.Host
{
    public partial class EventRoster : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Make sure the user is logged in and is host.
            PageHelper.LoginAsHost(this, false);

            FillRosterTable();
        }

        private void FillRosterTable()
        {
            int eventId = RequestUtil.GetEventId(Request);
            int eventHc = DataModels.EventRoster.GetEventTotalParticipant(eventId);
            int eventCc = DataModels.EventRoster.GetEventTotalCarpoolCapacity(eventId);

            try
            {
                using (SqlDataReader reader = DataModels.EventRoster.GetReaderEventRoster(eventId))
                {
                    while (reader.Read())
                    {
                        TableCell nameCell = new TableCell();
                        nameCell.Text = HttpUtility.HtmlEncode(reader["name"].ToString());
                        nameCell.CssClass = "rosterTableCell";

                        TableCell partySizeCell = new TableCell();
                        partySizeCell.Text = reader["headCount"].ToString();
                        partySizeCell.CssClass = "rosterTableCell_CenterAlign";

                        TableCell vehicleCapCell = new TableCell();
                        vehicleCapCell.Text = reader["vehicleCap"].ToString();
                        vehicleCapCell.CssClass = "rosterTableCell_CenterAlign";

                        TableRow row = new TableRow();
                        row.Cells.Add(nameCell);
                        row.Cells.Add(partySizeCell);
                        row.Cells.Add(vehicleCapCell);
                        RosterTable.Rows.Add(row);
                    }
                }

                //Table Footer
                TableFooterRow footerRow = new TableFooterRow();
                TableCell c1 = new TableCell();
                c1.Text = "Total";
                c1.CssClass = "rosterTableFooterCell";
                footerRow.Cells.Add(c1);
                TableCell c2 = new TableCell();
                c2.Text = eventHc.ToString();
                footerRow.Cells.Add(c2);
                c2.CssClass = "rosterTableFooterCell_CenterAlign";
                TableCell c3 = new TableCell();
                c3.Text = eventCc.ToString();
                footerRow.Cells.Add(c3);
                c3.CssClass = "rosterTableFooterCell_CenterAlign";

                RosterTable.Rows.Add(footerRow);

            }
            catch (Exception)
            {
                //TODO:Display and log exception.
            }
        }
    }
}