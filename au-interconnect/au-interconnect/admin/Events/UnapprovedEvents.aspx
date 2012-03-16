<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Events/AdminEvents.master" AutoEventWireup="true" CodeBehind="UnapprovedEvents.aspx.cs" Inherits="AUInterconnect.admin.Events.UnapprovedEvents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadCount" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<h1>Unapproved Events</h1>
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
        AutoGenerateColumns="False" DataSourceID="SqlDataSource1" CellPadding="5" 
        ForeColor="#333333" GridLines="None" Width="100%" DataKeyNames="eventId">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:TemplateField HeaderText="Event Name" SortExpression="eventName">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("eventName") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLink6" runat="server" 
                        NavigateUrl='<%# "EventDetails.aspx?EventId="+Eval("eventId") %>' 
                        Text='<%# Eval("eventName") %>'></asp:HyperLink>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:BoundField DataField="Expr1" HeaderText="Proposed By" 
                SortExpression="Expr1" ReadOnly="True" >
            <HeaderStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="startTime" HeaderText="Start Time" 
                SortExpression="startTime">
            <HeaderStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="eventId" HeaderText="eventId" InsertVisible="False" 
                ReadOnly="True" SortExpression="eventId" Visible="False" />
            <asp:TemplateField HeaderText="Approve">
                <ItemTemplate>
                    <asp:RadioButton ID="ApproveRadioButton" runat="server" AutoPostBack="True" 
                        GroupName='<%# Eval("eventId") %>' 
                        oncheckedchanged="ApproveRadioButton_CheckedChanged" />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Decline">
                <ItemTemplate>
                    <asp:RadioButton ID="DeclineRadioButton" runat="server" AutoPostBack="True" 
                        GroupName='<%# Eval("eventId") %>' 
                        oncheckedchanged="DeclineRadioButton_CheckedChanged" />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
        <EditRowStyle BackColor="#999999" />
        <EmptyDataTemplate>
            Currently there is no unapproved event.
        </EmptyDataTemplate>
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
        
        
        
        
        SelectCommand="SELECT Events.eventName, Users.fname + ' ' + Users.lname AS Expr1, Events.startTime, Events.eventId FROM Events INNER JOIN Users ON Events.creatorId = Users.uid WHERE (Events.approved = 0) AND (Events.declined = 0) AND (Events.startTime &gt; { fn NOW() }) ORDER BY Events.startTime">
    </asp:SqlDataSource>
    </asp:Content>
