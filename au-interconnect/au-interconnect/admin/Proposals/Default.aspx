<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Admin.master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AUInterconnect.admin.Proposals.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadCount" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<h1>Pending Proposals</h1>
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
        AutoGenerateColumns="False" CellPadding="4" DataKeyNames="proposalId" 
        DataSourceID="SqlDataSource1" ForeColor="#333333" GridLines="None" Width="100%">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:HyperLinkField DataNavigateUrlFields="proposalId" 
                DataNavigateUrlFormatString="View.aspx?ProposalId={0}" DataTextField="title" 
                HeaderText="Title">
            <HeaderStyle HorizontalAlign="Left" />
            </asp:HyperLinkField>
            <asp:BoundField DataField="createTime" HeaderText="Proposed" 
                SortExpression="createTime">
            <HeaderStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="ProposedBy" HeaderText="Proposed By" ReadOnly="True" 
                SortExpression="ProposedBy">
            <HeaderStyle HorizontalAlign="Left" />
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
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SqlServer %>" 
        SelectCommand="SELECT Proposals.proposalId, Proposals.createTime, Proposals.title, Users.fname + ' ' + Users.lname AS ProposedBy FROM Proposals INNER JOIN Users ON Proposals.creatorId = Users.uid WHERE (Proposals.approved = 0) AND (Proposals.declined = 0)">
    </asp:SqlDataSource>
</asp:Content>
