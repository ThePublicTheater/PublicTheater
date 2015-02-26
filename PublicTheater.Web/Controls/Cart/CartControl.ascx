<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CartControl.ascx.cs"
    Inherits="PublicTheater.Web.Controls.Cart.CartControl" %>
<%@ Register Src="~/controls/cart/CartItems.ascx" TagPrefix="cart" TagName="CartItems" %>
<%@ Register Src="~/controls/cart/CartTotals.ascx" TagPrefix="cart" TagName="CartTotals" %>

<div id="cartContainer">
    <div id="cartMainDisplay">
        <div id="packageDisplay" style="display:none;">
            <div class="row-fluid">
                <div class="span8">
                    <div class="packageDescription"></div>
                    <div id="cartExpire">
                        <asp:Literal runat="server" ID="ltrTimerText" />
                    </div>
                </div>
            </div>
        </div>
        <div class="purchaseHeader">
            <asp:Literal runat="server" ID="ltrTessituraHeader" />
        </div>

        <asp:Repeater runat="server" ID="rptVenueLinks">
            <HeaderTemplate>
                <div id="venueLinks">
                    <h4>View Seating:</h4>
                    <ul>
            </HeaderTemplate>
            <ItemTemplate>
                <li>
                    <asp:HyperLink runat="server" ID="lnkVenueLink" Target="_blank" />
                </li>
            </ItemTemplate>
            <FooterTemplate>
                    </ul>
                </div>
            </FooterTemplate>
        </asp:Repeater>

        <cart:CartItems id="cartItems" runat="server" ShowRemoveButton="true" />

        <asp:PlaceHolder runat="server" ID="phEmptyCart" Visible="false">
            <div class="emptyCartAlert">
                <asp:Literal runat="server" ID="ltrEmptyCartText" />
            </div>
        </asp:PlaceHolder>

        <asp:Panel runat="server" ID="pnlError" Visible="false">
            <div class="invalidCartAlert">
                <asp:Literal ID="ltrCartError" runat="server" />
            </div>
        </asp:Panel>
    </div>
    
    <div id="cartActions">
        <cart:CartTotals runat="server" ID="cartTotals" />
        <div class="continueBtn checkoutBtns">
            <asp:LinkButton runat="server" ID="lbCheckout" CssClass="btn solid btnStandOut" Text="Checkout" />
            <asp:Literal runat="server" ID="ltrContinueShopping" />
        </div>
    </div>

</div>
