<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPages/Interior.master"
    AutoEventWireup="true" CodeBehind="VisitUs.aspx.cs" Inherits="PublicTheater.Web.Views.Pages.VisitUs" %>

<asp:content id="Content1" contentplaceholderid="HeadContent" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="PrimaryContent" runat="server">
    <div class="large-12 columns">
        <EPiServer:Property runat="server" ID="propHeader" PropertyName="Heading"
            CustomTagName="h2">
        </EPiServer:Property>
    </div>
    <EPiServer:Property runat="server" ID="propMainBody" PropertyName="MainBody">
    </EPiServer:Property>
    <EPiServer:Property runat="server" ID="propLocationList" PropertyName="LocationList">
        <RenderSettings />
    </EPiServer:Property>
</asp:content>
<asp:content id="Content5" contentplaceholderid="BeforeCloseBodyContent" runat="server">
</asp:content>
