<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ResetPasswordControl.ascx.cs"
    Inherits="TheaterTemplate.Web.Controls.AccountControls.ResetPasswordControl" %>
<div id="resetPasswordControl">
    <EPiServer:Property runat="server" ID="MissingInformation" PropertyName="MainBody"
        Visible="false"></EPiServer:Property>
    <EPiServer:Property runat="server" ID="LoginFailed" PropertyName="Intro"
        Visible="false"></EPiServer:Property>
</div>
