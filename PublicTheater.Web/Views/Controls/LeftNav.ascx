<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LeftNav.ascx.cs" Inherits="PublicTheater.Web.Views.Controls.LeftNav" %>
<%@ Import Namespace="PublicTheater.Custom.Episerver.Utility" %>
<%@ Register Src="~/views/Controls/UtilityNav.ascx" TagPrefix="uc" TagName="UtilityNav" %>
<%@ Register TagPrefix="uc" TagName="Search" Src="~/views/Controls/SearchTextBox.ascx" %>
<header class="top-bar publicHeader" runat="server" id="navHeader">

    <div class="toggleSubHomePage" runat="server" id="divToggle" Visible="False">
        <a href="#"></a>
        <p class="menuNameLarge">menu</p>
    </div>
    <div class="navWrapperHeightHack">
    <div class="title-area logo">
        <div class="name">
            <a href="/">
                <img src="/Static/img/publicLogo.jpg" class="hide-for-small" />
                <img src="/Static/img/logo_mobile.png" class="hide-for-medium-up" />
            </a>
        </div>
        <div class="toggle-topbar menu-icon hide-for-medium-up">
            <a href="#" class="toggleMenu"></a><p class="menuName">menu</p>
        </div>
    </div>
    <uc:UtilityNav runat="server" ID="utilityNav1"></uc:UtilityNav>
    <section class="top-bar-section mainNav">
    
        <asp:Repeater runat="server" ID="rptFirstTier">
        <HeaderTemplate>
            <ul>
        </HeaderTemplate>
            <ItemTemplate>
                <li runat="server" id="liNav">
                    <a href='<%# ((EPiServer.Core.PageData)Container.DataItem).GetSiteThemeUrl() %>' target='<%#Utility.GetTarget((EPiServer.Core.PageData)Container.DataItem) %>'>
                        <%# Eval("PageName")%>
                    </a>
                    <asp:Repeater runat="server" ID="rptMobileTier">
                        <HeaderTemplate>
                            <ul class="dropdown">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <li>
                                <a href='<%# ((EPiServer.Core.PageData)Container.DataItem).GetSiteThemeUrl() %>' target='<%#Utility.GetTarget((EPiServer.Core.PageData)Container.DataItem) %>'>
                                    <%# Eval("PageName")%>
                                </a>
                            </li>
                        </ItemTemplate>
                        <FooterTemplate>
                            </ul>
                        </FooterTemplate>
                    </asp:Repeater>

                    <asp:Repeater runat="server" ID="rptSecondTier">
                        <HeaderTemplate>
                            <div class="dropdownMenu">
                                <div class="innerDropdown">
                                    <div class="innerArrow"></div>
                                    <ul>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <li>
                                <a href='<%# ((EPiServer.Core.PageData)Container.DataItem).GetSiteThemeUrl() %>' target='<%#Utility.GetTarget((EPiServer.Core.PageData)Container.DataItem) %>'>
                                    <%# Eval("PageName")%>
                                </a>
                            </li>
                        </ItemTemplate>
                        <FooterTemplate>
                            </ul></div></div>
                        </FooterTemplate>
                    </asp:Repeater>
                </li>
            </ItemTemplate>
            <FooterTemplate>
                <li class="switchToSubNavWrapper" runat="server" id="liBackToSubNav" Visible="False" >
                    <a href="#" class="switchToSubNav">Back to Sub Navigation</a>
                </li>
                 <li class="mobileLogin">
                    <asp:HyperLink runat="server" ID="lnkLogin"></asp:HyperLink>
                </li>
             </ul>        
            </FooterTemplate>
        </asp:Repeater>
        
        <uc:Search runat="server" ID="ucSearch"></uc:Search>
   </section>
        </div>
</header>
<EPiServer:Property ID="Property10" PropertyName="SubNavigations" DisplayMissingMessage="false"
    PageLink='<%# EPiServer.Core.ContentReference.StartPage %>' EnableViewState="false"
    runat="server" />
