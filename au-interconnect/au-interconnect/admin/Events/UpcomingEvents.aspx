<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Events/AdminEvents.master" AutoEventWireup="true" CodeBehind="UpcomingEvents.aspx.cs" Inherits="AUInterconnect.admin.Events.UpcomingEvents" %>

<script runat="server">
/// <summary>
/// Format DateTime string to be displayed on the grid.
/// </summary>
/// <param name="datetime">The DateTime instance to be formatted.</param>
/// <returns>Formatted string</returns>
private string GetFormattedDateTime(DateTime datetime)
{
    return datetime.ToShortDateString() + "<br />" +
        datetime.ToShortTimeString();
}

private string GetFormattedPhone(object phone)
{
    StringBuilder builder = new StringBuilder(phone.ToString());
    builder.Insert(0, '(');
    builder.Insert(4, ')');
    builder.Insert(8, '-');
    return builder.ToString();
}

</script>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadCount" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Upcoming Events</h1>
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
        AutoGenerateColumns="False" CellPadding="4" DataKeyNames="eventId" 
        DataSourceID="SqlDataSource1" ForeColor="#333333" GridLines="None" Width="100%">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:BoundField DataField="eventId" HeaderText="eventId" InsertVisible="False" 
                ReadOnly="True" SortExpression="eventId" Visible="False" >
            <HeaderStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Event Name" SortExpression="eventName">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("eventName") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                <asp:HyperLink ID="EventNameHyperlink" runat="server" 
                        NavigateUrl='<%# "EventDetails.aspx?EventId="+Eval("eventId") %>' 
                        Text='<%# Eval("eventName") %>'></asp:HyperLink>
                    <%--<asp:Label ID="Label1" runat="server" Text='<%# Bind("eventName") %>'></asp:Label>--%>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Start Time" SortExpression="startTime">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("startTime") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# GetFormattedDateTime((DateTime)Eval("startTime")) %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:BoundField DataField="hostName" HeaderText="Contact Name" 
                SortExpression="hostName" >
            <HeaderStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="hostEmail" HeaderText="Contact Email" 
                SortExpression="hostEmail" >
            <HeaderStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Contact Phone" SortExpression="hostPhone">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("hostPhone") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# GetFormattedPhone(Eval("hostPhone")) %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:HyperLinkField DataNavigateUrlFields="eventId" 
                DataNavigateUrlFormatString="Roster.aspx?EventId={0}" HeaderText="Roster" 
                Text="View">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
            </asp:HyperLinkField>
        </Columns>
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#E9E7E2" />
        <SortedAscendingHeaderStyle BackColor="#506C8C" />
        <SortedDescendingCellStyle BackColor="#FFFDF8" />
        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SqlServer %>" 
        SelectCommand="SELECT Events.eventId, Events.eventName, Users.fname + ' ' + Users.lname AS creatorName, Events.startTime, Events.hostName, Events.hostEmail, Events.hostPhone FROM Events INNER JOIN Users ON Events.creatorId = Users.uid WHERE (Events.approved = 1) AND (Events.declined = 0) AND (Events.startTime &gt; { fn NOW() }) ORDER BY Events.startTime">
    </asp:SqlDataSource>
</asp:Content>
