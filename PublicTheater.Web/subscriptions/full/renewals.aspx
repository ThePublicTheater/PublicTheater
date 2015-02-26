<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPages/Interior.master" AutoEventWireup="true" CodeBehind="renewals.aspx.cs" Inherits="TheaterTemplate.Web.Pages.Subscriptions.Full.Renewals" %>
<%@ Register Src="~/controls/subscriptions/full/RenewalLanding.ascx" TagPrefix="subscriptions" TagName="RenewalLanding" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <div id="subscriptionBuilder">
        <subscriptions:RenewalLanding runat="server" ID="subsRenewalLanding" />
    </div>
</asp:Content>
