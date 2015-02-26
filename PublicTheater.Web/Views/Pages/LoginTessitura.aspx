<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPages/Interior.Master"
    AutoEventWireup="true" CodeBehind="LoginTessitura.aspx.cs" Inherits="TheaterTemplate.Web.Views.Pages.LoginTessitura" %>
<%@ Register Src="~/controls/account/LoginControl.ascx" TagPrefix="account" TagName="LoginControl" %>

<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <account:LoginControl runat="server" ID="loginControl" RememberMeEnabled="true"/>
</asp:Content>
