<%@ Page Title="" Language="C#" MasterPageFile="~/AULayout1.Master" AutoEventWireup="true" CodeBehind="Signup.aspx.cs" Inherits="AUInterconnect.Views.Events.Signup" %>

<asp:Content ID="TitleContent" ContentPlaceHolderID="title" runat="server">
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
<style type="text/css">
.fieldName
{
    font-weight:bold;
}
    .style1
    {
        color: #C0C0C0;
    }
</style>
</asp:Content>

<asp:Content ID="Breadcrumb" ContentPlaceHolderID="breadcrumb" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Event Sign Up</h1>

    <asp:Literal ID="ErrorLit" runat="server"></asp:Literal>

    <table cellspacing="5">
        <tr>
            <td class="fieldName">Event Name</td>
            <td>
                <asp:Literal ID="EventName" runat="server"></asp:Literal></td>
        </tr>
        <tr>
            <td class="fieldName">Hosted By</td>
            <td>
                <asp:Literal ID="EventHost" runat="server"></asp:Literal></td>
        </tr>
        <tr>
            <td class="fieldName">Date</td>
            <td>
                <asp:Literal ID="Date" runat="server"></asp:Literal></td>
        </tr>
        <tr>
            <td class="fieldName">Time</td>
            <td>
                <asp:Literal ID="Time" runat="server"></asp:Literal></td>
        </tr>
    </table>

    <br />

    <table cellspacing="5">
        <tr>
            <td>Party size<br />
                <span class="style1">(Include yourself)</span></td>
            <td>
                <asp:TextBox ID="HeadCount" runat="server" Width="80px">1</asp:TextBox>
            </td>
            <td>
                <asp:RangeValidator ID="RangeValidator1" runat="server" 
                    ControlToValidate="HeadCount" Display="Dynamic" 
                    ErrorMessage="Must be 1 or greater" MinimumValue="1" Type="Integer" 
                    MaximumValue="25"></asp:RangeValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="HeadCount" Display="Dynamic" ErrorMessage="Required"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>I can drive</td>
            <td>
                <asp:CheckBox ID="CanDrive" runat="server" />
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>Number of person
                <br />
                I can take<br />
                <span class="style1">(Include yourself)</span></td>
            <td>
                <asp:TextBox ID="VehicleCap" runat="server" Width="80px">0</asp:TextBox>
            </td>
            <td>
                <asp:RangeValidator ID="RangeValidator2" runat="server" 
                    ControlToValidate="VehicleCap" Display="Dynamic" 
                    ErrorMessage="Must be positive" MinimumValue="0" Type="Integer" 
                    MaximumValue="100"></asp:RangeValidator>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:CheckBox ID="AgreeCheckbox" runat="server" />
                I agree the <a href="SignupCondition.htm" target="_blank">term and conditions</a>.
            </td>
            <td>
                &nbsp;</td>
        </tr>
    </table>

    <br />

    <asp:Button ID="regBtn" runat="server" Text="Sign Me Up!" 
        onclick="regBtn_Click" />
    <asp:Button ID="UpdateButton" runat="server" onclick="UpdateButton_Click" 
        Text="Update" />
</asp:Content>
