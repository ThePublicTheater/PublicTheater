<%@ Control Language="C#" AutoEventWireup="True" 
    Inherits="PublicTheater.Web.Views.Theater.Controls.MediaInputReference" %>
<%@ Register TagPrefix="epi" Assembly="EpiServer" Namespace="EPiServer.Web.WebControls" %>
<asp:Label ID="lblRelatedPageName" runat="server" Text="Performances:" />
<epi:InputPageReference runat="server" ID="ucInputPageReference" />
<asp:UpdatePanel ID="upMediaRelationship" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:ListView runat="server" ID="lvMediaRelationship">
            <ItemTemplate>
                <div class="mediaItem">
                    <asp:HiddenField ID="HfMediaRelationshipID" runat="server" />
                    <div class="lineItem">
                    <asp:Label ID="RelatedPageName" runat="server" CssClass="pageName" />
                    <asp:LinkButton ID="Remove" Text="Remove" runat="server" CommandName="Remove" DisablePageLeaveCheck="true" />
                    </div>
                </div>
            </ItemTemplate>
        </asp:ListView>
        <div id="bulkSelect" class="addMore" runat="server" visible="false">
            <div class="multClick">
                Click to Add Multiple Artists for Performance:</div>
            <div class="multArtists">
                <asp:ListView ID="lvArtists" runat="server">
                    <ItemTemplate>
                        <div>
                            <asp:CheckBox ID="cbSelect" runat="server" CssClass="selectArtist" />
                            <label>
                                <asp:Literal runat="server" ID="litArtistName"></asp:Literal></label>
                        </div>
                    </ItemTemplate>
                </asp:ListView>
            </div>
        </div>
        <asp:Button runat="server" ID="Refresh" style="display:none;" CssClass="refresh_button" />
    </ContentTemplate>
</asp:UpdatePanel>
