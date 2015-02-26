<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TextBlockControl.ascx.cs" Inherits="PublicTheater.Web.Views.Blocks.TextBlockControl" %>
<div class="block <%= CurrentBlock.HeaderStyle ? "textBlockHeaderStyle" : "" %> <%= CurrentBlock.Property["CssClassOverride"].ToString() ?? "" %>">
<EPiServer:Property runat="server" PropertyName="Text"></EPiServer:Property>
    </div>