<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPages/Interior.master" AutoEventWireup="true" CodeBehind="reset.aspx.cs" Inherits="TheaterTemplate.Web.Pages.Account.Forgot.reset" %>

<%@ Register Src="~/controls/account/ResetPasswordControl.ascx" TagPrefix="account" TagName="ResetPasswordControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <account:ResetPasswordControl runat="server" ID="ResetPasswordControl" />

</asp:Content>