<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RenewalLanding.ascx.cs" Inherits="TheaterTemplate.Web.Controls.Subscriptions.FullControls.RenewalLanding" %>
<%@ Register Src="~/controls/cart/CartItems.ascx" TagPrefix="adg" TagName="CartItems"  %>
<%@ Register Src="~/controls/cart/CartTotals.ascx" TagPrefix="cart" TagName="CartTotals" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>

<EPiServer:Property runat="server" ID="RenewalNotAvailable" PropertyName="RenewalNotAvailable" Visible="false" />
<EPiServer:Property runat="server" ID="OtherRenewalMessage" PropertyName="OtherRenewalMessage" Visible="false" CssClass="otherRenewal test" />
<asp:Literal runat="server" ID="renewalErrorMessage" />

<asp:Literal runat="server" ID="ltrHeader" />

<div class="cartItems">
    <adg:CartItems runat="server" id="cartItems" />
</div>
<br />
<asp:Panel runat="server" ID="renewalCheckoutOptions" CssClass="renewalOptions">
    <div class="orderSubTotal">
        <cart:CartTotals runat="server" ID="cartTotals" />
    </div>
    <div class="actions">
        <asp:Button runat="server" ID="completeRenewalAsIs" CssClass="btn" Text="Complete Renewal As Is" />
        <br />
        <br />
        <asp:Button runat="server" ID="editRenewal" CssClass="btn" Text="Edit Renewal" />
        <br />
        <br />
        <asp:Button runat="server" ID="removeSubscription" CssClass="btn" Text="Remove Subscription and Add New" />
    </div>
</asp:Panel>

<asp:Panel runat="server" ID="pnlRemoveSubscription" CssClass="selectNewSectionContainer"
    Style="display: none;">
    <asp:ModalPopupExtender ID="mdlChangeAddress" runat="server" TargetControlID="removeSubscription"
        PopupControlID="pnlRemoveSubscription" DropShadow="false" RepositionMode="none" BackgroundCssClass="bgOverlay"
        CancelControlID="btnCancel" />
    <div class="subsModalInner">
        <div class="subsModalContent">
            <EPiServer:Property runat="server" ID="RenewalRemoveMessage" PropertyName="RenewalRemoveMessage" />
        </div>
        <div class="continueBtn updateButton">
            <asp:LinkButton runat="server" ID="btnCancel" Text="Cancel" CssClass="btn" CausesValidation="false" />
            <asp:LinkButton runat="server" ID="btnRemoveSubscriptionConfirmed" Text="Remove Subscription" CssClass="btn btnStandOut" />
        </div>
    </div>
</asp:Panel>