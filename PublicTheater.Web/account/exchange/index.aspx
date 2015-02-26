<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPages/Interior.master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="TheaterTemplate.Web.Pages.Account.Exchange.index" %>
<%@ Register TagPrefix="account" TagName="Exchange" Src="~/Controls/Account/ExchangeControl.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <div id="subscriptionBuilder">    
        <account:Exchange runat="server" ID="exchangeOptions" />
    </div>
</asp:Content>
