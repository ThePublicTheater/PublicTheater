<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PaymentControl.ascx.cs"
    Inherits="PublicTheater.Web.Controls.Checkout.PaymentControl" %>
<%@ Register Src="~/controls/cart/CartItems.ascx" TagPrefix="cart" TagName="CartItems" %>
<%@ Register Src="~/controls/cart/CartTotals.ascx" TagPrefix="cart" TagName="CartTotals" %>
<%@ Register Src="~/Controls/Contribute/PaymentDonationAsk.ascx" TagPrefix="checkout"
    TagName="PaymentDonationAsk" %>
<%@ Register Src="~/controls/checkout/PaymentOptions.ascx" TagPrefix="checkout" TagName="PaymentOptions" %>
<%@ Import Namespace="PublicTheater.Custom.Episerver.Utility" %>


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
            <%--<asp:Panel ID="digitalDelivery" runat="server" ClientIDMode="Static">
                <asp:CheckBox ID="cbDigi" TextAlign="Right" ClientIDMode="Static" runat="server" Text="Also send me a digital version of my tickets to be scanned from my smartphone." />
            </asp:Panel>--%>
        </div>
        <div id="cartActions">
            <div class="large-6 medium-12 small-12">
                <div class="donationDescription">
                    &nbsp;<asp:literal runat="server" id="litDonationDescription" />
                </div>
                <div runat="server" ID="SourceSelectionDiv" Visible="False">
                    <style type="text/css">
                        .errorMsg {
                            display: block;
                            color: red;
                        }
                    </style>
                    <asp:RequiredFieldValidator EnableClientScript="false" Display="Dynamic" Enabled="False" ID="ReqSource" Text="Please select an option below. (required)" CssClass="errorMsg" runat="server" ControlToValidate="SourceDD"
                        InitialValue="-1">
                    </asp:RequiredFieldValidator>
                    <h3 style="font-size: 18px; margin-bottom: 0;">What prompted your purchase/support today?</h3>
                    <asp:DropDownList ID="SourceDD" runat="server" Visible="False">
                        <asp:ListItem Enabled="True" Text="Please Select..." Value="-1" />
                    </asp:DropDownList>

                    
                </div>
             
                <div class="magazineOffer">
                    <asp:Literal runat="server" ID="litEmailSignUpDescription">
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
                            <asp:checkbox runat="server" id="cbMail2Utr" text="Under The Radar" /></li>
                        <li>
                            <asp:checkbox runat="server" id="cbMail2Forum" text="Public Forum" /></li>
                        <li>
                            <asp:checkbox runat="server" id="cbMail2Weekly" text="This Week at The Public" /></li>
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
        <EPiServer:Property runat="server" ID="PropertyNote" PropertyName="final_note" CssClass="floatRight" DisplayMissingMessage="False"></EPiServer:Property>
        <EPiServer:Property runat="server" ID="JPNote" PropertyName="jp_note" CssClass="floatLeft" DisplayMissingMessage="False"></EPiServer:Property>
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
    <asp:HiddenField ID="hfDigitalDelivery" runat="server" ClientIDMode="Static" />
</div>
<%--<script type="text/javascript">

    $('input:radio').change(function () {
        if ($(this).val() == '1') {
            $('#digitalDelivery').css('float', 'right');
        } else {
            $('#digitalDelivery').css('float', 'none');
        }
    });
</script>--%>