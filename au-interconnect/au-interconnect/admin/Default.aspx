<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Admin.master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AUInterconnect.admin.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">Admin Dashboard</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadCount" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<h1><img src="../images/chart_bar.png" width="32px" /> Admin Dashboard</h1>

<table cellspacing="5">
<tr><td><b>Events</b></td><td rowspan="3" width="15px"></td></tr>
<tr>
    <td>
    <asp:HyperLink ID="ActiveEventsCount" runat="server" 
            NavigateUrl="Events/ActiveEvents.aspx"> Events Happening Now</asp:HyperLink></td>
    <td><asp:HyperLink ID="UpcomingEventsCount" runat="server" NavigateUrl="Events/UpcomingEvents.aspx"> Upcoming Events</asp:HyperLink></td>
</tr>
<tr>
<td>
    <asp:HyperLink ID="UnapprovedEventsCount" runat="server" NavigateUrl="Events/UnapprovedEvents.aspx"> Unapproved Events</asp:HyperLink></td>
<td><asp:HyperLink ID="DeclinedEventsCount" runat="server" NavigateUrl="Events/DeclinedEvents.aspx"> Declined Events</asp:HyperLink></td>
</tr>
<td><asp:HyperLink ID="TotalEventsCount" runat="server" NavigateUrl="Events"> Total Events in System</asp:HyperLink></td>
</table>

<hr />

<table cellspacing="5">
<tr><td><b>Users</b></td><td rowspan="3" width="15px"></td></tr>
<tr>
    <td>
    <asp:HyperLink ID="TotalUserCount" runat="server" 
            NavigateUrl="Users/"> Total Users</asp:HyperLink></td>
    <td></td>
</tr>
<tr>
<td></td>
<td></td>
</tr>
<td></td>
<td></td>
</table>
</asp:Content>
