<%@ Page Title="" Language="C#" MasterPageFile="~/AULayout1.master" AutoEventWireup="true" CodeBehind="UserPasswordChange.aspx.cs" Inherits="AUInterconnect.Views.User.UserPasswordChange" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="breadcrumb" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
<h1>Change Password</h1>
    <asp:Literal ID="ErrorLiteral" runat="server"></asp:Literal>
<table>
<tr><td>Old Password</td><td>
    <asp:TextBox ID="OldPwd" runat="server" TextMode="Password"></asp:TextBox></td><td>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
            ControlToValidate="OldPwd" ErrorMessage="Required" Display="Dynamic"></asp:RequiredFieldValidator>
    </td></tr>
    <tr><td>New Password</td><td>
    <asp:TextBox ID="NewPwd" runat="server" TextMode="Password"></asp:TextBox></td><td>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                ControlToValidate="NewPwd" ErrorMessage="Required" Display="Dynamic"></asp:RequiredFieldValidator>
        </td></tr>
    <tr><td>Retype New Password</td><td>
    <asp:TextBox ID="NewPwdRetype" runat="server" TextMode="Password"></asp:TextBox></td><td>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                ControlToValidate="NewPwdRetype" ErrorMessage="Required" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator1" runat="server" 
                ControlToCompare="NewPwd" ControlToValidate="NewPwdRetype" 
                ErrorMessage="New password don't match" Display="Dynamic"></asp:CompareValidator>
        </td></tr>
    <tr><td colspan="3">
        <asp:Button ID="SubmitButton" runat="server" Text="Submit" 
            onclick="SubmitButton_Click" /></td></tr>
</table>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="sidebar" runat="server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="lastUpdated" runat="server">
</asp:Content>
