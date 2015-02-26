<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ExchangeControl.ascx.cs"
    Inherits="TheaterTemplate.Web.Controls.AccountControls.ExchangeControl" %>
<%@ Register Assembly="TheaterTemplate.Shared" Namespace="TheaterTemplate.Shared.WebControls"
    TagPrefix="tts" %>
<%@ Register TagPrefix="uc" TagName="HtmlSyosControl" Src="~/Controls/reserve/HtmlSyosControl.ascx" %>
<%@ Register TagPrefix="uc" TagName="BestAvailable" Src="~/Controls/reserve/BestAvailable.ascx" %>
<div id="genericHeader">
    <div class="genericDescription">
        <EPiServer:Property runat="server" ID="HistoryHeader" PropertyName="HistoryHeader"
            DisplayMissingMessage="false" />
    </div>
</div>
<div class="ticketHistoryBody">
    <EPiServer:Property runat="server" ID="HistoryBody" PropertyName="HistoryBody" DisplayMissingMessage="false" />
</div>
<asp:Panel runat="server" ID="ticketSelection">
    <div id="ticketHistoryDisplay">
        <ul>
            <li>
                <asp:Image runat="server" ID="imgPerformance" />
                <h3>
                    <asp:Literal runat="server" ID="ltrPerformanceTitle" />
                </h3>
                <asp:Label runat="server" ID="lblPerformanceDate" />
                <asp:Label runat="server" ID="lblVenue" />
                <asp:Repeater runat="server" ID="groupedReturnSeats">
                    <HeaderTemplate>
                        <ul>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <li>
                            <asp:Repeater runat="server" ID="returnSeats">
                                <HeaderTemplate>
                                    <ul>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <li>
                                        <asp:HiddenField runat="server" ID="ticketNumber" />
                                        <asp:CheckBox runat="server" ID="exchangeTicketNumber" />
                                        -
                                        <asp:Label runat="server" ID="lblSeat" CssClass="seatNumber" AssociatedControlID="exchangeTicketNumber" />
                                    </li>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </ul>
                                </FooterTemplate>
                            </asp:Repeater>
                        </li>
                    </ItemTemplate>
                    <FooterTemplate>
                        </ul>
                    </FooterTemplate>
                </asp:Repeater>
            </li>
        </ul>
        <asp:Label runat="server" ID="errorSelectSeats" Visible="false" CssClass="error"
            Text="At least one ticket must be selected" />
    </div>
    <div id="newPerformance">
        <EPiServer:Property runat="server" ID="PerformanceAreaHeader" PropertyName="PerformanceAreaHeader"
            DisplayMissingMessage="false" />
        <asp:Repeater runat="server" ID="exchangePerformances">
            <HeaderTemplate>
                <ul>
            </HeaderTemplate>
            <ItemTemplate>
                <li>
                    <tts:GroupRadioButton runat="server" ID="performanceTitle" GroupName="exchangePerformanceId"
                        OnCheckedChanged="performanceTitle_CheckedChanged" />
                    -
                    <asp:Literal runat="server" ID="performanceDate" />
                </li>
            </ItemTemplate>
            <FooterTemplate>
                </ul>
            </FooterTemplate>
        </asp:Repeater>
        <asp:Label runat="server" ID="errorSelectPerformance" Visible="false" CssClass="error"
            Text="A new performance must be selected" />
    </div>
    <div id="exchangeButton">
        <asp:Button runat="server" ID="exchangeTickets" Text="Exchange" />
    </div>
    <input type="hidden" runat="server" id="exchangeTicketsCount" class="exchangeTicketsCount" />
</asp:Panel>
<asp:Panel runat="server" ID="exchangeNewTickets" Visible="false">
    <div id="newPerformanceDetails">
        <uc:BestAvailable runat="server" id="newBestAvailable" />
    </div>
</asp:Panel>
<div id="ticketHistoryFooter">
    <EPiServer:Property runat="server" ID="ExchangeFooter" PropertyName="ExchangeFooter"
        DisplayMissingMessage="false" />
</div>
