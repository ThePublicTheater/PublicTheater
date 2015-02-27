<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ConfirmationOptions.ascx.cs" Inherits="PublicTheater.Web.Controls.Checkout.ConfirmationOptions" %>
<%@ Register Src="~/controls/checkout/AddressDisplayControl.ascx" TagPrefix="uc" TagName="AddressDisplay" %>

<asp:Literal runat="server" ID="confirmationThankYou" />                        

<h3 class="orderNumber">Order #: <asp:Literal runat="server" id="ltrOrderNumber"></asp:Literal></h3>

<div class="step paymentInformation span4">
    <div class="addressBilling">
        <asp:Literal runat="server" ID="billingAddressHeader" />                        
        <uc:AddressDisplay runat="server" ID="billngAddress" />
    </div>
</div>

<asp:Panel runat="server" ID="pnlDeliveryMethod" CssClass="step paymentInformation span4">
    <div class="addressShipping">
        <asp:Literal runat="server" ID="deliveryMethodHeader"  />
        <p>
            <asp:Literal runat="server" ID="ltrDeliveryMethod" />
        </p>
        <h3 runat="server" id="shippingAddressHeader" visible="false">
            Shipping Address
        </h3>
        <uc:AddressDisplay runat="server" ID="deliveryAddress" ShowEmailAddress="false" Visible="false" />
    </div>
</asp:Panel>

<asp:Panel runat="server" ID="orderCommentsStep" CssClass="step orderComments span4">
    <asp:Literal runat="server" ID="orderCommentsHeader" />
    <p>
        <asp:Literal runat="server" ID="ltrOrderNotes"></asp:Literal>
    </p>
</asp:Panel>

<div class="step nextSteps span12">
    <asp:Literal runat="server" ID="nextStepsText" />
</div>

<%--<asp:Literal runat="server" ID="confirmationThankYou" />                        

<h3>Order #: <asp:Literal runat="server" id="ltrOrderNumber"></asp:Literal></h3>

<div class="step">
    <asp:Literal runat="server" ID="billingAddressHeader" />                        
    <uc:AddressDisplay runat="server" ID="billngAddress" />
</div>

<asp:Panel runat="server" ID="pnlDeliveryMethod" CssClass="step">
    <asp:Literal runat="server" ID="deliveryMethodHeader"  />
    <p>
        <asp:Literal runat="server" ID="ltrDeliveryMethod" />
    </p>
    <h3 runat="server" id="shippingAddressHeader" visible="false">
        Shipping Address
    </h3>
    <uc:AddressDisplay runat="server" ID="deliveryAddress" ShowEmailAddress="false" Visible="false" />
</asp:Panel>

<asp:Panel runat="server" ID="orderCommentsStep" CssClass="step">
    <asp:Literal runat="server" ID="orderCommentsHeader" />
    <p>
        <asp:Literal runat="server" ID="ltrOrderNotes"></asp:Literal>
    </p>
</asp:Panel>
<div class="step nextSteps">
    <asp:Literal runat="server" ID="nextStepsText" />
</div>--%>