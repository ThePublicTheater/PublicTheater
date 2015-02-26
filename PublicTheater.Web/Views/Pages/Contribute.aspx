<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Views/MasterPages/Interior.master" CodeBehind="Contribute.aspx.cs" Inherits="PublicTheater.Web.Views.Pages.Contribute" %>

<%@ Register Src="~/Controls/Contribute/ContributeControl.ascx" TagPrefix="uc" TagName="ContributeControl" %>
<%@ Register Src="~/controls/contribute/ContributeControlMembershipLevels.ascx" TagPrefix="uc" TagName="ContributeControlMembershipLevels" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="PrimaryContent" runat="server">

    <div class="indvGiveWrap">
        <asp:Panel runat="server" ID="pnlWrapper"> 
            <div class="large-8 medium-6 small-12 indvImgArea">
                <asp:Image runat="server" ID="imgDonationSmallBanner"/>
                <EPiServer:Property runat="server" ID="donationSmallBannerText" PropertyName="SmallBannerText" CssClass="donationSmallBannerText"></EPiServer:Property>
            </div>
            <div class="large-4 medium-6 small-12">
                <EPiServer:Property runat="server" ID="donationContactInfo" PropertyName="DonationContactInfo" CssClass="donationContactInfo"></EPiServer:Property>
                <%--<asp:Button runat="server" ID="btnRenewDonation" CssClass="btn solid renewDonation" Text="Renew a donation" />--%>
            </div>
        </asp:Panel>
    </div>

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="PrimaryContentBottomSection" runat="server">

    <div class="indvGiveWrap">
        <div class="interiorPage">
            <asp:Panel runat="server" ID="Panel1">
                <div class="supportUsSection">
                    <uc:ContributeControl ID="ucContribute" runat="server"/>
                </div>
               
                <div class="donorBenefits">
                    <uc:ContributeControlMembershipLevels ID="ucMembershipLevels" runat="server" />
                </div>
            </asp:Panel>
        </div>
    </div>

</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="BeforeCloseBodyContent" runat="server">
</asp:Content>

