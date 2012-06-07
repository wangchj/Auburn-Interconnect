﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AUInterconnect.DataModels;

namespace AUInterconnect.Views.admin
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Note:Permission is checked by Admin.Master

            //Proposal stats
            PenPropLink.Text = DataModels.Proposal.GetPendingProposalCount() + PenPropLink.Text;

            //Event stats
            ActiveEventsCount.Text = Event.GetActiveEventCount().ToString() + ActiveEventsCount.Text;
            UpcomingEventsCount.Text = Event.GetUpcomingEventCount().ToString() + UpcomingEventsCount.Text;
            UnapprovedEventsCount.Text = Event.GetUpcomingUnapprovedEventCount().ToString() +
                UnapprovedEventsCount.Text;
            DeclinedEventsCount.Text = Event.GetAllDeclinedEventCount().ToString() +
                DeclinedEventsCount.Text;
            TotalEventsCount.Text = Event.GetTotalEventCount().ToString() + TotalEventsCount.Text;

            //User stats
            TotalUserCount.Text = DataModels.User.GetAllUserCount().ToString() + TotalUserCount.Text;
        }
    }
}