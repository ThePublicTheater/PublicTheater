<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TemporaryAccountPrompt.ascx.cs" Inherits="TheaterTemplate.Web.Controls.CheckoutControls.TemporaryAccountPrompt" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>

<asp:Panel runat="server" ID="pnlUpdatePassword" CssClass="selectNewSectionContainer"
    Style="display: none;">
    <asp:ModalPopupExtender ID="mdlUpdatePassword" runat="server" TargetControlID="btnShowModal"
        PopupControlID="pnlUpdatePassword" DropShadow="false" RepositionMode="none" BackgroundCssClass="bgOverlay"
        CancelControlID="btnClose" />
    <asp:Button runat="server" ID="btnShowModal" Text="Show Modal" Style="display: none;" />
    <asp:Button runat="server" ID="btnClose" Text="Show Modal" Style="display: none;" />
        <div class="subsModalInner">
            <div class="packageDescription">
                <asp:Literal runat="server" ID="ltrTempAccountHeader" />
            </div>
            <div class="subsModalContent">
                <div class="tempAccount">
                    <asp:Literal runat="server" ID="ltrTempDescription" />
                    <ul>
                        <li>
                            <asp:Label runat="server" ID="lblNewPassword" AssociatedControlID="txtNewPassword" Text="New Password:" />
                            <asp:TextBox runat="server" ID="txtNewPassword" TextMode="Password" />
                        </li>
                    </ul>
                    <asp:RequiredFieldValidator runat="server" ID="reqNewPassword" ControlToValidate="txtNewPassword"
                        Text="New Password Required" Display="Dynamic" CssClass="error" ValidationGroup="TempAccount"/>
                </div>
            </div>
            <div class="continueBtn updateButton">
                <asp:LinkButton runat="server" ID="btnUpdate" Text="Update Password" CssClass="btn btnStandOut" ValidationGroup="TempAccount" />
            </div>
        </div>
</asp:Panel>