<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPages/Interior.master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="PublicTheater.Web.gift.index" %>
<%@ Register TagPrefix="gift" TagName="GiftCertificatePurchase" Src="~/Controls/Gift/GiftCertificatePurchaseControl.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <div class="giftCerts">
        <gift:GiftCertificatePurchase runat="server" ID="ctrlGiftPurchase" />
    </div>
</asp:Content>
<%--<asp:Content ID="Content3" ContentPlaceHolderID="Sidebar" runat="server">
</asp:Content>--%>