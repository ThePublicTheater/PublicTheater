<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ConfirmationControl.ascx.cs" Inherits="PublicTheater.Web.Controls.Checkout.ConfirmationControl" %>
<%@ Register Src="~/controls/cart/CartItems.ascx" TagPrefix="cart" TagName="CartItems" %>
<%@ Register Src="~/controls/cart/CartTotals.ascx" TagPrefix="cart" TagName="CartTotals" %>
<%@ Register Src="~/controls/checkout/ConfirmationOptions.ascx" TagPrefix="checkout" TagName="ConfirmationOptions" %>

<div id="confirmationContainer">
    
    <div id="confirmationMainDisplay">
        
        <div id="packageDisplay" style="display:none;">
            <div class="packageDescription">
                <asp:Literal runat="server" ID="tessituraHeader" />                
            </div>
        </div>

        <div id="confirmationOptions">
            <div class="row-fluid">
                <checkout:ConfirmationOptions runat="server" ID="checkoutConfirmationOptions" />
            </div>           
        </div>

        <cart:CartItems id="cartItems" runat="server" />

        <div id="cartActions">
            <%--<div class="orderSubTotal">--%>
                <cart:CartTotals runat="server" ID="cartTotals" IsConfirmationPage="true" />
            <%--</div>--%>
        </div>

    </div>

</div>