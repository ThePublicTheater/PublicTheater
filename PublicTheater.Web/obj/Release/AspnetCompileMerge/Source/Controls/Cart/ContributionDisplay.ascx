<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ContributionDisplay.ascx.cs"
    Inherits="PublicTheater.Web.Controls.Cart.ContributionDisplay" %>
<div class="packageArea">
    <div class="miniCart">
        <div class="packageCart giftcertificates">
            <ul class="giftCertCartHead">
                <li>
                    <asp:literal runat="server" id="ltrDonationHeader" /></li>
                <li class="price">
                    <asp:literal runat="server" id="ltrDonationTotalHeader" /></li>
            </ul>
            <ul class="cartBody giftCertCart">
                <li>
                    <asp:linkbutton runat="server" id="lbRemove" text="X" commandname="Remove" tooltip="Remove this from your cart"
                        cssclass="remove lTeal" />
                    <asp:literal runat="server" id="ltrDonationThankYouText" />
                </li>
                <li class="price">
                    <asp:literal runat="server" id="ltrPrice" /></li>
            </ul>
        </div>
    </div>
</div>
