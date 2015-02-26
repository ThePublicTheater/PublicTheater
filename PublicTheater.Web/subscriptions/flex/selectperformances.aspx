<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPages/Interior.master" AutoEventWireup="true" CodeBehind="selectperformances.aspx.cs" 
    Inherits="TheaterTemplate.Web.Pages.Subscriptions.Flex.SelectPerformances" %>
<%@ Register TagPrefix="subscription" TagName="FlexContainer" Src="~/controls/subscriptions/flex/FlexContainer.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<%--<script type="text/javascript" src="../../js/lib/bxslider.js"></script>--%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
<div id="subscriptionBuilder" class="large-12 medium-12 small-12 column">
    <subscription:FlexContainer runat="server" ID="subsFlexContainer" FlexJavaScriptFile="/js/flex/select.js?bust=20141105"  />
</div>


</asp:Content>

