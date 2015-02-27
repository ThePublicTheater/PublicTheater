<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FullContainer.ascx.cs" Inherits="TheaterTemplate.Web.Controls.Subscriptions.FullControls.FullContainer" %>

<%@ Register Src="~/controls/subscriptions/full/FullSelectSubscription.ascx" TagPrefix="subscriptions" TagName="FullSelectSubscription" %>
<%@ Register Src="~/controls/subscriptions/full/FullFilterSelection.ascx" TagPrefix="subscriptions" TagName="FullFilterSelection" %>
<%@ Register Src="~/controls/subscriptions/full/FullPackageDisplay.ascx" TagPrefix="subscriptions" TagName="FullPackageDisplay" %>


<div id="packageDisplayContainer" class="row-fluid">
    <div class="span12">
        <subscriptions:FullPackageDisplay runat="server" ID="packageDisplay" />
    </div>
</div>

<div class="row-fluid">
    <div id="filterContainer" class="span2">
        <subscriptions:FullFilterSelection runat="server" ID="subsFilterSelection" />        
    </div>

    <div id="selectSubscriptionContainer" class="span10">
        <subscriptions:FullSelectSubscription runat="server" ID="subsSelectSubscrptions" />    
    </div>
</div>