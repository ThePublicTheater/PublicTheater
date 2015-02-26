<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPages/Interior.master" AutoEventWireup="true" CodeBehind="MediaAlbum.aspx.cs" Inherits="PublicTheater.Web.Views.Pages.MediaAlbum" %>
<%--<asp:content id="Content1" contentplaceholderid="HeadContent" runat="server">
</asp:content>--%>
<asp:content id="Content2" contentplaceholderid="PrimaryContent" runat="server"></
    <section>
        <EPiServer:Property runat="server" ID="Property1" PropertyName="MainBody" DisplayMissingMessage="False">
        </EPiServer:Property>

        <div class="content">
            <ul class="photo-list">
    	         <asp:Repeater runat="server" ID="rptPhotoItems">
                    <ItemTemplate>
                        <li>
                            <asp:HyperLink runat="server" ID="mediaItemLink">
                                <asp:Image runat="server" ID="mediaItemImage" />
                            </asp:HyperLink>
                            <asp:Literal runat="server" ID="mediaItemTitle"></asp:Literal>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
    </section>
</asp:content>
<asp:Content ID="Content5" ContentPlaceHolderID="BeforeCloseBodyContent" runat="server">
</asp:Content>
