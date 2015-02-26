<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FullSelectNewSeating.ascx.cs" Inherits="TheaterTemplate.Web.Controls.Subscriptions.FullControls.FullSelectNewSeating" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<asp:Panel runat="server" ID="pnlSelectNewSection" CssClass="selectNewSectionContainer" Style="display: none;">
    <asp:ModalPopupExtender ID="mdlSelectNewSection" runat="server" TargetControlID="btnShowModal"
        PopupControlID="pnlSelectNewSection" DropShadow="false" RepositionMode="none"
        BackgroundCssClass="bgOverlay" CancelControlID="lbClose" />
    <asp:Button runat="server" ID="btnShowModal" Text="Show Modal" Style="display: none;" />
    <asp:HiddenField runat="server" ID="activeVenueId" />
    <div class="subsModalInner">
        <div class="packageDescription">
            <h2>Update Seating</h2>
        </div>
        <div class="subsModalContent">
            <div class="performancesSection">
                <div class="performancesError">
                    <h3>
                        The section you chose is not available for the all of your packages.
                    </h3>
                    <p>
                        Please select a different arrangement for:
                    </p>
                </div>
                <div class="soldOutPerformances">
                    <asp:Repeater runat="server" ID="rptFailedPackages">
                        <HeaderTemplate>
                            <table border="0" cellspacing="0" class="packageCart">
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="packageInfo">
                                    <asp:Literal runat="server" ID="ltrPackageTitle" />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </tbody> </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </div>
            <div class="sectionSelection">
                <asp:Image CssClass="venueImage" runat="server" ID="venueImage" />
                <div class="newSelection">
                    <asp:Repeater runat="server" ID="rptFailedPackagesSelection">
                        <ItemTemplate>
                            <div class="failedPerformanceRow">
                                <%-- USED TO DETERMINE WHICH TAB IS SELECTED WHEN UPDATING SELECTIONS --%>
                                <asp:HiddenField runat="server" ID="tabSelected" />
                                <asp:HiddenField runat="server" ID="originalSectionId" />
                                <asp:HiddenField runat="server" ID="packageId" />
                                <h3>
                                    <asp:Literal ID="ltrPackageName" runat="server" />
                                </h3>
                                <ul class="tabLinks">
                                    <li id="diffSeats" class="selected">Select a Different Section </li>
                                    <li id="diffDates">Choose a New Date </li>
                                </ul>
                                <div class="section">
                                    <asp:Repeater runat="server" ID="rptSections">
                                        <HeaderTemplate>
                                            <div class="theaterSectionList">
                                                <ul>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <li runat="server" id="radioContainer">
                                                <asp:HiddenField runat="server" ID="venueId" />
                                                <asp:HiddenField runat="server" ID="sectionId" />
                                                <asp:RadioButton runat="server" ID="rbSection" />
                                                <asp:Label ID="seatingLabel" runat="server" AssociatedControlID="rbSection"><span
                                                    class="prices">
                                                    <asp:Literal runat="server" ID="ltrSectionPrice" /></span> <strong>
                                                        <asp:Literal runat="server" ID="ltrSectionName" /></strong>
                                                    <asp:Literal runat="server" ID="ltrSectionDescription" />
                                                </asp:Label>
                                            </li>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </ul> <small>Prices are per ticket</small> </div>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </div>
                                <div class="date" style="display: none;">
                                    <asp:ListBox runat="server" ID="lbxAlternatePackages" DataTextField="Description"
                                        CssClass="" DataValueField="PackageId" DataTextFormatString="{0:dddd, MMM d, h:mmtt}" />
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
        <asp:Panel runat="server" ID="pnlReserveError" Visible="false" CssClass="reserveError errorBox">
            <asp:Literal runat="server" ID="ltrError" />
        </asp:Panel>
        <div class="continueBtn updateButton">
            <asp:LinkButton runat="server" ID="btnUpdateSelections" Text="Update Selection" CssClass="btn btnStandOut fullUpdateSelection" />
        </div>
    </div>
</asp:Panel>