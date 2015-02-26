<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPages/Interior.master" AutoEventWireup="true" CodeBehind="selectseating.aspx.cs" 
    Inherits="TheaterTemplate.Web.Pages.Subscriptions.Flex.SelectSeating" %>

<%@ Register Src="~/controls/subscriptions/flex/FlexSelectSeating.ascx" TagPrefix="subscription" TagName="FlexSelectSeating" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
<div id="subscriptionBuilder">
    <div class="row-fluid">
        <subscription:FlexSelectSeating runat="server" ID="subsSelectSeating" SeatingJavaScriptFile="/js/flex/seating.js" />
    </div>
</div>
</asp:Content>
