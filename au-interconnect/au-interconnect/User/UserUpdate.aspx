<%@ Page Title="Sign Up" Language="C#" MasterPageFile="~/AULayout1.master" AutoEventWireup="true"
    CodeBehind="UserUpdate.aspx.cs" Inherits="AUInterconnect.Views.User.UserUpdate" %>

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
    <h1>User Information Update</h1>
    <table border="0" cellspacing="5">
    <tr>
        <td>First Name</td>
        <td>
            <asp:TextBox ID="fnTxb" runat="server" Width="200px" EnableViewState="False"></asp:TextBox>
        </td>
        <td>
            <asp:RequiredFieldValidator ID="FnReqFldVal" runat="server" ErrorMessage="First Name is required" ControlToValidate="fnTxb"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td>Last Name</td>
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
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>Email</td>
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
        <td></td>
        <td colspan="2">
            <asp:Label ID="ErrorLbl" runat="server" EnableViewState="False"></asp:Label></td>
    </tr>
    <tr>
        <td></td>
        <td>
            <asp:Button ID="UpdateButton" runat="server" Text="Update" 
            EnableViewState="False" onclick="UpdateButton_Click" /></td>
        <td></td>
    </tr>
    </table>
</asp:Content>
