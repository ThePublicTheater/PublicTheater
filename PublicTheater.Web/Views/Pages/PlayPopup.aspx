<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PlayPopup.aspx.cs" Inherits="TheaterTemplate.Web.Views.Pages.PlayPopup" %>

<%@ Register TagPrefix="uc" TagName="MainBody" Src="~/Views/Controls/MainBody.ascx" %>
<%@ Register TagPrefix="uc" TagName="PageList" Src="~/Views/Controls/PageList.ascx" %>

<head id="Head1" runat="server" />

<body>
    <form runat="server">
        <asp:LinkButton runat="server" ID="lbClose" Text="X" />
        <asp:Image runat="server" ID="imgImage" />
        <EPiServer:Property ID="Property1" runat="server" PropertyName="PageName" CustomTagName="h1" />
        <uc:MainBody runat="server" ID="MainBody" />
        <uc:PageList runat="server" ID="MainListing" PageLinkProperty="MainListRoot" MaxCountProperty="MainListCount" />
    </form>
</body>


