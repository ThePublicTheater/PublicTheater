<%@ Page Language="C#" MasterPageFile="~/Views/MasterPages/Interior.Master" AutoEventWireup="true" CodeBehind="FindSubscription.aspx.cs" Inherits="TheaterTemplate.Web.Views.Pages.FindSubscription" %>

<%@ Register TagPrefix="uc" TagName="MainBody" Src="~/Views/Controls/MainBody.ascx" %>
<%@ Register TagPrefix="MAP" TagName="SubscriptionFinder" Src="~/controls/subscriptions/finder/SubscriptionFinder.ascx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    

    <MAP:SubscriptionFinder runat="server" ID="subFinder" />

    <uc:MainBody runat="server" ID="MainBody" /> 
</asp:Content>
