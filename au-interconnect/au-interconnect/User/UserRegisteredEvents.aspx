<%@ Page Title="" Language="C#" MasterPageFile="~/AULayout1.master" AutoEventWireup="true" CodeBehind="UserRegisteredEvents.aspx.cs" Inherits="AUInterconnect.Views.User.UserRegisteredEvents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css"">
.eventCell
{
    border-bottom-style:dashed;
    border-bottom-width:1px;
    border-bottom-color:LightGray;
}
</style>

<script language="javascript" type="text/javascript">
    $(function () {
        //qtip
        $('.ShortEventInfo_EventName').click(function (event) { event.preventDefault(); });
        $('.ShortEventInfo_EventName').qtip(
        {
            content:
            { text:
                function () {
                    var eventId = getParameterByName("EventId", $(this).attr("href"));
                    var userId = $("#<%=UserID.ClientID %>").attr("value");
                    var template = $("#EventMenuTemplate");
                    var r = "<table><tr><td>" +
                    "<a href='../Events/EventDetails.aspx?EventId=" + eventId + "'>View Event Details</a></td></tr>" +
                    "<tr><td><a href='../Events/Signup.aspx?EventId=" + eventId + "&Update=1'>Change My Registration</a></td></tr>" +
                    "<tr><td><a href='javascript:removeEvent(" + eventId + ","  + userId + ")'>Remove Event</a></td></tr></table>";
                    return r;
                }
            },
            position: { my: 'left center', at: 'right center' },
            show: {
                event: 'click', // Show it on click...
                solo: true // ...and hide all other tooltips...
            },
            hide: 'unfocus',
            style: {
                classes: 'ui-tooltip-light ui-tooltip-shadow ui-tooltip-rounded'
            }
        });
    });

    
    function getParameterByName(name, url)
    {
        name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
        var regexS = "[\\?&]" + name + "=([^&#]*)";
        var regex = new RegExp(regexS);
        var results = regex.exec(url);
        if (results == null)
            return "";
        else
            return decodeURIComponent(results[1].replace(/\+/g, " "));
    }

    function removeEvent(eventId, userId)
    {
        $.ajax({
            type: "POST",
            url: "UserRegisteredEvents.aspx/RemoveRegistration",
            data: "{'eventId':" + eventId + ",'userId':" + userId + "}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                if (msg.d) {
                    location.reload();
                }
                else {
                    alert("Unable to delete event. Please contact adminstrator.");
                }
            }
        });
    }
</script>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="breadcrumb" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="UserID" runat="server" />
<h1>Upcoming Events</h1>
<asp:Table ID="EventListTable" runat="server" CellPadding="5" CellSpacing="5">
</asp:Table>

<table id="EventMenuTemplate" style="display:none">
<tr><td><a id="EventMenuTemplate_1" href="">View Event Details</a></td></tr>
<tr><td><a id="EventMenuTemplate_2" href="">Change My Registration</a></td></tr>
<tr><td><a id="EventMenuTemplate_3" href="">Remove Event</a></td></tr>
</table>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="sidebar" runat="server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="lastUpdated" runat="server">
</asp:Content>
