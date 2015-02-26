<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPages/Interior.master" AutoEventWireup="true" CodeBehind="change.aspx.cs" Inherits="TheaterTemplate.Web.Pages.Account.Promo.change" %>

<%@ Register Src="~/controls/account/PromoBox.ascx" TagPrefix="account" TagName="PromoBox" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <account:PromoBox runat="server" ID="PromoBox" />

</asp:Content>
