<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TicketBlockControl.ascx.cs"
    Inherits="PublicTheater.Web.Views.Blocks.TicketBlockControl" %>
<div class="small-12 medium-6 large-3 columns height-Single">
    <asp:panel runat="server" id="pnlLargeView" cssclass="block calendar">
        <div class="eventDetails">
            <span class="title">
                <%= CurrentData.Name %>
            </span><span class="venue">
                <%= CurrentData.VenueName %></span> <span class="date">
                    <%= CurrentData.StartEndDateShort%></span>
            <div class="popupWrapper">
                <EPiServer:Property ID="propNotAvail" runat="server" PropertyName="TicketNotAvailableMessage" Visible="False"/>
                <EPiServer:Property ID="propSoldOut" runat="server" PropertyName="SoldOutMessage" Visible="False" CssClass=" buyTixBtn"/>
                <a class="btn" data-bb-control="calendarPerformanceSelector" runat="server" id="tixLink" >
                    Tickets</a>
                <div runat="server" id="popupData" data-bb="popupData" style="display: none;">
                </div>
            </div>
        </div>
        <img title="The Library" src='<%= CurrentData.Thumbnail150x75 %>' alt='<%= CurrentData.Heading %>'>
    </asp:panel>
</div>
