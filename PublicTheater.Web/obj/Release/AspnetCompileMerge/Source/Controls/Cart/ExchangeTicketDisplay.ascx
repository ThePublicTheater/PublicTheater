<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ExchangeTicketDisplay.ascx.cs" Inherits="TheaterTemplate.Web.Controls.CartControls.ExchangeTicketDisplay" %>
<div class="packageArea">
    <div class="minicart">
    <!--new tickets -->
        <asp:Panel runat="server" ID="pnlHeader" CssClass="singleTicketWrapper">
            <div class="preformaceName">
                <%-- IF NOT FIRST LINE THIS HEADER LINE IS REMOVED - SEE /js/common.js--%>
                <span class="headerLine">
                    <asp:Literal runat="server" ID="ltrPerformanceNameHeader" />
                </span>
                <div class="actionBtns">
                    <asp:LinkButton runat="server" ID="lbRemove" Text="X" CommandName="Remove" CssClass="remove"
                        ToolTip="Remove this from your cart" />
                    <asp:Literal runat="server" ID="ltrPerformanceDescription" />
                </div>
            </div>
            <div class="date">
                <span class="headerLine">
                    <asp:Literal runat="server" ID="ltrPerformanceTimeHeader" />
                </span>
                <asp:Literal runat="server" ID="ltrPerformanceTime" />
            </div>
            <div class="venue">
                <span class="headerLine">
                    <asp:Literal runat="server" ID="ltrVenueHeader" />
                </span>
                <asp:Literal runat="server" ID="ltrVenue" />
            </div>

            <div class="ticketGroup">
                <span class="headerLine">
                    <asp:Literal runat="server" ID="ltrPriceTypeHeader" />
                </span>
                <span class="headerLine">
                    <asp:Literal runat="server" ID="ltrSectionHeader" />
                </span>
            </div>
            <asp:Repeater runat="server" ID="groupedTickets">
                <ItemTemplate>
                    <div class="ticketGroup">
                        <div class="unitTix">
                            <asp:Literal runat="server" ID="ltrPriceTypes" />
                        </div>
                        <div class="seating">
                            <asp:Literal runat="server" ID="ltrSection" />
                        </div>
                    </div>
                </ItemTemplate>            
            </asp:Repeater>
            <input type="hidden" runat="server" id="lid" class="lid" />
        </asp:Panel>
        <!-- add on tickets -->
        <asp:Panel ID="pnlAddOnTickets" runat="server" CssClass="exchangeAddOnWrapper">
            <div class="preformaceName">
                <%-- IF NOT FIRST LINE THIS HEADER LINE IS REMOVED - SEE /js/common.js--%>
                <span class="headerLine">
                    <asp:Literal runat="server" ID="ltrAddOnPerfHeader" />
                </span>
                <div class="actionBtns">
                   <%-- <asp:LinkButton runat="server" ID="addOnRemove" Text="X" CommandName="Remove" CssClass="remove"
                        ToolTip="Remove this from your cart" />--%>
                    <asp:Literal runat="server" ID="ltrAddOnPerformanceDescription" Visible="false"/>
                </div>
            </div>
            <div class="date">
                <span class="headerLine">
                    <asp:Literal runat="server" ID="ltrAddOnDateHeader" Visible="false" />
                </span>
                <asp:Literal runat="server" ID="ltrAddOnPerfDate" Visible="false" />
            </div>
            <div class="venue">
                <span class="headerLine">
                    <asp:Literal runat="server" ID="ltrAddOnVenueHeader" Visible="false" />
                </span>
                <asp:Literal runat="server" ID="ltrAddOnVenue" Visible="false" />
            </div>

            <div class="ticketGroup">
                <span class="headerLine">
                    <asp:Literal runat="server" ID="ltrAddOnPriceTypeHeader" Visible="false" />
                </span>
                <span class="headerLine">
                    <asp:Literal runat="server" ID="ltrAddOnSectionHeader" Visible="false" />
                </span>
            </div>
            <asp:Repeater runat="server" ID="rptAddOnGroupedTickets">
                <ItemTemplate>
                    <div class="ticketGroup">
                        <div class="unitTix">
                            <asp:Literal runat="server" ID="ltrPriceTypes" />
                        </div>
                        <div class="seating">
                            <asp:Literal runat="server" ID="ltrSection" />
                        </div>
                    </div>
                </ItemTemplate>            
            </asp:Repeater>
            <input type="hidden" runat="server" id="Hidden1" class="lid" />
        </asp:Panel>

        <!-- Returned tickets -->
        <asp:Panel runat="server" ID="returnedSeatsPnl" CssClass="exchangeTicketWrapper">
            <div class="preformaceName">
                <span class="headerLine">
                    <asp:Literal ID="ltrReturnedPerformanceHeader" runat="server" />
                </span>
                <span class="strike"><asp:Literal runat="server" ID="ltrReturnedPerformanceName" /></span>
            </div>
           <div class="date returnedSeats">
                <span class="headerLine">
                    <asp:Literal runat="server" ID="ltrReturnedPerformanceTimeHeader" Visible="false"/>
                </span>
               
                <span class="strike"><asp:Literal runat="server" ID="ltrReturnedPerformanceTime" /> </span>
            </div>
            <div class="venue returnedSeats">
                <span class="headerLine">
                    <asp:Literal runat="server" ID="ltrReturnedVenueHeader" Visible="false" />
                </span>
                <span class="strike"><asp:Literal runat="server" ID="ltrReturnedVenue" /> </span>

            </div>


            <div class="ticketGroup returnedSeats">
                <span class="headerLine">
                    <asp:Literal runat="server" ID="ltrReturnedPriceTypeHeader" Visible="false" />
                </span>
                <span class="strike"><asp:Literal ID="ltrReturnedPriceTypes" runat="server" /></span>
                <span class="headerLine">
                    <asp:Literal runat="server" ID="ltrReturnedSectionHeader" Visible="false" />
                </span>
                <span class="strike"><asp:Literal runat="server" ID="ltrReturnedSection" /> </span>
            </div>


            <asp:Repeater runat="server" ID="returnedGroupedTickets">
                <ItemTemplate>
                    <div class="ticketGroup returnedSeats">
                        <div class="unitTix">
                            <span class="strike"><asp:Literal runat="server" ID="ltrPriceTypes" /> </span>
                        </div>
                        <div class="seating">
                            <span class="strike"><asp:Literal runat="server" ID="ltrSection" /> </span>
                        </div>
                    </div>
                </ItemTemplate>            
            </asp:Repeater>
        </asp:Panel>
    </div>
</div>
