<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ArtistProductionsBlockControl.ascx.cs"
    Inherits="PublicTheater.Web.Views.Blocks.ArtistProductionsBlockControl" %>



<asp:panel id="pnlFeaturedArtist" runat="server" cssclass="block noHeight mediaBlock">
                    <h2>
                        Other Shows with
                        <asp:literal id="ltrFeaturedArtistName" runat="server" /></h2>
                    <asp:repeater id="rptFeaturedArtistPlays" runat="server">
                        <itemtemplate>
                                <div class="large-6 medium-6 small-12">
                                    <asp:HyperLink ID="lnkPDPImage" runat="server" ImageUrl='<%# Eval("ThumbnailUrl") %>' NavigateUrl='<%# Eval("PlayDetailLink") %>'>
                                    </asp:HyperLink>
                                    <asp:HyperLink ID="lnkPdpText" runat="server" Text='<%# Eval("Heading") %>' NavigateUrl='<%# Eval("PlayDetailLink") %>'></asp:HyperLink>
                                </div>
                            </itemtemplate>
                    </asp:repeater>
                </asp:panel>