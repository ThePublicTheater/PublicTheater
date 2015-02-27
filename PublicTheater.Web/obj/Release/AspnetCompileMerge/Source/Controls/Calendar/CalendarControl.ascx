<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="CalendarControl.ascx.cs"
    Inherits="PublicTheater.Web.Controls.Calendar.CalendarControl" %>

<div class="calendarMonthDisplay">
    <div class="calendarMonthSelector row-fluid">
        <div class="span12">

            <asp:LinkButton runat="server" ID="lbBack" CssClass="changeMonthPrev" />
            <h2>
                <asp:Literal runat="server" ID="ltrMonth" /></h2>
            <asp:LinkButton runat="server" ID="lbForward" CssClass="changeMonthNext" />
        </div>
    </div>
</div>


<asp:Panel ID="pnlCalendarFilter" CssClass="calFilters" runat="server">

    <asp:HiddenField ID="SelectedTheme" runat="server" />
        <div class="loadingCal">
        <img class="loading-image" src="/views/Theater/img/ajax-loader.gif" alt="Loading..." />
    </div>
    <div class="hideTheWrapperCal" style="display: none">
        <asp:Repeater ID="rptVenues" runat="server">
            <HeaderTemplate>
                <ul class="venueFilters">
                    <li class="active selected">
                        <a href="#" class="allVenues">All Venues</a>
                    </li>
            </HeaderTemplate>
            <ItemTemplate>
                <li>
                    <asp:HyperLink ID="lnkVenueFilter" runat="server" href="#" />
                </li>
            </ItemTemplate>
            <FooterTemplate>
                </ul>
            </FooterTemplate>
        </asp:Repeater>
    </div>
</asp:Panel>

<table border="0" cellspacing="0" id="calendarTable" class="table">
    <thead>
        <tr>
            <th>Sun
            </th>
            <th>Mon
            </th>
            <th>Tue
            </th>
            <th>Wed
            </th>
            <th>Thu
            </th>
            <th>Fri
            </th>
            <th>Sat
            </th>
        </tr>
    </thead>
    <tbody>
        <asp:Repeater runat="server" ID="rptCalendarWeeks">
            <ItemTemplate>
                <tr>
                    <asp:Repeater ID="rptCalendarDays" runat="server">
                        <ItemTemplate>
                            <td runat="server" id="calendarCell">
                                <div class="calendarCellContent">
                                    <span class="day">
                                        <asp:Literal runat="server" ID="ltrDay" />
                                    </span>
                                    <asp:Repeater runat="server" ID="rptDayVenues">
                                        <ItemTemplate>
                                            <asp:Panel ID="pnlVenueFilter" runat="server" CssClass="venueRow">
                                                <span class="venueName">
                                                    <asp:Literal ID="ltrThemeName" runat="server"></asp:Literal>
                                                </span>
                                                <asp:Repeater runat="server" ID="rptDayPerformances">
                                                    <ItemTemplate>
                                                        <div id="colorBox" class="performanceRow" data-bb="performancePopover" runat="server">
                                                            <span data-bb="popoverLink">
                                                                <asp:HyperLink runat="server" ID="lnkPopUp" CssClass="toolTipLink">
                                                                    <asp:Label runat="server" ID="lblPerformanceTitle" />
                                                                    <span class="performanceTime">
                                                                        <asp:Label runat="server" ID="lblPerformanceTime" AssociatedControlID="lnkPopUp" />
                                                                    </span>
                                                                </asp:HyperLink>
                                                            </span>
                                                            <div class="performanceToolTip" data-bb="popoverContent">
                                                                <asp:Panel runat="server" ID="pnlImage">
                                                                    <%--src="http://placehold.it/350x115"--%>
                                                                    <asp:Image runat="server" ID="imgPerformance" />
                                                                </asp:Panel>
                                                                <p class="lead">
                                                                    <asp:Literal runat="server" ID="ltrPerformanceTitle" />
                                                                </p>
                                                                <span>
                                                                    <asp:Literal runat="server" ID="ltrPerformanceDate" />
                                                                </span>
                                                                <span>
                                                                    <asp:Literal runat="server" ID="ltrProductionDetails" />
                                                                </span>
                                                                <hr />
                                                                <div class="buttons">
                                                                    <asp:HyperLink runat="server" ID="lnkBuyTicket" CssClass="btn" Text="Buy Tickets" />
                                                                    <asp:HyperLink runat="server" ID="lnkViewDetails" CssClass="btn" Text="View Details" />
                                                                </div>
                                                            </div>
                                                            <div runat="server" data-replace="friendsCharm" id="gncFriendsCharm"></div>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </asp:Panel>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </td>
                        </ItemTemplate>
                    </asp:Repeater>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </tbody>
</table>
