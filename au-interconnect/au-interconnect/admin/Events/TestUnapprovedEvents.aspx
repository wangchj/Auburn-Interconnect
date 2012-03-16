<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Events/AdminEvents.master" AutoEventWireup="true" CodeBehind="TestUnapprovedEvents.aspx.cs" Inherits="AUInterconnect.admin.Events.TestUnapprovedEvents" %>
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
            <asp:BoundField DataField="eventName" HeaderText="eventName" 
                SortExpression="eventName" >
            </asp:BoundField>
            <asp:BoundField DataField="Expr1" HeaderText="Expr1" 
                SortExpression="Expr1" ReadOnly="True">
            </asp:BoundField>
            <asp:BoundField DataField="startTime" HeaderText="startTime" 
                SortExpression="startTime" />
            <asp:BoundField DataField="eventId" HeaderText="eventId" InsertVisible="False" 
                ReadOnly="True" SortExpression="eventId" />
            <asp:CheckBoxField DataField="approved" HeaderText="approved" 
                SortExpression="approved" />
            <asp:CheckBoxField DataField="declined" HeaderText="declined" 
                SortExpression="declined" />
            <asp:CommandField ShowEditButton="True" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:RadioButton ID="RadioButton1" runat="server" AutoPostBack="True" 
                        GroupName='<%# Eval("eventId") %>' 
                        oncheckedchanged="RadioButton1_CheckedChanged" />
                    <asp:RadioButton ID="RadioButton2" runat="server" 
                        GroupName='<%# Eval("eventId") %>' />
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
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SqlServer %>" 
        
        
        
        
        
        
        SelectCommand="SELECT Events.eventName, Users.fname + ' ' + Users.lname AS Expr1, Events.startTime, Events.eventId, Events.approved, Events.declined FROM Events INNER JOIN Users ON Events.creatorId = Users.uid WHERE (Events.approved = 0) AND (Events.startTime &gt; { fn NOW() }) ORDER BY Events.startTime" 
        UpdateCommand="UPDATE Events SET approved = 1 WHERE (eventId = @eventId)">
        <UpdateParameters>
            <asp:Parameter Name="eventId" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    </asp:Content>
