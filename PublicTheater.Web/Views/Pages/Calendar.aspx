<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPages/MasterPage.master"
    AutoEventWireup="true" CodeBehind="Calendar.aspx.cs" Inherits="PublicTheater.Web.Views.Pages.Calendar" %>

<%@ Register TagPrefix="calendar" TagName="CalendarContainer" Src="~/Controls/Calendar/CalendarContainer.ascx" %>
<%@ Register TagPrefix="calendar" TagName="CalendarMobile" Src="~/Controls/Calendar/CalendarMobile.ascx" %>
<%@ Register TagPrefix="uc" TagName="HeroImageControl" Src="~/Views/Controls/HeroImageControl.ascx" %>
<asp:content id="Content3" contentplaceholderid="Head" runat="server">
</asp:content>
<asp:content id="Content4" contentplaceholderid="beforeWrapper" runat="server">
</asp:content>
<asp:content id="Content5" contentplaceholderid="MainContent" runat="server">
    <uc:HeroImageControl runat="server" ID="HeroImg1">
    </uc:HeroImageControl>
    <div id="calendarPage" data-bb="calendar">
        <div class="row-fluid">
            <div class="span12">
                <div class="visible-desktop visible-tablet">
                    <calendar:CalendarContainer runat="server" ID="calendarContainer" />
                </div>
            </div>
        </div>
    </div>
</asp:content>
<asp:content id="Content6" contentplaceholderid="BeforeCloseBody" runat="server">
</asp:content>
