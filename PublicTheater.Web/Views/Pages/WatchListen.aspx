<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPages/WatchListen.master"
    AutoEventWireup="true" CodeBehind="WatchListen.aspx.cs" Inherits="PublicTheater.Web.Views.Pages.WatchListen" %>

<%@ Register TagPrefix="uc" TagName="headingControl" Src="~/Views/Controls/PageHeading.ascx" %>
<asp:content id="Content1" contentplaceholderid="HeadContent" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="PrimaryContent" runat="server">
    <header class="watchListenHeader">
        <div class="large-6 medium-12 small-12">
            <a href="/"><img src="/Static/img/logo_watchAndListen.jpg" /></a>
        </div>

        <div class="large-6 medium-12 small-12">
            <ul class="utilityNavLinks">
                <li><a href="<%= CurrentPage.YouTubeChannel %>"><span>YouTube Channel</span></a></li>
                <li><a href="<%= CurrentPage.PodcastUrl %>" class="subscribe">Subscribe to Podcasts </a></li>
            </ul>
        </div>
        <style type ="text/css" runat="server">
            @media (min-width: 651px) {
                .categories {
                    width: <%= 100 / (1 + rptCategories.Items.Count) %>%;
                }
            }
        </style>
    </header>
    <div class="large-5 medium-12 small-12">
        <uc:headingControl runat="server" ID="headingControl" />
    </div>
    <div class="large-7 medium-12 small-12">
        <asp:repeater runat="server" id="rptCategories">
            <headertemplate>
                <div class="tabs">
                    <ul>
                        <li class="categories small-12"><a href="#" class="selected" data-filterproperty="CategoryString" data-filter="all">All</a></li>
            </headertemplate>
            <itemtemplate>
                        <li class="categories small-12"><a href="#" data-filterproperty="CategoryString"data-filter='<%# Eval("Name") %>'><%# Eval("Name") %></a></li>
            </itemtemplate>
            <footertemplate>
                        <%--<li class="large-3 medium-3 small-12"><a href="#" data-filterproperty="CategoryString" data-filter="other">Other</a></li>--%>
                    </ul>
                </div>
            </FooterTemplate>
        </asp:Repeater>
    </div>
    <div id="playerArea" class="large-12 medium-12 small-12">
    </div>
    <div id="gridArea">
        <!-- Automatic grid rendering -->
        <div class="gridColumn large-3 medium-12 small-12">
        </div>
        <div class="gridColumn large-3 medium-12 small-12">
        </div>
        <div class="gridColumn large-3 medium-12 small-12">
        </div>
        <div class="gridColumn large-3 medium-12 small-12">
        </div>
    </div>
</asp:content>
<asp:content id="Content5" contentplaceholderid="BeforeCloseBodyContent" runat="server">
    <asp:hiddenfield clientidmode="Static" id="mediaCenterData" runat="server" />
    <script src="/Static/backbone/lib/require.js" data-main="/Static/backbone/main"></script>
    <script src="../../js/jquery-1.10.2.min.js"></script>
</asp:content>
