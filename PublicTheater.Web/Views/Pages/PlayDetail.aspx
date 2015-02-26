<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPages/MasterPage.Master"
    AutoEventWireup="true" CodeBehind="PlayDetail.aspx.cs" Inherits="PublicTheater.Web.Views.Pages.PlayDetail" %>
<%@ Import Namespace="Adage.Common.ExtensionMethods.Generic" %>

<%@ Register Src="~/Views/Blocks/PDPTicketBlockControl.ascx" TagPrefix="EpiBlock"
    TagName="PDPCalendarBlock" %>
<%@ Register Src="~/controls/account/PromoBox.ascx" TagPrefix="account" TagName="Promobox" %>
<%@ Register Src="~/Views/Controls/HeroImageControl.ascx" TagPrefix="uc" TagName="HeroImageControl_1" %>
<%@ Register TagPrefix="Public" Namespace="PublicTheater.Web.Views.Controls" Assembly="PublicTheater.Web" %>
<%@ Register Src="~/Views/Blocks/PDPPackageBlockControl.ascx" TagPrefix="account" TagName="PDPPackageBlockControl" %>

<asp:content id="Content1" contentplaceholderid="Head" runat="server">
    <% if(CurrentPage.Thumbnail150x75 != null) {%>
    <meta property="og:image" content="<%= HttpContext.Current.Request.Url.Scheme 
    + "://"
    + HttpContext.Current.Request.Url.Authority 
    + HttpContext.Current.Request.ApplicationPath + (CurrentPage.Thumbnail150x75.ToString())%>">
    <%} %>
</asp:content>
<asp:content id="Content2" contentplaceholderid="beforeWrapper" runat="server">
</asp:content>
<asp:content id="Content3" contentplaceholderid="MainContent" runat="server">
    <uc:HeroImageControl_1 runat="server" ID="HeroImg1"></uc:HeroImageControl_1>
    <div class="playDetailWrapper">
          <div class="row firstRow">
            <div class="large-9 medium-12 small-12">
                <div class="slideshowCaptions PDP">
                    <div class="large-6 medium-6 small-12">
                        <Public:Property ID="propHeading" runat="server" PropertyName="Heading" CustomTagName="h2" />
                        <Public:Property ID="propHeadinghtml" runat="server" PropertyName="HeadingHtml" DisplayMissingMessage="False" CssClass="HeadingHtml" />
                        <h3>
                            <%= CurrentPage.StartEndDate %><br />
                            <asp:literal runat="server" id="litBox1Venue" Visible="False"></asp:literal>
                        </h3>
                        <Public:Property ID="prop" runat="server" PropertyName="Box1Content" CustomTagName="h4" />
                    </div>
                    <div class="large-6 medium-6 small-12">
                        <div class="showDetails">
                            <asp:panel runat="server" id="pnlPricing">
                                <h3>
                                    <%
                                        string venue = CurrentPage.VenueName;
                                        try
                                       {
                                           venue = CurrentPage.Property["VenueOverride"].ToString();
                                       }
                                       catch
                                       {
                                       }
                                        if (string.IsNullOrEmpty(venue))
                                        {
                                            venue = CurrentPage.VenueName;
                                        }
                                    %>
                                    <%= venue %>
                                    <br />
                                    <%= CurrentPage.TicketPriceRange %>
                                    <br />
                                    <asp:literal runat="server" id="litMemberPrice"></asp:literal>
                                    <asp:literal runat="server" id="litDoorPrice"></asp:literal>
                                </h3>
                            </asp:panel>
                            <Public:Property ID="propRunTime" runat="server" PropertyName="RunTime" CustomTagName="h4" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="large-3 medium-12 small-12">
                <div class="buyTicketsExpander">
                    <EpiBlock:PDPCalendarBlock runat="server" ID="ticketBlock"></EpiBlock:PDPCalendarBlock>
                    <account:Promobox runat="server" ID="promoBox" />
                    <account:PDPPackageBlockControl runat="server" id="PDPPackageBlockControl" />
                </div>
            </div>
        </div>
        <div class="row">
            <div class="large-8 medium-12 small-12">
                <div class="small-12">
                    <Public:Property ID="propMainBody" runat="server" PropertyName="MainBody" CustomTagName="div" CssClass="block noHeight">
                    </Public:Property>
                </div>
                <div class="small-12">
                <asp:Panel runat="server" ID="pnlMedia" CssClass="block noHeight mediaBlock">
                    
                        <h2>
                            Media</h2>
                        <asp:repeater runat="server" id="rptRelatedMediaItems">
                            <headertemplate>
                                <ul class="slideshow adaptiveSlideshow">
                            </headertemplate>
                            <itemtemplate>
                                
                                <%# (Eval("CoverImage")!="" && Eval("FriendlyURL")!="") ? "": "<!--"  %>
                                <li>
                                    <asp:HyperLink runat="server" ID="mediaItemLink" NavigateUrl='<%# Eval("FriendlyURL") %>' ImageUrl='<%# Eval("CoverImage") %>' CssClass="relatedMedia">
                                    </asp:HyperLink>
                                </li>
                                <%# (Eval("CoverImage")!="" && Eval("FriendlyURL")!="") ? "": "//-->"  %>
                                
                            </itemtemplate>
                            <footertemplate>
                                </ul>
                            </footertemplate>
                        </asp:repeater>
                    </asp:Panel>
                </div>
                
                <Public:Property ID="Property1" runat="server" PropertyName="MediaBox" DisplayMissingMessage="False" /> 
                <Public:Property ID="Property2" runat="server" PropertyName="FooterContent" CssClass="block noHeight small-12" />
                
            </div>
            <div class="large-4 medium-12 small-12">
                <Public:Property ID="propRightContent" runat="server" PropertyName="Credits" CustomTagName="div"
                    CssClass="block noHeight creditBlock" />
                <Public:Property ID="Property3" runat="server" PropertyName="RightContent">
                    <RenderSettings Tag="PDP"></RenderSettings>
                </Public:Property>
            </div>
        </div>
    </div>
    <Public:Property runat="server" PropertyName="BottomContent" DisplayMissingMessage="False">
                    <RenderSettings Tag="PDP"></RenderSettings>
                </Public:Property>
</asp:content>
<asp:content id="Content4" contentplaceholderid="BeforeCloseBody" runat="server">
</asp:content>
