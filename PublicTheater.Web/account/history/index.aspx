<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPages/Interior.master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="TheaterTemplate.Web.Pages.Account.History.index" %>
<%@ Register TagPrefix="account" TagName="TicketHistory" Src="~/Controls/Account/TicketHistoryControl.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <div class="accountNav row">
        <ul>
            <li class="active"><asp:HyperLink runat="server" ID="MyAccountBtn" NavigateUrl="/account/Ticket-History">Ticket History</asp:HyperLink></li>
            <li><asp:HyperLink  runat="server" ID="BenefitsBtn" NavigateUrl="/account/MyBenefits">Benefits</asp:HyperLink></li>
            <li><asp:HyperLink  runat="server" ID="MyProfileBtn" NavigateUrl="/account/MyProfile">My Profile</asp:HyperLink></li>
        </ul>
    </div>

    <div id="subscriptionBuilder">    
        <account:TicketHistory runat="server" ID="accountTicketHistory" />
    </div>
</asp:Content>
