using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using AUInterconnect.Configuration;

namespace AUInterconnect.Views.Events
{
    public partial class Create : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PageHelper.Login(this, false);
        }

        protected void createBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateInput())
                {
                    decimal eventId = CreateEvent();
                    Response.Redirect("CreateComplete.aspx");
                }
            }
            catch (Exception)
            {
            }
        }

        private bool ValidateInput()
        {
            return Page.IsValid &&
                ValidateEventTimes() &&
                ValidateInput_RegDeadline() &&
                ValidateAgreeCondition();
        }

        private bool ValidateEventTimes()
        {
            //Check that start and end time are present
            if (string.IsNullOrEmpty(StartTimeCtr.Text) ||
                string.IsNullOrEmpty(EndTimeCtr.Text))
            {
                msgLbl.Text = "Start Time and End Time are required";
                return false;
            }

            DateTime startTime;
            DateTime endTime;
            if (!DateTime.TryParse(StartTimeCtr.Text, out startTime))
            {
                msgLbl.Text = "Start Time is invalid";
                return false;
            }

            if (!DateTime.TryParse(EndTimeCtr.Text, out endTime))
            {
                msgLbl.Text = "End Time is invalid";
                return false;
            }

            //Check that start time is not before end time
            if (startTime.CompareTo(endTime) > 0)
            {
                msgLbl.Text = "Start time must come before end time";
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validates the user input of the RegDeadline field.
        /// </summary>
        /// <remarks>
        /// Field is valid if:
        /// 1. Field is not empty
        /// 2. Time is not after start time
        /// </remarks>
        /// <returns>
        /// true if field is valid; false otherwies
        /// </returns>
        private bool ValidateInput_RegDeadline()
        {
            //Check time is present
            if (string.IsNullOrEmpty(RegDeadlineCtr.Text))
            {
                msgLbl.Text = "Registration Deadline is required";
                return false;
            }


            //Check that time is before start time
            DateTime formRegDeadline = FormRegDeadline;
            DateTime formStartTime = FormStartTime;
            if (formRegDeadline == DateTime.MinValue)
            {
                msgLbl.Text = "Registration Deadline is invalid";
                return false;
            }
            else if (formRegDeadline >= formStartTime)
            {
                msgLbl.Text = "Registration Deadline is after start time";
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validate that the agreement checkbox is checked.
        /// </summary>
        /// <returns>
        /// true if box is checked; false otherwise.
        /// </returns>
        private bool ValidateAgreeCondition()
        {
            if (!agreeChk.Checked)
            {
                msgLbl.Text = "You must agree conditions";
                return false;
            }

            return true;
        }
        
        /// <summary>
        /// Gets the Registration Deadline DateTime value from the form.
        /// </summary>
        private DateTime FormRegDeadline
        {
            get
            {
                return FormatHelper.ParseDateTimeOrMinValue(RegDeadlineCtr.Text);
            }
        }

        public DateTime FormStartTime
        {
            get
            {
                return FormatHelper.ParseDateTimeOrMinValue(StartTimeCtr.Text);
            }
        }

        /// <summary>
        /// Insert a new event into the database and return the event ID.
        /// </summary>
        /// <returns>-1 if event not inserted.</returns>
        private decimal CreateEvent()
        {
            string queryStr =
                "INSERT INTO Events (creatorId, createTime, guestLimit, hostOrg, " +
                "hostName, hostEmail, hostPhone, eventName, startTime, endTime, regDeadline, " +
                "location, descr, meetLocation, meetTime, transportation, requestDrivers, " +
                "costs, equipment, food, other) " +
                "VALUES (@creatorId, @createTime, @guestLimit, @hostOrg, @hostName, " +
                "@hostEmail, @hostPhone, @eventName, @startTime, @endTime, @regDeadline, " +
                "@location, @descr, @meetLocation, @meetTime, @transportation, " +
                "@requestDrivers, @costs, @equipment, @food, @other)";

            using (SqlConnection con = new SqlConnection(Config.SqlConStr))
            {
                SqlCommand command = new SqlCommand(queryStr, con);
                command.Parameters.Add(new SqlParameter("creatorId",
                    ((DataModels.User)Session[Const.User]).Uid));
                command.Parameters.Add(new SqlParameter("createTime",
                    DateTime.Now));
                command.Parameters.Add(
                   string.IsNullOrEmpty(GuestLimit.Text.Trim()) ?
                   new SqlParameter("guestLimit", DBNull.Value) :
                   new SqlParameter("guestLimit",
                       int.Parse(GuestLimit.Text.Trim())));
                command.Parameters.Add(
                    string.IsNullOrEmpty(HostOrg.Text.Trim()) ?
                    new SqlParameter("hostOrg", DBNull.Value) :
                    new SqlParameter("hostOrg", HostOrg.Text.Trim()));
                command.Parameters.Add(new SqlParameter("hostName",
                    HostName.Text.Trim()));
                command.Parameters.Add(new SqlParameter("hostEmail",
                    HostEmail.Text.Trim()));
                command.Parameters.Add(new SqlParameter("hostPhone",
                    FormatHelper.ParsePhoneNum(HostPhone.Text)));
                command.Parameters.Add(new SqlParameter("eventName",
                    EventName.Text.Trim()));
                command.Parameters.Add(new SqlParameter("startTime",
                    DateTime.Parse(StartTimeCtr.Text)));
                command.Parameters.Add(new SqlParameter("endTime",
                    DateTime.Parse(EndTimeCtr.Text)));
                command.Parameters.Add(new SqlParameter("regDeadline",
                    DateTime.Parse(RegDeadlineCtr.Text)));
                command.Parameters.Add(new SqlParameter("location",
                    Location.Text.Trim()));
                command.Parameters.Add(new SqlParameter("descr",
                    Desc.Text.Trim()));
                command.Parameters.Add(new SqlParameter("meetLocation",
                    MeetLocation.Text.Trim()));
                command.Parameters.Add(new SqlParameter("meetTime",
                    DateTime.Parse(MeetTime.Text)));
                command.Parameters.Add(new SqlParameter("transportation",
                    Transport.Text.Trim()));
                command.Parameters.Add(new SqlParameter("requestDrivers",
                    RequestDrivers.Checked));
                command.Parameters.Add(
                    string.IsNullOrEmpty(Costs.Text.Trim()) ?
                    new SqlParameter("costs", DBNull.Value) :
                    new SqlParameter("costs", Costs.Text.Trim()));
                command.Parameters.Add(
                    string.IsNullOrEmpty(Equipment.Text.Trim()) ?
                    new SqlParameter("equipment", DBNull.Value) :
                    new SqlParameter("equipment", Equipment.Text.Trim()));
                command.Parameters.Add(
                    string.IsNullOrEmpty(Food.Text.Trim()) ?
                    new SqlParameter("food", DBNull.Value) :
                    new SqlParameter("food", Food.Text.Trim()));
                command.Parameters.Add(
                    string.IsNullOrEmpty(Other.Text.Trim()) ?
                    new SqlParameter("other", DBNull.Value) :
                    new SqlParameter("other", Other.Text.Trim()));

                //command.Parameters.Add(
                //    string.IsNullOrEmpty(maxRegTxb.Text.Trim())?
                //    new SqlParameter("maxReg", DBNull.Value) :
                //    new SqlParameter("maxReg", 
                //        int.Parse(maxRegTxb.Text.Trim())));
                //command.Parameters.Add(
                //    string.IsNullOrEmpty(guestTxb.Text.Trim()) ?
                //    new SqlParameter("maxGuest", DBNull.Value) :
                //    new SqlParameter("maxGuest",
                //        int.Parse(maxRegTxb.Text.Trim())));
                //command.Parameters.Add(new SqlParameter("adminOk",
                //    "0"));
                //command.Parameters.Add(new SqlParameter("hostOk",
                //    "1"));
                con.Open();
                int r = command.ExecuteNonQuery();

                if (r < 1)
                    return -1;

                //Get the ID of the event created.
                queryStr = "SELECT @@IDENTITY";
                command = new SqlCommand(queryStr, con);
                object o = command.ExecuteScalar();
                return (decimal)o;
            }
        }
    }
}