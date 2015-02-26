<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPages/Interior.master"
    AutoEventWireup="true" CodeBehind="PackageVoucher.aspx.cs" Inherits="PublicTheater.Web.Views.Pages.PackageVoucher" %>

<asp:content id="Content1" contentplaceholderid="HeadContent" runat="server">
</asp:content>
<asp:content id="Content5" contentplaceholderid="PageNameContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <div class="large-12 medium-12 small-12 package-voucher">
        <EPiServer:Property ID="Property2" runat="server" PropertyName="MainBody" CssClass="mainBody">
        </EPiServer:Property>
        

       <asp:Panel runat="server" ID="purchasePanel" CssClass="package-voucher-purchase">
            <div>
                <div><span class="label">Price</span></div>
                <div><asp:Label runat="server" ID="lblUnitPrice"></asp:Label></div>
            </div>
            <div>
                <div><span class="label">Quantity</span></div>
                <div><asp:DropDownList runat="server" ID="ddlQuantity"/></div>
            </div>
            <div class="button-container">
                <div><asp:LinkButton runat="server" ID="lbAddToCart" CssClass="btn solid">Add to Cart</asp:LinkButton></div>
            </div>
            <asp:Label runat="server" ID="lblError" CssClass="error"></asp:Label>
       </asp:Panel>
        <EPiServer:Property ID="notLoggedIn" runat="server" PropertyName="NotLoggedIn" Visible="false">
        </EPiServer:Property>
        <EPiServer:Property ID="notInvited" runat="server" PropertyName="NotInvited" Visible="False">
        </EPiServer:Property>
        <EPiServer:Property ID="alreadyPurchased" runat="server" PropertyName="AlreadyPurchased" Visible="False">
        </EPiServer:Property>
    </div>
</asp:content>
<asp:content id="Content3" contentplaceholderid="PrimaryContentBottomSection" runat="server">
</asp:content>
<asp:content id="Content4" contentplaceholderid="BeforeCloseBodyContent" runat="server">
</asp:content>
