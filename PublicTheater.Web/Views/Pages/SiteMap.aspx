<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPages/Interior.master" AutoEventWireup="true" CodeBehind="SiteMap.aspx.cs" Inherits="PublicTheater.Web.Views.Pages.SiteMap" %>

<%@ Register TagPrefix="uc" Namespace="PublicTheater.Web.Views.Controls" Assembly="PublicTheater.Web" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    
    <uc:SiteMapPageTree runat="server" ExpandAll="true" ID="SiteMapTree" PageLink="<%# IndexRoot %>">
            <HeaderTemplate>
                <div id="siteMap" class="contain">
            </HeaderTemplate>
            <IndentTemplate>
                <ul>
            </IndentTemplate>
            <ItemHeaderTemplate>
                <li>
            </ItemHeaderTemplate>
            <ItemTemplate>
                <EPiServer:Property ID="Property1" PropertyName="PageLink" runat="server" />
            </ItemTemplate>
            <ItemFooterTemplate>
                </li>
            </ItemFooterTemplate>
            <UnindentTemplate>
                </ul>
            </UnindentTemplate>
            <FooterTemplate>
                </div>
            </FooterTemplate>
        </uc:SiteMapPageTree>

</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="BeforeCloseBodyContent" runat="server">
</asp:Content>
