<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPages/Interior.master"
    AutoEventWireup="true" CodeBehind="Leadership.aspx.cs" Inherits="PublicTheater.Web.Views.Pages.Leadership" %>

<asp:content id="Content1" contentplaceholderid="HeadContent" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="PrimaryContent" runat="server">
    <EPiServer:Property ID="Property1" runat="server" PropertyName="Group1">
    </EPiServer:Property>
    <EPiServer:Property ID="Property2" runat="server" PropertyName="Group2">
    </EPiServer:Property>
    <EPiServer:Property ID="Property3" runat="server" PropertyName="Group3">
    </EPiServer:Property>

    <EPiServer:Property ID="Property4" runat="server" PropertyName="Group4">
    </EPiServer:Property>
</asp:content>
<asp:content id="Content5" contentplaceholderid="BeforeCloseBodyContent" runat="server">
</asp:content>
