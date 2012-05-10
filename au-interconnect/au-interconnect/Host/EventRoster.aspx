<%@ Page Title="" Language="C#" MasterPageFile="~/AULayout1.master" AutoEventWireup="true" CodeBehind="EventRoster.aspx.cs" Inherits="AUInterconnect.Views.Host.EventRoster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
<style>
.rosterTable
{
    border-width:0px;
    border-collapse:collapse;
    border-spacing:none;
}
.rosterTableHeaderCell
{
    border-spacing:none;
    border-bottom-width:2px;
    border-bottom-style:solid;
    padding:10px;
    border-bottom-color:Black;
    font-weight:bold;
}
.rosterTableFooterCell
{
    border-spacing:none;
    border-top-width:2px;
    border-top-style:solid;
    padding:10px;
    border-top-color:Black;
    font-weight:bold;
}
.rosterTableFooterCell_CenterAlign
{
    border-spacing:none;
    border-top-width:2px;
    border-top-style:solid;
    padding:10px;
    border-top-color:Black;
    font-weight:bold;
    text-align:center;
}
.rosterTableCell
{
    padding:10px;
    border-spacing:none;
    border-bottom-width:1px;
    border-bottom-style:solid;
    border-bottom-color:Black;
}
.rosterTableCell_CenterAlign
{
    padding:10px;
    border-spacing:none;
    border-bottom-width:1px;
    border-bottom-style:solid;
    border-bottom-color:Black;
    text-align:center;
}
</style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="breadcrumb" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
<h1>Event Roster</h1>
    <asp:Table ID="RosterTable" runat="server" CellPadding="5" 
        CellSpacing="5" class="rosterTable">
        <asp:TableHeaderRow CssClass="rosterTableHeaderRow">
        <asp:TableHeaderCell CssClass="rosterTableHeaderCell">Participant</asp:TableHeaderCell>
        <asp:TableHeaderCell CssClass="rosterTableHeaderCell">Party Size</asp:TableHeaderCell>
        <asp:TableHeaderCell CssClass="rosterTableHeaderCell">Vehicle Capacity</asp:TableHeaderCell>
        </asp:TableHeaderRow>
    </asp:Table>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="sidebar" runat="server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="lastUpdated" runat="server">
</asp:Content>
