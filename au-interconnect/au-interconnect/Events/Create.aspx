<%@ Page Title="" Language="C#" MasterPageFile="~/AULayout1.master" AutoEventWireup="true"
    CodeBehind="Create.aspx.cs" Inherits="AUInterconnect.Events.Create" %>

<asp:Content ID="Content3" ContentPlaceHolderID="title" runat="server">
Auburn Interconnect - Propose an Event
</asp:Content>

<asp:Content ID="BreadcrumbContent" runat="server" ContentPlaceHolderID="breadcrumb"></asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<link rel="Stylesheet" type="text/css" href="../Styles/smoothness/jquery-ui-1.8.15.custom.css" />
<style type="text/css">
/* css for timepicker */
.ui-timepicker-div .ui-widget-header{ margin-bottom: 8px; }
.ui-timepicker-div dl{ text-align: left; }
.ui-timepicker-div dl dt{ height: 25px; }
.ui-timepicker-div dl dd{ margin: -25px 0 10px 65px; }
.ui-timepicker-div td { font-size: 90%; }
</style>

<style tyle="text/css">
td
{
    vertical-align:middle;
}
</style>

<script type="text/javascript" src="../Scripts/jquery-ui-1.8.15.custom.min.js"></script>
<script type="text/javascript" src="../Scripts/jquery-ui-timepicker-addon.js"></script>
<script type="text/javascript" src="../Scripts/jquery.maskedinput-1.3.min.js"></script>
<script type="text/javascript" src="../Scripts/jquery.dateFormat-1.0.js"></script>

<!-- qtip 2 -->
<link rel="Stylesheet" type="text/css" href="../Styles/jquery.qtip.min.css" />
<script type="text/javascript" src="../Scripts/jquery.qtip.min.js"></script>

<script language="javascript" type="text/javascript">
    var startTimeCtrId = '#<%= StartTimeCtr.ClientID %>';
    var endTimeCtrId = '#<%= EndTimeCtr.ClientID %>';
    var regDeadlineCtrId = '#<%= RegDeadlineCtr.ClientID %>';
    var meetTimeCtrId = '#<%= MeetTime.ClientID %>';
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
        $('#<%= HostPhone.ClientID %>').mask("(999) 999-9999");

        //qtip
        $('.fieldName').qtip(
        {
            content: { attr: 'alt' },
            position: { my: 'left center', at: 'right center' }
        });
    });
</script>

<style type="text/css">
.catHeaderBox 
{
    border-bottom: 1px solid #808080;
    width:430px;
    margin-top:5px;
    margin-left:10px;
}

.catFieldBox
{
    margin-top:5px;
    margin-left:15px;
}
    .style1
    {
        color: #CCCCCC;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Propose an Event</h1>
        <br />
        <div class="catHeaderBox">
        <strong>Host Info</strong></div>
        <table border="0" cellspacing="5" class="catFieldBox">
            <tr>
                    <td  style="width:100px">
                        <span class="fieldName"
                            alt="The name of the organization hosting this event. Ex: Bass Club."
                            >Organization</span><br />
                        <span class="style1">(optional)</span></td>
                    <td>
                        <asp:TextBox ID="HostOrg" runat="server" Width="300px" MaxLength="300"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
            <tr>
                    <td><span class="fieldName"
                            alt="Provide your name even if you are representing an organization.">Contact Name</span>
                        </td>
                    <td>
                        <asp:TextBox ID="HostName" runat="server" Width="300px" MaxLength="300"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="hostNameReq" runat="server" 
                        ErrorMessage="Host name is required" ControlToValidate="HostName" 
                            Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td><span class="fieldName"
                            alt="Description">Contact Email</span></td>
                    <td>
                        <asp:TextBox ID="HostEmail" runat="server" Width="300px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" 
                            ControlToValidate="HostEmail" ErrorMessage="Host email is invalid" 
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                            Display="Dynamic"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="HostEmailReq" runat="server" 
                            ControlToValidate="HostEmail" Display="Dynamic" 
                            ErrorMessage="Host email is required"></asp:RequiredFieldValidator>
                    </td>
                 </tr>
                 <tr>
                    <td><span class="fieldName"
                            alt="Description">Contact Phone</span></td>
                    <td>
                        <asp:TextBox ID="HostPhone" runat="server" Width="300px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                        ErrorMessage="Host phone is required" ControlToValidate="HostPhone" 
                            Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>
                 </tr></table>
        <br />
        <div class="catHeaderBox">
            <strong>Event Info</strong></div>
            <table border="0" cellspacing="5" class="catFieldBox">
                <tr>
                    <td style="width:100px"><span class="fieldName"
                            alt="Carefully select 2-4 words that identify the main idea. 
                            Details should be reserved for the description section that follows.">Event Name</span></td>
                    <td>
                        <asp:TextBox ID="EventName" runat="server" Width="300px" MaxLength="255"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="EventNameReq" runat="server" 
                        ErrorMessage="Give this event a name" ControlToValidate="EventName" 
                            Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td><span class="fieldName"
                            alt="Description">Date and Time</span></td>
                    <td>
                        <asp:TextBox ID="StartTimeCtr" runat="server" Width="138px"></asp:TextBox>
                        to
                        <asp:TextBox ID="EndTimeCtr" runat="server" Width="138px"></asp:TextBox>
                    </td>
                    <td>
                    </td>
                 </tr>
                <tr>
                    <td>Registration<br />
                        Deadline</td>
                    <td>
                        <asp:TextBox ID="RegDeadlineCtr" runat="server" Width="138px"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;</td>
                 </tr>
                 <tr>
                    <td><span class="fieldName"
                            alt="Where the event actually occurs, not the place where
                            people meet for transport.">Location</span></td>
                    <td>
                        <asp:TextBox ID="Location" runat="server" Width="300px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="LocationReq" runat="server" 
                        ErrorMessage="Give this event a name" ControlToValidate="Location" 
                            Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>
                 </tr>
                 <tr>
                    <td><span class="fieldName"
                            alt="The time when people need to meet in order to arrange
                            travel to the event location.">Meeting Time</span></td>
                    <td>
                        <asp:TextBox ID="MeetTime" runat="server" Width="140px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="MeetTimeReq" runat="server" 
                        ErrorMessage="Meeting time is required" ControlToValidate="MeetTime" 
                            Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>
                 </tr>
                 <tr>
                    <td><span class="fieldName"
                            alt="Location where people meet to arrange travel or other logistical items.">
                            Meeting Location</span></td>
                    <td>
                        <asp:TextBox ID="MeetLocation" runat="server" Width="300px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="MeetLocReq" runat="server" 
                        ErrorMessage="Meet Location is required" ControlToValidate="MeetLocation" 
                            Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>
                 </tr>
                 <tr>
            <td  style="vertical-align:middle"><span class="fieldName"
                            alt="Anything relevent to the participants of the event such as activities and
                            schedule.">Description</span></td>
            <td>
                <asp:TextBox ID="Desc" runat="server" TextMode="MultiLine" Height="95px" 
                    Width="300px"></asp:TextBox>                
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                    ErrorMessage="Give this event more info" ControlToValidate="Desc" 
                    Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
        </tr>
                 </table>       
<br />
        <div class="catHeaderBox">
            <strong><span class="fieldName"
                            alt="Description">Additional Info</span></strong></div>
            
<table border="0" cellspacing="5" class="catFieldBox">
                 <tr>
                    <td style="width:100px; vertical-align:middle"><span class="fieldName"
                            alt="Form of transportation. Let participants know whether
                            the host will provide it…or if participants must provide
                            their own. Also, specify here whether other students will
                            be providing transportation.">Transportation</span></td>
                    <td>
                <asp:TextBox ID="Transport" runat="server" TextMode="MultiLine" Height="95px" 
                    Width="300px" MaxLength="255"></asp:TextBox>                
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="TransportReq" runat="server" 
                        ErrorMessage="Transportation info is required" ControlToValidate="Transport" 
                            Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>
                 </tr>
                 <tr>
                    <td style="width:100px"><span class="fieldName"
                            alt="Please check here if you'd like to ask participants to
                            provide transportation for other participants.">Request Drivers</span></td>
                    <td>
                        <asp:CheckBox ID="RequestDrivers" runat="server" />
                    </td>
                    <td>
                        &nbsp;</td>
                 </tr>
                 <tr>
                    <td style="vertical-align:middle"><span class="fieldName"
                            alt="Describe the cost per student (e.g., entrance fees,
                            equipment rental or gas). You will need a simple plan to
                            manage collection of this fee.">Costs</span></td>
                    <td>
                <asp:TextBox ID="Costs" runat="server" TextMode="MultiLine" Height="95px" 
                    Width="300px" MaxLength="255"></asp:TextBox>                
                    </td>
                    <td>
                    </td>
                 </tr>
                 <tr>
                    <td><span class="fieldName"
                            alt="Specify clearly any necessary equipment that may be needed.">Equipment</span></td>
                    <td>
                <asp:TextBox ID="Equipment" runat="server" TextMode="MultiLine" Height="95px" 
                    Width="300px" MaxLength="255"></asp:TextBox>                
                    </td>
                    <td>
                    </td>
                 </tr>
                 <tr>
            <td><span class="fieldName"
                            alt="If your event span a normal meal time you must specify
                            who is responsible for it. If you (host) plan to provide it,
                            specify what it will be so that people can decide whether or
                            not to bring their own. Remember, some people have dietary
                            restrictions.">Food</span></td>
            <td>
                <asp:TextBox ID="Food" runat="server" TextMode="MultiLine" Height="95px" 
                    Width="300px" MaxLength="255"></asp:TextBox>                
            </td>
            <td>
                &nbsp;</td>
        </tr>
                 <tr>
            <td><span class="fieldName"
                            alt="Include notes on anything important that was not
                            previously identified.">Other</span></td>
            <td>
                <asp:TextBox ID="Other" runat="server" TextMode="MultiLine" Height="95px" 
                    Width="300px" MaxLength="255"></asp:TextBox>                
            </td>
            <td>
                &nbsp;</td>
        </tr>
                 <tr>
            <td><span class="fieldName" alt="The maximum number people who can register
            for this event. If blank, there is no limit.">Event Capacity</span></td>
            <td>
                <asp:TextBox ID="GuestLimit" runat="server" 
                    Width="59px"></asp:TextBox>                
                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                    ControlToValidate="GuestLimit" ErrorMessage="This should be a positive number" 
                    ValidationExpression="\d+" Display="Dynamic"></asp:RegularExpressionValidator>
            </td>
            <td>
            </td>
        </tr>
                 </table>

    <br />
    <table border="0" cellspacing="5" class="catFieldBox">
        
        <tr>
            <td style="width:100px"></td>
            <td>
                <asp:CheckBox ID="agreeChk" runat="server" EnableViewState="False" />&nbsp;I agree to
                the <a href="EventHostAgreement.htm" target="_blank">conditions</a> of hosting a community event
                 
            </td>
            <td></td>
        </tr>

        <tr>
            <td></td>
            <td colspan="2">
                <asp:Label ID="msgLbl" runat="server" EnableViewState="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:Button ID="createBtn" runat="server" Text="Create" 
                    onclick="createBtn_Click" />
                &nbsp;&nbsp;
                <asp:Button ID="cancelBtn" runat="server" Text="Cancel" />
            </td>
            <td></td>
        </tr>
    </table>
</asp:Content>
