<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MediaNonPaged.ascx.cs"
    Inherits="PublicTheater.Web.Views.Theater.Controls.MediaNonPaged" %>
<asp:Repeater runat="server" ID="mediaRepeater">
    <HeaderTemplate>
        <ul class="grid fourColGrid">
    </HeaderTemplate>
    <ItemTemplate>
        <li runat="server" id="mediaThumbnailItem">
            <asp:HyperLink runat="server" ID="thumbImage" />
            <div class="duration">
                <asp:Literal runat="server" ID="date" /></div>
            <div class="title">
                <h3>
                    <asp:Literal runat="server" ID="title" /></h3>
                <asp:Literal runat="server" ID="description" />
            </div>
        </li>
    </ItemTemplate>
    <FooterTemplate>
        </ul>
    </FooterTemplate>
</asp:Repeater>