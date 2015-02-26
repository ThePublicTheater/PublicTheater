<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FlexSelectSeating.ascx.cs"
    Inherits="PublicTheater.Web.Controls.Subscriptions.flex.FlexSelectSeating" %>
<%@ Register Src="~/controls/subscriptions/flex/FlexSelectNewSection.ascx" TagPrefix="subscription" TagName="SelectNewSection" %>
<script type="text/javascript" src="/js/lib/jquery-migrate-1.2.1.min.js"></script>
<div id="flexUpdatePanelContainer" class="small-12 medium-12 large-12 columns">
    <asp:UpdatePanel runat="server" ID="pnlSelectSeating">
        <ContentTemplate>
            <%-- MAINTAIN CHANGED SECTION VARIABLES HERE, ALLOWS FOR CORRECT UPDATING OF UPDATE PANEL --%>
            <asp:Button runat="server" ID="btnSectionChanged" Style="display: none" ClientIDMode="Static" />
            <asp:HiddenField runat="server" ID="updatedSectionValue" ClientIDMode="Static" />
            <asp:HiddenField runat="server" ID="updatedVenueValue" ClientIDMode="Static" />
            <asp:Panel CssClass="errorBox" runat="server" ID="pnlError" Visible="false">
                <asp:Literal runat="server" ID="ltrError" />
            </asp:Panel>
            <div id="selectSeatingContainer" class="row-fluid">
                <div id="packageDisplayContainer">
                    <div id="packageDisplay">
                        <div class="packageArea">
                            <div class="subsFlexHeader">
                                <div class="flexGuidelines">
                                    <h2>
                                        <asp:Literal runat="server" ID="ltrPackageInformation" />
                                    </h2>
                                    <div class="flexDescHeader">
                                        <asp:PlaceHolder runat="server" ID="phMinPerformanceContainer" Visible="false">
                                            <span class="plays"><asp:Literal runat="server" ID="ltrAmountPlays" /></span> shows 
                                            <small>(<asp:Literal runat="server" ID="ltrMinPerformances" />)</small>
                                        </asp:PlaceHolder>

                                        </div>
                                </div>
                            </div>

                            <div class="miniCart">
                                <asp:Repeater runat="server" ID="rptPerformances">
                                    <HeaderTemplate>
                                        <table border="0" cellspacing="0" class="packageCart">
                                            <tbody>
                                                <tr class="headerTr">
                                                    <td>
                                                        <div class="headerLine" >
                                                            &nbsp;
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <div class="headerLine">
                                                            Play
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <div class="headerLine">
                                                            Date
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <div class="headerLine">
                                                            Venue
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <div class="headerLine">
                                                            Tickets
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <div class="headerLine">
                                                            Section
                                                        </div>
                                                    </td>
                                                </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr class="perfRow">
                                            <td>
                                                <asp:LinkButton runat="server" ID="lnkEdit" Text="Edit" Visible="false" CommandName="Edit"
                                                    CssClass="edit" />
                                            </td>
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
                        <div class="clearfix flexBtnContainer">
                            <div class="continueBtn pull-right">
                            <asp:LinkButton Text="Add To Cart" runat="server" ID="btnAddToCart" CssClass="btn solid" ClientIDMode="Static"  />
                            </div>
                        </div>
                    </div>
                </div>

                <div id="selectSeatingArea" style="display:none;">
                        <EPiServer:Property runat="server" ID="tessituraHeader" PropertyName="Header" CssClass="packageDescription" />

                    <asp:Repeater runat="server" ID="rptVenue" ClientIDMode="AutoID">
                        <ItemTemplate>
                            <asp:HiddenField runat="server" ID="currentVenueRowId" />
                            <div class="theaterContainer">
                                <div class="theaterLeftColumn">
                                    <h3>
                                        <asp:Literal runat="server" ID="ltrVenueName" Visible="False" />
                                    </h3>
                                    <p id="selectSeatingTag" class="select-seating" runat="server">
                                        Select your seating for:
                                    </p>
                                    <asp:Repeater runat="server" ID="rptVenuePerformances">
                                        <HeaderTemplate>
                                            <ul class="unstyled venue-name">
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <li>
                                                <h3><asp:Literal runat="server" ID="ltrPerformanceName" /></h3>
                                            </li>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </ul>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </div>
                                <div class="theaterSectionSelection">
                                    <div class="soldOutMessaging">
                                        <asp:Label runat="server" ID="lblSoldOut" Text="All sections sold out. Please select another performance." Visible="false"  />
                                        <asp:LinkButton runat="server" ID="lbSoldOut" Text="View Performances" Visible="false" CssClass="btn btnStandOut" />    
                                    </div>                         
                                    <asp:Repeater runat="server" ID="rptSections">
                                        <HeaderTemplate>
                                            <div class="sectionContainer">
                                                <div class="theaterSectionList">
                                                    <ul class="unstyled">
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <li runat="server" id="radioContainer">
                                                <asp:HiddenField runat="server" ID="venueId" />
                                                <asp:HiddenField runat="server" ID="sectionId" />
                                                <label class="radio">
                                                <asp:RadioButton runat="server" ID="rbSection" />
                                                    <asp:Label ID="seatingLabel" runat="server" AssociatedControlID="rbSection">
                                                        <span class="prices">
                                                            <asp:Literal runat="server" ID="ltrSectionPrice" />
                                                        </span>
                                                        <strong>
                                                            <asp:Literal runat="server" ID="ltrSectionName" />
                                                        </strong>
                                                        <asp:Literal runat="server" ID="ltrSectionDescription" />
                                                    </asp:Label>
                                                </label>
                                            </li>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                                    </ul> 
                                                    <small>Prices are per ticket</small> 
                                                    <asp:Literal runat="server" ID="ltrAdditionalVenueInfo" />
                                                </div> 
                                            </div>
                                            <div class="seatingMapContainer">
                                                <asp:Image runat="server" ID="imgTheater" AlternateText="Theater Image" CssClass="seatMapImg" />
                                            </div>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                    <asp:PlaceHolder runat="server" ID="phGeneralAdmin" Visible="false">
                                        <div class="generalAdminDescription">
                                            <asp:Literal runat="server" ID="ltrGenAdminAdditionalInfo" /> 
                                        </div>
                                        <div class="generalAdminPerformances">
                                            <div class="successGeneralAdmin">
                                                <h3>
                                                    Confirmed Performances</h3>
                                                <asp:Repeater runat="server" ID="rptGeneralAdminPerformances">
                                                    <HeaderTemplate>
                                                        <ul class="unstyled">
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <li><strong>
                                                            <asp:Literal runat="server" ID="ltrPerformanceName" /></strong>
                                                            <asp:Literal runat="server" ID="ltrPerformanceTime" /><br />
                                                            <asp:Literal runat="server" ID="ltrVenue" /><br />
                                                            <asp:Literal runat="server" ID="ltrPriceTypes" /><br />
                                                            <asp:Literal runat="server" ID="ltrPrice" /><br />
                                                            <asp:Literal runat="server" ID="ltrSection" /><br />
                                                        </li>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        </ul>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                            </div>
                                            <asp:Panel runat="server" ID="pnlFailedPerformances" CssClass="failedGeneralAdmin"
                                                Visible="false">
                                                <h3>
                                                    We were unable to seat you for the following performance(s). Please select new date(s).</h3>
                                                <asp:Repeater runat="server" ID="rptGeneralAdminSoldOutPerformances">
                                                    <HeaderTemplate>
                                                        <ul>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <li>
                                                            <div>
                                                                <strong>
                                                                    <asp:Literal runat="server" ID="ltrPerformanceName" /></strong>
                                                                <asp:Literal runat="server" ID="ltrPerformanceTime" /><br />
                                                                <asp:Literal runat="server" ID="ltrVenue" />
                                                            </div>
                                                            <div>
                                                                <asp:Literal runat="server" ID="ltrPriceTypes" /><br />
                                                                <asp:Literal runat="server" ID="ltrSection" />
                                                                <asp:HiddenField runat="server" ID="failedPerformanceId" />
                                                                <asp:HiddenField runat="server" ID="originalSectionId" />
                                                            </div>
                                                        </li>
                                                        <li class="availablePerformances">
                                                            <asp:ListBox runat="server" ID="lbxAvailableGeneralAdmin" DataTextField="PerformanceDate"
                                                                DataValueField="PerformanceId" DataTextFormatString="{0:dddd, MMM d, h:mmtt}" />
                                                        </li>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        </ul>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                                <div class="generalAdminUpdate">
                                                    <asp:LinkButton runat="server" ID="lbUpdateGeneralAdmin" Text="Update Selections"
                                                        CssClass="btn btnStandOut generalAdminBtn" CommandName="GenAdminUpdate" />
                                                </div>
                                            </asp:Panel>
                                        </div>
                                    </asp:PlaceHolder>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
            <subscription:SelectNewSection runat="server" ID="subsSelectNewSection" />
            <div class="loadingContainer bgOverlay">
                <asp:Image runat="server" ID="loadingGif" ImageUrl="~/images/ajax-loader.gif" CssClass="loadingSpinner" />            
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
<script type="text/javascript">
    $(document).ready(function () { $(".btn").click(); });
</script>