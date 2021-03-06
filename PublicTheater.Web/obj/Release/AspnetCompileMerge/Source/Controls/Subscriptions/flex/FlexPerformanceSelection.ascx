﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FlexPerformanceSelection.ascx.cs" Inherits="TheaterTemplate.Web.Controls.Subscriptions.FlexControls.FlexPerformanceSelection" %>

<div id="performanceSelection">
    <div class="flexError errorBox alert alert-error" style="display: none;"></div>
    
    <asp:Repeater runat="server" ID="rptProductions">
        <HeaderTemplate>
            <div id="allProductions">            
        </HeaderTemplate>
        <ItemTemplate>
            <div class="productionRow row-fluid">
                <%-- FIELD USED TO RESET AFTER REMOVING FROM MINICART--%>                
                <asp:HiddenField runat="server" ID="performanceSelected" />
                <div class="productionDisplay row-fluid">
                    <div class="productionImage span3">
                        <asp:HyperLink runat="server" ID="hlDetailPopup" NavigateUrl="javascript:void();">
                            <asp:Image runat="server" ID="prodImage" />
                        </asp:HyperLink>
                    </div>
                    <div class="productionInformation span6">
                        <h3 class="flexProdTitle">
                            <asp:Literal runat="server" ID="ltrProductionName" />
                        </h3>
                        <div class="productionData">
                            <asp:Literal runat="server" ID="ltrProductionData" />
                        </div>
                        <div class="filterableValues">
                            <asp:HiddenField runat="server" ID="venueId" />
                        </div>
                    </div>
                    <div class="productionButton span3">
                        <asp:LinkButton runat="server" ID="btnSelectPlay" Text="Select Play" CssClass="btn pull-right selectPlay" /> 
                        <asp:LinkButton runat="server" ID="btnAddToPackage" Text="Add to package" CssClass="btn btn-primary pull-right addToPackage addToPackageBtn"  Style="display: none;" /> 
                    </div> 
                </div>
                <div class="reserveSelection" style="display:none;">
                    <div class="expandArrow">&nbsp;</div>
                    
                    <div class="quantity">
                        <asp:Repeater runat="server" ID="rptPriceTypes">
                            <HeaderTemplate>
                                <h4>Quantity:</h4>                                
                                <ul>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <li>
                                    <asp:Label runat="server" ID="lblPriceType" AssociatedControlID="ddlQuantity" />
                                    <asp:DropDownList runat="server" ID="ddlQuantity">
                                        <asp:ListItem Text="0" Value="0" />
                                        <asp:ListItem Selected="True" Text="1" Value="1" />
                                        <asp:ListItem Text="2" Value="2" />
                                        <asp:ListItem Text="3" Value="3" />
                                        <asp:ListItem Text="4" Value="4" />
                                        <asp:ListItem Text="5" Value="5" />
                                        <asp:ListItem Text="6" Value="6" />
                                        <asp:ListItem Text="7" Value="7" />
                                        <asp:ListItem Text="8" Value="8" />
                                        <asp:ListItem Text="9" Value="9" />
                                        <asp:ListItem Text="10" Value="10" />
                                    </asp:DropDownList>
                                    <asp:HiddenField runat="server" ID="priceTypeID" />
                                </li>
                            </ItemTemplate>
                            <FooterTemplate>
                                </ul>
                            </FooterTemplate>
                        </asp:Repeater>


                        <div class="performanceFilters">
                            <asp:Repeater runat="server" ID="rptPerformanceFilters">
                                <ItemTemplate>
                                    <div class="perfFilter">
                                        <!-- DO WE NEED THIS? WILL THE FILTERS THEMSELVES GIVE ENOUGH EXPLANATION?
                                        -->
                                        <h4>
                                            Narrow by <asp:Literal runat="server" ID="ltrFilterTitle" />
                                        </h4>
                                        <asp:Repeater runat="server" ID="rptFilters">
                                            <HeaderTemplate>
                                                <ul class="unstyled">
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <li>
                                                    <label class="checkbox">
                                                        <asp:CheckBox runat="server" ID="chkFilter" />
                                                    </label>
                                                    <%-- This span is used to select the value of the filter --%>
                                                    <span style="display: none" class="hiddenValue">                
                                                        <asp:HiddenField runat="server" ID="filterValue" />
                                                    </span>                                                    
                                                </li>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </ul>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div> <!--End .performanceFilters -->
                    </div>
                    
                    <div class="selectionContainer">

                        <div class="availablePerformances">
                            <h4>Choose a Date:</h4>
                            <div class="currentPerformances">
                                <div class="selectionDiv"> 
                                    <%-- IMPORTANT: ANY CHANGES TO DATATEXTFORMATSTRING MAY AFFECT DAY FILTERS --%>
                                    <asp:ListBox runat="server" ID="lbxDates" DataTextField="PerformanceDate" CssClass="perfDateSelection"
                                        DataValueField="PerformanceId" DataTextFormatString="{0:dddd, MMM d, h:mmtt}" />

                                    <%-- THIS LBX CONTAINS THE SAME VALUES, BUT IS USED TO REBUILD FILTERED OPTIONS--%>
                                    <asp:ListBox runat="server" ID="lbxDatesHidden" DataTextField="PerformanceDate" CssClass="perfDateSelectionCopy"
                                        DataValueField="PerformanceId" DataTextFormatString="{0:dddd, MMM d, h:mmtt}" Style="display: none" />
                                </div>
                            </div>
                        </div> <!--End .availablePerformances -->
                    </div>
                </div>
            </div>
        </ItemTemplate>
        <FooterTemplate>
            </div>
        </FooterTemplate>
    </asp:Repeater>
    <asp:HiddenField runat="server" ID="minimumPerformances" ClientIDMode="Static" />
</div>




<%--<div id="performanceSelection">
    <div class="flexError errorBox alert alert-error" style="display: none;">
    </div>
    <asp:Repeater runat="server" ID="rptProductions">
        <HeaderTemplate>
            <div id="allProductions">
        </HeaderTemplate>
        <ItemTemplate>
            <div class="productionRow row-fluid">

                <asp:HiddenField runat="server" ID="performanceSelected" />
                <div class="productionDisplay row-fluid">
                    <div class="productionImage span3">
                        <asp:HyperLink CssClass="img-polaroid" runat="server" ID="hlDetailPopup" NavigateUrl="javascript:void();">
                        <asp:Image runat="server" ID="prodImage" /></asp:HyperLink>
                    </div>
                    <div class="productionInformation span6">
                        <h3 class="title">
                            <asp:Literal runat="server" ID="ltrProductionName" />
                        </h3>
                        <div class="productionData">
                            <asp:Literal runat="server" ID="ltrProductionData" />
                        </div>
                        <div class="filterableValues">
                            <asp:HiddenField runat="server" ID="venueId" />
                        </div>
                    </div>
                    <div class="productionButton span3">
                        <asp:LinkButton runat="server" ID="btnSelectPlay" Text="Select Play" CssClass="btn pull-right selectPlay" />
                        <asp:LinkButton runat="server" ID="btnAddToPackage" Text="Add to package" CssClass="btn btn-primary pull-right addToPackage" Style="display: none;" />
                    </div>
                </div>
                <div class="reserveSelection" style="display: none;">
                    <div class="quantity">
                        <asp:Repeater runat="server" ID="rptPriceTypes">
                            <HeaderTemplate>
                                <h4>Quantity:</h4>
                                <ul>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <li>
                                    <asp:Label runat="server" ID="lblPriceType" AssociatedControlID="ddlQuantity" />
                                    <asp:DropDownList runat="server" ID="ddlQuantity">
                                        <asp:ListItem Text="0" Value="0" />
                                        <asp:ListItem Selected="True" Text="1" Value="1" />
                                        <asp:ListItem Text="2" Value="2" />
                                        <asp:ListItem Text="3" Value="3" />
                                        <asp:ListItem Text="4" Value="4" />
                                        <asp:ListItem Text="5" Value="5" />
                                        <asp:ListItem Text="6" Value="6" />
                                        <asp:ListItem Text="7" Value="7" />
                                    </asp:DropDownList>
                                    <asp:HiddenField runat="server" ID="priceTypeID" />
                                </li>
                            </ItemTemplate>
                            <FooterTemplate>
                                </ul>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>


                    <div class="selectionContainer">
                        <h4>Choose a Date:</h4>

                        <div class="availablePerformances">
                            <div class="currentPerformances">
                                <div class="selectionDiv">
  
                                    <asp:ListBox runat="server" ID="lbxDates" DataTextField="PerformanceDate" CssClass="perfDateSelection"
                                        DataValueField="PerformanceId" DataTextFormatString="{0:dddd, MMM d, h:mmtt}" />

                                    <asp:ListBox runat="server" ID="lbxDatesHidden" DataTextField="PerformanceDate" CssClass="perfDateSelectionCopy"
                                        DataValueField="PerformanceId" DataTextFormatString="{0:dddd, MMM d, h:mmtt}" Style="display: none" />
                                </div>
                            </div>
                        </div>

                        <div class="performanceFilters">
                            <asp:Repeater runat="server" ID="rptPerformanceFilters">
                                <ItemTemplate>
                                    <div class="perfFilter">
                                        <h5>Narrow by
                                            <asp:Literal runat="server" ID="ltrFilterTitle" />
                                        </h5>
                                        <asp:Repeater runat="server" ID="rptFilters">
                                            <HeaderTemplate>
                                                <ul class="unstyled">
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <li>
                                                    <label class="checkbox">
                                                        <asp:CheckBox runat="server" ID="chkFilter" />
                                                    </label>

                                                    <span style="display: none" class="hiddenValue">
                                                        <asp:HiddenField runat="server" ID="filterValue" />
                                                    </span>
                                                </li>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </ul>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>

                    </div>
                </div>
            </div>
        </ItemTemplate>
        <FooterTemplate>
            </div>
        </FooterTemplate>
    </asp:Repeater>
    <asp:HiddenField runat="server" ID="minimumPerformances" ClientIDMode="Static" />
</div>--%>
