<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPages/Interior.master" AutoEventWireup="true" CodeBehind="SectionLanding.aspx.cs" Inherits="PublicTheater.Web.Views.Pages.SectionLanding" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    
    <EPiServer:Property ID="Property1" runat="server" PropertyName="BlockList" CssClass="blockListWrap">
        <RenderSettings CustomTag="div" CssClass="sectionLandingBlocks" ChildrenCustomTag="div" Tag="Ticket" />
    </EPiServer:Property>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="BeforeCloseBodyContent" runat="server">
</asp:Content>
