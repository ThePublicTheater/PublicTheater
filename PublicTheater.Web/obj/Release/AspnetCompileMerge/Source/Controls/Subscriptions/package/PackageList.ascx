<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PackageList.ascx.cs" Inherits="TheaterTemplate.Web.Controls.Subscriptions.PackageControls.PackageList" %>
<%@ Register TagPrefix="subscriptions" TagName="PackagePerformanceList" Src="~/Controls/Subscriptions/package/PackagePerformanceList.ascx" %>

<EPiServer:Property runat="server" ID="propMainBody" DisplayMissingMessage="false" PropertyName="MainBody" />

<asp:Repeater runat="server" ID="rptPackageAnchors">
    <HeaderTemplate>
        <ul class="packageLinks nav nav-pills">
    </HeaderTemplate>
    <ItemTemplate>
        <li>
            <a href='#PageAnchor_<asp:Literal runat="server" ID="ltrAnchorId" />' class="packageAnchor">
                <asp:Literal runat="server" ID="ltrTitle" />
            </a>
        </li>
    </ItemTemplate>
    <FooterTemplate>
        </ul>
    </FooterTemplate>
</asp:Repeater>

<asp:Repeater runat="server" ID="rptPackages">
    <HeaderTemplate>
        <div id="listOfPackages" class="container">
    </HeaderTemplate>
    <ItemTemplate>
        <a id='PageAnchor_<asp:Literal runat="server" ID="ltrAnchorId" />'></a>
        <div class="packageListItemWrapper row-fluid fwidth">
            <div class="span12">
                <div class="span8">
                    <asp:Literal runat="server" ID="ltrPackageTitle" />
                    <asp:Literal runat="server" ID="ltrSummary" />
                </div>
                <asp:Panel runat="server" ID="pnlSubscribeArea" CssClass="subscribeArea packageSubscribe">
                    <asp:Panel runat="server" ID="pnlPriceRange" class="priceRange" Visible="false">
                        Price Range:
                        <strong>
                            <asp:Literal runat="server" ID="ltrPriceRange" />
                        </strong>
                    </asp:Panel>
                    <div class="buttonWrapper">
                        <asp:HyperLink ID="lnkLearnMore" runat="server" Text="Learn More" CssClass="btn" />
                        <asp:HyperLink ID="lnkSubscribeNow" runat="server" Text="Subscribe Now" CssClass="btn" />
                        <asp:HyperLink ID="lnkRenewalNow" runat="server" Text="Renew Now" CssClass="btn" Visible="false" />
                    </div>
                </asp:Panel>
            </div>
            <div class="playsListing span12">
                <subscriptions:PackagePerformanceList runat="server" ID="packagePerformances" />
            </div>
        </div>
        <hr />
    </ItemTemplate>
    <FooterTemplate>
        </div>
    </FooterTemplate>
</asp:Repeater>