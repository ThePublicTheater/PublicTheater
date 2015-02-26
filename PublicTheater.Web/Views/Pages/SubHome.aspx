<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPages/MasterPage.master"
    AutoEventWireup="true" CodeBehind="SubHome.aspx.cs" Inherits="PublicTheater.Web.Views.Pages.SubHome" %>
<%@ Register TagPrefix="uc" TagName="HeroImageControl_1" Src="~/Views/Controls/HeroImageControl.ascx" %>

<asp:content id="Content1" contentplaceholderid="Head" runat="server">
</asp:content>
<asp:content id="Content4" contentplaceholderid="beforeWrapper" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="MainContent" runat="server">
    
    <uc:HeroImageControl_1 runat="server" ID="HeroImg1">
    </uc:HeroImageControl_1>

    <div class="homeWrapper subHomeWrapper">
        <div class="row subHome">
            <EPiServer:Property ID="Property1" runat="server" PropertyName="CaptionedRotatorItems" />
            <EPiServer:Property ID="Property2" runat="server" PropertyName="UpcomingPerformances" />
            <EPiServer:Property ID="subhomeRotatorProperty" runat="server" PropertyName="BlockList" CssClass="blockListWrap" />
        </div>
    </div>
</asp:content>
<asp:content id="Content3" contentplaceholderid="BeforeCloseBody" runat="server">
</asp:content>