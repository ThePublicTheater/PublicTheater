<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPages/Interior.master" AutoEventWireup="true" CodeBehind="Print.aspx.cs" Inherits="TheaterTemplate.Web.account.history.Print" %>
<%@ Register TagPrefix="account" TagName="PrintTickets" Src="~/Controls/Account/PrintTicketsControl.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
<account:PrintTickets id="PrintTicketsControl" runat="server" />
</asp:Content>
