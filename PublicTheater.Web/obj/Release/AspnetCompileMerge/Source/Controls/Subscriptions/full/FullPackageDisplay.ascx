<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FullPackageDisplay.ascx.cs"
    Inherits="TheaterTemplate.Web.Controls.Subscriptions.FullControls.FullPackageDisplay" %>

<div id="packageDisplay">
    <EPiServer:Property runat="server" ID="tessituraHeader" PropertyName="Header" CssClass="packageDescription" Visible="false"  />
    
    <div class="packageArea">
        <div class="subsFlexHeader">
            <div class="flexGuidelines">
                <h2>
                    <asp:Literal runat="server" ID="ltrPackageDescription" />
                </h2>
            </div>
        </div>
        <div class="miniCart">
            <asp:Repeater runat="server" ID="rptMiniCart">
                <HeaderTemplate>
                    <table border="0" cellspacing="0" class="packageCart">
                        <tbody>
                            <tr class="headerTr">
                                <td>
                                    <div class="headerLine" style="display:none;">
                                        &nbsp;
                                    </div>
                                </td>
                                <td>
                                    <div class="headerLine" style="display:none;">
                                        Subscription
                                    </div>
                                </td>
                                <td>
                                    <div class="headerLine" style="display:none;">
                                        <span class="subDateLine">Play</span>
                                        <span class="subsPref">Date</span>
                                    </div>
                                </td>
                                <td>
                                    <div class="headerLine" style="display:none;">
                                       &nbsp;
                                    </div>
                                </td>
                            </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr style="display: none" class="perfRow fullPackage">                    
                        <td><a title="Remove from subscription" class="remove btn btn-mini btn-danger removePlayBtn">X</a></td>
                        <td class="packageInfo">
                                <h3>
                                
                                </h3>
                                <asp:HiddenField runat="server" ID="packageId" />
                        </td>
                        <td class="packagePerformances">
                                
                        </td>
                        <td class="priceTypes" style="visibility:hidden; display:none;">
                                <span></span>
                                <asp:HiddenField runat="server" ID="priceTypeQuantity" />                                
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
        <asp:Repeater runat="server" ID="rptPriceTypes">
                <HeaderTemplate>
                    <div class="flexPackagePriceType pull-left">
                        <h4>
                            Quantity:
                        </h4>
                        <ul class="unstyled">
                </HeaderTemplate>
                <ItemTemplate>
                    <li>
                        <span class="priceTypeName"><asp:Literal runat="server" ID="ltrPriceTypeDescription" /></span>
                        <<asp:Label runat="server" ID="lblPriceType" AssociatedControlID="ddlQuantity" />
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
                        <asp:HiddenField runat="server" ID="priceTypeId" />
                    </li>
                </ItemTemplate>
                <FooterTemplate>
                        </ul>
                    </div>
                </FooterTemplate>
        </asp:Repeater>
        <div class="continueBtn pull-right">
            <asp:LinkButton runat="server" ID="btnContinueToSeating" Text="Continue to Seating" CssClass="btn btnStandOut" />
        </div>
    </div>
    <asp:HiddenField runat="server" ID="packagePriceTypes" ClientIDMode="Static" />
</div>

  
<%--<div id="packageDisplay">
    <div class="packageDescription">
        <h2>
            Select Options
        </h2>
    </div>
    <div class="packageArea">
        <div class="flexGuidelines">
            <h3>
                <asp:Literal runat="server" ID="ltrPackageDescription" />  
            </h3>
        </div>
        <div class="miniCart">
            <asp:Repeater runat="server" ID="rptMiniCart">
                <HeaderTemplate>
                    <table cellspacing="0" border="0" class="packageCart">
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                        <tr style="display:none">
                            <td class="removeButton">
                                <a title="Remove from subscription" class="remove">X</a>
                                <asp:HiddenField runat="server" ID="venueId" />
                            </td>
                            <td class="packageInfo">
                                <%-- PLACEHOLDERS NEED TO REMAIN TO MAINTAIN INTEGRITY OF THE HIDDEN FIELD --%/>
                                <h3>
                                
                                </h3>
                                <asp:HiddenField runat="server" ID="packageId" />
                            </td>
                            <td class="packagePerformances">
                                
                            </td>
                            <td class="priceTypes" style="visibility:hidden">
                                <%-- PLACEHOLDERS NEED TO REMAIN TO MAINTAIN INTEGRITY OF THE HIDDEN FIELD --%/>
                                <span></span>
                                <asp:HiddenField runat="server" ID="priceTypeQuantity" />                                
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
        <asp:LinkButton runat="server" ID="btnContinueToSeating" Text="Continue to Seating"
            CssClass="btn btnStandOut" />
        <asp:Repeater runat="server" ID="rptPriceTypes">
            <HeaderTemplate>
                <ul class="priceTypes">
            </HeaderTemplate>
            <ItemTemplate>
                <li>
                    <span class="priceTypeName"><asp:Literal runat="server" ID="ltrPriceTypeDescription" /></span>
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
                    </asp:DropDownList>
                    <asp:HiddenField runat="server" ID="priceTypeId" />
                </li>
            </ItemTemplate>
            <FooterTemplate>
                </ul>
            </FooterTemplate>
        </asp:Repeater>
    </div>
    <asp:HiddenField runat="server" ID="packagePriceTypes" ClientIDMode="Static" />
</div>--%>
