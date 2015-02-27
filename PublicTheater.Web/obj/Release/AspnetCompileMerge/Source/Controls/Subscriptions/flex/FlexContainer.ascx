<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FlexContainer.ascx.cs" Inherits="TheaterTemplate.Web.Controls.Subscriptions.FlexControls.FlexContainer" %>

<%@ Register TagPrefix="subscription" TagName="FlexFilterSelection" Src="~/controls/subscriptions/flex/FlexFilterSelection.ascx" %>
<%@ Register TagPrefix="subscription" TagName="FlexPackageDisplay" Src="~/controls/subscriptions/flex/FlexPackageDisplay.ascx" %>
<%@ Register TagPrefix="subscription" TagName="FlexPerformanceSelection" Src="~/controls/subscriptions/flex/FlexPerformanceSelection.ascx" %>

<div id="packageDisplayContainer" class="row-fluid"> <!-- Header (show packages) -->
    <div class="span12">
        <subscription:FlexPackageDisplay runat="server" ID="packageDisplay" />
    </div>
</div>

<div class="row-fluid">
    <div id="filterContainer" class="span2"> <!-- Left Side (Venue) -->
        <subscription:FlexFilterSelection runat="server" ID="filterSelection" />
    </div>
    <div id="performanceSelectionContainer" class="span10"> <!-- Main Content (select play) -->
        <subscription:FlexPerformanceSelection runat="server" ID="performanceSelection" />
    </div>
</div>


