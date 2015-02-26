<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPages/Interior.master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="TheaterTemplate.Web.Pages.Account.Register.index" %>
<%@ Register Src="~/controls/account/RegisterControl.ascx" TagPrefix="account" TagName="RegisterControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <account:RegisterControl runat="server" ID="registerControl" />

</asp:Content>