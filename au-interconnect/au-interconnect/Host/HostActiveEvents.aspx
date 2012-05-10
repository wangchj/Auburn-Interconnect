<%@ Page Title="" Language="C#" MasterPageFile="~/AULayout1.master" AutoEventWireup="true" CodeBehind="HostActiveEvents.aspx.cs" Inherits="AUInterconnect.Views.Host.HostActiveEvents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
Auburn Interconnect - Host - Active Events
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
<style type="text/css"">
.eventCell
{
    border-bottom-style:dashed;
    border-bottom-width:1px;
    border-bottom-color:LightGray;
}

a.eventActionImageLink:hover
{
    text-decoration:none;
}
</style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="breadcrumb" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
<h1>Active Events</h1>
<asp:Table ID="EventTable" runat="server" CellPadding="5" CellSpacing="5" Width="100%" Border="1">
</asp:Table>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="sidebar" runat="server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="lastUpdated" runat="server">
</asp:Content>
