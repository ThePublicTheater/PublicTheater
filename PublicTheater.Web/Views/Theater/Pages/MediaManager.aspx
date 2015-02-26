<%@ Page Title="" Language="C#" AutoEventWireup="true" MasterPageFile="~/Views/Theater/MasterPages/MasterMediaManager.master"
    Inherits="PublicTheater.Web.Views.Theater.Pages.MediaManager" %>

<%@ Register TagPrefix="uc" TagName="MediaManager" Src="~/Views/Theater/Controls/MediaManager.ascx" %>
<asp:Content runat="server" ID="content" ContentPlaceHolderID="Content">
    <asp:HyperLink runat="server" ID="returnTo" Text=" &lt;&lt; Return" Visible="false" />
    <uc:MediaManager ID="ucMediaManager" runat="server" />
</asp:Content>
