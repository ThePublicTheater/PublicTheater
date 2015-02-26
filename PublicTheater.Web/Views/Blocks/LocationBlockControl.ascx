<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LocationBlockControl.ascx.cs"
    Inherits="PublicTheater.Web.Views.Blocks.LocationBlockControl" %>
<asp:panel runat="server" id="pnlLargeView" visible="false">
    <div class="locationList">
        <div class="large-8 medium-12">
            <div class="locationDetails">
                <EPiServer:Property ID="Property1" runat="server" PageLink="<%# CurrentData.LocationPage %>"
                    PropertyName="Location" CustomTagName="h3">
                </EPiServer:Property>
                <h4>
                    <%= CurrentLocationPage.PageName %>
                </h4>
                <EPiServer:Property runat="server" PageLink="<%# CurrentData.LocationPage %>" PropertyName="Address">
                </EPiServer:Property>
            </div>
        </div>
        <div class="large-4 medium-12">
            <nav class="subNav">
                <ul>
                    <li><a href="<%= CurrentLocationPage.DirectionLink %>" target="_blank">Directions</a></li>
                    <li><a href="<%= CurrentLocationPage.MapLink %>" target="_blank">View in Google Maps</a></li>
                </ul>
            </nav>
        </div>
        <div class="large-8 medium-12">
            <a href="<%= CurrentLocationPage.MapLink %>" target="_blank">
                <img src="<%= CurrentLocationPage.MapImageLarge %>" alt=""></a>
        </div>
        <div class="large-4 medium-12">
            <a href="<%= CurrentLocationPage.DirectionLink %>" target="_blank">
                <img src="<%= CurrentLocationPage.MapImageSmall %>" alt=""></a>
        </div>
    </div>
</asp:panel>
<asp:panel runat="server" id="pnlHalfView" visible="false">
    <div class="locationList">
        <a href="<%= CurrentLocationPage.MapLink %>" target="_blank">
            <img src="<%= CurrentLocationPage.MapImageLarge %>" alt="">
        </a>
        <div class="locationDetails">
            <%= CurrentLocationPage.Location %>
            <h4>
                <%= CurrentLocationPage.PageName %>
            </h4>
            <EPiServer:Property ID="Property2" runat="server" PageLink="<%# CurrentData.LocationPage %>"
                PropertyName="Address">
            </EPiServer:Property>
        </div>
        <p>
            <%= CurrentLocationPage.MainBody %></p>
    </div>
</asp:panel>
