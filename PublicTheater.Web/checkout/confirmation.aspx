<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPages/Interior.master" AutoEventWireup="true" CodeBehind="confirmation.aspx.cs" Inherits="TheaterTemplate.Web.Pages.Checkout.Confirmation" %>

<%@ Register Src="~/controls/checkout/ConfirmationControl.ascx" TagPrefix="checkout" TagName="Confirmation" %>
<%@ Register Src="~/Controls/Checkout/ConversionTagControl.ascx" TagPrefix="checkout" TagName="ConversionTagControl" %>



<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
<div id="subscriptionBuilder">
    <div class="row-fluid">
        <checkout:Confirmation runat="server" ID="checkoutConfirmation" />
    </div>
</div>
    <checkout:ConversionTagControl runat="server" ID="ConversionTagControl" />
</asp:Content>
