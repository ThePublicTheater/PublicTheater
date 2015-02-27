<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PopUpDetails.ascx.cs" Inherits="TheaterTemplate.Web.Controls.PerformanceControls.PopUpDetails" %>

<asp:LinkButton runat="server" ID="lbClose" Text="X" />
<asp:Literal runat="server" ID="litError" />
<div id="mainContent" runat="server" class="playDetails">
    <asp:Image runat="server" ID="imgImage" />
    <EPiServer:Property ID="propName" runat="server" PropertyName="PageName" CustomTagName="h1" />
    <EPiServer:Property ID="propSynopsis" runat="server" PropertyName="ProductionSynopsis" DisplayMissingMessage="false" />
</div>