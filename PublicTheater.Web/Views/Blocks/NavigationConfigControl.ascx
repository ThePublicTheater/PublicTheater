<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NavigationConfigControl.ascx.cs"
    Inherits="PublicTheater.Web.Views.Blocks.NavigationConfigControl" %>
<%@ Import Namespace="Microsoft.Ajax.Utilities" %>
<%@ Import Namespace="PublicTheater.Custom.Episerver" %>
<%@ Import Namespace="PublicTheater.Custom.Episerver.Utility" %>
<%@ Register TagPrefix="uc" TagName="UtilityNav" Src="/views/Controls/UtilityNav.ascx" %>
<%@ Register TagPrefix="uc" TagName="Search" Src="/views/Controls/SearchTextBox.ascx" %>

<header class="subHeader top-bar">
    <script type="text/C#" runat="server">
        public string getcssOveride() 
        {
            try
            {
                return (CurrentData.Property["cssOveride"]).Value.ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }
    
    </script>
    <%= getcssOveride() %>
    <div class="navWrapperHeightHack">
    <div class="title-area logo">
        <div class="name">
            <asp:HyperLink runat="server" ID="lnkSubNavLogo" CssClass="hide-for-small"></asp:HyperLink>
            <asp:HyperLink runat="server" ID="lnkSubNavLogoMobile" CssClass="hide-for-medium-up" Visible="False" ></asp:HyperLink>
        </div>
        <div class="toggle-topbar menu-icon hide-for-medium-up">
            <a href="#" class="toggleMenu"><p class="subMenuName">menu</p></a>
        </div>
    </div>
    <uc:UtilityNav runat="server" ID="utilityNavSub"></uc:UtilityNav>
    <section class="mainNav top-bar-section">
        <ul>
            <asp:repeater runat="server" id="rptJoesPub">
                <itemtemplate>
                    <li>
                        <a href='<%#GetCorrectLink((EPiServer.Core.PageData) Container.DataItem) %>' target='<%#Utility.GetTarget((EPiServer.Core.PageData)Container.DataItem) %>'>
                            <EPiServer:Property ID="Property3" runat="server" PropertyName="PageName" CustomTagName="span" />
                        </a>
                    </li>
                </itemtemplate>
            </asp:repeater>    
            <li>
                <a class="mobileHome" href="/">Public Theater Home</a>
            </li>

            <a href="/" class="publicHome">Public Theater Home</a>

        </ul>

        <uc:Search  runat="server" ID="ucSearch"></uc:Search>

        
    </section>
        
        </div>
    
</header>
