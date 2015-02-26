<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPages/Interior.master" AutoEventWireup="true" CodeBehind="PackageDetail.aspx.cs" Inherits="TheaterTemplate.Web.Pages.Subscriptions.Packages.PackageDetail" %>
<%@ Register TagPrefix="packages" TagName="PackageDetail" Src="~/Controls/Subscriptions/package/PackageDetail.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <div id="subscriptionBuilder" class="small-12 medium-12 large-12 columns height-Auto">
        <packages:PackageDetail runat="server" ID="packagesPackageDetail" />
    </div>
</asp:Content>
