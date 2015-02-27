<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TicketHistoryControl.ascx.cs"
    Inherits="PublicTheater.Web.Controls.Account.TicketHistoryControl" %>
<div class="formHeader accountPages">
    <div class="large-12 medium-12 small-12">
        <EPiServer:Property runat="server" ID="HistoryHeader" PropertyName="HistoryHeader"
            DisplayMissingMessage="false" />
        <div class="ticketHistoryBody">
            <EPiServer:Property runat="server" ID="HistoryBody" PropertyName="HistoryBody" DisplayMissingMessage="false" />
        </div>
    </div>
</div>
<div id="ticketHistoryDisplay">
    <div class="large-12 medium-12 small-12">
        <div data-replace="friendsStatus">
        </div>
    </div>
    
    
    <ul class="ticketChart">
        <li>
            <EPiServer:Property runat="server" ID="NoTicketHistory" PropertyName="NoTicketHistory"
                DisplayMissingMessage="false" Visible="false" />
            <EPiServer:Property runat="server" ID="PerformanceAreaHeader" PropertyName="PerformanceAreaHeader"
                DisplayMissingMessage="false" />
        </li>
    </ul>
    <asp:repeater runat="server" id="rptTicketHistory">
        <headertemplate>
            <h2>Upcoming Performances</h2>
            <ul class="ticketChart">
        </headertemplate>
        <itemtemplate>
            <li>
                <div class ="large-3 medium-4 small-12">
                    <span class="imageHolder">
                        <asp:Image runat="server" ID="imgPerformance" CssClass="image-performance" />
                    </span>
                </div>

                <div class ="large-9 medium-8 small-12">
                    <h3>
                        <asp:Literal runat="server" ID="ltrPerformanceTitle" />
                        <asp:HyperLink runat="server" ID="lnkPerformanceTitle" />
                    </h3>
                    <div runat="server" id="gncFriendsReservation" data-replace="friendsReservation"></div>
                    <div runat="server" id="gncFriendsEvent" data-replace="friendsEvent"></div>

                    <asp:Label runat="server" ID="lblPerformanceDate" CssClass="subLabel"/>
                    <asp:Label runat="server" ID="lblVenue" CssClass=""/>
                    <asp:Repeater runat="server" ID="rptSeats">
                        <HeaderTemplate>
                            <ul class="tableChart">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <li>
                                <asp:Label runat="server" ID="lblSection" />
                                <asp:Label runat="server" ID="lblSeat" CssClass="seatNumber" />
                            </li>
                        </ItemTemplate>
                        <FooterTemplate>
                            </ul>
                        </FooterTemplate>
                    </asp:Repeater>

                    <asp:PlaceHolder runat="server" ID="phGenAdmin" Visible="false">
                        <span class="genSeat">
                            <asp:Literal runat="server" ID="ltrGenAdminSeats" />
                            <asp:Literal runat="server" ID="ltrHiddenSeasonSeating" />
                        </span>
                    </asp:PlaceHolder>
                        
                    <asp:LinkButton runat="server" ID="printTickets" Text="Print Tickets" Visible="false" CssClass="printBtn"/>
                    <asp:LinkButton runat="server" ID="exchangeTickets" Text="Exchange" Visible="false" OnClick="ExchangeTickets_Click" CssClass="exchangeBtn"/>                   
                </div>

            </li>
        </itemtemplate>
        <footertemplate>
            </ul>
        </footertemplate>
    </asp:repeater>
    
    <asp:repeater runat="server" id="rptPastTicketHistory">
        <headertemplate>
            <h2>Past Performances</h2>

            <asp:HiddenField runat="server" ID="itemPerPage" Value="3" />
            <ul class="ticketChart pastItems">
        </headertemplate>
        <itemtemplate>
            <li>
                <div class ="large-3 medium-4 small-12">
                    <span class="imageHolder">
                        <asp:Image runat="server" ID="imgPerformance" CssClass="image-performance" />
                    </span>
                </div>
                <div class ="large-9 medium-8 small-12">
                    <h3>
                        <asp:Literal runat="server" ID="ltrPerformanceTitle" />
                        <asp:HyperLink runat="server" ID="lnkPerformanceTitle" Visible="False" />
                    </h3>
                    <div runat="server" id="gncFriendsReservation" data-replace="friendsReservation"></div>
                    <div runat="server" id="gncFriendsEvent" data-replace="friendsEvent"></div>

                    <asp:Label runat="server" ID="lblPerformanceDate" CssClass="subLabel"/>
                    <asp:Label runat="server" ID="lblVenue" CssClass=""/>
                    <asp:Repeater runat="server" ID="rptSeats">
                        <HeaderTemplate>
                            <ul class="tableChart">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <li>
                                <asp:Label runat="server" ID="lblSection" />
                                <asp:Label runat="server" ID="lblSeat" CssClass="seatNumber" />
                            </li>
                        </ItemTemplate>
                        <FooterTemplate>
                            </ul>
                        </FooterTemplate>
                    </asp:Repeater>

                    <asp:PlaceHolder runat="server" ID="phGenAdmin" Visible="false">
                        <span class="genSeat">
                            <asp:Literal runat="server" ID="ltrGenAdminSeats" />
                            <asp:Literal runat="server" ID="ltrHiddenSeasonSeating" />
                        </span>
                    </asp:PlaceHolder>
                        
                    <asp:LinkButton runat="server" ID="printTickets" Text="Print Tickets" Visible="false" CssClass="printBtn"/>
                    <asp:LinkButton runat="server" ID="exchangeTickets" Text="Exchange" Visible="false" OnClick="ExchangeTickets_Click" CssClass="exchangeBtn"/>                   
                </div>
            </li>
        </itemtemplate>
        <footertemplate>
            </ul>
        </footertemplate>
    </asp:repeater>
  
    <%--  <div class="alsoInterested">
        <h2>You May Also Be Interested In...</h2>
        <ul class="ticketChart">
        <li>
            <div class ="large-3 medium-4 small-12">
                <span class="imageHolder">
                    <img src="http://placehold.it/274x176" />
                </span>
            </div>
            <div class ="large-9 medium-8 small-12">
                <h3>Show Title</h3>                                  
            </div>
        </li>
    </ul>
    </div>--%>
</div>
<asp:panel runat="server" id="pnlAddOns" visible="false" cssclass="AddOnArea">
    <EPiServer:Property runat="server" ID="AddOnHeader" PropertyName="AddOnHeader" DisplayMissingMessage="false" />
    <asp:repeater runat="server" id="rptAddOns">
        <itemtemplate>
            <div class="ticketBlock">
                <ul>
                    <li>
                        <h3>
                            <asp:Label runat="server" ID="lblAddOnTitle" />
                        </h3>
                    </li>
                    <li>
                        <asp:Label runat="server" ID="lblNumber" CssClass="numberPurchased" />
                    </li>
                </ul>
            </div>
        </itemtemplate>
    </asp:repeater>
</asp:panel>
<div id="ticketHistoryFooter">
    <EPiServer:Property runat="server" ID="HistoryFooter" PropertyName="HistoryFooter"
        DisplayMissingMessage="false" />
</div>
