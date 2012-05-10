<%@ Page Title="" Language="C#" MasterPageFile="~/AULayout1.master" AutoEventWireup="true" CodeBehind="UserAccountInfo.aspx.cs" Inherits="AUInterconnect.Views.User.UserAccountInfo"%>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="breadcrumb" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
<h1>Account Information</h1>
<table>
<tr><td><img src="../images/1324450383_user.png" /></td>
<td style="vertical-align:middle">
    <asp:Label ID="NameLabel" runat="server" Text=""></asp:Label></td></tr>
<tr><td>Email:</td>
<td>
    <asp:Label ID="EmailLabel" runat="server" Text=""></asp:Label></td></tr>
<tr><td>Phone:</td>
<td>
    <asp:Label ID="PhoneLabel" runat="server" Text=""></asp:Label></td></tr>
<tr><td colspan="2">
[<a href="UserUpdate.aspx">change info</a>]
[<a href="UserPasswordChange.aspx">chage password</a>]
</td></tr>
</table>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="sidebar" runat="server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="lastUpdated" runat="server">
</asp:Content>
