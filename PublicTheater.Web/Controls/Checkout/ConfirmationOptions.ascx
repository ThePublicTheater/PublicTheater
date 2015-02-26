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
<asp:Panel runat="server" ID="GoogleAnalyticsPanel">
    <asp:HiddenField ID="hfOrderNo" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfOrganization" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfTotal" runat="server" ClientIDMode="Static"/>
    <asp:HiddenField ID="hfFees" runat="server" ClientIDMode="Static"/>
    <asp:HiddenField ID="hfShipping" runat="server" ClientIDMode="Static"/>
    <asp:HiddenField ID="hfCity" runat="server" ClientIDMode="Static"/>
    <asp:HiddenField ID="hfState" runat="server" ClientIDMode="Static"/>
    <asp:HiddenField ID="hfCountry" runat="server" ClientIDMode="Static"/>
</asp:Panel>

<script type="text/javascript">
    var orderNo = $('#hfOrderNo').val();
    var organization = $('#hfOrganization').val();
    var total= $('#hfTotal').val();
    var fees = $('#hfFees').val();
    var shipping = $('#hfShipping').val();
    var city = $('#hfCity').val();
    var state = $('#hfState').val();
    var country= $('#hfCountry').val();


    

    // add item might be called for every item in the shopping cart
    // where your ecommerce engine loops through each item in the cart and
    // prints out _addItem for each
    var itemString = $('#LineItem0').val();
    var i = 0;
    var items = [];
    while (itemString != null) {
        var itemArray = itemString.split(";");
        items.push({
            'sku': itemArray[1], // SKU/code - required
            'name': itemArray[2], // product name
            'category':itemArray[3], // category or variation
            'price':itemArray[4], // unit price - required
            'quantity':itemArray[5] // quantity - required
           });
        i = i + 1;
        itemString = $('#LineItem' + i.toString()).val();
        //alert('#LineItem' + i.toString() + $('#LineItem' + i.toString()).val());
    }
    dataLayer = [
    {
        'transactionId': orderNo,
        'transactionAffiliation': organization,
        'transactionTotal': total,
        'transactionTax': fees,
        'transactionShipping': 0,
        'transactionProducts':items
    }
    ];

    //_gaq.push(['_trackTrans']); //submits transaction to the Analytics servers
    
</script>
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