<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PackageDetail.ascx.cs" Inherits="TheaterTemplate.Web.Controls.Subscriptions.PackageControls.PackageDetail" %>
<%@ Register TagPrefix="subscriptions" TagName="PackagePerformanceList" Src="~/Controls/Subscriptions/package/PackagePerformanceList.ascx" %>

<EPiServer:Property runat="server" ID="propHeader" DisplayMissingMessage="false" PropertyName="DetailHeading" />

<div id="listOfPackages" class="packageDetail">
    <div id="leftSide">
        <EPiServer:Property runat="server" ID="propMainBody" DisplayMissingMessage="false" PropertyName="SubscriptionDetail"  />
    </div>
    <div id="subscribeArea">
        <asp:Panel runat="server" ID="pnlPriceRange" CssClass="priceRange" Visible="false">
            Price Range:
            <EPiServer:Property runat="server" ID="PriceRange" PropertyName="PriceRange" CustomTagName="strong" DisplayMissingMessage="false" />
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlButtons" Visible="false">
            <asp:HyperLink ID="lnkSubscribe" runat="server" Text="Subscribe Now" CssClass="btn" />
            <asp:HyperLink ID="lnkRenewalNow" runat="server" Text="Renew Now" CssClass="btn" Visible="false" />
        </asp:Panel>
    </div>
    <div class="playsListing">                
        <subscriptions:PackagePerformanceList runat="server" ID="packagePerformances" />
    </div>
</div>
