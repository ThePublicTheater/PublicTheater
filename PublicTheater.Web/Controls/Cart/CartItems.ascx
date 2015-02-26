<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CartItems.ascx.cs" Inherits="PublicTheater.Web.Controls.Cart.CartItems" %>

<%@ Register Src="~/controls/cart/FullPackageDisplay.ascx" TagPrefix="cart" TagName="FullPackageDisplay" %>
<%@ Register Src="~/controls/cart/MasterPackageDisplay.ascx" TagPrefix="cart" TagName="MasterPackageDisplay" %>
<%@ Register Src="~/controls/cart/FlexPackageDisplay.ascx" TagPrefix="cart" TagName="FlexPackageDisplay" %>
<%@ Register Src="~/controls/cart/SingleTicketDisplay.ascx" TagPrefix="cart" TagName="SingleTicketDisplay" %>
<%@ Register Src="~/controls/cart/ContributionDisplay.ascx" TagPrefix="cart" TagName="ContributionDisplay" %>
<%@ Register Src="~/controls/cart/GiftCertificateDisplay.ascx" TagPrefix="cart" TagName="GiftCertificateDisplay" %>
<%@ Register Src="~/controls/cart/ExchangeTicketDisplay.ascx" TagPrefix="cart" TagName="ExchangeDisplay" %>
<%@ Register Src="~/controls/cart/RenewalEditDisplay.ascx" TagPrefix="cart" TagName="RenewalEditDisplay" %>
<%@ Register Src="~/controls/cart/CartAddOnDisplay.ascx" TagPrefix="cart" TagName="CartAddOns" %>

<div id="cartItemDisplay">
    <ul class="thumbnails">
        <asp:Repeater ID="rptCartItems" runat="server">
            <ItemTemplate>
                <li class="span12">
                  
                    <cart:MasterPackageDisplay runat="server" ID="cartMasterPackageDisplay" Visible="false" />
                    <cart:FullPackageDisplay runat="server" ID="cartFullPackage" Visible="false" HideHeader="false" />
                    <cart:FlexPackageDisplay runat="server" ID="cartFlexPackage" Visible="false" />
                    <cart:SingleTicketDisplay runat="server" ID="cartSingleTicket" Visible="false" />
                    <cart:GiftCertificateDisplay runat="server" ID="cartGiftCertificate" Visible="false" />
                    <cart:ExchangeDisplay runat="server" ID="cartExchange" Visible="false" />
                    
                </li>
            </ItemTemplate>
        </asp:Repeater>
        <asp:Repeater runat="server" ID="rptContributions">
            <ItemTemplate>
                <cart:ContributionDisplay runat="server" ID="cartContribution" />
            </ItemTemplate>
        </asp:Repeater>
    </ul>
</div>

<asp:Repeater runat="server" ID="rptRenewalEdits">
    <HeaderTemplate>
        <div class="renewalEdits">
    </HeaderTemplate>
    <ItemTemplate>
        <cart:RenewalEditDisplay runat="server" ID="cartRenewalEditDisplay" />
    </ItemTemplate>
    <FooterTemplate>
        </div>
    </FooterTemplate>
</asp:Repeater>

<asp:Repeater runat="server" ID="rptCardAddOns">
    <ItemTemplate>
        <div id="cartParkingArea">
            <cart:CartAddOns runat="server" id="cartAddOn" />
        </div>
    </ItemTemplate>
</asp:Repeater>

<asp:Panel runat="server" ID="pnlAddOnError" Visible="false" CssClass="alert alert-error">
    <asp:Literal runat="server" ID="ltrAddOnError" />
</asp:Panel>