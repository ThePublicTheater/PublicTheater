<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ContributeControlMembershipLevels.ascx.cs" Inherits="PublicTheater.Web.Controls.ContributeControls.ContributeControlMembershipLevels" %>

<asp:Repeater runat="server" ID="rptMembershipLevelCategories">
    <HeaderTemplate>
        <%--<div class="donorBenefits">--%>
    </HeaderTemplate>
    <ItemTemplate>
        <h3 class="donationHeader" style="display: none;"><asp:Literal ID="CategoryTitle" runat="server" /></h3>
        <div class="benefits">
            <asp:Repeater runat="server" ID="rptMembershipLevels">
                <HeaderTemplate>
                    <ul class="thumbnails">
                </HeaderTemplate>
                <ItemTemplate>
                    <li class="span12">
                        <div class="level" data-target="donationLevel">
                            <asp:HiddenField ID="hdnStartingDonationLevel" runat="server"/>

                            <p class="levelTitle" data-target="donationSummaryArea" style="display: none;">
                                <strong class="donationTitle"><asp:Literal runat="server" ID="ltLevelTitle" /></strong>
                                <span><asp:Literal runat="server" ID="ltAmountRange" /></span>
                            </p>

                            <div class="donorInfo" style="display: none;" data-target="donationDescriptionArea">
                                <asp:Literal runat="server" ID="ltLevelDescription" />
                            </div>
                        </div>
                    </li>
                </ItemTemplate>
                <FooterTemplate>
                    </ul>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </ItemTemplate>
    <FooterTemplate>
        <%--</div>--%>
    </FooterTemplate>
</asp:Repeater>

<%--<asp:Repeater runat="server" ID="rptMembershipLevelCategories">
    <HeaderTemplate>
        <div class="donorBenefits">
    </HeaderTemplate>
    <ItemTemplate>
        <p class="lead">
            <asp:Literal ID="CategoryTitle" runat="server" />
        </p>
        <div class="benefits">
            <asp:Repeater runat="server" ID="rptMembershipLevels">
                <HeaderTemplate>
                    <ul class="thumbnails">
                </HeaderTemplate>
                <ItemTemplate>
                    <li class="span4">
                        <div class="thumbnail">
                            <div class="level" data-target="donationLevel">
                                <asp:HiddenField ID="hdnStartingDonationLevel" runat="server"/>
                                <p>
                                    <strong><asp:Literal runat="server" ID="ltLevelTitle" /></strong>
                                </p>
                                <div class="donorInfo">
                                    <ul>
                                        <li><asp:Literal runat="server" ID="ltAmountRange" /></li>
                                        <li><asp:Literal runat="server" ID="ltLevelDescription" /></li>
                                    </ul>
                                    <div class="clearfix">
                                        <a href="#" class="pull-right btn btn-small" data-action="setStartingLevel">Set Starting Level</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </li>
                </ItemTemplate>
                <FooterTemplate>
                    </ul>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </ItemTemplate>
    <FooterTemplate>
        </div>
    </FooterTemplate>
</asp:Repeater>--%>