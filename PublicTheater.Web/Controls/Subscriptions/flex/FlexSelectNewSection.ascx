<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FlexSelectNewSection.ascx.cs"
    Inherits="TheaterTemplate.Web.Controls.Subscriptions.FlexControls.FlexSelectNewSection" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<asp:Panel runat="server" ID="pnlSelectNewSection" CssClass="selectNewSectionContainer"
    Style="display: none;">
    <asp:ModalPopupExtender ID="mdlSelectNewSection" runat="server" TargetControlID="btnShowModal"
        PopupControlID="pnlSelectNewSection" DropShadow="false" RepositionMode="none"
        BackgroundCssClass="bgOverlay" CancelControlID="lbClose" />
    <asp:Button runat="server" ID="btnShowModal" Text="Show Modal" Style="display: none;" />
    <asp:PlaceHolder runat="server" ID="phFailedSection" Visible="false">
        <div class="subsModalInner">
            <div class="packageDescription">
                <h2>Update Performances</h2>
            </div>
            <div class="subsModalContent">
                <div class="performancesSection">
                    <div class="performancesError">
                        <h3>
                            The section you chose is not available for the all of your performances.</h3>
                        <p>
                            Please select a different arrangement for:</p>
                    </div>
                    <div class="soldOutPerformances">
                        <asp:Repeater runat="server" ID="rptFailedPerformances">
                            <HeaderTemplate>
                                <table border="0" cellspacing="0" class="packageCart">
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:Literal runat="server" ID="ltrPerformanceName" />
                                    </td>
                                    <td>
                                        <asp:Literal runat="server" ID="ltrPerformanceTime" />
                                    </td>
                                    <td>
                                        <asp:Literal runat="server" ID="ltrVenue" />
                                    </td>
                                    <td>
                                        <asp:Literal runat="server" ID="ltrPriceTypes" />
                                    </td>
                                    <td>
                                        <asp:Literal runat="server" ID="ltrSection" />
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
                        <asp:Repeater runat="server" ID="rptFailedPerformanceSelection">
                            <ItemTemplate>
                                <div class="failedPerformanceRow">
                                    <%-- USED TO DETERMINE WHICH TAB IS SELECTED WHEN UPDATING SELECTIONS --%>
                                    <asp:HiddenField runat="server" ID="tabSelected" />
                                    <asp:HiddenField runat="server" ID="originalSectionId" />
                                    <asp:HiddenField runat="server" ID="performanceId" />
                                    <h3>
                                        <asp:Literal ID="ltrPerformanceName" runat="server" />
                                    </h3>
                                    <ul class="tabLinks">
                                        <li id="diffSeats" class="selected"><span>Select a Different Section</span></li>
                                        <li id="diffDates"><span>Choose a New Date</span></li>
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
                                        <asp:ListBox runat="server" ID="lbxAlternatePerformances" DataTextField="PerformanceDate"
                                            CssClass="" DataValueField="PerformanceId" DataTextFormatString="{0:dddd, MMM d, h:mmtt}" />
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
                <asp:LinkButton runat="server" ID="btnUpdateSelections" Text="Update Selection" CssClass="btn btnStandOut flexUpdateSelection" />
            </div>
        </div>
    </asp:PlaceHolder>
    <asp:PlaceHolder runat="server" ID="phEditSection" Visible="false">
        <div class="subsModalInner">
            <asp:LinkButton runat="server" ID="lbClose" Text="X" CssClass="remove" />
            <div class="packageDescription">
                <h2>Change Seating</h2>
            </div>
            <div class="subsModalContent">
                <div class="editSectionContainer">
                    <asp:HiddenField runat="server" ID="editedPerformanceId" />
                    <div class="productionDisplay">
                        <div class="productionImage">
                            <asp:Image runat="server" ID="perfImage" AlternateText="Performance thumbnail" />
                        </div>
                        <div class="productionInformation">
                            <h3>
                                <asp:Literal runat="server" ID="ltrPerformanceTitle" /></h3>
                            <h4>
                                <asp:Literal runat="server" ID="ltrPerformanceVenue" /></h4>
                            <asp:Literal runat="server" ID="ltrProductionSynopsis" />
                        </div>
                    </div>
                    <asp:Panel runat="server" ID="pnlEditSectionError" Visible="false" CssClass="reserveError">
                        <asp:Literal runat="server" ID="ltrEditSectionError" />
                    </asp:Panel>
                    <div class="sectionSelection">
                        <asp:Image runat="server" ID="editVenueImage" CssClass="venueImage" />
                        <div class="newSelection">
                                <asp:Repeater runat="server" ID="rptEditSections">
                                    <HeaderTemplate>
                                        <div class="theaterSectionList">
                                            <ul>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <li runat="server" id="radioContainer">
                                            <asp:HiddenField runat="server" ID="sectionId" />
                                            <asp:HiddenField runat="server" ID="venueId" />
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
                    </div>
                </div>
            </div>
            <div class="continueBtn">
                <asp:LinkButton runat="server" ID="btnEditSection" Text="Update Selection" CssClass="btn btnStandOut flexUpdateSelection" />
            </div>
        </div>
    </asp:PlaceHolder>
    <asp:PlaceHolder runat="server" ID="phSoldOutPerformance">
        <div class="subsModalInner">
            <asp:HiddenField runat="server" ID="soldOutVenueId" />        
            <div class="packageDescription">
                <h2>Update Performances</h2>
            </div>
            <div class="subsModalContent select-new-performance">
                <div class="performancesSection">
                    <div class="performancesError">
                        <h3>
                            The performances you have selected have no available sections.
                        </h3>
                        <p>
                            Please select a different date for:
                        </p>
                    </div>
                    <div class="soldOutPerformances">
                        <asp:Repeater runat="server" ID="rptSoldOutPerformances">
                            <HeaderTemplate>
                                <table border="0" cellspacing="0" class="packageCart">
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:Literal runat="server" ID="ltrPerformanceName" />
                                    </td>
                                    <td>
                                        <asp:Literal runat="server" ID="ltrPerformanceTime" />
                                    </td>
                                    <td>
                                        <asp:Literal runat="server" ID="ltrVenue" />
                                    </td>
                                    <td>
                                        <asp:Literal runat="server" ID="ltrPriceTypes" />
                                    </td>
                                    <td>
                                        <asp:Literal runat="server" ID="ltrSection" />
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
                    <div class="newSelection">
                        <asp:Repeater runat="server" ID="rptSoldOutPerformancesSelection">
                            <ItemTemplate>
                                <div class="failedPerformanceRow">
                                    <asp:HiddenField runat="server" ID="soldOutPerformanceId" />                                    
                                    <h3>
                                        <asp:Literal ID="ltrPerformanceName" runat="server" />
                                    </h3>
                                    <div class="date">
                                        <asp:ListBox runat="server" ID="lbxAlternatePerformances" DataTextField="PerformanceDate"
                                            CssClass="" DataValueField="PerformanceId" DataTextFormatString="{0:dddd, MMM d, h:mmtt}" />
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
            <div class="continueBtn">
                <asp:LinkButton runat="server" ID="lbUpdateSoldOut" Text="Update Performances" CssClass="btn btnStandOut flexUpdateSelection" />
            </div>
        </div>
    </asp:PlaceHolder>
</asp:Panel>
