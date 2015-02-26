<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PageList.ascx.cs" Inherits="TheaterTemplate.Web.Views.Controls.PageList" %>
<%@ Register TagPrefix="MAPTemplate" TagName="Document" Src="~/Views/Controls/Document.ascx" %>

<adage:PageList runat="server" ID="simplePageList">
    <HeaderTemplate>
        <ul>
    </HeaderTemplate>
    <ItemTemplate>
        <li><a href="<%# Container.CurrentPage.LinkURL %>">
            <%# Container.CurrentPage.PageName %></a></li> 
    </ItemTemplate>
    <FooterTemplate>
        </ul>
    </FooterTemplate>
</adage:PageList>
<adage:PageList runat="server" ID="detailedPageList">
    <HeaderTemplate>
        <ul class="thumblist">
    </HeaderTemplate>   
    <ItemTemplate> 
        <li>
            <img src="<%# Container.CurrentPage["Thumbnail"] %>" alt="" />
            <h4>
                <a href="<%# Container.CurrentPage.LinkURL %>">
                    <%# Container.CurrentPage.PageName %></a>
            </h4>
            <p><a href="<%# Container.CurrentPage.LinkURL %>">More </a></p>
        </li>
    </ItemTemplate>
    <FooterTemplate>
        </ul>
    </FooterTemplate>
</adage:PageList>
<adage:PageList runat="server" ID="newsPageList">
    <HeaderTemplate>
        <div class="newsArchive">
            <ul>
    </HeaderTemplate>
    <ItemTemplate>
        <li>
            <div class="left">
                <%--<%# Container.CurrentPage["MainBody"] %>--%>
            </div>
        </li>
    </ItemTemplate>
    <FooterTemplate>
        </ul> </div>
    </FooterTemplate>
</adage:PageList>
<adage:PageList runat="server" ID="blogPageList">
    <HeaderTemplate>
        <ul>
    </HeaderTemplate>
    <ItemTemplate>
        <li>
            <h2>
                Blog: <a href="<%# Container.CurrentPage.LinkURL %>">
                    <%# Container.CurrentPage.PageName %></a>
            </h2>
            <div class="contentPushRight">
                <%# Container.CurrentPage["MainBody"] %></div>
        </li>
    </ItemTemplate>
    <FooterTemplate>
        </ul>
    </FooterTemplate>
</adage:PageList>
