<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Events/AdminEvents.master" AutoEventWireup="true" CodeBehind="Roster.aspx.cs" Inherits="AUInterconnect.Views.admin.Events.Roster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadCount" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<h1>
    <asp:Label ID="EventNameLabel" runat="server" Text=""></asp:Label></h1>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        CellPadding="4" DataSourceID="SqlDataSource1" ForeColor="#333333" 
        GridLines="None" ShowFooter="True" Width="100%"> 
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:BoundField DataField="name" HeaderText="Name" ReadOnly="True" 
                SortExpression="name" >
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="headCount" HeaderText="Group Size" 
                SortExpression="headCount" >
            <FooterStyle HorizontalAlign="Center" />
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="vehicleCap" HeaderText="Vehicle Capapacity" 
                SortExpression="vehicleCap" >
            <FooterStyle HorizontalAlign="Center" />
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
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
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SqlServer %>" 
        SelectCommand="SELECT [eventName] FROM [Events] WHERE ([eventId] = @eventId)">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="2" Name="eventId" 
                QueryStringField="EventId" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SqlServer %>" 
        SelectCommand="SELECT Users.fname + ' ' + Users.lname AS name, EventRegs.headCount, EventRegs.vehicleCap FROM Users INNER JOIN EventRegs ON Users.uid = EventRegs.userId WHERE (EventRegs.eventId = @eventId)">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="2" Name="eventId" 
                QueryStringField="EventId" />
        </SelectParameters>
    </asp:SqlDataSource>
    <br />
    <br />
    <h1>Waiting List</h1>
    <p>
        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
            CellPadding="4" DataSourceID="SqlDataSource3" ForeColor="#333333" 
            GridLines="None" Width="100%">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:BoundField DataField="name" HeaderText="Name" ReadOnly="True" 
                    SortExpression="name">
                <HeaderStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="email" HeaderText="Email" SortExpression="email">
                <HeaderStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="phone" HeaderText="Phone" SortExpression="phone">
                <HeaderStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="signupTime" HeaderText="Signup Time" SortExpression="signupTime">
                <HeaderStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:Button ID="Button1" runat="server" Text="Attend" onclick="Button1_Click" 
                            ToolTip='<%# Eval("eventId") + "-" + Eval("userId") %>' UseSubmitBehavior="False" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="RemoveBtn" runat="server" onclick="RemoveBtn_Click" 
                            Text="Remove" ToolTip='<%# Eval("eventId") + "-" + Eval("userId") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
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
        <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
            ConnectionString="<%$ ConnectionStrings:SqlServer %>" 
            SelectCommand="SELECT WaitingList.eventId, WaitingList.userId, Users.fname + ' ' + Users.lname AS name, Users.email, Users.phone, WaitingList.signupTime FROM Users INNER JOIN WaitingList ON Users.uid = WaitingList.userId WHERE (WaitingList.eventId = @eventId) ORDER BY signupTime DESC">
            <SelectParameters>
                <asp:QueryStringParameter DefaultValue="1" Name="eventId" 
                    QueryStringField="EventId" />
            </SelectParameters>
        </asp:SqlDataSource>
    </p>
</asp:Content>
