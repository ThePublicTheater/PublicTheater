<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPages/Interior.master" AutoEventWireup="true" CodeBehind="payment.aspx.cs" Inherits="TheaterTemplate.Web.Pages.Checkout.Payment" %>
<%@ Register Src="~/controls/checkout/PaymentControl.ascx" TagPrefix="checkout" TagName="Payment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <div id="subscriptionBuilder">
        <div class="row-fluid">
            <checkout:Payment runat="server" ID="checkoutPayment" />
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="PrimaryContentBottomSection" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="BeforeCloseBodyContent" runat="server">
</asp:Content>