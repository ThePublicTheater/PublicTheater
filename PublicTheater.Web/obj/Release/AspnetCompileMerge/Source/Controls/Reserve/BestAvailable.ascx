<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BestAvailable.ascx.cs"
    Inherits="PublicTheater.Web.Controls.Reserve.BestAvailable" %>
<%@ Register Assembly="TheaterTemplate.Shared" TagPrefix="theaterTemplate" Namespace="TheaterTemplate.Shared.WebControls" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=4.1.51116.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>


        
<div id="bestAvailableTable" class="table table-condensed table-bordered">

    <ul class="large-12 medium-12 small-12 tableHead">
        <li>Section</li>               
        <asp:ListView runat="server" ID="lvPriceTypeNames">
            <ItemTemplate>
                <li>
                    <asp:Label runat="server" ID="lblPriceTypeName"></asp:Label>
                </li>
            </ItemTemplate>
        </asp:ListView>
    </ul>

    <asp:ListView runat="server" ID="lvSectionSelection">
        <ItemTemplate>
            <ul class="large-12 medium-12 small-12 tableBody">
                <li><theaterTemplate:GroupRadioButton runat="server" ID="rbSection" GroupName="SectionRadios" /></li>
                <asp:ListView runat="server" ID="lvSectionPriceTypes">
                    <ItemTemplate>
                        <li>
                            <asp:Label runat="server" ID="lblSectionPrice"></asp:Label>
                        </li>
                    </ItemTemplate>
                </asp:ListView>
            </ul>
        </ItemTemplate>
    </asp:ListView>

    <ul class="large-12 medium-12 small-12 tableFoot">
        <li>Quantity</li>
        <asp:ListView runat="server" ID="lvPriceTypeQuantitySelection">
                <ItemTemplate>
                    <li>
                        <asp:DropDownList runat="server" ID="ddlPriceTypeQuantity">
                        </asp:DropDownList>
                    </li>
                </ItemTemplate>
        </asp:ListView>
    </ul>

</div>

<div class="large-12 medium-12 small-12">
    <asp:LinkButton runat="server" ID="btnSubmit" Text="Reserve" class="btn bestAvailableButton" />
    <asp:HiddenField runat="server" ID="submitClicked" ClientIDMode="Static" Value="false" />
    <episerver:Property runat="server" id="BestAvailableMainBody" PropertyName="CustomProperty3" />

    <asp:Panel runat="server" ID="pnlError" CssClass="errorBox" Visible="false">
        <asp:Label runat="server" ID="lblErrors" />
    </asp:Panel>
    <asp:Image runat="server" id="VenueImage" />
</div>


<asp:Panel runat="server" ID="pnlMultiZoneDialog" CssClass="selectNewSectionContainer" Style="display: none;">
    <asp:ModalPopupExtender ID="mdlMultiZoneConfirm" runat="server" TargetControlID="btnShowModal"
        PopupControlID="pnlMultiZoneDialog" DropShadow="false" RepositionMode="none" BackgroundCssClass="bgOverlay"
        CancelControlID="lbClose" />
    <asp:Button runat="server" ID="btnShowModal" Text="Show Modal" Style="display: none;" />
    <div class="subsModalInner">
        <div class="packageDescription">
            <h2>Note: Your order will not all be seated together.</h2>
        </div>
        <div class="subsModalContent">
            You have selected both priority and standard seats, which are located in different parts of the theater. Do you want to continue?
        </div>
        <div class="continueBtn updateButton">
            <asp:LinkButton runat="server" ID="lbOK" CssClass="btn btnStandOut" CausesValidation="false">Yes - Continue</asp:LinkButton>
            <asp:LinkButton runat="server" ID="lbClose" CssClass="btn btnStandOut" CausesValidation="false">No - Cancel</asp:LinkButton>
        </div>
    </div>
</asp:Panel>