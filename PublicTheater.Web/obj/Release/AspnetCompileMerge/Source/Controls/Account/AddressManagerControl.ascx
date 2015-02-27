<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddressManagerControl.ascx.cs"
    Inherits="PublicTheater.Web.Controls.Account.AddressManagerControl" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<%@ Register TagPrefix="uc" Src="~/controls/checkout/UpdateAddressControl.ascx" TagName="UpdateAddressControl" %>
<div class="large-6 medium-6 small-12">
    <h3>
        Billing Address</h3>
    <asp:Label runat="server" ID="lblBillingAddress" />
    <asp:LinkButton runat="server" ID="lbBillingEditAddress">Edit</asp:LinkButton>
</div>

<div class="large-6 medium-6 small-12">
    <h3>
        Shipping Addresses</h3>
    <asp:ListView runat="server" ID="lvExistingAddresses" OnItemDataBound="lvExistingAddresses_ItemDataBound">
        <LayoutTemplate>
            <ul class="shippingAddressList">
                <li runat="server" id="itemPlaceholder" />
            </ul>
        </LayoutTemplate>
        <ItemTemplate>
            <li>
                <asp:Label runat="server" ID="lblAddress" />
                <asp:LinkButton runat="server" ID="lbEditAddress" CssClass="profileEdit" CommandName="EditItem">Edit</asp:LinkButton>
                <asp:LinkButton runat="server" ID="lbDeleteAddress" CssClass="profileDelete" Visible="False" CommandName="DeleteItem">Delete</asp:LinkButton>
            </li>
        </ItemTemplate>
    </asp:ListView>
    <asp:LinkButton runat="server" ID="btnCreateNewAddress" CssClass="btn profileAdd" Visible="False">New Address</asp:LinkButton>
</div>

<asp:Panel runat="server" ID="pnlAddressChange" CssClass="selectNewSectionContainer"
    Style="display: none;">
    <asp:ModalPopupExtender ID="mdlChangeAddress" runat="server" TargetControlID="btnShowModal"
        PopupControlID="pnlAddressChange" DropShadow="false" RepositionMode="none" BackgroundCssClass="bgOverlay"
        CancelControlID="lbClose" />
    <asp:Button runat="server" ID="btnShowModal" Text="Show Modal" Style="display: none;" />
    <div class="subsModalInner">
        <div class="packageDescription">
            <h2>
                Edit Address</h2>
        </div>
        
        <div class="subsModalContent">
            <div id="addresses">
                <uc:UpdateAddressControl runat="server" ID="UpdateAddressControl"></uc:UpdateAddressControl>
            </div>
        </div>
        <div class="continueBtn updateButton">
            <asp:LinkButton runat="server" ID="btnCancel" Text="Cancel" CssClass="btn btnCancel"
                CausesValidation="false" />
            <asp:LinkButton runat="server" ID="btnUpdate" Text="Update Address" CssClass="btn btnStandOut"
                ValidationGroup="AddressEdit" />
        </div>
    </div>
</asp:Panel>
