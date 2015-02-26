<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPages/Interior.master"
    AutoEventWireup="true" CodeBehind="NewsItem.aspx.cs" Inherits="TheaterTemplate.Web.Views.Pages.NewsItem" %>
<%@ Import Namespace="TheaterTemplate.Web.Code" %>

<%@ Register TagPrefix="MAPTemplate" TagName="MainBody" Src="~/Views/Controls/MainBody.ascx" %>
<%@ Register TagPrefix="MAPTemplate" TagName="PageList" Src="~/Views/Controls/PageList.ascx" %>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <MAPTemplate:MainBody id="MainBody1" runat="server" />
    <MAPTemplate:PageList id="PageList1" previewtextlength="0" pagelinkproperty="MainListRoot"
        maxcountproperty="MainListCount" showtopruler="true" runat="server" />
    <div id="SecondaryBody">
        <dl>
            <dt>
                <EPiServer:Translate ID="Translate1" Text="/news/writername" runat="server" />
            </dt>
            <dd>
                <EPiServer:Property ID="Property1" PropertyName="Author" runat="server" />
            </dd>
            <dt>
                <EPiServer:Translate ID="Translate2" Text="/news/publishdate" runat="server" />
            </dt>
            <dd>
                <%=CurrentPage.GetFormattedPublishDate() %></dd>
        </dl>
    </div>
</asp:Content>
