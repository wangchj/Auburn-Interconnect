<%@ Page Title="" Language="C#" MasterPageFile="~/AULayout1.master" AutoEventWireup="true" CodeBehind="View.aspx.cs" Inherits="AUInterconnect.Views.Proposal.View" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">View Proposal</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
 <script type="text/javascript" src="../Scripts/tiny_mce/tiny_mce.js"></script>

<script type="text/javascript">
    tinyMCE.init({
        // General options
        readonly: "true",
        mode: "textareas",
        theme: "advanced",
        plugins: "autolink,lists,pagebreak,style,advhr,advlink,iespell,inlinepopups,insertdatetime,preview,searchreplace,contextmenu,paste,noneditable,visualchars,nonbreaking,xhtmlxtras,template,wordcount,advlist,visualblocks",

        // Theme options
        theme_advanced_buttons1: "bold,italic,underline,strikethrough,|,forecolor,backcolor,removeformat,|,justifyleft,justifycenter,justifyright,justifyfull,formatselect,fontselect,fontsizeselect",
        theme_advanced_buttons2: "cut,copy,paste,pastetext,pasteword,|,replace|,undo,redo,|,sub,sup,|,charmap,iespell,|,link,unlink,cleanup,|,insertdate,inserttime,|,search,code,preview",
        theme_advanced_buttons3: "",
        theme_advanced_buttons4: "",
        theme_advanced_toolbar_location: "top",
        theme_advanced_toolbar_align: "left",
        theme_advanced_statusbar_location: "bottom",
        theme_advanced_resizing: true,

        // Example content CSS (should be your site CSS)
        content_css: "../admin/Proposals/content.css,../admin/Proposals/style.css",

        // Drop lists for link/image/media/template dialogs
        template_external_list_url: "lists/template_list.js",
        external_link_list_url: "lists/link_list.js",
        external_image_list_url: "lists/image_list.js",
        media_external_list_url: "lists/media_list.js",

        // Style formats
        style_formats: [
			{ title: 'Bold text', inline: 'b' },
			{ title: 'Red text', inline: 'span', styles: { color: '#ff0000'} },
			{ title: 'Red header', block: 'h1', styles: { color: '#ff0000'} },
			{ title: 'Example 1', inline: 'span', classes: 'example1' },
			{ title: 'Example 2', inline: 'span', classes: 'example2' },
			{ title: 'Table styles' },
			{ title: 'Table row 1', selector: 'tr', classes: 'tablerow1' }
		],

        // Replace values for the template plugin
        template_replace_values: {
            username: "Some User",
            staffid: "991234"
        }
    });
</script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="breadcrumb" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" runat="server">
<asp:Literal ID="ErrorLit" runat="server"></asp:Literal>

<asp:TextBox ID="Content" runat="server" TextMode="MultiLine" 
    Height="600px" Width="100%"></asp:TextBox>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="sidebar" runat="server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="lastUpdated" runat="server">
</asp:Content>
