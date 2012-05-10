<%@ Page Title="" Language="C#" MasterPageFile="~/AULayout1.master" AutoEventWireup="true" CodeBehind="PwdReset.aspx.cs" Inherits="AUInterconnect.Views.User.PwdReset" ValidateRequest="false"%>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="breadcrumb" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Reset Password</h1>

<p>
    <asp:Literal ID="ErrorLit" runat="server"></asp:Literal>
</p>

<table cellspacing="5">
<tr><td>Email</td><td>
    <asp:TextBox ID="Email" runat="server" EnableViewState="False"></asp:TextBox></td><td>
        <asp:RequiredFieldValidator ID="EmailReq" runat="server" 
            ControlToValidate="Email" Display="Dynamic" 
            ErrorMessage="Email is required"></asp:RequiredFieldValidator>
    </td></tr>
<tr><td>Reset Code</td><td>
    <asp:TextBox ID="ResetCode" runat="server" EnableViewState="False"></asp:TextBox></td><td>
        <asp:RequiredFieldValidator ID="CodeReq" runat="server" 
            ControlToValidate="ResetCode" Display="Dynamic" 
            ErrorMessage="Reset code is required"></asp:RequiredFieldValidator>
    </td></tr>
    <tr><td></td><td>
        <asp:Button ID="Submit" runat="server" Text="Submit" onclick="Submit_Click" /></td><td></td></tr>
</table>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="sidebar" runat="server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="lastUpdated" runat="server">
</asp:Content>
