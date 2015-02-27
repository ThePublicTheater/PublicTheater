<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GiftCertificateDisplay.ascx.cs" Inherits="TheaterTemplate.Web.Controls.CartControls.GiftCertificateDisplay" %>

<div class="packageArea">
    <div class="miniCart">
        <div class="packageCart giftcertificates">           
            <ul class="giftCertCartHead">
                <li><asp:Literal runat="server" ID="ltrHeader" /></li>
                <li class="price"><asp:Literal runat="server" ID="ltrTotalHeader" /></li>
            </ul>
            
            <ul class="cartBody giftCertCart">
                <li>
                    <asp:LinkButton runat="server" ID="lbRemoveGiftCert" Text="X" CommandName="RemoveItem" CssClass="remove lTeal" ToolTip="Remove this from your cart" />
                    <asp:Literal runat="server" ID="ltrGiftCertificateText" />
                </li>
                <li class="price"><asp:Literal runat="server" ID="ltrPrice" /></li>
            </ul>
        </div>
    </div>
</div>









