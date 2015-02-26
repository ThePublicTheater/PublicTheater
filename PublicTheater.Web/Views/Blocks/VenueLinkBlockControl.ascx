<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VenueLinkBlockControl.ascx.cs"
    Inherits="PublicTheater.Web.Views.Blocks.VenueLinkBlockControl" %>
<%@ Import Namespace="PublicTheater.Custom.Episerver.Properties" %>
<div class="venues block">
    <EPiServer:Property ID="RatatorHeaderTitle" PropertyName="Heading" runat="server"
        CustomTagName="h2" />
    <asp:repeater runat="server" id="rptVenues">
        <headertemplate>
            <ul>   
        </headertemplate>
        <itemtemplate>
             <li class="large-12 medium-12 small-12">
                 <img src="<%# ((VenueLink)Container.DataItem).ImageUrl %>" />
                 <h3><%# ((VenueLink)Container.DataItem).VenueName %></h3>
                 <h4><%# ((VenueLink)Container.DataItem).Address %></h4>
                 <div class="actionBtns">
                     <a href="<%# ((VenueLink)Container.DataItem).DirectionsUrl %>">Directions</a>
                     <a href="<%# ((VenueLink)Container.DataItem).WhatsOnUrl %>">What's On</a>
                 </div>
             </li>
        </itemtemplate>
        <footertemplate>
            </ul>
        </footertemplate>
    </asp:repeater>
</div>
