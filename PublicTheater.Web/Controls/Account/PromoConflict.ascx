<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PromoConflict.ascx.cs"
    Inherits="TheaterTemplate.Web.Controls.AccountControls.PromoConflict" %>
<%@ Register Src="~/controls/cart/CartItems.ascx" TagPrefix="cart" TagName="CartItems" %>

<div id="packageDisplay">
    <div class="packageDescription">
        <EPiServer:Property runat="server" ID="header" PropertyName="Header" />
    </div>
</div>

<div id="cartItemDisplay">
    <cart:CartItems ID="cartItems" runat="server" ShowRemoveButton="false" HideAddOns="true" />
</div>

<div class="conflictDescription">
    <EPiServer:Property runat="server" ID="conflictDescription" PropertyName="ConflictDescription" />
    <asp:Label runat="server" id="lblPromoError" Visible="false" CssClass="errorMsg">Promo code failed to apply.</asp:Label>
</div>

<div id="conflictOptions">
    <asp:LinkButton runat="server" ID="lbEmptyCart" CssClass="btn btnStandOut" CausesValidation="False" />
    <asp:LinkButton runat="server" ID="lbGoBack" CssClass="btn btnStandOut" Style="float:right" CausesValidation="False" />
</div>