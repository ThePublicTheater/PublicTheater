<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPages/Interior.master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="TheaterTemplate.Web.Pages.Contribute.index" %>

<%@ Register Src="~/controls/contribute/CartDonationControl.ascx" TagPrefix="contribute" TagName="DonationControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <div id="subscriptionBuilder" class="large-12 medium-12 small-12 column">
        <div id="packageDisplayContainer">
            <contribute:DonationControl runat="server" ID="donationControl" DonationJavaScriptFile="/js/contribute/donation.js" />
        </div>
    </div>

</asp:Content>

