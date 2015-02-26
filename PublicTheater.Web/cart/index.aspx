<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPages/Interior.master"
    AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="TheaterTemplate.Web.Pages.Cart.index" %>

<%@ Register Src="~/controls/cart/CartControl.ascx" TagPrefix="cart" TagName="CartControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageNameContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PrimaryContent" runat="server">
    <div id="subscriptionBuilder" class="large-12 medium-12 small-12 column">
        <cart:CartControl runat="server" ID="cartControl" />
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PrimaryContentBottomSection" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="BeforeCloseBodyContent" runat="server"></asp:Content>
