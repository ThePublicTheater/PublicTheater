<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PaymentOptions.ascx.cs"
    Inherits="PublicTheater.Web.Controls.Checkout.PaymentOptions" %>
<%@ Register Src="~/controls/checkout/BillingAddressControl.ascx" TagPrefix="uc"
    TagName="ChangeBillingAddress" %>
<%@ Register Src="~/controls/checkout/ShippingAddressControl.ascx" TagPrefix="uc"
    TagName="ChangeShippingAddress" %>
<%@ Register Src="~/controls/checkout/AddressDisplayControl.ascx" TagPrefix="uc"
    TagName="AddressDisplay" %>
<%@ Register Src="~/controls/checkout/TemporaryAccountPrompt.ascx" TagPrefix="checkout"
    TagName="TempAccountPrompt" %>

<script type="text/javascript">
    $(document).ready(function () {
        
        $("#ddlExpirationMonth").change(function () { $("#ddlExpirationMonthHF").val($("#ddlExpirationMonth").val()); });
        $("#ddlExpirationYear").change(function () { $("#ddlExpirationYearHF").val($("#ddlExpirationYear").val()); });
        $("#ddlCreditCardType").change(function () { $("#ddlCreditCardTypeHF").val($("#ddlCreditCardType").val()); });
        if ($("#ddlCreditCardTypeHF").val().length > 0) {
            $("#ddlCreditCardType").val($("#ddlCreditCardTypeHF").val());
        }
        if ($("#ddlExpirationMonthHF").val().length > 0) {
            $("#ddlExpirationMonth").val($("#ddlExpirationMonthHF").val());
        }
        if ($("#ddlExpirationYearHF").val().length > 0) {
            $("#ddlExpirationYear").val($("#ddlExpirationYearHF").val());
        }
    });
    
</script>
<asp:HiddenField ID="ddlExpirationMonthHF" runat="server" ClientIDMode="Static" />
<asp:HiddenField ID="ddlExpirationYearHF" runat="server" ClientIDMode="Static" />
<asp:HiddenField ID="ddlCreditCardTypeHF" runat="server" ClientIDMode="Static" />


<div class="step paymentInformation large-6 medium-12 small-12">
    <asp:literal runat="server" id="paymentInfoHeader" />
    
    <asp:panel runat="server" id="pnlCreditCard" cssclass="creditCard">
        <ul class="unstyled">
            <li>
                <asp:label runat="server" id="lblCreditCardType" associatedcontrolid="ddlCreditCardType"
                    text="Credit Card Type" />
                <asp:dropdownlist runat="server" id="ddlCreditCardType" datatextfield="Description" EnableViewState="true" ClientIDMode="Static"
                    datavaluefield="ID" />
                <asp:requiredfieldvalidator runat="server" id="reqCreditCardType" controltovalidate="ddlCreditCardType"
                    text="Credit card type required" display="Dynamic" cssclass="errorMsg" enableclientscript="false" />
            </li>
            <li>
                <asp:label runat="server" id="lblCardNumber" associatedcontrolid="txtCreditCardNumber"
                    text="Card Number" />
                <asp:textbox runat="server" id="txtCreditCardNumber" maxlength="16" />
                <asp:requiredfieldvalidator runat="server" id="reqCreditCardNumber" controltovalidate="txtCreditCardNumber"
                    text="Credit card number required" display="Dynamic" cssclass="errorMsg" enableclientscript="false" />
            </li>
            <li class="expr">
                <asp:label runat="server" id="lblExpirationDate" associatedcontrolid="ddlExpirationMonth"
                    text="Exp. Dater" />
                <asp:dropdownlist runat="server" id="ddlExpirationMonth" EnableViewState="true" ClientIDMode="Static">
                    <asp:listitem value="1" text="Jan" />
                    <asp:listitem value="2" text="Feb" />
                    <asp:listitem value="3" text="Mar" />
                    <asp:listitem value="4" text="Apr" />
                    <asp:listitem value="5" text="May" />
                    <asp:listitem value="6" text="Jun" />
                    <asp:listitem value="7" text="Jul" />
                    <asp:listitem value="8" text="Aug" />
                    <asp:listitem value="9" text="Sept" />
                    <asp:listitem value="10" text="Oct" />
                    <asp:listitem value="11" text="Nov" />
                    <asp:listitem value="12" text="Dec" />
                </asp:dropdownlist>
                <asp:dropdownlist runat="server" id="ddlExpirationYear" cssclass="exprYear" EnableViewState="true" ClientIDMode="Static" >
                    <asp:listitem value="2014" text="2014" />
                    <asp:listitem value="2015" text="2015" />
                    <asp:listitem value="2016" text="2016" />
                    <asp:listitem value="2017" text="2017" />
                    <asp:listitem value="2018" text="2018" />
                    <asp:listitem value="2019" text="2019" />
                    <asp:listitem value="2020" text="2020" />
                </asp:dropdownlist>
                <asp:customvalidator runat="server" id="reqDate" controltovalidate="ddlExpirationYear"
                    enableclientscript="false" text="Expiration date must be after today's date"
                    display="Dynamic" cssclass="errorMsg" />
                <asp:label runat="server" id="lblCVV" text="CVV" associatedcontrolid="txtCVV" cssclass="cvv" />
                <asp:textbox runat="server" id="txtCVV" />
                <asp:requiredfieldvalidator runat="server" id="reqCVV" controltovalidate="txtCVV"
                    enableclientscript="false" text="CVV number is required" display="Dynamic" cssclass="errorMsg" />
            </li>
            <li>
                <asp:label runat="server" id="lblCreditCardName" associatedcontrolid="txtCreditCardName"
                    text="Name on Card" />
                <asp:textbox runat="server" id="txtCreditCardName" />
                <asp:requiredfieldvalidator runat="server" id="reqCreditCardName" controltovalidate="txtCreditCardName"
                    text="Name on card required" display="Dynamic" cssclass="errorMsg" enableclientscript="false" />
            </li>
        </ul>
    </asp:panel>
    <asp:panel runat="server" id="pnlGiftCard" cssclass="giftCard">
        <asp:label runat="server" id="lblGiftCard" text="Apply Gift Card" associatedcontrolid="txtGiftCard" />
        <div class="input-append">
            <asp:textbox runat="server" id="txtGiftCard" />
            <asp:linkbutton runat="server" id="lbApplyGiftCard" text="Apply" cssclass="btn solid btnStandOut"
                validationgroup="GiftCardValidation" />
        </div>
        <asp:label runat="server" id="lblGiftCardApplied" text="Gift card applied" cssclass="applied"
            visible="false" />
        <asp:label runat="server" id="lblGiftCardDescription" cssclass="giftDesc" />
    </asp:panel>
</div>
<div class="step shippingInfo">
    <div class="addressBilling large-3 medium-6 small-12">
        <asp:hiddenfield runat="server" id="hfShippingAddressTypeID" clientidmode="Static" />
        <asp:hiddenfield runat="server" id="hfSingleShippingMethod" clientidmode="Static" />
        <asp:literal runat="server" id="billingAddressHeader" />

        <uc:AddressDisplay runat="server" ID="BillingAddressDisplay" />

        <asp:linkbutton runat="server" id="lbEditAddress" text="Edit" causesvalidation="false"
            cssclass="editLnk" />
    </div>
    <asp:placeholder runat="server" id="phDeliveryMethod">
        <div class="addressShipping large-3 medium-6 small-12">
            <asp:literal runat="server" id="deliveryMethodHeader" />
            <asp:radiobuttonlist runat="server" id="rblShippingMethod" datavaluefield="ID" datatextfield="Description"
                repeatlayout="UnorderedList" />
            <asp:panel runat="server" id="pnlSingleDeliveryMessage" visible="false">
                <em>
                    <asp:literal runat="server" id="ltrSingleDeliveryMessage" /></em>
            </asp:panel>
            <asp:requiredfieldvalidator runat="server" id="reqShippingMethod" controltovalidate="rblShippingMethod"
                text="Shipping method required" display="Dynamic" cssclass="errorMsg" />
            <asp:panel runat="server" id="pnlShippingAddress" clientidmode="Static">
                <uc:AddressDisplay runat="server" ID="ShippingAddressDisplay" />
                <asp:linkbutton runat="server" id="lbEditShippingAddress" text="Edit" causesvalidation="false" />
            </asp:panel>
        </div>
    </asp:placeholder>
</div>
<asp:Panel runat="server" ID="pnlCommentsWrapper" CssClass="commentsWrapper" Visible="false">
    <asp:panel runat="server" id="paymentPlanOptions" visible="false" cssclass="paymentPlans paymentPlan large-6 medium-12 small-12">
        <div class="optInPaymentPlan">
            <asp:checkbox runat="server" id="paymentPlanOptIn" />
            <asp:literal runat="server" id="PaymentPlanHelp" />
        </div>
        <div id="paymentPlanSelectionLI" runat="server" visible="false" class="paymentPlanSelect">
            <asp:dropdownlist runat="server" id="paymentPlanSelections" appenddatabounditems="true"
                datavaluefield="ID" datatextfield="Description" enabled="false">
                <asp:listitem value="">Select a payment plan</asp:listitem>
            </asp:dropdownlist>
        </div>
        <ul id="paymentPlansAmts">
            <asp:repeater runat="server" id="paymentPlanEstimates">
                <itemtemplate>
                            <li runat="server" id="paymentPlanInfo">Here's what your estimated payments might look
                                like:
                                <ul>
                                    <asp:Repeater runat="server" ID="estimatedPaymentAmounts">
                                        <ItemTemplate>
                                            <li><strong>
                                                <%# Eval("BillAmount", "{0:c}") %></strong> on
                                                <%# Eval("BillDate", "{0:d}") %>
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                            </li>
                        </itemtemplate>
            </asp:repeater>
        </ul>
    </asp:panel>
    <div class="step orderComments large-6 medium-12 small-12">
        <asp:literal runat="server" id="orderCommentsHeader" />
        <asp:literal runat="server" id="orderCommentsDescription" />
        <asp:textbox runat="server" id="txtOrderComments" textmode="MultiLine" />
    </div>
</asp:Panel>
<uc:ChangeBillingAddress runat="server" id="ChangeBillingAddress">
</uc:ChangeBillingAddress>
<uc:ChangeShippingAddress runat="server" id="ChangeShippingAddress" JavaScriptFile="\js\checkout\shippingAddress.js">
</uc:ChangeShippingAddress>
<checkout:TempAccountPrompt runat="server" ID="checkoutTempAccount" />
