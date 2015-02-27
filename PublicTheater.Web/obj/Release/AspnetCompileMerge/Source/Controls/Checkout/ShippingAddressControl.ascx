<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ShippingAddressControl.ascx.cs" Inherits="PublicTheater.Web.Controls.Checkout.ShippingAddressControl" %>
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
                Edit Shipping Address</h2>
        </div>
        <div class="subsModalContent">
            <div id="addresses">
                <asp:Panel ID="pnlOldAddress" runat="server" CssClass="oldAddress">
                    <asp:HiddenField runat="server" ID="hfSelectedAddressID" ClientIDMode="Static" />
                    <asp:ListView runat="server" ID="lvExistingAddresses" OnItemDataBound="lvExistingAddresses_ItemDataBound">
                        <LayoutTemplate>
                            <ul class="shippingAddressList">
                                <li runat="server" id="itemPlaceholder" />
                            </ul>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <li>
                                <asp:HiddenField runat="server" ID="hfShippingAddressID" />
                                <asp:RadioButton runat="server" ID="rbShippingAddress" />
                            </li>
                        </ItemTemplate>
                    </asp:ListView>                    
                    <asp:LinkButton runat="server" ID="btnUpdate" Visible="False" Text="Use Selected Address" CssClass="btn btnStandOut" ValidationGroup="ShippingAddress" CausesValidation="false" />
                </asp:Panel>
                <uc:UpdateAddressControl runat="server" ID="UpdateAddressControl" ></uc:UpdateAddressControl>
            </div>                        
        </div>
        <div class="continueBtn updateButton">        
            <asp:LinkButton runat="server" ID="btnCancel" Text="Cancel" CssClass="btn" CausesValidation="false" />            
            <asp:LinkButton runat="server" ID="btnCreateNewAddress" Text="Update Address"  ValidationGroup="AddressControl" CssClass="btn btnStandOut"/>
        </div>
    </div>
</asp:Panel>
