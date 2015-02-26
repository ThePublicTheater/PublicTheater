<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPages/Interior.Master"
    AutoEventWireup="true" CodeBehind="Page.aspx.cs" Inherits="TheaterTemplate.Web.Views.Pages.Page" %>

<%@ Register TagPrefix="uc" TagName="MainBody" Src="~/Views/Controls/MainBody.ascx" %>
<%@ Register TagPrefix="uc" TagName="PageList" Src="~/Views/Controls/PageList.ascx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

<div id="subscriptionBuilder">
    <uc:MainBody runat="server" ID="MainBody" />
    <uc:PageList runat="server" ID="MainListing" PageLinkProperty="MainListRoot" MaxCountProperty="MainListCount" />
</div>
</asp:Content>
