<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SingleTicketDisplay.ascx.cs"
    Inherits="PublicTheater.Web.Controls.Cart.SingleTicketDisplay" %>


<div class="packageArea">
    <div class="miniCart">
        <asp:Panel runat="server" ID="pnlHeader" CssClass="singleTicketWrapper">
            <div class="packageCart">
                
                <ul class="cartHead" runat="server" id="ulCartHeader"> <!-- can we get rid of this?-->
                    <li><asp:Literal runat="server" ID="ltrPerformanceNameHeader" /><%--Production--%></li>
                    <li><asp:Literal runat="server" ID="ltrPerformanceTimeHeader" /><%--Date--%></li>
                    <li><asp:Literal runat="server" ID="ltrVenueHeader" /><%--Venue--%></li>
                    <li><asp:Literal runat="server" ID="ltrPriceTypeHeader" /><%--Prices & Seating--%></li>
                    <li><asp:Literal runat="server" ID="ltrSectionHeader" /></li><!-- can we get rid of this?-->
                </ul> 
            
                <ul class="cartBody perfRow">
                    <li>
                        <asp:LinkButton runat="server" ID="lbRemove" Text="X" CommandName="Remove" CssClass="remove lTeal" ToolTip="Remove this from your cart" />
                        <asp:Literal runat="server" ID="ltrPerformanceDescription" />
                    </li>
                    <li><asp:Literal runat="server" ID="ltrPerformanceTime" /></li>
                    <li>
                        <span runat="server" id="venueColumn">
                            <asp:Literal runat="server" ID="ltrVenue" />
                        </span>
                    </li>
                    <li class="priceAndSeats">
                        <asp:Repeater runat="server" ID="groupedTickets">
                            <HeaderTemplate>
                                <ul>
                            </HeaderTemplate>
                            <ItemTemplate> 
                                <li>
                                    <span class="priceTickets">
                                        <asp:Literal runat="server" ID="ltrPriceTypes" />
                                    </span>
                                    <span runat="server" id="sectionCell" class="seatingTickets">
                                        <asp:Literal runat="server" ID="ltrSection" />
                                    </span>
                                </li>
                            </ItemTemplate>
                            <FooterTemplate>
                                </ul>
                            </FooterTemplate>
                        </asp:Repeater>
                    </li>
                    
                </ul>
    
            </div>
            <input type="hidden" runat="server" id="lid" class="lid" />
        </asp:Panel>
    </div>
</div>






