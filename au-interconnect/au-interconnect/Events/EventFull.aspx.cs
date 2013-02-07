using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AUInterconnect.DataModels;

namespace AUInterconnect.Views.Events
{
    public partial class EventFull : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                DataModels.User user = PageHelper.Login(this, true);
                int eventId = RequestUtil.GetEventId(Request);

                //Check to see if the user is already registered for the event.
                if (EventRegistration.HasRegistration(user.Uid, eventId))
                    MsgLit.Text = "You are already registered for the event!";

                //Check to see if the user is already on the waiting list for the event.
                else if (WaitingList.IsOnWaitingList(user.Uid, eventId))
                    MsgLit.Text = "You are already on the waiting list for this event!";

                //Put user on the waiting list.
                else
                {
                    WaitingList.Insert(user.Uid, eventId);
                    MsgLit.Text = "You have been added to the waiting list.";
                }
            }
            catch (Exception)
            {
                MsgLit.Text = "An error has occurred while adding you to the waiting list. " +
                    "If problem persists, please contact InterConnect coordinators.";
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                
            }
            catch (Exception)
            {
                
            }
        }
    }
}