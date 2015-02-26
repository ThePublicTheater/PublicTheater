<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PackageVoucherRedemptionBlockControl.ascx.cs"
    Inherits="PublicTheater.Web.Views.Blocks.PackageVoucherRedemptionBlockControl " %>

<asp:Panel runat="server" ID="pnlPromoEntry" DefaultButton="lbApplyRedemptionCode" CssClass="block pnlPromoEntry packageVoucher">

    <EPiServer:Property ID="propDescription" PropertyName="Description" runat="server" CustomTagName="div" />

    <div>
        <asp:TextBox runat="server" ID="txtRedemptionCode" placeholder="Redemption Code"></asp:TextBox>
        <asp:LinkButton runat="server" ID="lbApplyRedemptionCode" CssClass="btn solid" Text="Apply" CausesValidation="False" />
    </div>

    <a href="#" class="promoWhatsThisLink">What's this?</a>
    <div class="promoWhatsThisContent">
        <a href="#" class="close">x</a><EPiServer:Property runat="server" ID="WhatsThisContentProperty" PropertyName="WhatsThisContentProperty" />
    </div>

    <asp:Label runat="server" ID="lblError" CssClass="errorMsg"></asp:Label>
</asp:Panel>

