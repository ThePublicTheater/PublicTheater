<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PDPTicketBlockControl.ascx.cs"
    Inherits="PublicTheater.Web.Views.Blocks.PDPTicketBlockControl" %>

<EPiServer:Property ID="propNotAvail" runat="server" PropertyName="TicketNotAvailableMessage" Visible="False" CssClass="ticketExpanderText"/>
<EPiServer:Property ID="propSoldOut" runat="server" PropertyName="SoldOutMessage" Visible="False" CssClass="ticketExpanderText" />

<a runat="server" id="tixLink" class="btn buyTixBtn" data-bb-control="calendarPerformanceSelector"
    data-bb-control-style="inline" visible='True'>Buy Tickets</a>
<div runat="server" id="popupData" data-bb="popupData" style="display: none;">
</div>
