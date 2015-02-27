<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PaymentControl.ascx.cs"
    Inherits="PublicTheater.Web.Controls.Checkout.PaymentControl" %>
<%@ Register Src="~/controls/cart/CartItems.ascx" TagPrefix="cart" TagName="CartItems" %>
<%@ Register Src="~/controls/cart/CartTotals.ascx" TagPrefix="cart" TagName="CartTotals" %>
<%@ Register Src="~/Controls/Contribute/PaymentDonationAsk.ascx" TagPrefix="checkout"
    TagName="PaymentDonationAsk" %>
<%@ Register Src="~/controls/checkout/PaymentOptions.ascx" TagPrefix="checkout" TagName="PaymentOptions" %>
<div id="paymentContainer">
    <div id="paymentMainDisplay">
        <div id="packageDisplay">
            <div class="packageDescription">
                <asp:literal runat="server" id="tessituraHeader" />
            </div>
        </div>
        <asp:panel runat="server" id="pnlCheckoutError" visible="false" cssclass="checkoutError errorBox">
                <asp:literal runat="server" id="ltrError" />
            </asp:panel>
        <cart:CartItems ID="cartItems" runat="server" />
        <div id="checkoutOptions">
            <checkout:PaymentOptions runat="server" ID="checkoutPaymentOptions" JavaScriptFile="/js/checkout/paymentOptions.js" />
        </div>
        <div id="cartActions">
            <div class="large-6 medium-12 small-12">
                <div class="donationDescription">
                    &nbsp;<asp:literal runat="server" id="litDonationDescription" />
                </div>
                <div class="magazineOffer">
                    <asp:literal runat="server" id="litEmailSignUpDescription">
                        <h3>
                            Update your email subscriptions to receive programming updates and breaking news:</h3>
                    </asp:literal>
                    <ul>
                        <li>
                            <asp:checkbox runat="server" id="cbMail2PublicTheater" text="The Public Theater" /></li>
                        <li>
                            <asp:checkbox runat="server" id="cbMail2JoesPub" text="Joe’s Pub" /></li>
                        <li>
                            <asp:checkbox runat="server" id="cbMail2Shakespeare" text="Shakespeare in the Park" /></li>
                        <li>
                            <asp:checkbox runat="server" id="cbNYMagazine" text="I would like to receive a free subscription to New York Magazine with my order, courtesy of our season sponsor New York Media."
                                visible="False" /></li>
                    </ul>
                </div>
            </div>
            <div class="orderSubTotal large-6 medium-12 small-12">
                <cart:CartTotals runat="server" ID="cartTotals" />
                <checkout:PaymentDonationAsk runat="server" ID="paymentDonationAskDonationAsk" DonationJavaScriptFile="/js/contribute/donation.js"
                    Visible="false" />
            </div>
        </div>
        <div id="checkoutBtn" class="continueBtn checkoutBtns">
            <asp:linkbutton runat="server" id="lbCheckout" cssclass="btn solid btnStandOut checkout"
                text="Confirm Transaction" />
            <a class="btn solid btnStandOut" href="/Cart/">Return to Cart</a>
        </div>
    </div>
</div>
<div class="loadingContainer bgOverlay">
    <div class="loadingMessage">
        One Moment While We Process Your Order</div>
    <asp:image runat="server" id="loadingGif" imageurl="~/images/ajax-loader.gif" cssclass="loadingSpinner" />
</div>
