<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPages/Interior.master" AutoEventWireup="true" CodeBehind="selectseating.aspx.cs" 
    Inherits="TheaterTemplate.Web.Pages.Subscriptions.Full.SelectSeating" %>
<%@ Register Src="~/controls/subscriptions/full/FullSelectSeating.ascx" TagPrefix="subscription" TagName="FullSelectSeating" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
<div id="subscriptionBuilder">
    <div class="row-fluid">
        <subscription:FullSelectSeating ID="subsSelectSeating" runat="server" SeatingJavaScriptFile="/js/full/seating.js" />
    </div>
</div>
</asp:Content>
