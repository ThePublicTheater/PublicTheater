<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPages/Interior.master"
    AutoEventWireup="true" CodeBehind="Benefits.aspx.cs" Inherits="PublicTheater.Web.Views.Pages.Benefits" %>

<asp:content id="Content1" contentplaceholderid="HeadContent" runat="server">
</asp:content>
<asp:content id="Content3" contentplaceholderid="PrimaryContent" runat="server">
    <div class="accountNav row">
        <ul>
            <li>
                <asp:hyperlink runat="server" id="MyAccountBtn" navigateurl="/account/Ticket-History">Ticket History</asp:hyperlink></li>
            <li class="active">
                <asp:hyperlink runat="server" id="BenefitsBtn" navigateurl="/account/MyBenefits">Benefits</asp:hyperlink></li>
            <li>
                <asp:hyperlink runat="server" id="MyProfileBtn" navigateurl="/account/MyProfile">My Profile</asp:hyperlink></li>
        </ul>
    </div>
    <div class="benefitsPage">
        <div class="memberInfo">
            <h2>
                <asp:literal runat="server" id="litCustomerName"></asp:literal></h2>
            <h4>
                Patron ID:
                <asp:literal runat="server" id="litCustomerNo"></asp:literal></h4>
            <asp:panel runat="server" id="pnlMemberStatus">
                <ul class="large-9 medium-8 small-12">
                    <li class="large-4 medium-4 small-12">
                        <asp:literal runat="server" id="litMemberLevel"></asp:literal></li>
                    <li class="large-4 medium-4 small-12">
                        <asp:literal runat="server" id="litExpDate"></asp:literal></li>
                    <li class="large-4 medium-4 small-12">
                        <asp:literal runat="server" id="litMemberStatus"></asp:literal></li>
                </ul>
                <span class="large-3 medium-4 small-12">
                    <asp:hyperlink runat="server" id="lnkRenewBtnBenefits" cssclass="btn solid">Renew Now!</asp:hyperlink>
                    <asp:literal runat="server" id="litRenewed" visible="False"></asp:literal>
                </span>
            </asp:panel>
        </div>
    </div>
</asp:content>
<asp:content id="Content2" contentplaceholderid="PrimaryContentBottomSection" runat="server">
    <div class="interiorPage benefitsPage">
        <div class="section-auto-sample-accordion" data-section="accordion">
    <script type="text/javascript">
        function openAccordian() {
            $(".accordRow:contains('" + (function (a) {
                if (a == "") return {};
                var b = {};
                for (var i = 0; i < a.length; ++i) {
                    var p = a[i].split('=');
                    if (p.length != 2) continue;
                    b[p[0]] = decodeURIComponent(p[1].replace(/\+/g, " "));
                }
                return b;
            })(window.location.search.substr(1).split('&')).position + "')").addClass('active');
        }

        $(openAccordian);

    </script>


            <section class="accordRow active">

            <div class="title" data-section-title><a href="#section1">Benefits for Current Membership</a></div>
           
            <div class="content" data-slug="section1" data-section-content>
        <%--Downtown Tickets--%>
        <asp:panel runat="server" id="pnlDTOverview" cssclass="tableSection noPadding">
            <ul class="tableHead">
                <li class="large-4 medium-4 small-4">Tickets at Astor Place</li>
                <li class="large-8 medium-8 small-8">
                    <ul>
                        <li class="large-4 medium-4 small-4">Total</li>
                        <li class="large-4 medium-4 small-4">Used</li>
                        <li class="large-4 medium-4 small-4">Remaining</li>
                    </ul>
                </li>
            </ul>
            <asp:repeater runat="server" id="rptDTBenefits">
                <itemtemplate>
                 <ul class="tableRow">
                    <li class="large-4 medium-4 small-4">
                        <asp:literal runat="server" id="litBenefits"></asp:literal>
                    </li>
                    <li class="large-8 medium-8 small-8">
                        <ul>
                            <li class="large-4 medium-4 small-4"><asp:literal runat="server" id="litTotal">-</asp:literal></li>
                            <li class="large-4 medium-4 small-4"><asp:literal runat="server" id="litUsed">-</asp:literal></li>
                            <li class="large-4 medium-4 small-4"><asp:literal runat="server" id="litRemaining">-</asp:literal></li>
                        </ul>
                    </li>
                </ul>   
                </itemtemplate>
            </asp:repeater>
        </asp:panel>
        <%--Downtown details--%>
        <asp:panel runat="server" id="pnlDTBenefitProductions" cssclass="tableSection">
            <ul class="tableHead">
                <li class="large-4 medium-4 small-4">
                    <asp:literal runat="server" id="litDTBenefitProductionsTitle">Tickets at Astor Place</asp:literal>
                </li>
                <asp:repeater runat="server" id="rptDTBenefitPriceTypes">
                    <itemtemplate>
                     <li class="large-4 medium-4 small-4 tableBreak">
                         <asp:literal runat="server" id="litPriceTypeName"></asp:literal>
                         <asp:Label runat="server" id="lbPriceTypeDesc" CssClass="explanationText"></asp:Label>
                         <ul>
                            <li class="small-6 medium-6 large-6">Used</li>
                            <li class="small-6 medium-6 large-6">Remaining</li>
                        </ul>
                    </li>   
                    </itemtemplate>
                </asp:repeater>
            </ul>
            <asp:repeater runat="server" id="rptDTBenefitProductions">
                <itemtemplate>
                    <ul class="tableRow">
                        <li class="large-4 medium-4 small-4">
                            <asp:HyperLink runat="server" ID="lnkProduction"></asp:HyperLink>
                        </li>
                        <asp:Repeater runat="server" ID="rptDTBenefitProductionPriceTypes">
                            <ItemTemplate>
                            <li class="large-4 medium-4 small-4 tableBreak">
                                <ul>
                                    <li class="small-6 medium-6 large-6"><asp:literal runat="server" id="litUsed">-</asp:literal></li>
                                    <li class="small-6 medium-6 large-6"><asp:literal runat="server" id="litRemaining">-</asp:literal></li>
                                </ul>
                            </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </itemtemplate>
            </asp:repeater>
            <EPiServer:Property ID="propNoDTUsage" runat="server" DisplayMissingMessage="False"
            Visible="False" PropertyName="NoDowntownUsage" />
        </asp:panel>
        
        <%--Shakespeare--%>
        <asp:panel runat="server" id="pnlSITPBenefits" cssclass="tableSection">
            <ul class="tableHead">
                <li class="large-4 medium-4 small-4">Shakespeare in the Park</li>
                <li class="large-8 medium-8 small-8">
                    <ul>
                        <li class="large-4 medium-4 small-4">Total</li>
                        <li class="large-4 medium-4 small-4">Used</li>
                        <li class="large-4 medium-4 small-4">Remaining</li>
                    </ul>
                </li>
            </ul>
            <asp:repeater runat="server" id="rptSITPBenefits">
                <itemtemplate>
                 <ul class="tableRow">
                    <li class="large-4 medium-4 small-4">
                        <asp:literal runat="server" id="litBenefits"></asp:literal>
                    </li>
                    <li class="large-8 medium-8 small-8">
                        <ul>
                            <li class="large-4 medium-4 small-4"><asp:literal runat="server" id="litTotal">-</asp:literal></li>
                            <li class="large-4 medium-4 small-4"><asp:literal runat="server" id="litUsed">-</asp:literal></li>
                            <li class="large-4 medium-4 small-4"><asp:literal runat="server" id="litRemaining">-</asp:literal></li>
                        </ul>
                    </li>
                </ul>   
                </itemtemplate>
            </asp:repeater>
        </asp:panel>
                </div>
                </section>
            <asp:Panel runat="server" ID="pendingPanel">
        <%-- PENDING TEST --%>
        <div class="section-auto-sample-accordion" data-section="accordion">
    <script type="text/javascript">
        function openAccordian() {
            $(".accordRow:contains('" + (function (a) {
                if (a == "") return {};
                var b = {};
                for (var i = 0; i < a.length; ++i) {
                    var p = a[i].split('=');
                    if (p.length != 2) continue;
                    b[p[0]] = decodeURIComponent(p[1].replace(/\+/g, " "));
                }
                return b;
            })(window.location.search.substr(1).split('&')).position + "')").addClass('active');
        }

        $(openAccordian);

    </script>


            <section class="accordRow">

            <div class="title" data-section-title><a href="#section0">  <asp:Literal runat="server" ID="PendingHeader">Benefits for Pending Membership</asp:Literal></a></div>
           
            <div class="content" data-slug="section0" data-section-content>
                         <%--Downtown Tickets--%>
        <asp:panel runat="server" id="pnlPendingDTOverview" cssclass="tableSection noPadding">
            <ul class="tableHead">
                <li class="large-4 medium-4 small-4">Tickets at Astor Place</li>
                <li class="large-8 medium-8 small-8">
                    <ul>
                        <li class="large-4 medium-4 small-4">Total</li>
                        <li class="large-4 medium-4 small-4">Used</li>
                        <li class="large-4 medium-4 small-4">Remaining</li>
                    </ul>
                </li>
            </ul>
            <asp:repeater runat="server" id="rptPendingDTBenefits">
                <itemtemplate>
                 <ul class="tableRow">
                    <li class="large-4 medium-4 small-4">
                        <asp:literal runat="server" id="litBenefits"></asp:literal>
                    </li>
                    <li class="large-8 medium-8 small-8">
                        <ul>
                            <li class="large-4 medium-4 small-4"><asp:literal runat="server" id="litTotal">-</asp:literal></li>
                            <li class="large-4 medium-4 small-4"><asp:literal runat="server" id="litUsed">-</asp:literal></li>
                            <li class="large-4 medium-4 small-4"><asp:literal runat="server" id="litRemaining">-</asp:literal></li>
                        </ul>
                    </li>
                </ul>   
                </itemtemplate>
            </asp:repeater>
        </asp:panel>
        <%--Downtown details--%>
        <asp:panel runat="server" id="pnlPendingDTBenefitProductions" cssclass="tableSection">
            <ul class="tableHead">
                <li class="large-4 medium-4 small-4">
                    <asp:literal runat="server" id="litPendingDTBenefitProductionsTitle">Tickets at Astor Place</asp:literal>
                </li>
                <asp:repeater runat="server" id="rptPendingDTBenefitPriceTypes">
                    <itemtemplate>
                     <li class="large-4 medium-4 small-4 tableBreak">
                         <asp:literal runat="server" id="litPriceTypeName"></asp:literal>
                         <asp:Label runat="server" id="lbPriceTypeDesc" CssClass="explanationText"></asp:Label>
                         <ul>
                            <li class="small-6 medium-6 large-6">Used</li>
                            <li class="small-6 medium-6 large-6">Remaining</li>
                        </ul>
                    </li>   
                    </itemtemplate>
                </asp:repeater>
            </ul>
            <asp:repeater runat="server" id="rptPendingDTBenefitProductions">
                <itemtemplate>
                    <ul class="tableRow">
                        <li class="large-4 medium-4 small-4">
                            <asp:HyperLink runat="server" ID="lnkProduction"></asp:HyperLink>
                        </li>
                        <asp:Repeater runat="server" ID="rptDTBenefitProductionPriceTypes">
                            <ItemTemplate>
                            <li class="large-4 medium-4 small-4 tableBreak">
                                <ul>
                                    <li class="small-6 medium-6 large-6"><asp:literal runat="server" id="litUsed">-</asp:literal></li>
                                    <li class="small-6 medium-6 large-6"><asp:literal runat="server" id="litRemaining">-</asp:literal></li>
                                </ul>
                            </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </itemtemplate>
            </asp:repeater>
            <EPiServer:Property ID="propNoPendingDTUsage" runat="server" DisplayMissingMessage="False"
            Visible="False" PropertyName="NoDowntownUsage" />
            <%--<EPiServer:Property ID="propNoPendingDTUsage" runat="server" DisplayMissingMessage="False"
            Visible="False" PropertyName="NoPendingDowntownUsage" />--%>
        </asp:panel>
        
        
        
        

                <%--Shakespeare--%>
        <asp:panel runat="server" id="pnlPendingSITPBenefits" cssclass="tableSection">
            <ul class="tableHead">
                <li class="large-4 medium-4 small-4">Shakespeare in the Park</li>
                <li class="large-8 medium-8 small-8">
                    <ul>
                        <li class="large-4 medium-4 small-4">Total</li>
                        <li class="large-4 medium-4 small-4">Used</li>
                        <li class="large-4 medium-4 small-4">Remaining</li>
                    </ul>
                </li>
            </ul>
            <asp:repeater runat="server" id="rptPendingSITPBenefits">
                <itemtemplate>
                 <ul class="tableRow">
                    <li class="large-4 medium-4 small-4">
                        <asp:literal runat="server" id="litBenefits"></asp:literal>
                    </li>
                    <li class="large-8 medium-8 small-8">
                        <ul>
                            <li class="large-4 medium-4 small-4"><asp:literal runat="server" id="litTotal">-</asp:literal></li>
                            <li class="large-4 medium-4 small-4"><asp:literal runat="server" id="litUsed">-</asp:literal></li>
                            <li class="large-4 medium-4 small-4"><asp:literal runat="server" id="litRemaining">-</asp:literal></li>
                        </ul>
                    </li>
                </ul>   
                </itemtemplate>
            </asp:repeater>
        </asp:panel>

            </div>
                </section>

</div>
       </asp:Panel>
        <asp:panel runat="server" id="pnlAvailableProductions" cssclass="redeemSection large-8 medium-12 small-12">
            <h3>
                Redeem Your Benefits for These Shows Now On Sale:</h3>
            <ul>
                <asp:repeater runat="server" id="rptAvailableProductions">
                    <itemtemplate>
                    <li>
                        <asp:HyperLink runat="server" ID="lnkPDP"></asp:HyperLink>
                    </li>    
                    </itemtemplate>
                </asp:repeater>
            </ul>
        </asp:panel>
        <EPiServer:Property ID="propNoUpcomingPerformancesMessage" runat="server" DisplayMissingMessage="False" Visible="False"
            PropertyName="NoUpcomingPerformancesMessage" CssClass="redeemSection large-8 medium-12 small-12" />
        
        <EPiServer:Property ID="propMessage" runat="server" DisplayMissingMessage="False"
            Visible="False" CssClass="BenefitsOpenText" />
        <EPiServer:Property ID="propProgramContentBox" runat="server" DisplayMissingMessage="False"
            PropertyName="ProgramContentBox" CssClass="BenefitsOpenText" />
    </div>
</asp:content>
<asp:content id="Content6" contentplaceholderid="BeforeCloseBodyContent" runat="server">
</asp:content>
