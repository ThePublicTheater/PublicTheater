<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPages/Interior.master" AutoEventWireup="true" CodeBehind="selectsubscription.aspx.cs" 
    Inherits="TheaterTemplate.Web.Pages.Subscriptions.Full.SelectSubscription" %>

<%@ Register Src="~/controls/subscriptions/full/FullContainer.ascx" TagPrefix="subscriptions" TagName="FullContainer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">
<div id="subscriptionBuilder">
    <subscriptions:FullContainer runat="server" ID="subsFullContainer" FullJavaScriptFile="/js/full/select.js" />
</div>
</asp:Content>
