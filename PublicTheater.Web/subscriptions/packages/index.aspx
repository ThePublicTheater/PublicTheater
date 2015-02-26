<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPages/Interior.master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="TheaterTemplate.Web.Pages.Subscriptions.Packages.index" %>

<%@ Register TagPrefix="packages" TagName="PackageList" Src="~/Controls/Subscriptions/package/PackageList.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    
<%--    <EPiServer:Property ID="Property1" runat="server" PropertyName="PackageVoucherRedemptionBlock" DisplayMissingMessage="False"> 
    </EPiServer:Property>--%>

    <div id="subscriptionBuilder" class="small-12 medium-12 large-12 columns height-Auto package-list-area">
        <div class="row-fluid">
            <packages:PackageList runat="server" ID="packagesPackageList" />
        </div>
    </div>
</asp:Content>
