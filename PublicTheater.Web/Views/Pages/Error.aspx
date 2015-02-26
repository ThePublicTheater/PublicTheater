<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" MasterPageFile="~/Views/MasterPages/NoTessInterior.Master" Inherits="TheaterTemplate.Web.Views.Pages.Error" %>

<%@ Register TagPrefix="uc" TagName="MainBody" Src="~/Views/Controls/MainBody.ascx" %>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <div id="subscriptionBuilder">
        <div id="packageDisplay">
            <div class="packageDescription">
                <EPiServer:Property runat="server" PropertyName="Heading" CustomTagName="h2" DisplayMissingMessage="false" />
            </div>
        </div>
        <div class="errorConflict">
            <div class="errorMessage">
                                    
                <uc:MainBody runat="server" ID="MainBody" />
                <episerver:property ID="Property3" runat="server" PropertyName="ErrorMessage" DisplayMissingMessage="false" />
            </div>
            <div class="errorSolutions">
                <asp:HyperLink runat="server" ID="lnkHome" CssClass="btn btnStandOut left">
                    <EPiServer:Property ID="Property1" runat="server" PropertyName="HomeText" DisplayMissingMessage="false" />
                </asp:HyperLink>
                <asp:HyperLink runat="server" ID="lnkCart" CssClass="btn btnStandOut">
                    <EPiServer:Property ID="Property2" runat="server" PropertyName="CartText" DisplayMissingMessage="false" />
                </asp:HyperLink>
            </div>
        </div>
    </div>
</asp:Content>
