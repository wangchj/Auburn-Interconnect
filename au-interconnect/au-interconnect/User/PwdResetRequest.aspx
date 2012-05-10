<%@ Page Title="" Language="C#" MasterPageFile="~/AULayout1.master" AutoEventWireup="true" CodeBehind="PwdResetRequest.aspx.cs" Inherits="AUInterconnect.Views.User.PwdResetRequest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="breadcrumb" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
<h1>Reset Password</h1>

<p>
Give us a little information about your account and we will send a password reset
code to your email address. If you already have a reset code, enter it 
<a href="PwdReset.aspx">here</a>.
</p>

<p>
    <asp:Literal ID="ErrorLit" runat="server"></asp:Literal>
</p>

<table cellspacing="5">
<tr><td>First Name</td><td>
    <asp:TextBox ID="FirstName" runat="server" EnableViewState="False"></asp:TextBox></td><td>
        <asp:RequiredFieldValidator ID="FirstNameReq" runat="server" 
            ControlToValidate="FirstName" Display="Dynamic" 
            ErrorMessage="First name is required"></asp:RequiredFieldValidator>
    </td></tr>
<tr><td>Email</td><td>
    <asp:TextBox ID="Email" runat="server" EnableViewState="False"></asp:TextBox></td><td>
        <asp:RequiredFieldValidator ID="EmailReq" runat="server" 
            ControlToValidate="Email" Display="Dynamic" ErrorMessage="Email is required"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="EmailVal" runat="server" 
            ControlToValidate="Email" Display="Dynamic" ErrorMessage="Email is invalid" 
            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
    </td></tr>
    <tr><td></td><td>
        <asp:Button ID="Submit" runat="server" Text="Submit" onclick="Submit_Click" /></td><td></td></tr>
</table>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="sidebar" runat="server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="lastUpdated" runat="server">
</asp:Content>
