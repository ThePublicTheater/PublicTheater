<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPages/Interior.master" AutoEventWireup="true" CodeBehind="mosconflict.aspx.cs" Inherits="TheaterTemplate.Web.Pages.Cart.MosConflict" %>

<%@ Register Src="~/controls/cart/MosConflict.ascx" TagPrefix="cart" TagName="MosConflict" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <div id="subscriptionBuilder">
        <cart:MosConflict runat="server" ID="cartMosConflict" />
    </div>        

</asp:Content>
