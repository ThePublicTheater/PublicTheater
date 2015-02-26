<%@ Page Language="C#" MasterPageFile="~/Views/MasterPages/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="ContentPage404.aspx.cs" Inherits="PublicTheater.Web.Views.Pages.ContentPage404" %>
<%@ Import Namespace="PublicTheater.Custom.Episerver.Utility" %>
<%@ Register TagPrefix="uc" TagName="HeroImageControl_1" Src="~/Views/Controls/HeroImageControl.ascx" %>
<%@ Register TagPrefix="uc" TagName="headingControl" Src="~/Views/Controls/PageHeading.ascx" %>
<asp:content id="Content1" contentplaceholderid="Head" runat="server">
</asp:content>
<asp:content id="Content4" contentplaceholderid="beforeWrapper" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="MainContent" runat="server">
    <uc:HeroImageControl_1 runat="server" ID="HeroImg1">
    </uc:HeroImageControl_1>
    <div class="homeWrapper generalWrapper">
        
        <uc:headingControl runat="server" ID="propHeading"/>

        <div class="generalContentWrapper">
            <div class="row">
                <div class="large-9 medium-8 small-12">
                    <EPiServer:Property ID="Property2" runat="server" PropertyName="MainBody" CssClass="block noHeight contentChunk">
                    </EPiServer:Property>
                </div>
                <div class="large-3 medium-4 small-12 height-Auto">
                    <EPiServer:Property ID="Property3" runat="server" PropertyName="CallOuts">
                        <RenderSettings ChildrenCssClass="generalRightRail"></RenderSettings>
                    </EPiServer:Property>
                </div>
            </div>
        </div>
    </div>
</asp:content>
<asp:content id="Content3" contentplaceholderid="BeforeCloseBody" runat="server">
</asp:content>