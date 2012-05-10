<%@ Page Title="" Language="C#" MasterPageFile="~/AULayout1.master" AutoEventWireup="true" CodeBehind="UpcomingEvents.aspx.cs" Inherits="AUInterconnect.Views.Events.UpcomingEvents" %>
<%@ Register TagPrefix="uc" TagName="DateIcon" Src="~/UserControls/DateIcon.ascx" %>
<%@ Register TagPrefix="uc" TagName="ShortEventInfo" Src="~/UserControls/ShortEventInfo.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
Auburn Interconnect - Upcoming Events
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="breadcrumb" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">

<h1>Upcoming Events</h1>
    <asp:Table ID="EventListTable" runat="server" CellPadding="5" CellSpacing="5">
    </asp:Table>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="lastUpdated" runat="server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="sidebar" runat="server">
</asp:Content>
