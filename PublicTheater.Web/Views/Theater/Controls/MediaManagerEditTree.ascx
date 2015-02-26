<%@ Control Language="C#" AutoEventWireup="true" Inherits="PublicTheater.Web.Views.Theater.Controls.MediaManagerEditTree" %>
<style type="text/css">
    .treeItem {padding:10px;}
</style>

<asp:HyperLink ID="lnkMediaManager" runat="server" NavigateUrl="~/Views/Theater/Pages/MediaManager.aspx" target="_blank">Media Manager</asp:HyperLink>
<script type="text/javascript">
    $("#<%=lnkMediaManager.ClientID %>").parent().addClass("treeItem");
</script>