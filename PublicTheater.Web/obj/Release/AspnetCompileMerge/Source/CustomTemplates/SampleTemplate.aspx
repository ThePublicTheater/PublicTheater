<%@ Page Language="C#" MasterPageFile="~/Views/MasterPages/MasterPage.Master" AutoEventWireup="true" CodeBehind="SampleTemplate.aspx.cs" Inherits="TheaterTemplate.Web.CustomTemplates.SampleTemplate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    Tessitura functionality will occur on this page.
    <h1>
        <EPiServer:Property runat="server" ID="h1" PropertyName="h1" />
    </h1>
    <div id="homeMain">
        <div id="intro">
            <EPiServer:Property runat="server" ID="Property2" PropertyName="MainBody" />
        </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="beforeCloseBody" runat="server">
</asp:Content>

