<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomPropertyValue.ascx.cs" Inherits="TheaterTemplate.Web.Views.Controls.CustomPropertyValue" %>
<%= OutputText %><EPiServer:Property runat="server" ID="currentEpiProperty" Editable="True" PropertyName="CustomPropertyValues" Visible="false" />
<asp:HiddenField runat="server" ID="hfPropertyName" Visible="false"/>