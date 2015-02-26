<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPages/Interior.master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="TheaterTemplate.Web.Pages.Account.Forgot.index" %>

<%@ Register Src="~/controls/account/ForgotPasswordControl.ascx" TagPrefix="account" TagName="ForgotPasswordControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <account:ForgotPasswordControl runat="server" ID="ForgotPasswordControl" />

</asp:Content>