<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReserveControl.ascx.cs"
    Inherits="PublicTheater.Web.Controls.Reserve.ReserveControl" %>
<%@ Register TagPrefix="uc" TagName="HtmlSyosControl" Src="~/Controls/reserve/HtmlSyosControl.ascx" %>
<%@ Register TagPrefix="uc" TagName="BestAvailable" Src="~/Controls/reserve/BestAvailable.ascx" %>
<%@ Register TagPrefix="uc" TagName="PromoBox" Src="~/Controls/account/PromoBox.ascx" %>
<link href="../../css/smoothness/jquery-ui-1.8.21.custom.css" rel="stylesheet" type="text/css" />
<div id="selectSeatHeader" data-bb="reserveControl">
    <div id="selectedPerformance">
        <asp:Panel runat="server" ID="pnlProductionThumbnail" CssClass="small-12 medium-5 large-4">
            <asp:Image runat="server" ID="ProductionThumbnail" />
        </asp:Panel>
        <div class="small-12 medium-7 large-8">
            <div class="performanceInfo">
                <h4>
                    <asp:HyperLink runat="server" ID="ProductionTitle">
                    </asp:HyperLink></h4>

                <h5 class="dateSelected"></h5>
               
                <h5>
                    <asp:Label runat="server" ID="VenueName"></asp:Label></h5>
                <br />
                 <h5>
                    <asp:Label runat="server" ID="pricePage"></asp:Label></h5>
                <asp:HiddenField ID="HfSelectedPerformanceId" runat="server" ClientIDMode="static" />
                <asp:HiddenField ID="HfSelectedProductionId" runat="server" ClientIDMode="static" />
                <a href="#" id="changePerformance" class="changePrefBtn btn" runat="server" clientidmode="Static">Choose Date/Time</a>
                <div class="calendarWrap">
                    <div id="changeDateDatepicker" style="clear: both;">
                    </div>
                    <div id="changeDateDatepickerTimes" style="display: none">
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="promoLinkWrapper">
        <div class="small-12 medium-12 large-12">
            <uc:PromoBox runat="server" ID="PromoBox" />
        </div>
    </div>
</div>


<asp:Panel ID="seatSelectionPanel" runat="server" ClientIDMode="Static">
<div class="syosWrapper">
    <div class="tabWrapper">
        <div class="small-12">
                <asp:Panel runat="server" ID="choiceToggle" ClientIDMode="Static">
                <ul class="nav-tabs tabs-2" id="myTab">
                    <li class="active"><a href="#syos-tab" id="chooseOwn">Choose My Own Seats</a></li>
                    <li><a href="#best-tab" id="chooseBest">Choose the Best Available</a></li>
                </ul>
                </asp:Panel>
        </div>
        <div class="tab-content">
            <!-- choose own seat section syos -->
            <div class="tab-pane" id="syos-tab">
                <div id="syosOnPage">
                    <uc:HtmlSyosControl runat="server" ID="HtmlSyosControl"></uc:HtmlSyosControl>
                </div>
            </div>
            <!-- choose best available -->
            <div class="tab-pane" id="best-tab">
                <div class="large-12 medium-12 small-12">
                    <div id="bestAvail">
                            <asp:UpdatePanel runat="server" ID="updatePnlBestAvailable">
                                <ContentTemplate>

                                    <uc:BestAvailable runat="server" ID="BestAvailableControl"></uc:BestAvailable>

                                    <div id="bestAvailablePanelControls">
                                        <asp:Button runat="server" Style="display: none;" ID="PerformanceChangeButton" OnClick="PerformanceChange_Click" ClientIDMode="static" CssClass="PerformanceChangeButton" />
                                        <asp:HiddenField runat="server" ID="HfIsSyosEnabled" Value="0" ClientIDMode="Static" />
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                    </div>
                    <EPiServer:Property runat="server" ID="SYOSSpecificMainBody" PropertyName="MainBody"
                        CssClass="syosSubCopy" DisplayMissingMessage="false" />
                    <EPiServer:Property runat="server" ID="SYOSMainBody" PropertyName="CustomProperty4"
                        CssClass="syosSubCopy" DisplayMissingMessage="false" />
                    <div class="reserveSubCopy">
                            <asp:Label runat="server" ID="ReservePageContent"></asp:Label>
                    </div>
                </div>
                
            </div>
        </div>
    </div>
    <!-- tabwrap -->
</div>
<!-- syos wrap-->
<div class="seatingTableWrap">
        <asp:HiddenField runat="server" ID="HfShowPromoMessaging" Value="0" ClientIDMode="Static" />
        <asp:HiddenField runat="server" ID="displayDateFormat" ClientIDMode="Static" />
        <asp:Panel runat="server" ID="pnlPromoDisplay" CssClass="promoDisplay">
        <div class="eligible">
                <asp:Label runat="server" ID="lblPromoPerformances" associatecontrol="ddlPromoPerformances"
                    Text="Promo eligible performances:"></asp:Label>
                <asp:DropDownList runat="server" ID="ddlPromoPerformances" CssClass="PromoPerfDropDown">
                </asp:DropDownList>
        </div>
        <EPiServer:Property runat="server" ID="PromoApplicableMessage" PropertyName="CustomProperty5"
            CssClass="promoMessage PromoApplicableMessage" Style="display: none;" />
        <EPiServer:Property runat="server" ID="PromoNotApplicableMessage" PropertyName="CustomProperty6"
            CssClass="promoMessage PromoNotApplicableMessage" Style="display: none;" />
        </asp:Panel>

                    <EPiServer:Property runat="server" ID="PerformanceNotAvailable" PropertyName="CustomProperty1" CssClass="promoMessage" />
                    <EPiServer:Property runat="server" ID="PerformanceSoldOut" PropertyName="CustomProperty2" CssClass="promoMessage" />
                    <asp:HiddenField runat="server" ID="HfDataIssue" ClientIDMode="Static" />
                    <asp:Button runat="server" Style="display: none;" ID="btnPerformanceChange" OnClick="PerformanceChange_Click" CssClass="PerformanceChangeButton" />

</div>
</asp:Panel>
<!-- seatingwrap -->
