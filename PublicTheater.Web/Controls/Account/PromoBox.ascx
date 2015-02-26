<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PromoBox.ascx.cs" Inherits="PublicTheater.Web.Controls.Account.PromoBox" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=4.1.51116.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>


<div class="formSection">



    <asp:Panel runat="server" ID="pnlPromoEntry" DefaultButton="btnApplyPromo" CssClass="pnlPromoEntry">

        <div class="promo">
            <a href="#" id="havePromo" class="promoLink btn">
                <EPiServer:Property runat="server" ID="MainBodyProp" PropertyName="MainBody" />
            </a>
            <asp:Label runat="server" ID="lblPromoError" CssClass="errorMsg" onclick="$(this).siblings('.promoLink').click()"></asp:Label>
            <a href="#" class="promoWhatsThisLink">What's this?</a>
            <div class="promoWhatsThisContent">
                <a href="#" class="close">x</a><EPiServer:Property runat="server" ID="WhatsThisContentProperty" PropertyName="CustomProperty3" />
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
            <asp:LinkButton runat="server" ID="lnkDummy2" Text="Remove" CausesValidation="False" OnClientClick="$find('adaPopup').show();"></asp:LinkButton></span>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlWrongPromoDisplay" CssClass="promoDesc" Visible="false">
        <span><strong>
            <asp:Label runat="server" ID="lblWrongPromoCode"></asp:Label></strong></span>
    </asp:Panel>
</div>
<asp:ModalPopupExtender runat="server" BehaviorID="cartEmptyPop" TargetControlID="lnkDummy2" PopupControlID="cartEmptyPopPanel" DropShadow="false" RepositionMode="none" BackgroundCssClass="bgOverlay" />
<asp:Panel runat="server" ID="cartEmptyPopPanel" CssClass="selectNewSectionContainer"
    Style="display: none;">
    <div class="subsModalInner" style="width:95%">
        <div class="packageDescription">
            <h2>Notice</h2>
        </div>
        <div class="subsModalContent">
           Removing the promo code will also remove everything from your cart.
                                    <br />
        </div>
        <div class="continueBtn updateButton">
             <asp:LinkButton ID="LinkButton2" runat="server" CssClass="btn btnStandOut"  OnClientClick="$find('adaPopup').hide(); return false;" CausesValidation="false">Cancel</asp:LinkButton>
            <asp:LinkButton ID="btnRemovePromo" runat="server" CssClass="btn btnStandOut" CausesValidation="false">Continue</asp:LinkButton>
        </div>
    </div>

</asp:Panel>


