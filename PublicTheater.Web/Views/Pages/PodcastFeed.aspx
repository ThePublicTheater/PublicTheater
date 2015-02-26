
<%@ Page Language="c#" Inherits="PublicTheater.Web.Views.Pages.PodcastFeed" AutoEventWireup="true" CodeBehind="PodcastFeed.aspx.cs" %><?xml version="1.0" encoding="UTF-8"?>
<%@ OutputCache Duration="60" VaryByParam="Type" %>
<%@ Import Namespace="PublicTheater.Custom.Episerver.Utility" %>

<rss xmlns:itunes="http://www.itunes.com/dtds/podcast-1.0.dtd" version="2.0">

<channel>

<title><%=Utility.escapeXML(CurrentPage.PodcastTitle)%></title>

<link><%=CurrentPage.PodcastLinkUrl!=null ? Utility.AbsoluteUrl(Request.Url,CurrentPage.PodcastLinkUrl.ToString()) :""%></link>

<language>en-us</language>

<copyright>&#xA9; <%=Utility.escapeXML(CurrentPage.Copyright)%></copyright>

<itunes:subtitle><%=Utility.escapeXML(CurrentPage.PodcastSubtitle)%></itunes:subtitle>

<itunes:author><%=Utility.escapeXML(CurrentPage.ItunesOwnerName)%></itunes:author>

<itunes:summary><%=Utility.escapeXML(CurrentPage.PodcastSummary)%></itunes:summary>

<description><%=Utility.escapeXML(CurrentPage.PodcastSummary)%></description>

<itunes:owner>

<itunes:name><%=CurrentPage.ItunesOwnerName%></itunes:name>

<itunes:email><%= CurrentPage.ItunesOwnerEmail %></itunes:email>

</itunes:owner>

<itunes:image href="<%=Utility.AbsoluteUrl(Request.Url,CurrentPage.CImageUrl.ToString())%>" />

<itunes:category text="<%=Utility.escapeXML(CurrentPage.ItunesCategory)%>">

<itunes:category text="<%=Utility.escapeXML(CurrentPage.ItunesSubCategory)%>"/>

</itunes:category>

    <asp:Repeater ID="Repeater1" runat="server" >
        <ItemTemplate>

<item>


<title><%# Utility.escapeXML(Eval("Title").ToString()) %></title>

<itunes:subtitle><%# Utility.escapeXML(Eval("SubTitle").ToString()) %></itunes:subtitle>

<itunes:summary><%#Utility.escapeXML(Eval("Summary").ToString())%></itunes:summary>

<description><%#Utility.escapeXML(Eval("Summary").ToString())%></description>

<itunes:image href="<%# Utility.AbsoluteUrl(Request.Url,Eval("ImageUrl").ToString())%>" />

<enclosure url="<%# Utility.AbsoluteUrl(Request.Url,Eval("Mp3Url").ToString())%>" length="<%# Eval("Length")%>" type="audio/x-mp3" />


<pubDate><%# Eval("PublicationDate")%></pubDate>

<itunes:duration><%# Eval("Duration")%></itunes:duration>

</item>     
  

        </ItemTemplate>
    </asp:Repeater>
    



</channel>

</rss>