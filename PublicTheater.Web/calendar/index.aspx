<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPages/MasterPage.Master" AutoEventWireup="true" 
    CodeBehind="index.aspx.cs" Inherits="TheaterTemplate.Web.Pages.Calendar.index" MaintainScrollPositionOnPostback="true" %>
<%@ Register TagPrefix="calendar" TagName="CalendarContainer" Src="~/Controls/Calendar/CalendarContainer.ascx" %>
<%@ Register TagPrefix="calendar" TagName="CalendarMobile" Src="~/Controls/Calendar/CalendarMobile.ascx" %>
<%@ Register TagPrefix="uc" Namespace="PublicTheater.Web.Views.Controls" Assembly="PublicTheater.Web" %>

<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="beforeWrapper" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" runat="server">

    <uc:HeroImageControl runat="server" ID="HeroImg1"></uc:HeroImageControl>

    <div id="calendarPage" data-bb="calendar">

        <div class="row-fluid">
            <div class="span12">
                <div class="visible-desktop visible-tablet">
                    <calendar:CalendarContainer runat="server" ID="calendarContainer" />
                </div>
            </div>
        </div>

    </div>

</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="BeforeCloseBody" runat="server">
</asp:Content>
