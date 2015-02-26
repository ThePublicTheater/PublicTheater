<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPages/Interior.master"
    AutoEventWireup="true" CodeBehind="Artist.aspx.cs" Inherits="PublicTheater.Web.Views.Pages.Artist" %>

<asp:content id="Content1" contentplaceholderid="HeadContent" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="PrimaryContent" runat="server">
    <div class="large-8 list artist">
        <asp:Image runat="server" ID="imgHeadShot" Visible="False"/>
        <Public:Property runat="server" ID="propBio" CssClass="text" CustomTagName="div" PropertyName="Bio" DisplayMissingMessage="False" />
    </div>
</asp:content>
<asp:content id="Content5" contentplaceholderid="BeforeCloseBodyContent" runat="server">
</asp:content>
