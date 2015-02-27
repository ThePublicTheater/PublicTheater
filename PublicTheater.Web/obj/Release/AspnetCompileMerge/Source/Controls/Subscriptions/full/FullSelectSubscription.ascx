<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FullSelectSubscription.ascx.cs" Inherits="TheaterTemplate.Web.Controls.Subscriptions.FullControls.FullSelectSubscription" %>

<asp:Repeater runat="server" ID="rptTabs">
    <HeaderTemplate>
        <div class="venueNames tabSet">
    </HeaderTemplate>
    <ItemTemplate>
        <div class="venueHeader tab">
            <asp:HiddenField runat="server" ID="venueId" />            
            <h2 runat="server" id="header">
                <asp:HyperLink runat="server" ID="lnkHeader" CssClass="venueTab" />
            </h2>
        </div>
    </ItemTemplate>
    <FooterTemplate>
        </div>
    </FooterTemplate>
</asp:Repeater>

<div class="fullError errorBox" style="display:none;">
    
</div>
<asp:HiddenField runat="server" ID="maxPerVenue" ClientIDMode="Static" />

<asp:Repeater runat="server" ID="rptTabbedPackages">
    <HeaderTemplate>
        <div class="venues">
    </HeaderTemplate>
    <ItemTemplate>
        <div class="venueRow row-fluid">
            <%-- USED FOR HIDE/SHOW/VALIDATION LOGIC --%>
            <div class="venueInfo" style="display:none;">
                <asp:HiddenField runat="server" ID="venueId" />
            </div>
            <div runat="server" id="allPackages" class="allPackages">
                <div class="numberAdded" style="display: none;">
                    <asp:HiddenField runat="server" ID="numberAdded" />
                </div>
                <asp:Repeater runat="server" ID="rptPackages">
                    <ItemTemplate>
                        <div class="packageRow">
                            <div class="packageInformation">
                                <h3>
                                    <asp:Literal runat="server" ID="ltrTitle" />
                                </h3>
                                <asp:HiddenField runat="server" ID="packageId" />
                            </div>
                            <div class="packagePerformances">
                                <asp:Repeater runat="server" ID="rptPackagePerformances">
                                    <HeaderTemplate>
                                        <ul class="performanceRow">
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <li>
                                            <strong><asp:Literal runat="server" ID="ltrPerformanceTitle" /></strong>
                                            <span><asp:Literal runat="server" ID="ltrPerformanceDate" /></span>
                                        </li>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </ul>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>
                            <div class="selectPackageContainer">
                                <asp:Hyperlink runat="server" ID="lnkSelectSubscription" CssClass="btn selectPackage" Text="Add to Package" />
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </ItemTemplate>
    <FooterTemplate>
        </div>
    </FooterTemplate>
</asp:Repeater>
