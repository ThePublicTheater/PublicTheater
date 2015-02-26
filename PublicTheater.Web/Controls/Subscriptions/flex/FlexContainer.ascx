<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FlexContainer.ascx.cs" Inherits="PublicTheater.Web.Controls.Subscriptions.flex.FlexContainer" %>

<%@ Register TagPrefix="subscription" TagName="FlexFilterSelection" Src="~/controls/subscriptions/flex/FlexFilterSelection.ascx" %>
<%@ Register TagPrefix="subscription" TagName="FlexPackageDisplay" Src="~/controls/subscriptions/flex/FlexPackageDisplay.ascx" %>
<%@ Register TagPrefix="subscription" TagName="FlexPerformanceSelection" Src="~/controls/subscriptions/flex/FlexPerformanceSelection.ascx" %>

<div id="packageDisplayContainer"> <!-- Header (show packages) -->
        <subscription:FlexPackageDisplay runat="server" ID="packageDisplay" />
</div>
    <asp:Panel id="performanceSelectionContainer" runat="server" CssClass="span10" ClientIDMode="Static"> <!-- Main Content (select play) -->
<div class="row-fluid package-select-area">
    <div id="filterContainer" class="span2"> <!-- Left Side (Venue) -->
        <%--<subscription:FlexFilterSelection runat="server" ID="filterSelection" />--%>
    </div>
    
        <subscription:FlexPerformanceSelection runat="server" ID="performanceSelection" />
</div>
    </asp:Panel>
    <asp:Label ID="notLoggedInLbl" runat="server" Text="Label" Visible="False"></asp:Label>
    <asp:Label ID="notInvitedLbl" runat="server" Text="Label" Visible="False"></asp:Label>
    <asp:Label ID="alreadyPurchasedLbl" runat="server" Text="Label" Visible="False"></asp:Label>


<div class="loadingContainer bgOverlay">
    <img id="loadingGif" class="loadingSpinner" src="/images/ajax-loader.gif">
</div>