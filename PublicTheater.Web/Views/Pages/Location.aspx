<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPages/Interior.master"
    AutoEventWireup="true" CodeBehind="Location.aspx.cs" Inherits="PublicTheater.Web.Views.Pages.Location" %>

<asp:content id="Content1" contentplaceholderid="HeadContent" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="PrimaryContent" runat="server">
    <div class="locationList">
        <div class="locationDetails">
            <h4>
                <%= CurrentPage.Location %></h4>
            <div class="large-6 columns">
                <EPiServer:Property runat="server" ID="property2" PropertyName="Address" CustomTagName="p">
                </EPiServer:Property>
                <EPiServer:Property runat="server" ID="property1" PropertyName="MainBody" CustomTagName="p">
                </EPiServer:Property>
            </div>
            <div class="large-6 columns">
                <a href="<%= CurrentPage.MapLink %>" target="_blank">
                    <img class="imageMap" src="<%= CurrentPage.MapImageLarge %>" alt="">
                </a>
            </div>
        </div>
    </div>
</asp:content>
<asp:content id="Content5" contentplaceholderid="BeforeCloseBodyContent" runat="server">
</asp:content>
