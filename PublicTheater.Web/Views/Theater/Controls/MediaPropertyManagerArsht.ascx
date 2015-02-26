<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MediaPropertyManagerArsht.ascx.cs" Inherits="PublicTheater.Web.Views.Theater.Controls.MediaPropertyManagerArsht" %>
<%@ Register TagPrefix="uc" TagName="MediaInputReference" Src="~/Views/Theater/Controls/MediaInputReference.ascx" %>

<%@ Register TagPrefix="uc" Namespace="PublicTheater.Web.Views.Theater.Controls" Assembly="PublicTheater.Web" %>

<EPiServer:FileSystemDataSource runat="server" ID="FileDataSource" IncludeRoot="true"
    Root="/Global" />
<script src="/Views/Theater/js/MediaPropertyScripts.js" type="text/javascript"></script>

<asp:HiddenField runat="server" ID="HfImageGuid" />


<div class="fields firstCol">
    <div class="mediaTitle">
        <label>
            Title:</label>
        <asp:TextBox ID="mediaTitle" runat="server" title="media title" />
    </div>
    <div class="mediaCredits">
        <label>
            Credits:</label>
        <asp:TextBox ID="mediaCredits" runat="server" />
    </div>
    <div class="mediaDate">
        <label>
            Date:</label>
        <asp:TextBox ID="mediaDate" runat="server" title="date" />
    </div>
    <div class="mediaDescription">
        <label>
            Description:</label>
        <asp:TextBox ID="mediaDescription" runat="server" TextMode="Multiline" Rows="3" />
    </div>
    <div runat="server" id="divThumbnail" visible="True" class="mediaThumbnail">
        <label>
            Thumbnail Path:</label>
             <%--<asp:TextBox ID="mediaThumbnail" runat="server" />--%>
        <uc:EPiServerFileBrowser runat="server" ID="mediaThumbnailFile"></uc:EPiServerFileBrowser>
    </div>
</div>


<div class="fields">
    <div class="performances">
        <uc:MediaInputReference ID="ucPerformances" runat="server" MediaText="Performances:" />
    </div>
</div>


<div class="fields">
    <div class="artists">
        <uc:MediaInputReference ID="ucArtists" runat="server" MediaText="Artists:" />
    </div>
</div>

<div class="fields">
    <div class="events">
        <uc:MediaInputReference ID="ucEvents" runat="server" MediaText="Events:" />
    </div>
</div>

<div class="fields">
    <div class="subscriptions">
        <uc:MediaInputReference ID="ucSubscriptions" runat="server" MediaText="Subscriptions:" />
    </div>
</div>
