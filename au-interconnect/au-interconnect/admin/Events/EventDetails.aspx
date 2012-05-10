<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Events/AdminEvents.master" AutoEventWireup="true" CodeBehind="EventDetails.aspx.cs" Inherits="AUInterconnect.Views.admin.Events.EventDetails" %>
<%@ Import Namespace="AUInterconnect" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server"></asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadCount" runat="server">
<link rel="Stylesheet" type="text/css" href="<%=RequestUtil.AppPath(this) + "Styles/smoothness/jquery-ui-1.8.15.custom.css"%>" />
<style type="text/css">
/* css for timepicker */
.ui-timepicker-div .ui-widget-header{ margin-bottom: 8px; }
.ui-timepicker-div dl{ text-align: left; }
.ui-timepicker-div dl dt{ height: 25px; }
.ui-timepicker-div dl dd{ margin: -25px 0 10px 65px; }
.ui-timepicker-div td { font-size: 90%; }
</style>
<script type="text/javascript" src="<%=RequestUtil.AppPath(this) + "Scripts/jquery-ui-1.8.15.custom.min.js"%>"></script>
<script type="text/javascript" src="<%=RequestUtil.AppPath(this) + "Scripts/jquery-ui-timepicker-addon.js"%>"></script>
<script type="text/javascript" src="<%=RequestUtil.AppPath(this) + "Scripts/jquery.maskedinput-1.3.min.js"%>"></script>
<script type="text/javascript" src="<%=RequestUtil.AppPath(this) + "Scripts/jquery.dateFormat-1.0.js"%>"></script>

<%if (FormView1.CurrentMode == FormViewMode.Edit)
  { %>
<script language="javascript" type="text/javascript">
    var startTimeCtrId = '#MainContent_MainContent_MainContent_FormView1_startTimeTextBox';
    var endTimeCtrId = '#MainContent_MainContent_MainContent_FormView1_endTimeTextBox';
    var regDeadlineCtrId = '#MainContent_MainContent_MainContent_FormView1_regDeadlineTextBox';
    var meetTimeCtrId = '#MainContent_MainContent_MainContent_FormView1_meetTimeTextBox';
    var deadlineGap = 2;
    var dateFormat = 'MM/dd/yyyy hh:mm a';
    $(function () {
        $(startTimeCtrId).datetimepicker(
        {
            ampm: true,
            stepMinute: 15,

            onClose: function (dateText, inst) {

                var startDate = new Date(dateText);

                //Check end time
                var endDateTextBox = $(endTimeCtrId);
                if (endDateTextBox.val() != '') {
                    var endDate = new Date(endDateTextBox.val());
                    if (startDate > endDate)
                        endDateTextBox.val(dateText);
                }
                else {
                    endDateTextBox.val(dateText);
                }

                //Check registration deadline.
                //Move deadline date deadlineGap days before start date.
                var deadlineTextBox = $(regDeadlineCtrId);
                var d = new Date(startDate);
                d.setDate(d.getDate() - deadlineGap);
                deadlineTextBox.val($.format.date(d, dateFormat));

                //Meeting Time
                $(meetTimeCtrId).val(dateText);

            }
        });
        $(endTimeCtrId).datetimepicker({
            ampm: true,
            stepMinute: 15,

            onClose: function (dateText, inst) {
                var startDateTextBox = $(startTimeCtrId);
                if (dateText == '') { }
                else if (startDateTextBox.val() == '') {
                    //Set start time
                    startDateTextBox.val(dateText);
                    //Set reg deadline
                    var d = new Date(startDateTextBox.val());
                    d.setDate(d.getDate() - deadlineGap);
                    $(regDeadlineCtrId).val($.format.date(d, dateFormat));
                    //Set meeting time
                    $(meetTimeCtrId).val(dateText);
                }
                else {
                    var startDate = new Date(startDateTextBox.val());
                    var endDate = new Date(dateText);
                    if (endDate < startDate) {
                        $(endTimeCtrId).val($(startDateTextBox).val());
                    }
                }
            }
        });
        $(regDeadlineCtrId).datetimepicker({ ampm: true, stepMinute: 15 });
        $(meetTimeCtrId).datetimepicker({ ampm: true, stepMinute: 15 });
        //$('#MainContent_MainContent_MainContent_FormView1_hostPhoneTextBox').mask("(999) 999-9999");

    });
</script><%} %>

    <style type="text/css">
        .style1
        {
            width: 150px;
            vertical-align:top;
        }
    </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<h1>Event Details</h1>

    <asp:FormView ID="FormView1" runat="server" DataSourceID="SqlDataSource1"
        Width="100%">
        <EditItemTemplate>
            <table>
            <tr><td>Event ID</td><td><asp:TextBox ID="eventIdTextBox" runat="server" 
                    ReadOnly="True" Text='<%# Bind("eventId") %>' Enabled="False"></asp:TextBox></td></tr>
            <tr><td class="style1">Host Organization</td><td>
                <asp:TextBox ID="EditHostOrgTextBox" runat="server" Text='<%# Bind("hostOrg") %>' />
                </td></tr>
            <tr><td class="style1">Contact Name</td><td>
                <asp:TextBox ID="hostNameTextBox" runat="server" 
                    Text='<%# Bind("hostName") %>' />
                </td></tr>
            <tr><td class="style1">Contact Email</td><td>
                <asp:TextBox ID="hostEmailTextBox" runat="server" 
                    Text='<%# Bind("hostEmail") %>' />
                </td></tr>
            <tr><td class="style1">Contact Phone</td><td>
                <asp:TextBox ID="hostPhoneTextBox" runat="server" 
                    Text='<%# Bind("hostPhone") %>' />
                </td></tr>
            <tr><td class="style1">Event Name</td><td>
                <asp:TextBox ID="eventNameTextBox" runat="server" 
                    Text='<%# Bind("eventName") %>' />
                </td></tr>
            <tr><td class="style1">Start Time</td><td>
                <asp:TextBox ID="startTimeTextBox" runat="server" 
                    Text='<%# Bind("startTime") %>' />
                </td></tr>
            <tr><td class="style1">End Time</td><td>
                <asp:TextBox ID="endTimeTextBox" runat="server" Text='<%# Bind("endTime") %>' />
                </td></tr>
            <tr><td class="style1">Registration Deadline</td><td>
                <asp:TextBox ID="regDeadlineTextBox" runat="server" 
                    Text='<%# Bind("regDeadline") %>' />
                </td></tr>
            <tr><td class="style1">Event Location</td><td>
                <asp:TextBox ID="locationTextBox" runat="server" 
                    Text='<%# Bind("location") %>' />
                </td></tr>
            <tr><td class="style1">Meeting Time</td><td>
                <asp:TextBox ID="meetTimeTextBox" runat="server" 
                    Text='<%# Bind("meetTime") %>' />
                </td></tr>
            <tr><td class="style1">Meeting Location</td><td>
                <asp:TextBox ID="meetLocationTextBox" runat="server" 
                    Text='<%# Bind("meetLocation") %>' />
                </td></tr>
            <tr><td class="style1">Event Capacity</td><td>
                <asp:TextBox ID="guestLimitTextBox" runat="server" 
                    Text='<%# Bind("guestLimit") %>' />
                </td></tr>
            <tr><td class="style1">Description</td><td>
                <asp:TextBox ID="descrTextBox" runat="server" Text='<%# Bind("descr") %>' 
                    Height="200px" Width="475px" TextMode="MultiLine"/>
                </td></tr>
            <tr><td class="style1">Transportation</td><td>
                <asp:TextBox ID="transportationTextBox" runat="server" 
                    Text='<%# Bind("transportation") %>' Height="200px" TextMode="MultiLine" 
                    Width="475px" />
                </td></tr>
            <tr><td class="style1">Request Drivers</td><td>
                <asp:CheckBox ID="requestDriversCheckBox" runat="server" 
                    Checked='<%# Bind("requestDrivers") %>' />
                </td></tr>
            <tr><td class="style1">Costs</td><td>
                <asp:TextBox ID="costsTextBox" runat="server" Text='<%# Bind("costs") %>' 
                    Height="200px" TextMode="MultiLine" Width="475px" />
                </td></tr>
            <tr><td class="style1">Equipment</td><td>
                <asp:TextBox ID="equipmentTextBox" runat="server" 
                    Text='<%# Bind("equipment") %>' Height="200px" TextMode="MultiLine" 
                    Width="475px" />
                </td></tr>
            <tr><td class="style1">Food</td><td>
                <asp:TextBox ID="foodTextBox" runat="server" Text='<%# Bind("food") %>' 
                    Height="200px" TextMode="MultiLine" Width="475px" />
                </td></tr>
            <tr><td class="style1">Other</td><td>
                <asp:TextBox ID="otherTextBox" runat="server" Text='<%# Bind("other") %>' 
                    Height="200px" TextMode="MultiLine" Width="475px" />
                </td></tr>
            <tr><td class="style1"></td><td>
                <asp:RadioButton ID="ApprovedRadioButton" runat="server" 
                    Checked='<%# Bind("approved") %>' GroupName="Approval" Text="Approve" />
                <br />
                <asp:RadioButton ID="DeclinedRadioButton" runat="server" 
                    Checked='<%# Bind("declined") %>' GroupName="Approval" Text="Decline" />
                <br />
                </td></tr>
            </table>
            <br />
            <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" 
                CommandName="Update" Text="Update" />
            &nbsp;<asp:LinkButton ID="UpdateCancelButton" runat="server" 
                CausesValidation="False" CommandName="Cancel" Text="Cancel" />
        </EditItemTemplate>
        <InsertItemTemplate>
            approved:
            <asp:CheckBox ID="approvedCheckBox" runat="server" 
                Checked='<%# Bind("approved") %>' />
            <br />
            declined:
            <asp:CheckBox ID="declinedCheckBox" runat="server" 
                Checked='<%# Bind("declined") %>' />
            <br />
            guestLimit:
            <asp:TextBox ID="guestLimitTextBox" runat="server" 
                Text='<%# Bind("guestLimit") %>' />
            <br />
            hostOrg:
            <asp:TextBox ID="hostOrgTextBox" runat="server" Text='<%# Bind("hostOrg") %>' />
            <br />
            hostName:
            <asp:TextBox ID="hostNameTextBox" runat="server" 
                Text='<%# Bind("hostName") %>' />
            <br />
            hostEmail:
            <asp:TextBox ID="hostEmailTextBox" runat="server" 
                Text='<%# Bind("hostEmail") %>' />
            <br />
            hostPhone:
            <asp:TextBox ID="hostPhoneTextBox" runat="server" 
                Text='<%# Bind("hostPhone") %>' />
            <br />
            descr:
            <asp:TextBox ID="descrTextBox" runat="server" Text='<%# Bind("descr") %>' />
            <br />
            startTime:
            <asp:TextBox ID="startTimeTextBox" runat="server" 
                Text='<%# Bind("startTime") %>' />
            <br />
            endTime:
            <asp:TextBox ID="endTimeTextBox" runat="server" Text='<%# Bind("endTime") %>' />
            <br />
            regDeadline:
            <asp:TextBox ID="regDeadlineTextBox" runat="server" 
                Text='<%# Bind("regDeadline") %>' />
            <br />
            location:
            <asp:TextBox ID="locationTextBox" runat="server" 
                Text='<%# Bind("location") %>' />
            <br />
            meetLocation:
            <asp:TextBox ID="meetLocationTextBox" runat="server" 
                Text='<%# Bind("meetLocation") %>' />
            <br />
            meetTime:
            <asp:TextBox ID="meetTimeTextBox" runat="server" 
                Text='<%# Bind("meetTime") %>' />
            <br />
            transportation:
            <asp:TextBox ID="transportationTextBox" runat="server" 
                Text='<%# Bind("transportation") %>' />
            <br />
            costs:
            <asp:TextBox ID="costsTextBox" runat="server" Text='<%# Bind("costs") %>' />
            <br />
            requestDrivers:
            <asp:CheckBox ID="requestDriversCheckBox" runat="server" 
                Checked='<%# Bind("requestDrivers") %>' />
            <br />
            eventName:
            <asp:TextBox ID="eventNameTextBox" runat="server" 
                Text='<%# Bind("eventName") %>' />
            <br />
            equipment:
            <asp:TextBox ID="equipmentTextBox" runat="server" 
                Text='<%# Bind("equipment") %>' />
            <br />
            food:
            <asp:TextBox ID="foodTextBox" runat="server" Text='<%# Bind("food") %>' />
            <br />
            other:
            <asp:TextBox ID="otherTextBox" runat="server" Text='<%# Bind("other") %>' />
            <br />
            <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" 
                CommandName="Insert" Text="Insert" />
            &nbsp;<asp:LinkButton ID="InsertCancelButton" runat="server" 
                CausesValidation="False" CommandName="Cancel" Text="Cancel" />
        </InsertItemTemplate>
        <ItemTemplate>
            <table>
            <tr><td class="style1">Event ID</td><td>
                <asp:Label ID="eventIdLabel" runat="server" Text='<%# Bind("eventId") %>' />
                </td></tr>
            <tr><td class="style1">Host Organization</td><td>
                <asp:Label ID="hostOrgLabel" runat="server" Text='<%# Bind("hostOrg") %>' />
                </td></tr>
            <tr><td class="style1">Contact Name</td><td>
                <asp:Label ID="hostNameLabel" runat="server" Text='<%# Bind("hostName") %>' />
                </td></tr>
            <tr><td class="style1">Contact Email</td><td>
                <asp:Label ID="hostEmailLabel" runat="server" Text='<%# Bind("hostEmail") %>' />
                </td></tr>
            <tr><td class="style1">Contact Phone</td><td>
                <asp:Label ID="hostPhoneLabel" runat="server" Text='<%# Bind("hostPhone") %>' />
                </td></tr>
            <tr><td class="style1">Event Name</td><td>
                <asp:Label ID="eventNameLabel" runat="server" Text='<%# Bind("eventName") %>' />
                </td></tr>
            <tr><td class="style1">Start Time</td><td>
                <asp:Label ID="startTimeLabel" runat="server" Text='<%# Bind("startTime") %>' />
                </td></tr>
            <tr><td class="style1">End Time</td><td>
                <asp:Label ID="endTimeLabel" runat="server" Text='<%# Bind("endTime") %>' />
                </td></tr>
            <tr><td class="style1">Registration Deadline</td><td>
                <asp:Label ID="regDeadlineLabel" runat="server" 
                    Text='<%# Bind("regDeadline") %>' />
                </td></tr>
            <tr><td class="style1">Event Location</td><td>
                <asp:Label ID="locationLabel" runat="server" Text='<%# Bind("location") %>' />
                </td></tr>
            <tr><td class="style1">Meeting Time</td><td>
                <asp:Label ID="meetTimeLabel" runat="server" Text='<%# Bind("meetTime") %>' />
                </td></tr>
            <tr><td class="style1">Meeting Location</td><td>
                <asp:Label ID="meetLocationLabel" runat="server" 
                    Text='<%# Bind("meetLocation") %>' />
                </td></tr>
            <tr><td class="style1">Event Capacity</td><td>
                <asp:Label ID="guestLimitLabel" runat="server" 
                    Text='<%# Bind("guestLimit") %>' />
                </td></tr>
            <tr><td class="style1">Description</td><td>
                <asp:Label ID="descrLabel" runat="server" Text='<%# PageHelper.TextToHtmlEncode(Eval("descr").ToString()) %>' />
                </td></tr>
            <tr><td class="style1">Transportation</td><td>
                <asp:Label ID="transportationLabel" runat="server" 
                    Text='<%# PageHelper.TextToHtmlEncode(Eval("transportation").ToString()) %>' />
                </td></tr>
            <tr><td class="style1">Request Drivers</td><td>
                <asp:CheckBox ID="requestDriversCheckBox" runat="server" 
                    Checked='<%# Bind("requestDrivers") %>' Enabled="false" />
                </td></tr>
            <tr><td class="style1">Costs</td><td>
                <asp:Label ID="costsLabel" runat="server" Text='<%# PageHelper.TextToHtmlEncode(Eval("costs").ToString()) %>' />
                </td></tr>
            <tr><td class="style1">Equipment</td><td>
                <asp:Label ID="equipmentLabel" runat="server" Text='<%# PageHelper.TextToHtmlEncode(Eval("equipment").ToString()) %>' />
                </td></tr>
            <tr><td class="style1">Food</td><td>
                <asp:Label ID="foodLabel" runat="server" Text='<%# PageHelper.TextToHtmlEncode(Eval("food").ToString()) %>' />
                </td></tr>
            <tr><td class="style1">Other</td><td>
                <asp:Label ID="otherLabel" runat="server" Text='<%# PageHelper.TextToHtmlEncode(Eval("other").ToString()) %>' />
                </td></tr>
            <tr><td class="style1"></td><td>
                <asp:CheckBox ID="approvedCheckBox" runat="server" 
                    Checked='<%# Bind("approved") %>' Enabled="false" Text="Approved" />
                <br />
                <asp:CheckBox ID="declinedCheckBox" runat="server" 
                    Checked='<%# Bind("declined") %>' Enabled="false" Text="Declined" />
                </td></tr>
            </table>

            <p>
            <asp:LinkButton ID="EditButton" runat="server" CausesValidation="False" 
                CommandName="Edit" Text="Edit" />
            </p>
        </ItemTemplate>
    </asp:FormView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SqlServer %>" 
        
        
        SelectCommand="SELECT [eventId], [approved], [declined], [guestLimit], [hostOrg], [hostName], [hostEmail], [hostPhone], [descr], [startTime], [endTime], [regDeadline], [location], [meetLocation], [meetTime], [transportation], [costs], [requestDrivers], [eventName], [equipment], [food], [other] FROM [Events] WHERE ([eventId] = @eventId)" 
        
        
        UpdateCommand="UPDATE [Events] SET approved = @approved, declined = @declined, guestLimit = @guestLimit, hostOrg = @hostOrg, hostName = @hostName, hostEmail = @hostEmail, hostPhone = @hostPhone, eventName = @eventName, descr = @descr, startTime = @startTime, endTime = @endTime, regDeadline = @regDeadline, location = @location, meetLocation = @meetLocation, meetTime = @meetTime, transportation = @transportation, requestDrivers = @requestDrivers, costs = @costs, equipment = @equipment, food = @food, other = @other WHERE eventId = @eventId">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="2" Name="eventId" 
                QueryStringField="EventId" Type="Int32" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="approved" />
            <asp:Parameter Name="declined" />
            <asp:Parameter Name="guestLimit" />
            <asp:Parameter Name="hostOrg" />
            <asp:Parameter Name="hostName" />
            <asp:Parameter Name="hostEmail" />
            <asp:Parameter Name="hostPhone" />
            <asp:Parameter Name="eventName" />
            <asp:Parameter Name="descr" />
            <asp:Parameter Name="startTime" />
            <asp:Parameter Name="endTime" />
            <asp:Parameter Name="regDeadline" />
            <asp:Parameter Name="location" />
            <asp:Parameter Name="meetLocation" />
            <asp:Parameter Name="meetTime" />
            <asp:Parameter Name="transportation" />
            <asp:Parameter Name="requestDrivers" />
            <asp:Parameter Name="costs" />
            <asp:Parameter Name="equipment" />
            <asp:Parameter Name="food" />
            <asp:Parameter Name="other" />
            <asp:Parameter Name="eventId" />
        </UpdateParameters>
    </asp:SqlDataSource>
</asp:Content>
