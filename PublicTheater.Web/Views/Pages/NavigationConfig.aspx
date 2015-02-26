<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Views/MasterPages/Interior.master" CodeBehind="NavigationConfig.aspx.cs" Inherits="PublicTheater.Web.Views.Pages.NavigationConfig" %>

<%@ Import Namespace="PublicTheater.Custom.Episerver.Utility" %>
<%@ Register TagPrefix="uc" TagName="UtilityNav" Src="/views/Controls/UtilityNav.ascx" %>
<asp:Content ID="Content3" ContentPlaceHolderID="PrimaryContent" runat="server">
<header class="subHeader top-bar">
        <script type="text/C#" runat="server">
        public string getcssOveride() 
        {
            try
            {
                return (CurrentPage.Property["cssOveride"]).Value.ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }
    
    </script>
    <%= getcssOveride() %>
    <div class="title-area logo">
        <div class="name">
            <asp:HyperLink runat="server" ID="lnkSubNavLogo"></asp:HyperLink>
        </div>
        <div class="toggle-topbar menu-icon hide-for-medium-up">
            <a href="#" class="toggleMenu"></a>
        </div>
    </div>
    <section class="mainNav top-bar-section">
        <ul>
            <asp:repeater runat="server" id="rptJoesPub">
                <itemtemplate>
                    <li>
                        <a href='<%# ((EPiServer.Core.PageData)Container.DataItem).GetRequestedSiteThemeUrl() %>' target='<%#Utility.GetTarget((EPiServer.Core.PageData)Container.DataItem) %>'>
                            <EPiServer:Property ID="Property3" runat="server" PropertyName="PageName" CustomTagName="span" />
                        </a>
                    </li>
                </itemtemplate>
            </asp:repeater>    
            <li class="switchToPubNavWrapper">
                <a href="#" class="switchToPubNav">Back to Main Navigation</a>
            </li>
        </ul>

        <asp:Panel runat="server" ID="pnlSearchSub" CssClass="search" Visible="False">
            <asp:TextBox runat="server" Text="Search" ID="txtSearchSub"></asp:TextBox>
            <asp:LinkButton runat="server" ID="btnSubmitSearchSub" CssClass="btnSearch"></asp:LinkButton>
        </asp:Panel>
    </section>
    <uc:UtilityNav runat="server" ID="utilityNavSub"></uc:UtilityNav>
</header>

  
</asp:Content>


