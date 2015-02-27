<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MosConflict.ascx.cs"
    Inherits="TheaterTemplate.Web.Controls.CartControls.MosConflict" %>
<%@ Register Src="~/controls/cart/CartItems.ascx" TagPrefix="cart" TagName="CartItems" %>

<div id="packageDisplay">
    <div class="packageDescription">
        <EPiServer:Property runat="server" ID="header" PropertyName="Header" />
    </div>
</div>

<div id="cartItemDisplay">
    <cart:CartItems ID="cartItems" runat="server" ShowRemoveButton="false" />
</div>

<div class="conflictDescription">
    <EPiServer:Property runat="server" ID="conflictDescription" PropertyName="ConflictDescription" />        
</div>

<div id="conflictOptions">
    <asp:LinkButton runat="server" ID="lbEmptyCart" CssClass="btn btnStandOut" />
    <asp:LinkButton runat="server" ID="lbContinueToCart" CssClass="btn btnStandOut" Style="float:right" />
</div>