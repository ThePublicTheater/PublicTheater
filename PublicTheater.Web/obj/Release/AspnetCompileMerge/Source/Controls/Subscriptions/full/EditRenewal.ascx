<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditRenewal.ascx.cs" Inherits="TheaterTemplate.Web.Controls.Subscriptions.FullControls.EditRenewal" %>
<%@ Register Src="~/controls/cart/CartItems.ascx" TagPrefix="adg" TagName="CartItems" %>
<%@ Register Src="~/Views/Controls/XFormControl.ascx" TagPrefix="adg" TagName="XFormControl" %>
<%@ Register Src="~/controls/cart/CartTotals.ascx" TagPrefix="cart" TagName="CartTotals" %>
<%@ Register Src="~/Controls/Subscriptions/full/RenewalChangeRequest.ascx" TagPrefix="subscription" TagName="RenewalChangeRequest" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<div class="cartItems">
    <EPiServer:Property ID="propHeader" runat="server" PropertyName="Heading" DisplayMissingMessage="false" />
    <EPiServer:Property ID="propMainBody" runat="server" PropertyName="MainBody" DisplayMissingMessage="false" />
    <adg:CartItems runat="server" ID="cartItems" />
</div>

<asp:Panel runat="server" ID="venuePanels" ClientIDMode="Static">
    <asp:Repeater runat="server" ID="rptChangeRequestItems">
        <ItemTemplate>
            <subscription:RenewalChangeRequest runat="server" ID="subsRenewalChangeRequest" 
                ShowAlternateSections="true" ShowAlternatePackages="true" ShowSeatingUpgradeChoices="true"
                ShowQuantityChange="true" ShowAdditionalNotes="true" />
        </ItemTemplate>
    </asp:Repeater>
</asp:Panel>

<div id="cartActions">
    <div class="orderSubTotal">
        <cart:CartTotals runat="server" ID="cartTotals" IsConfirmationPage="false" />
    </div>
</div>
<asp:Button runat="server" ID="submitChanges" Text="Submit Changes" CssClass="btn floatRight"
    ClientIDMode="Static" />
<asp:Panel ID="PopupPanel" runat="server" CssClass="simpleModal" Style="display: none;">
    <div class="">
        <EPiServer:Property ID="Property1" runat="server" PropertyName="ConfirmationMessage" DisplayMissingMessage="false" />
        <asp:Button ID="btnContinue" CssClass="close btn" runat="server" Text="Continue" />
        <asp:Button ID="btnCancel" CssClass="close btn" runat="server" Text="Cancel" />
    </div>
</asp:Panel>

<ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender" runat="server" Drag="true" CancelControlID="btnCancel"
    DropShadow="false" TargetControlID="submitChanges" PopupControlID="PopupPanel" BackgroundCssClass="bgOverlay" />
