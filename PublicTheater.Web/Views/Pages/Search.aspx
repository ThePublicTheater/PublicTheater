<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPages/Interior.Master"
    AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="TheaterTemplate.Web.Views.Pages.Search" %>

<%@ Register TagPrefix="MAPTemplate" Namespace="TheaterTemplate.Web.Views.Controls" Assembly="TheaterTemplate.Web" %>
<%@ Register TagPrefix="MAPTemplate" TagName="MainBody" Src="~/Views/Controls/MainBody.ascx" %>
<%@ Import Namespace="TheaterTemplate.Web.Code" %>
<%@ Import Namespace="EPiServer.Core" %>
<asp:Content ID="Content3" ContentPlaceHolderID="PrimaryContent" runat="server">
    <MAPTemplate:MainBody runat="server" />
    <div id="SearchArea">
        <asp:Label ID="SearchLabel" CssClass="hidden" runat="server" AssociatedControlID="SearchText"
            Text="<%$ Resources: EPiServer, search.searchstring %>" />
        <asp:TextBox ID="SearchText" CssClass="searchText" Text="<%$ Resources: EPiServer, search.searchstring %>"
            onfocus="this.value='';" TabIndex="1" runat="server" />
        <asp:Button ID="SearchButton" CssClass="button" runat="server" TabIndex="2" OnClick="SearchClick"
            Text="<%$ Resources: EPiServer, navigation.search %>" /><br />
        <asp:CustomValidator ID="CustomValidator1" ControlToValidate="SearchText" Display="Dynamic"
            runat="server" />
        <div id="AdvancedArea">
            <asp:CheckBox ID="SearchInFiles" runat="server" />
            <asp:Label ID="Label1" runat="server" AssociatedControlID="SearchInFiles" Text="<%$ Resources: EPiServer, search.searchfiles %>" />
            <asp:CheckBox ID="SearchOnlyWholeWords" runat="server" />
            <asp:Label ID="Label2" runat="server" AssociatedControlID="SearchOnlyWholeWords"
                Text="<%$ Resources: EPiServer, search.onlywholewords %>" />
        </div>
    </div>
    <div id="ResultArea">
        <asp:Repeater ID="SearchResult" DataSourceID="SearchDataSource" runat="server" Visible="false">
            <HeaderTemplate>
                <h2>
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources: EPiServer, search.searchresult %>" /></h2>
                <ol>
            </HeaderTemplate>
            <ItemTemplate>
                <li><a href="<%# GetLinkUrlWithLanguage(Container.DataItem as PageData) %>">
                    <EPiServer:Property ID="Property1" runat="server" PropertyName="PageName" />
                </a>(<span id="Span1" class="dateTime" runat="server"><%# DataBinder.Eval(Container.DataItem, "Changed", "{0:d}")%></span>)
                    <p>
                        <%# ((PageData)Container.DataItem).GetPreviewText(150) %>
                    </p>
                </li>
            </ItemTemplate>
            <FooterTemplate>
                </ol>
            </FooterTemplate>
        </asp:Repeater>
        <h2 id="NoSearchResult" runat="server" visible="false">
            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources: EPiServer, search.nosearchresult %>" /></h2>
    </div>
    <EPiServer:SearchDataSource ID="SearchDataSource" runat="server" EnableVisibleInMenu="false">
        <SelectParameters>
            <EPiServer:PropertyParameter Name="PageLink" PropertyName="SearchRoot" />
            <asp:ControlParameter Name="SearchQuery" ControlID="SearchText" PropertyName="Text" />
            <asp:ControlParameter Name="SearchFiles" ControlID="SearchInFiles" PropertyName="Checked" />
            <asp:ControlParameter Name="OnlyWholeWords" ControlID="SearchOnlyWholeWords" PropertyName="Checked" />
        </SelectParameters>
    </EPiServer:SearchDataSource>
</asp:Content>
