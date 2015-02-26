<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPages/Interior.master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="TheaterTemplate.Web.Pages.Account.MyProfile.index" %>
<%@ Register Src="~/controls/account/MyProfileControl.ascx" TagPrefix="account" TagName="MyProfileControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    
     <div class="accountNav row">
        <ul>
            <li><asp:HyperLink runat="server" ID="MyAccountBtn" NavigateUrl="/account/Ticket-History">Ticket History</asp:HyperLink></li>
            <li><asp:HyperLink  runat="server" ID="BenefitsBtn" NavigateUrl="/account/MyBenefits">Benefits</asp:HyperLink></li>
            <li class="active"><asp:HyperLink  runat="server" ID="MyProfileBtn" NavigateUrl="/account/MyProfile">My Profile</asp:HyperLink></li>
        </ul>
    </div>
    
    <div class="myProfileWrap">
        <div id="subscriptionBuilder">
            <account:MyProfileControl runat="server" ID="myProfile" />
        </div>
    </div>
</asp:Content>
