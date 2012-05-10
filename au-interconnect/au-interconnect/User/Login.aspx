<%@ Page Title="Log In" Language="C#" MasterPageFile="~/AULayout1.master" AutoEventWireup="true"
    CodeBehind="Login.aspx.cs" Inherits="AUInterconnect.Views.User.Login" %>

<asp:Content ID="Content3" ContentPlaceHolderID="title" runat="server">
Auburn Interconnect - Log In
</asp:Content>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>

<asp:Content ID="Breadcrumb" runat="server" ContentPlaceHolderID="breadcrumb"></asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h1>Log In</h1>
    
    <asp:Panel ID="Panel1" runat="server">
    
    <p>
        <asp:Label ID="emailLbl" runat="server" AssociatedControlID="emailTxb">Email</asp:Label>
        <br />
        <asp:TextBox ID="emailTxb" runat="server" CssClass="textEntry" Width="200px"></asp:TextBox>
        <asp:RequiredFieldValidator ID="emailReq" runat="server" ControlToValidate="emailTxb"
            ErrorMessage="Email is required" 
            ToolTip="User Name is required" Display="Dynamic"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="emailRegex" runat="server" 
            ErrorMessage="Email is invalid" Display="Dynamic" 
            ControlToValidate="emailTxb" 
            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
    </p>
    <p>
        <asp:Label ID="pwdLbl" runat="server" AssociatedControlID="pwdTxb">Password</asp:Label>
        <br />
        <asp:TextBox ID="pwdTxb" runat="server" CssClass="passwordEntry" TextMode="Password"
            Width="200"></asp:TextBox>
        <asp:RequiredFieldValidator ID="pwdReq" runat="server" ControlToValidate="pwdTxb"
            ErrorMessage="Password is required" ToolTip="Password is required">
        </asp:RequiredFieldValidator>
    </p>
    </asp:Panel>
    
    <asp:Panel ID="Panel2" runat="server">
    <br />
    Currently events are limited only to Auburn University students.
    Please verify that you're an Auburn student below.
    <p>
    Auburn User Name
    <br />
        <asp:TextBox ID="auusrTxb" runat="server" Width="200px"></asp:TextBox>
    </p>
    <p>
    Password
    <br />
        <asp:TextBox ID="aupwdTxb" runat="server" Width="200px" TextMode="Password"></asp:TextBox>
    </p>
    </asp:Panel>
    
    <asp:Literal ID="FailureText" runat="server"></asp:Literal>
    <asp:Literal ID="AuLoginLit" runat="server"></asp:Literal>
    <asp:Button ID="LoginButton" runat="server" CommandName="Login" 
    Text="Log In" onclick="LoginButton_Click" />
    &nbsp;&nbsp;&nbsp;
    <a href="Reg.aspx">Register</a> | <a href="PwdResetRequest.aspx">Forgot Password</a>
</asp:Content>
