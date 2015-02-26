<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPages/MasterPage.Master"
    AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="PublicTheater.Web.Views.Pages.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <link href="/Static/js/lib/addtohomescreen.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="beforeWrapper" runat="server">

    <asp:Repeater runat="server" ID="rptSlideShowImage">
        <HeaderTemplate>
            <div class="homeSlideShowWrapper">
                <ul class="homeSlideShow">
        </HeaderTemplate>
        <ItemTemplate>
            <li>
                <img src='<%# Eval("ImageUrl") %>' style="display: none;" />
            </li>
        </ItemTemplate>
        <FooterTemplate>
            </ul></div>
        </FooterTemplate>
    </asp:Repeater>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="homeWrapper">
        <div class="row">
            <div class="large-5 large-offset-7 medium-12 small-12 homeRotatorSlideCaption columns homeSlideShowFixed">
                <asp:Repeater runat="server" ID="rptSlideShowCaptions">
                    <HeaderTemplate>
                        <div class="captionNav"></div>
                        <div class="homeSlideShow slideshowCaptionsWrapper">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div class="slideshowCaptions" <%# Container.ItemIndex != 0 ? " style='display:none;'" : ""%>>
                            <div class="loading" style="height: 110px">
                                <img class="loading-image" src="/views/Theater/img/ajax-loader.gif" alt="Loading..." />
                            </div>
                            <div class="hideTheWrapper" style="display: none">
                                <h2>
                                    <%# Eval("ShowTitle") %>
                                    <span>
                                        <%# Eval("ShowTime") %></span>
                                </h2>
                            </div>
                            <div class="actionBtns">
                                <a href='<%# Eval("TicketsLink") %>'>Tickets</a> <a href='<%# Eval("DetailsLink") %>'>More Info</a>
                            </div>
                        </div>
                    </ItemTemplate>
                    <FooterTemplate>
                        </div>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
            <EPiServer:Property ID="Property1" runat="server" PropertyName="BlockList" CssClass="blockListWrap">
            </EPiServer:Property>

        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BeforeCloseBody" runat="server">
    
</asp:Content>
