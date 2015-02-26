<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SpacerBlockControl.ascx.cs" Inherits="PublicTheater.Web.Views.Blocks.SpacerBlockControl" %>
<style type="text/css">
    <% if (!CurrentBlock.ShowLarge)
       { %>
        @media (min-width: 979px) {
        #<%= spacer.ClientID %> {
        
            display: none;
        }
    }

    <% } %>
     <% if (!CurrentBlock.ShowMedium)
       { %>
        @media (max-width: 979px){
        #<%= spacer.ClientID %> {
        
            display: none;
        }
    }

    <% } %>
     <% if (!CurrentBlock.ShowLarge)
       { %>
        @media (max-width: 650px) {
        #<%= spacer.ClientID %> {
        
            display: none;
        }
    }

    <% } %>
     
</style>
<div class="block spacer" runat="server" ID="spacer">
    <EPiServer:Property runat="server" PropertyName="ImageUrl" DisplayMissingMessage="False"></EPiServer:Property>
</div>