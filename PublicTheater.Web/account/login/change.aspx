<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPages/Interior.master" AutoEventWireup="true" CodeBehind="change.aspx.cs" Inherits="TheaterTemplate.Web.Pages.Account.Login.change" %>

<%@ Register Src="~/controls/account/ChangePasswordControl.ascx" TagPrefix="account" TagName="ChangePasswordControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <div class="loginChange">
        <account:ChangePasswordControl runat="server" ID="ChangePasswordControl" />
    </div>

</asp:Content>