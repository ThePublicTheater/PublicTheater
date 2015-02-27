<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CourseListHeader.ascx.cs" Inherits="TheaterTemplate.Web.Controls.CourseControls.CourseListHeader" %>

<EPiServer:Property runat="server" ID="propHeader" PropertyName="CourseListHeader" DisplayMissingMessage="false" />

<div class="generalInformation">
    <EPiServer:Property runat="server" ID="propGeneralInfo" PropertyName="CourseListDescription" DisplayMissingMessage="false" />
</div>