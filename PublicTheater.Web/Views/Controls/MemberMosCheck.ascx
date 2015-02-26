<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MemberMosCheck.ascx.cs"
    Inherits="PublicTheater.Web.Views.Controls.MemberMosCheck" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<asp:Panel runat="server" ID="pnlMOSDialog" CssClass="selectNewSectionContainer"
    Style="display: none;">
    <asp:ModalPopupExtender ID="mdlChangeAddress" runat="server" TargetControlID="btnShowModal"
        PopupControlID="pnlMOSDialog" DropShadow="false" RepositionMode="none" BackgroundCssClass="bgOverlay"
        CancelControlID="lbClose" />
    <asp:Button runat="server" ID="btnShowModal" Text="Show Modal" Style="display: none;" />
    <div class="subsModalInner" style="float:none;">
        <div class="packageDescription">
            <h2>
                Cart Conflict</h2>
        </div>
        <div class="subsModalContent">
            <asp:Literal runat="server" ID="litContent"></asp:Literal>
        </div>
        <div class="continueBtn updateButton" style="height: 48px;">
            <asp:LinkButton runat="server" ID="lbOK" CssClass="btn btnStandOut" CausesValidation="false">Confirm</asp:LinkButton>
        </div>
    </div>
</asp:Panel>
