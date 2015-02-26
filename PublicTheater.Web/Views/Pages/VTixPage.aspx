<%@ Page Language="c#" Inherits="PublicTheater.Web.Views.Pages.VTixPage" CodeBehind="VTixPage.aspx.cs" MasterPageFile="~/Views/MasterPages/MasterPage.Master" AutoEventWireup="true" %>
<%@ Import Namespace="PublicTheater.Custom.Episerver.Utility" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<%@ Register TagPrefix="uc" TagName="HeroImageControl_1" Src="~/Views/Controls/HeroImageControl.ascx" %>
<%@ Register TagPrefix="uc" TagName="headingControl" Src="~/Views/Controls/PageHeading.ascx" %>
<asp:content id="Content1" contentplaceholderid="Head" runat="server">
</asp:content>
<asp:content id="Content4" contentplaceholderid="beforeWrapper" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="MainContent" runat="server">
    <uc:HeroImageControl_1 runat="server" ID="HeroImg1">
    </uc:HeroImageControl_1>
    <div class="homeWrapper generalWrapper">
        
        <uc:headingControl runat="server" ID="propHeading"/>

        <div class="generalContentWrapper">
            <div class="row">
                <div class="large-9 medium-8 small-12">
                    <div class="block noHeight contentChunk">

                        <EPiServer:Property ID="State01" runat="server" PropertyName="State01">
                        </EPiServer:Property>
                        <EPiServer:Property ID="State02" runat="server" PropertyName="State02">
                        </EPiServer:Property>
                        <EPiServer:Property ID="State03" runat="server" PropertyName="State03">
                        </EPiServer:Property>
                        <div class="VEntryStep1">
                            <EPiServer:Property ID="Step1Body" runat="server" PropertyName="Entry1">
                            </EPiServer:Property>
                        </div>
                        <div class="VEntryStep2" style="display: none">
                            <EPiServer:Property ID="Step2Body" runat="server" PropertyName="Entry2">
                            </EPiServer:Property>
                        </div>
                        <div class="VEntryStep3" style="display: none">
                            <EPiServer:Property ID="Step3Body" runat="server" PropertyName="Entry3">
                            </EPiServer:Property>
                        </div>
                        <EPiServer:Property ID="State05" runat="server" PropertyName="State05">
                        </EPiServer:Property>
                        <EPiServer:Property ID="State06" runat="server" PropertyName="State06">
                        </EPiServer:Property>
                        <EPiServer:Property ID="State07" runat="server" PropertyName="State07">
                        </EPiServer:Property>
                        <EPiServer:Property ID="State08" runat="server" PropertyName="State08">
                        </EPiServer:Property>
                        <EPiServer:Property ID="State09" runat="server" PropertyName="State09">
                        </EPiServer:Property>
                        
                        <asp:LinkButton ID="lnkDummy" runat="server"></asp:LinkButton>
                        <asp:ModalPopupExtender runat="server" BehaviorID="seniorPopup" TargetControlID="lnkDummy" PopupControlID="seniorPopupPanel" DropShadow="false" RepositionMode="none" BackgroundCssClass="bgOverlay" />
                        <asp:Panel runat="server" ID="seniorPopupPanel" CssClass="selectNewSectionContainer"
                            Style="display: none;">
                            <div class="subsModalInner">
                                <div class="packageDescription">
                                    <h2>Seating Type Confirmation</h2>
                                </div>
                                <div class="subsModalContent">
                                    You have selected seating reserved for senior citizens (65+). 
                                    If you are selected to receive tickets, the person picking them up tonight will be required to present a photo ID with proof of age (65+).
                                    Do you attest that these tickets are for a senior citizen?
                                    <br />
                                </div>
                                <div class="continueBtn updateButton">
                                    <asp:LinkButton runat="server" CssClass="btn btnStandOut" OnClientClick="$find('seniorPopup').hide(); return false;" CausesValidation="false">Yes</asp:LinkButton>
                                    <asp:LinkButton runat="server" CssClass="btn btnStandOut" OnClientClick="window.resetVTixLine(); $find('seniorPopup').hide(); return false;" CausesValidation="false">No</asp:LinkButton>
                                </div>
                            </div>

                        </asp:Panel>
                        <asp:LinkButton ID="lnkDummy2" runat="server"></asp:LinkButton>
                        <asp:ModalPopupExtender runat="server" BehaviorID="adaPopup" TargetControlID="lnkDummy2" PopupControlID="adaPopupPanel" DropShadow="false" RepositionMode="none" BackgroundCssClass="bgOverlay" />
                        <asp:Panel runat="server" ID="adaPopupPanel" CssClass="selectNewSectionContainer"
                            Style="display: none;">
                            <div class="subsModalInner">
                                <div class="packageDescription">
                                    <h2>Seating Type Confirmation</h2>
                                </div>
                                <div class="subsModalContent">
                                   You have selected ADA Accessibility seating, reserved for the use of patrons with disabilities and their guests.
                                   If you are selected to receive tickets, more information about specific needs is available at the Delacorte Box Office when you pick up your tickets.
                                   Do you attest that you require ADA Accessibility seating?
                                    <br />
                                </div>
                                <div class="continueBtn updateButton">
                                    <asp:LinkButton runat="server" CssClass="btn btnStandOut" OnClientClick="$find('adaPopup').hide(); return false;" CausesValidation="false">Yes</asp:LinkButton>
                                    <asp:LinkButton runat="server" CssClass="btn btnStandOut" OnClientClick="window.resetVTixLine(); $find('adaPopup').hide(); return false;" CausesValidation="false">No</asp:LinkButton>
                                </div>
                            </div>

                        </asp:Panel>
                        <script type="text/javascript">
                            Sys.Browser.WebKit = {};
                            if (navigator.userAgent.indexOf('WebKit/') > -1) {
                                Sys.Browser.agent = Sys.Browser.WebKit;
                                Sys.Browser.version = parseFloat(navigator.userAgent.match(/WebKit\/(\d+(\.\d+)?)/)[1]);
                                Sys.Browser.name = 'WebKit';
                            }
                            function ShowSeniorPopup() {
                                $find("seniorPopup").show();
                                return false;
                            }
                            function ShowAdaPopup() {
                                $find("adaPopup").show();
                                return false;
                            }
                        </script>

                    </div>

                </div>
                <div class="large-3 medium-4 small-12 height-Auto">
                    <EPiServer:Property ID="Property3" runat="server" PropertyName="CallOuts">
                        <RenderSettings ChildrenCssClass="generalRightRail"></RenderSettings>
                    </EPiServer:Property>
                </div>
            </div>
        </div>
    </div>
</asp:content>
<asp:content id="Content3" contentplaceholderid="BeforeCloseBody" runat="server">
</asp:content>