<%@ Page Title="Sign Up" Language="C#" MasterPageFile="~/AULayout1.master" AutoEventWireup="true"
    CodeBehind="Reg.aspx.cs" Inherits="AUInterconnect.Views.User.Reg" %>
<%@ Register TagPrefix="cc1" Namespace="WebControlCaptcha" Assembly="WebControlCaptcha" %>

<asp:Content ID="Title" runat="server" ContentPlaceHolderID="title">
Auburn Interconnect - User Registration
</asp:Content>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript" src="../Scripts/jquery.maskedinput-1.3.min.js"></script>

<script language="javascript" type="text/javascript">
    $(function () {
        $('#<%= phoneTxb.ClientID %>').mask("(999) 999-9999");
    });
</script>
</asp:Content>

<asp:Content ID="Breadcrumb" runat="server" ContentPlaceHolderID="breadcrumb"></asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h1>Sign Up</h1>
    <p>We just need a few pieces of information from you to add you as a
    new user.</p>
    <p>
    <table border="0" cellspacing="5">
    <tr>
        <td>First Name*</td>
        <td>
            <asp:TextBox ID="fnTxb" runat="server" Width="200px" EnableViewState="False"></asp:TextBox>
        </td>
        <td>
            <asp:RequiredFieldValidator ID="FnReqFldVal" runat="server" ErrorMessage="First Name is required" ControlToValidate="fnTxb"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td>Last Name*</td>
        <td>
            <asp:TextBox ID="lnTxb" runat="server" Width="200px"></asp:TextBox>
        </td>
        <td>
            <asp:RequiredFieldValidator ID="LnReqFldVal" runat="server" 
                ErrorMessage="Last Name is required" ControlToValidate="lnTxb" 
                Display="Dynamic"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td>Phone</td>
        <td>
            <asp:TextBox ID="phoneTxb" runat="server" Width="200px"></asp:TextBox></td>
        <td></td>
    </tr>
    <tr>
        <td>Email*</td>
        <td>
            <asp:TextBox ID="emailTxb" runat="server" Width="200px"></asp:TextBox></td>
        <td>
            <asp:RequiredFieldValidator ID="EmailReqFldVal" runat="server" 
                ErrorMessage="Email is required" ControlToValidate="emailTxb" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="EmailRegexVal" runat="server" 
                ErrorMessage="Email is invalid" ControlToValidate="emailTxb" 
                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                Display="Dynamic"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td>Re-enter Email*</td>
        <td>
            <asp:TextBox ID="vemailTxb" runat="server" Width="200px"></asp:TextBox></td>
        <td>
            <asp:CompareValidator ID="VemailCompVal" runat="server" 
                ErrorMessage="Emails do not match" ControlToValidate="vemailTxb" 
                ControlToCompare="emailTxb" Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    <tr>
        <td>New Password*</td>
        <td>
            <asp:TextBox ID="pwdTxb" runat="server" TextMode="Password" Width="200px"></asp:TextBox></td>
        <td>
            <asp:RequiredFieldValidator ID="PwdReqFldVal" runat="server" 
                ErrorMessage="Password is required" ControlToValidate="pwdTxb" 
                Display="Dynamic"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
    <td colspan="2">
        <cc1:captchacontrol id="CaptchaControl1" runat="server" 
            LayoutStyle="Vertical" CaptchaBackgroundNoise="High" ></cc1:captchacontrol></td>
    <td></td>
    </tr>
    <tr>
        <td></td>
        <td colspan="2">
            <asp:Label ID="ErrorLbl" runat="server" EnableViewState="False"></asp:Label></td>
    </tr>
    <tr>
        <td></td>
        <td>
            <asp:Button ID="submitBtn" runat="server" Text="Sign Up" 
            EnableViewState="False" onclick="submitBtn_Click" /></td>
        <td></td>
    </tr>
    </table>

    </p>
</asp:Content>
