﻿<%@ Page Title="" Language="C#" MasterPageFile="~/AuLayout1.Master" AutoEventWireup="true" CodeBehind="EventDetails.aspx.cs" Inherits="AUInterconnect.Views.Events.EventDetails" %>

<asp:Content ID="TitleContent" ContentPlaceHolderID="title" runat="server">
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<style type="text/css">
td{vertical-align:top}
td.fldLbl{font-weight:bold}
</style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="breadcrumb" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h1><asp:Literal ID="EventName" runat="server"></asp:Literal></h1>
        
        <asp:Literal ID="BigEventTime" runat="server" Visible="false"></asp:Literal>
        
    <p>
        <!-- strong>Date and time:</strong -->
        <asp:Literal ID="StartTime" runat="server"></asp:Literal> -
        <asp:Literal ID="EndTime" runat="server"></asp:Literal>
    </p>
    <p>
        <strong>Description</strong><br />
        <asp:Literal ID="Desc" runat="server"></asp:Literal>
    </p>
        <strong>Details</strong>
        <table>
        <tr><td>Host</td><td><asp:Literal ID="HostName" runat="server"></asp:Literal></td></tr>
        <tr><td>Capacity</td><td><asp:Literal ID="EventCap" runat="server"></asp:Literal></td></tr>
        <tr><td>Location</td><td><asp:Literal ID="Location" runat="server"></asp:Literal></td></tr>
        <tr><td>Meeting Time</td><td><asp:Literal ID="MeetTime" runat="server"></asp:Literal></td></tr>
        <tr><td>Meeting Location</td><td><asp:Literal ID="MeetLocation" runat="server"></asp:Literal></td></tr>
        <tr><td>Transportation</td><td><asp:Literal ID="Transportation" runat="server"></asp:Literal></td></tr>
        <tr><td>Equipment</td><td><asp:Literal ID="Equipments" runat="server"></asp:Literal></td></tr>
        <tr><td>Costs</td><td><asp:Literal ID="Costs" runat="server"></asp:Literal></td></tr>
        </table>

    <br />
    <p>
                <asp:Button ID="regBtn" runat="server" Text="Sign Up" onclick="regBtn_Click" />
    </p>
</asp:Content>
