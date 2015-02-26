<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPages/Interior.master" AutoEventWireup="true" CodeBehind="editRenewal.aspx.cs" Inherits="TheaterTemplate.Web.Pages.Subscriptions.Full.editRenewal" %>
<%@ Register Src="~/controls/subscriptions/full/EditRenewal.ascx" TagPrefix="adg" TagName="editRenewal" %>

<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <div id="subscriptionBuilder" class="renewalEdit">
        <adg:editRenewal runat="server" id="editRenewalControl" RenewalEditJavaScriptFile="/js/full/editRenewal.js" />
    </div>
</asp:Content>
