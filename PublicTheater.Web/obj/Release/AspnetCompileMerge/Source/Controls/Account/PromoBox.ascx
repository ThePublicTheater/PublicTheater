<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PromoBox.ascx.cs" Inherits="PublicTheater.Web.Controls.Account.PromoBox" %>


<div class="formSection">



    <asp:Panel runat="server" ID="pnlPromoEntry" DefaultButton="btnApplyPromo" CssClass="pnlPromoEntry">

        <div class="promo">
            <a href="#" id="havePromo" class="promoLink btn">
                <EPiServer:Property runat="server" ID="MainBodyProp" PropertyName="MainBody" />
            </a>
            <asp:Label runat="server" ID="lblPromoError" CssClass="errorMsg" onclick="$(this).siblings('.promoLink').click()"></asp:Label>
            <a href="#" class="promoWhatsThisLink">What's this?</a>
            <div class="promoWhatsThisContent"><a href="#" class="close">x</a><EPiServer:Property runat="server" ID="WhatsThisContentProperty" PropertyName="CustomProperty3" />
            </div>
        </div>

        <div id="enterPromoCode" style="display: none;">
            <asp:TextBox runat="server" ID="txtPromoCode"></asp:TextBox>
            <asp:Button runat="server" ID="btnApplyPromo" CssClass="btn" Text="Apply" CausesValidation="False" />
        </div>

    </asp:Panel>

            <asp:Panel runat="server" ID="pnlPromoDisplay" CssClass="promoDesc" Visible="false">
                <span><strong>Code accepted:</strong>
                    <asp:Label runat="server" ID="lblPromoCode"></asp:Label></span>
                <asp:Label runat="server" ID="lblPromoDescription"></asp:Label>
                <span>
                    <asp:LinkButton runat="server" ID="btnRemovePromo" Text="Remove" CausesValidation="False"></asp:LinkButton></span>
            </asp:Panel>
            <asp:Panel runat="server" ID="pnlWrongPromoDisplay" CssClass="promoDesc" Visible="false">
                <span><strong>
                    <asp:Label runat="server" ID="lblWrongPromoCode"></asp:Label></strong></span>
            </asp:Panel>
</div>

