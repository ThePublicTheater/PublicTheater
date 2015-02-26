<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FullSelectSeating.ascx.cs" Inherits="TheaterTemplate.Web.Controls.Subscriptions.FullControls.FullSelectSeating" %>
<%@ Register Src="~/controls/subscriptions/full/FullSelectNewSeating.ascx" TagPrefix="subscription" TagName="SelectNewSection" %>

<asp:UpdatePanel runat="server" ID="pnlSelectSeating">
    <ContentTemplate>
        <asp:Button runat="server" ID="btnSectionChanged" CssClass="sectionChanged" Text="SectionChanged" Style="display: none;" />
        <asp:HiddenField runat="server" ID="selectedSectionId" ClientIDMode="Static" />
        <asp:HiddenField runat="server" ID="selectedVenueId" ClientIDMode="Static" />
        <asp:HiddenField runat="server" ID="masterPackageId" />
         <div id="selectSeatingContainer" class="row-fluid">
            <div id="packageDisplayContainer">
                <div id="packageDisplay">
                    <%--<EPiServer:Property runat="server" ID="tessituraHeader" PropertyName="Header" CssClass="packageDescription" Visible="false"  />--%>
    
                    <div class="packageArea">
                        <div class="subsFlexHeader">
                            <div class="flexGuidelines">
                                <h2>
                                    <asp:Literal runat="server" ID="ltrPackageDescription" />
                                </h2>
                            </div>
                        </div>

                        <div class="miniCart">
                            <asp:Repeater runat="server" ID="rptPackages">
                                <HeaderTemplate>
                                    <table border="0" cellspacing="0" class="packageCart">
                                        <tbody>
                                            <tr class="headerTr">
                                                <td>
                                                    <div class="headerLine" >
                                                        &nbsp;
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="headerLine">
                                                        Subscription
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="headerLine">
                                                        <span class="subDateLine">Play</span>
                                                        <span class="subsPref">Date</span>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="headerLine">
                                                       Seating
                                                    </div>
                                                </td>
                                            </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class="perfRow fullPackage"> 
                                        <td>
                                            <asp:LinkButton runat="server" ID="lnkEdit" Text="Edit" Visible="false" CommandName="Edit"
                                                CssClass="edit" />
                                        </td> 
                                        <td class="packageInfo">
                                            <asp:Literal runat="server" ID="ltrPackageTitle" />
                                        </td>
                                        <td class="packagePerformances">
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
                                        </td>
                                        <td class="priceTypes">
                                            <span>
                                                <asp:Literal runat="server" ID="ltrPriceTypes" />
                                            </span>
                                            <br />
                                            <span>
                                                <asp:Literal runat="server" ID="ltrTotalPrice" />                                        
                                            </span>
                                            <br />
                                            <span>
                                                <asp:Literal runat="server" ID="ltrSection" />
                                            </span>
                                        </td>
                                    </tr>
                                </ItemTemplate>  
                                <FooterTemplate>
                                        </tbody>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                    <div class="clearfix flexBtnContainer">
                        <div class="continueBtn">
                            <asp:LinkButton runat="server" ID="btnContinueToCart" Text="Continue to Cart" CssClass="btn btnStandOut" />
                        </div>
                    </div>
                </div>
            </div>

            <div id="selectSeatingArea">
                <div class="packageDescription">
                    <h2>Select Seating</h2>
                </div>
                
                    <%--<EPiServer:Property runat="server" ID="tessituraHeader" PropertyName="Header" CssClass="packageDescription" />--%>

                <asp:Repeater runat="server" ID="rptVenue" ClientIDMode="AutoID">
                    <ItemTemplate>
                        <div class="theaterContainer">
                            <asp:HiddenField runat="server" ID="currentVenueRowId" />                                                    
                            <div class="theaterLeftColumn">
                                <h3>
                                    <asp:Literal runat="server" ID="ltrVenueName" />
                                </h3>
                                <p id="selectSeatingTag" runat="server">
                                    Select your seating for:
                                </p>
                                <asp:Repeater runat="server" ID="rptVenuePackages">
                                    <HeaderTemplate>
                                        <ul class="unstyled">
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <li>
                                            <asp:Literal runat="server" ID="ltrPackageName" />
                                        </li>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </ul>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>
                            <div class="theaterSectionSelection">
                                <asp:Repeater runat="server" ID="rptSections">
                                    <HeaderTemplate>
                                        <div class="sectionContainer">
                                            <div class="theaterSectionList">
                                                <ul class="unstyled">
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <li runat="server" id="radioContainer">
                                            <asp:HiddenField runat="server" ID="venueId" />
                                            <asp:HiddenField runat="server" ID="sectionId" />
                                            <label class="radio">
                                            <asp:RadioButton runat="server" ID="rbSection" />
                                                <asp:Label ID="seatingLabel" runat="server" AssociatedControlID="rbSection">
                                                    <span class="prices">
                                                        <asp:Literal runat="server" ID="ltrSectionPrice" />
                                                    </span> 
                                                    <strong>
                                                        <asp:Literal runat="server" ID="ltrSectionName" />
                                                    </strong>
                                                    <asp:Literal runat="server" ID="ltrSectionDescription" />
                                                </asp:Label>
                                            </label>
                                        </li>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                                </ul> 
                                                <small>Prices are per ticket</small> 
                                                <asp:Literal runat="server" ID="ltrAdditionalVenueInfo" />
                                            </div> 
                                        </div>
                                        <div class="seatingMapContainer">
                                            <asp:Image runat="server" ID="imgTheater" AlternateText="Theater Image" CssClass="seatMapImg" />
                                        </div>
                                    </FooterTemplate>
                                </asp:Repeater>
                                <asp:PlaceHolder runat="server" ID="phGeneralAdmin" Visible="false">
                                    <div class="generalAdminDescription">
                                        <asp:Literal runat="server" ID="ltrGenAdminAdditionalInfo" /> 
                                    </div>
                                    <div class="generalAdminPerformances">
                                        <div class="successGeneralAdmin">
                                            <h3>
                                                Confirmed Packages
                                            </h3>
                                            <asp:Repeater runat="server" ID="rptGeneralAdminPackages">
                                                <HeaderTemplate>
                                                    <ul class="unstyled">
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <li>
                                                        <strong><asp:Literal runat="server" ID="ltrPackageName" /></strong>
                                                        <asp:Literal runat="server" ID="ltrVenue" /><br />
                                                        <asp:Literal runat="server" ID="ltrPriceTypes" /><br />
                                                        <asp:Literal runat="server" ID="ltrPrice" /><br />
                                                    </li>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    </ul>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </div>
                                        <asp:Panel runat="server" ID="pnlFailedPackages" CssClass="failedGeneralAdmin"
                                            Visible="false">
                                            <h3>
                                                We were unable to seat you for the following package(s). Please select new date(s).</h3>
                                            <asp:Repeater runat="server" ID="rptGeneralAdminSoldOutPackages">
                                                <HeaderTemplate>
                                                    <ul>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <li>
                                                        <div>
                                                            <strong>
                                                                <asp:Literal runat="server" ID="ltrPackageName" /></strong>
                                                            <asp:Literal runat="server" ID="ltrVenue" />
                                                        </div>
                                                        <div>
                                                            <asp:Literal runat="server" ID="ltrPriceTypes" />
                                                            <asp:HiddenField runat="server" ID="failedPackageId" />
                                                            <asp:HiddenField runat="server" ID="originalSectionId" />
                                                        </div>
                                                    </li>
                                                    <li class="availablePerformances">
                                                        <asp:ListBox runat="server" ID="lbxAvailableGeneralAdmin" DataTextField="Description"
                                                            DataValueField="PackageId" DataTextFormatString="{0:dddd, MMM d, h:mmtt}" />
                                                    </li>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    </ul>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                            <div class="generalAdminUpdate">
                                                <asp:LinkButton runat="server" ID="lbUpdateGeneralAdmin" Text="Update Selections"
                                                    CssClass="btn btnStandOut generalAdminBtn" CommandName="GenAdminUpdate" />
                                            </div>
                                        </asp:Panel>
                                    </div>
                                </asp:PlaceHolder>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
        <subscription:SelectNewSection runat="server" ID="subsSelectNewSection" />
        <div class="loadingContainer bgOverlay">
            <asp:Image runat="server" ID="loadingGif" ImageUrl="~/images/ajax-loader.gif" CssClass="loadingSpinner" />
        </div>
        <asp:Panel runat="server" ID="pnlError" CssClass="fullError errorBox" Visible="false">
            <asp:Literal runat="server" ID="ltrError" />
        </asp:Panel>
        
    </ContentTemplate>
</asp:UpdatePanel>


<%--<asp:UpdatePanel runat="server" ID="pnlSelectSeating">
    <ContentTemplate>
        <asp:Button runat="server" ID="btnSectionChanged" CssClass="sectionChanged" Text="SectionChanged" Style="display: none;" />
        <asp:HiddenField runat="server" ID="selectedSectionId" ClientIDMode="Static" />
        <asp:HiddenField runat="server" ID="selectedVenueId" ClientIDMode="Static" />
        <asp:HiddenField runat="server" ID="masterPackageId" />
        <div id="selectSeatingContainer">
            <div id="packageDisplayContainer">
                <div id="packageDisplay">
                    <div class="packageDescription">            
                        <h2>
                            Select Seating
                        </h2>
                    </div>            
                    <div class="packageArea">
                        <div class="flexGuidelines">
                            <h3>
                                <asp:Literal runat="server" ID="ltrPackageDescription" Text="Master Package" />  
                            </h3>
                        </div>
                        <div class="miniCart">
                            <asp:Repeater runat="server" ID="rptPackages">
                                <HeaderTemplate>
                                    <table cellspacing="0" border="0" class="packageCart">
                                        <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td class="packageInfo">
                                            <asp:Literal runat="server" ID="ltrPackageTitle" />
                                        </td>
                                        <td class="packagePerformances">
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
                                        </td>
                                        <td class="priceTypes">
                                            <span>
                                                <asp:Literal runat="server" ID="ltrPriceTypes" />
                                            </span>
                                            <br />
                                            <span>
                                                <asp:Literal runat="server" ID="ltrTotalPrice" />                                        
                                            </span>
                                            <br />
                                            <span>
                                                <asp:Literal runat="server" ID="ltrSection" />
                                            </span>
                                        </td>
                                    </tr>
                                </ItemTemplate>  
                                <FooterTemplate>
                                        </tbody>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                    <div class="continueBtn">
                        <asp:LinkButton runat="server" ID="btnContinueToCart" Text="Continue to Cart"
                            CssClass="btn btnStandOut" />
                    </div>
                </div>
            </div>
            <div id="selectSeatingArea">
                <asp:Repeater runat="server" ID="rptVenue" ClientIDMode="AutoID">
                    <ItemTemplate>
                        <div class="theaterContainer">
                            <asp:HiddenField runat="server" ID="currentVenueRowId" />                                                    
                            <div class="theaterLeftColumn">
                                <h3>
                                    <asp:Literal runat="server" ID="ltrVenueName" />
                                </h3>
                                <p id="selectSeatingTag" runat="server">
                                    Select your seating for:
                                </p>
                                <asp:Repeater runat="server" ID="rptVenuePackages">
                                    <HeaderTemplate>
                                        <ul>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <li>
                                            <asp:Literal runat="server" ID="ltrPackageName" />
                                        </li>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </ul>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>
                            <div class="theaterSectionSelection">
                                <asp:Repeater runat="server" ID="rptSections">
                                    <HeaderTemplate>
                                        <div class="sectionContainer">
                                            <div class="theaterSectionList">
                                                <ul>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <li runat="server" id="radioContainer">
                                            <asp:HiddenField runat="server" ID="venueId" />
                                            <asp:HiddenField runat="server" ID="sectionId" />
                                            <asp:RadioButton runat="server" ID="rbSection" />
                                            <label runat="server" id="forRbSection">
                                                <span class="prices">
                                                    <asp:Literal runat="server" ID="ltrSectionPrice" />
                                                </span> 
                                                <strong>
                                                    <asp:Literal runat="server" ID="ltrSectionName" />
                                                </strong>
                                                <asp:Literal runat="server" ID="ltrSectionDescription" />
                                            </label>
                                        </li>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                                </ul> 
                                                <small>Prices are per ticket</small> 
                                                <asp:Literal runat="server" ID="ltrAdditionalVenueInfo" />
                                            </div> 
                                        </div>
                                        <asp:Image runat="server" ID="imgTheater" AlternateText="Theater Image" CssClass="seatMapImg" />
                                    </FooterTemplate>
                                </asp:Repeater>
                                <asp:PlaceHolder runat="server" ID="phGeneralAdmin" Visible="false">
                                    <div class="generalAdminDescription">
                                        <asp:Literal runat="server" ID="ltrGenAdminAdditionalInfo" /> 
                                    </div>
                                    <div class="generalAdminPerformances">
                                        <div class="successGeneralAdmin">
                                            <h3>
                                                Confirmed Packages
                                            </h3>
                                            <asp:Repeater runat="server" ID="rptGeneralAdminPackages">
                                                <HeaderTemplate>
                                                    <ul>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <li>
                                                        <strong><asp:Literal runat="server" ID="ltrPackageName" /></strong>
                                                        <asp:Literal runat="server" ID="ltrVenue" /><br />
                                                        <asp:Literal runat="server" ID="ltrPriceTypes" /><br />
                                                        <asp:Literal runat="server" ID="ltrPrice" /><br />
                                                    </li>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    </ul>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </div>
                                        <asp:Panel runat="server" ID="pnlFailedPackages" CssClass="failedGeneralAdmin"
                                            Visible="false">
                                            <h3>
                                                We were unable to seat you for the following package(s). Please select new date(s).</h3>
                                            <asp:Repeater runat="server" ID="rptGeneralAdminSoldOutPackages">
                                                <HeaderTemplate>
                                                    <ul>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <li>
                                                        <div>
                                                            <strong>
                                                                <asp:Literal runat="server" ID="ltrPackageName" /></strong>
                                                            <asp:Literal runat="server" ID="ltrVenue" />
                                                        </div>
                                                        <div>
                                                            <asp:Literal runat="server" ID="ltrPriceTypes" />
                                                            <asp:HiddenField runat="server" ID="failedPackageId" />
                                                            <asp:HiddenField runat="server" ID="originalSectionId" />
                                                        </div>
                                                    </li>
                                                    <li class="availablePerformances">
                                                        <asp:ListBox runat="server" ID="lbxAvailableGeneralAdmin" DataTextField="Description"
                                                            DataValueField="PackageId" DataTextFormatString="{0:dddd, MMM d, h:mmtt}" />
                                                    </li>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    </ul>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                            <div class="generalAdminUpdate">
                                                <asp:LinkButton runat="server" ID="lbUpdateGeneralAdmin" Text="Update Selections"
                                                    CssClass="btn btnStandOut generalAdminBtn" CommandName="GenAdminUpdate" />
                                            </div>
                                        </asp:Panel>
                                    </div>
                                </asp:PlaceHolder>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
        <div class="loadingContainer bgOverlay">
            <asp:Image runat="server" ID="loadingGif" ImageUrl="~/images/ajax-loader.gif" CssClass="loadingSpinner" />
        </div>
        <asp:Panel runat="server" ID="pnlError" CssClass="fullError errorBox" Visible="false">
            <asp:Literal runat="server" ID="ltrError" />
        </asp:Panel>
        <subscription:SelectNewSection runat="server" ID="subsSelectNewSection" />
    </ContentTemplate>
</asp:UpdatePanel>--%>