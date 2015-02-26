<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ArtistGroupBlockControl.ascx.cs"
    Inherits="PublicTheater.Web.Views.Blocks.ArtistGroupBlockControl" %>
<%@ Import Namespace="PublicTheater.Custom.Episerver.Properties" %>
<div class="artistList">
    <div class="artistRow">
        <div class="artistGroup">
            <asp:repeater runat="server" id="rptArtistGroups">
                <itemtemplate>
                    <div class="artistSection">
                       <h3> <%# ((ArtistGroup)Container.DataItem).GroupName %></h3>
                        <asp:repeater runat="server" id="rptArtists">
                            <headertemplate>
                                <ul>    
                            </headertemplate>
                            <itemtemplate>
                                <li class="">
                                    <img src='<%# ((ArtistLink)(Container.DataItem)).ArtistPageData.Thumbnail %>'/>
                                    <div class="desc">
                                        <strong><%# ((ArtistLink)(Container.DataItem)).ArtistPageData.PageName%></strong>
                                        <a href="#" data-reveal-id='artistModal<%# ((ArtistLink)(Container.DataItem)).ArtistPageData.PageLink.ID %>'>
                                            <%# ((ArtistLink)(Container.DataItem)).ArtistPageData.Bio != null ? "Bio" : string.Empty %>
                                        </a>
                                    </div>
                                    <div id='artistModal<%# ((ArtistLink)(Container.DataItem)).ArtistPageData.PageLink.ID %>' class="reveal-modal small">
                                        <div class="artistModalHeader">
                                            <a class="close-reveal-modal">&#215;</a>
                                            <a href='<%# ((ArtistLink)(Container.DataItem)).ArtistPageData.StaticLinkURL %>'>
                                                <img src='<%# ((ArtistLink)(Container.DataItem)).ArtistPageData.Headshot %>'/>
                                            </a>
                                            <div class="headerContent">
                                                <h2><%# ((ArtistLink)(Container.DataItem)).ArtistPageData.PageName%></h2>
                                                <h3><%# ((ArtistLink)(Container.DataItem)).ArtistRole %></h3>
                                            </div>
                                        </div>
                                        <p>
                                            <%# ((ArtistLink)(Container.DataItem)).ArtistPageData.Bio%>
                                        </p>
                                    </div>
                                </li>
                         </itemtemplate>
                         <footertemplate>
                            </ul> 
                         </footertemplate>
                    </asp:repeater>
                </div>
            </itemtemplate>
            </asp:repeater>
        </div>
    </div>
</div>
