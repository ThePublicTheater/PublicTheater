<%@ Page Language="C#" MasterPageFile="~/Views/MasterPages/Interior.Master" AutoEventWireup="true"
    CodeBehind="MembershipPurchasePage.aspx.cs" Inherits="PublicTheater.Web.Views.Pages.MembershipPurchasePage" %>

<asp:content id="Content1" contentplaceholderid="HeadContent" runat="server">
</asp:content>
<asp:content id="Content5" contentplaceholderid="PageNameContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <div class="large-12 medium-12 small-12">
        <EPiServer:Property ID="Property2" runat="server" PropertyName="MainBody" CssClass="mainBody">
        </EPiServer:Property>
        <div style="overflow: auto">
            <EPiServer:Property ID="Property1" runat="server" PropertyName="MembershipPurchaseBlock">
            </EPiServer:Property>
        </div>
        <EPiServer:Property ID="Property3" runat="server" PropertyName="FinePrint" CssClass="mainBody" DisplayMissingMessage="False">
        </EPiServer:Property>

    </div>
</asp:content>
<asp:content id="Content3" contentplaceholderid="PrimaryContentBottomSection" runat="server">
</asp:content>
<asp:content id="Content4" contentplaceholderid="BeforeCloseBodyContent" runat="server">
</asp:content>
