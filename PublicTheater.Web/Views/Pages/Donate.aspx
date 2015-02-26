<%@ Page Language="C#" MasterPageFile="~/Views/MasterPages/MasterPage.Master" AutoEventWireup="true"
     CodeBehind="Donate.aspx.cs" Inherits="PublicTheater.Web.Views.Pages.Donate" %>
<%@ Register TagPrefix="uc" TagName="HeroImageControl" Src="~/Views/Controls/HeroImageControl.ascx" %>
<%@ Register TagPrefix="uc" TagName="PageHeading" Src="~/Views/Controls/PageHeading.ascx" %>

<asp:content id="Content1" contentplaceholderid="Head" runat="server">
</asp:content>
<asp:content id="Content5" contentplaceholderid="beforeWrapper" runat="server">
    <%--<a class="joinButton">Join</a> <a class="renewButton">Renew</a>
    <h2 class="membershipCost">
        Membership - $60</h2>--%>
</asp:content>
<asp:content id="Content2" contentplaceholderid="MainContent" runat="server">
    <div class="generalWrapper">
        <uc:PageHeading runat="server" ID="propHeading"/>
        <uc:HeroImageControl runat="server" ID="HeroImg1"></uc:HeroImageControl>
        
        <div class="generalContentWrapper">
            <div class="row">
                <div class="large-9 medium-8 small-12">
    <div class="donateWrapper block noHeight">
        <div class="large-12 medium-12 small-12">
            <div class="donateImageBox">
                <EPiServer:Property runat="server" ID="donateImage" PropertyName="BannerImage">
                </EPiServer:Property>
                <EPiServer:Property runat="server" ID="donateImageText" PropertyName="BannerImageText">
                </EPiServer:Property>
            </div>
        </div>
        <div class="large-12 medium-12 small-12">
            <EPiServer:Property runat="server" ID="mainBodyProp" PropertyName="MainBody" CssClass="mainBody"
                CustomTagName="div">
            </EPiServer:Property>
        </div>
        <div class="large-12 medium-12 small-12">
            <asp:repeater runat="server" id="rptMembershipLevelCategories">
                <itemtemplate>
                    <div class="benefits">
                        <asp:Repeater runat="server" ID="rptMembershipLevels">
                            <HeaderTemplate>
                                <ul class="donateRadioButtons">
                            </HeaderTemplate>
                            <ItemTemplate>
                                <li class="donateRadio">
                                    <asp:RadioButton GroupName="donationAmount" ID="btnStartingDonationLevel" CssClass="cboxDonationAmount" runat="server" />
                                </li>
                            </ItemTemplate>
                            <FooterTemplate>
                                <li class="donateRadio">
                                    <asp:RadioButton runat="server" ClientIDMode="Static" ID="rdoOtherDonationAmount" CssClass="otherAmountClick" Text="Other" />
                                </li>
                                </ul>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
                </itemtemplate>
            </asp:repeater>
        </div>
        <div class="large-12 medium-12 small-12">
            <div class="otherAmountSection">
                <asp:label id="otherAmountTextboxLabel" runat="server" cssclass="otherLabel">Other Amount:</asp:label>
                <asp:textbox id="tboxDonationAmount" runat="server" cssclass="grayInput" text="0"
                    clientidmode="Static" />
                <asp:requiredfieldvalidator runat="server" id="valDonationAmtRequired" controltovalidate="tboxDonationAmount"
                    errormessage="Please enter a donation amount" validationgroup="DonationForm">*</asp:requiredfieldvalidator>
                <asp:customvalidator runat="server" id="valAmtIsNumerical" validationgroup="DonationForm"
                    onservervalidate="txtDonationAmt_ServerValidate" errormessage="Please enter a valid donation amount">*</asp:customvalidator>
            </div>
        </div>
        <div class="large-12 medium-12 small-12">
            <asp:button id="btnAddDonation" runat="server" cssclass="btn" text="Add to Cart"
                validationgroup="DonationForm" causesvalidation="true" onclick="btnAddDonation_Click">
            </asp:button>
        </div>
    </div>
</div>
    <div class="large-3 medium-4 small-12 height-Auto">
        <EPiServer:Property ID="Property3" runat="server" PropertyName="RightContent" DisplayMissingMessage="False">
            <RenderSettings ChildrenCssClass="generalRightRail"></RenderSettings>
        </EPiServer:Property>
    </div>
                </div>
            </div>
        </div>
</asp:content>
<asp:content id="Content3" contentplaceholderid="BeforeCloseBody" runat="server">
</asp:content>

