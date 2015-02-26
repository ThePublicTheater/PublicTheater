<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPages/Interior.master" AutoEventWireup="true" CodeBehind="conflict.aspx.cs" Inherits="TheaterTemplate.Web.Pages.Account.Promo.conflict" %>

<%@ Register Src="~/controls/account/PromoConflict.ascx" TagPrefix="account" TagName="PromoConflictControl" %>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PrimaryContent" runat="server">
<div id="subscriptionBuilder">
    <account:PromoConflictControl runat="server" ID="PromoConflictControl" />
</div>
</asp:Content>