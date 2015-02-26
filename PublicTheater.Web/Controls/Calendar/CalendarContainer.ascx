<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CalendarContainer.ascx.cs" Inherits="TheaterTemplate.Web.Controls.CalendarControls.CalendarContainer" %>
<%@ Register TagPrefix="calendar" TagName="CalendarHeader" Src="~/Controls/Calendar/CalendarHeader.ascx" %>
<%@ Register TagPrefix="calendar" TagName="CalendarControl" Src="~/Controls/Calendar/CalendarControl.ascx" %>

<div class="desktopCalendar">
    
    <div id="calendarHeader">
        <calendar:CalendarHeader runat="server" ID="CalendarHeader" />

    </div>
    
    <div class="clearfix">
        <div class="pull-right">
            <div data-replace="friendsStatus"></div>
        </div>
    </div>
    
    <div id="calendarArea">
        <calendar:CalendarControl runat="server" ID="CalendarControl" />
    </div>

</div>

<div class="mobileCalendar" data-bb-control="mobileCalendar">
    Loading the mobile calendar
    <div class="mobileCalendarSpinner"></div> <!-- If we want one. Shows until the mobile calendar renders. -->
</div>

<asp:HiddenField runat="server" ID="storedCurrentMonth" />
