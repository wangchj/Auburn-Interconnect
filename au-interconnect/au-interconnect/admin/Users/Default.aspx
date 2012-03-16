<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Admin.master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AUInterconnect.admin.Users.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadCount" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<h1>Users</h1>
    <asp:Literal ID="ErrorMessage" runat="server"></asp:Literal>
    <table width="100%">
    <tr>
            <td bgcolor="#5D7B9D" style="color: #FFFFFF; font-weight: bold" width="30%">First Name</td>
            <td bgcolor="#5D7B9D" style="color: #FFFFFF; font-weight: bold" width="30%">Last Name</td>
            <td bgcolor="#5D7B9D" style="color: #FFFFFF; font-weight: bold" width="30%">Email</td>
            <td align="center" bgcolor="#5D7B9D" style="color: #FFFFFF; font-weight: bold" 
                width="10%">Admin</td>
        </tr>
        <tr>
            <td bgcolor="#5D7B9D">
                <asp:TextBox ID="FirstNameTextbox" runat="server" AutoPostBack="True"></asp:TextBox></td>
           <td bgcolor="#5D7B9D">
                <asp:TextBox ID="LastNameTextbox" runat="server" AutoPostBack="True"></asp:TextBox></td>
            <td bgcolor="#5D7B9D">
                <asp:TextBox ID="EmailTextbox" runat="server" AutoPostBack="True"></asp:TextBox></td>
            <td bgcolor="#5D7B9D">
                &nbsp;</td>
       </tr>
    </table>
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
        AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" 
        DataSourceID="SqlDataSource1" ForeColor="#333333" 
        ShowHeader="False" Width="100%" BorderStyle="Solid" BorderWidth="1px">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:BoundField DataField="fname" HeaderText="fname" SortExpression="fname" >
            <ItemStyle Width="30%" />
            </asp:BoundField>
            <asp:BoundField DataField="lname" HeaderText="lname" SortExpression="lname" >
            <ItemStyle Width="30%" />
            </asp:BoundField>
            <asp:HyperLinkField DataNavigateUrlFields="uid" 
                DataNavigateUrlFormatString="UserDetails.aspx?uid={0}" DataTextField="email" 
                HeaderText="email">
            <ItemStyle Width="30%" />
            </asp:HyperLinkField>
            <asp:TemplateField HeaderText="admin" SortExpression="admin">
                <EditItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("admin") %>' />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("admin") %>' 
                        ToolTip='<%# Eval("uid") %>' AutoPostBack="True" 
                        oncheckedchanged="CheckBox1_CheckedChanged" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="10%" />
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
        
        SelectCommand="SELECT [uid], [fname], [lname], [email], [admin] FROM [Users] WHERE (([fname] LIKE '%' + @fname + '%') AND ([lname] LIKE '%' + @lname + '%') AND ([email] LIKE '%' + @email + '%'))">
        <SelectParameters>
            <asp:ControlParameter ControlID="FirstNameTextbox" DefaultValue="%" 
                Name="fname" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="LastNameTextbox" DefaultValue="%" Name="lname" 
                PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="EmailTextbox" DefaultValue="%" Name="email" 
                PropertyName="Text" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>