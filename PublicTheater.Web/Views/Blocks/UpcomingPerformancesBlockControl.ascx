<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UpcomingPerformancesBlockControl.ascx.cs"
    Inherits="PublicTheater.Web.Views.Blocks.UpcomingPerformancesBlockControl" %>
<div class="block noHeight ticketList">
    <div class="head">
        <h2>
            <asp:literal id="ltrHeader" runat="server" /></h2>
        <a href="/Tickets">Full Calendar</a>
    </div>
    <asp:repeater id="rptDayList" runat="server">
        <itemtemplate>
            <div class="calendarListSection">
                <h3>
                    <asp:Literal ID="ltrDayName" runat="server" /></h3>
                <ul>
                    <asp:Repeater ID="rptPerformanceList" runat="server">
                        <ItemTemplate>
                            <li>
                                <asp:HyperLink ID="lnkPDP" runat="server">
                                <div class="large-5 medium-4 small-12">
                                        <asp:Image ID="performanceImage" runat="server" />
                                </div>
                                <div class="large-5 medium-6 small-12">
                                        <div class="calendarListCopy">
                                            <h4>
                                                <asp:Literal ID="ltrPerformanceTitle" runat="server" /></h4>
                                            <span>
                                                <asp:Literal ID="ltrPerformanceTime" runat="server" /></span>
                                        </div>
                                </div>
                                </asp:HyperLink>
                                <div class="large-2 medium-2 small-12">
                                    <asp:HyperLink ID="lnkBuyTickets" runat="server" CssClass="btn">Tickets</asp:HyperLink>
                                </div>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
        </itemtemplate>
    </asp:repeater>
    <asp:panel runat="server" id="litNoPerformance" visible="False">
        <ul class="calendarListSection">
            <li>No Performance Available </li>
        </ul>
    </asp:panel>
</div>
