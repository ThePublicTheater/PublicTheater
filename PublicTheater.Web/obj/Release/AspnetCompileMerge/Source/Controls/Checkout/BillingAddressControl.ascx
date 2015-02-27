<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BillingAddressControl.ascx.cs" Inherits="PublicTheater.Web.Controls.Checkout.BillingAddressControl" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<%@ Register TagPrefix="uc" Src="~/controls/checkout/UpdateAddressControl.ascx" TagName="UpdateAddressControl" %>
<asp:Panel runat="server" ID="pnlAddressChange" CssClass="selectNewSectionContainer"
    Style="display: none;">
    <asp:ModalPopupExtender ID="mdlChangeAddress" runat="server" TargetControlID="btnShowModal"
        PopupControlID="pnlAddressChange" DropShadow="false" RepositionMode="none" BackgroundCssClass="bgOverlay"
        CancelControlID="btnCancel" />
    <asp:Button runat="server" ID="btnShowModal" Text="Show Modal" Style="display: none;" />
    <div class="subsModalInner">
        <div class="packageDescription">
            <h2>
                Edit Billing Address</h2>
        </div>
        <div class="subsModalContent">
            <div id="addresses">
                <asp:Panel ID="pnlOldAddress" runat="server" CssClass="oldAddress">
                    <h4>
                        We have your address as:</h4>
                    <asp:Literal runat="server" ID="ltrOldAddress"></asp:Literal>
                </asp:Panel>
                <asp:Panel runat="server" ID="pnlInvalidBilling" CssClass="oldAddress" Visible="false">
                    <asp:Literal runat="server" ID="ltrInvalidBillingAddress" />
                </asp:Panel>
                <uc:UpdateAddressControl runat="server" id="UpdateAddressControl"></uc:UpdateAddressControl>
            </div>
        </div>        
        <div class="continueBtn updateButton">
            <asp:LinkButton runat="server" ID="btnCancel" Text="Cancel" CssClass="btn btnCancel" CausesValidation="false" />
            <asp:LinkButton runat="server" ID="btnUpdate" Text="Update Address" CssClass="btn btnStandOut" ValidationGroup="BillingAddress" />
        </div>
    </div>
</asp:Panel>
