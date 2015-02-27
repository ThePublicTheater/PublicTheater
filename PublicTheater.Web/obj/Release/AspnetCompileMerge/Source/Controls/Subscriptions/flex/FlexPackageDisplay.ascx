<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FlexPackageDisplay.ascx.cs"
    Inherits="TheaterTemplate.Web.Controls.Subscriptions.FlexControls.FlexPackageDisplay" %>

<div id="packageDisplay">
    <EPiServer:Property runat="server" ID="tessituraHeader" PropertyName="Header" CssClass="packageDescription" Visible="false"  />
    
    <div class="packageArea">
        <div class="subsFlexHeader">
            <div class="flexGuidelines">
                <h2>
                    My <asp:Literal runat="server" ID="ltrPackageName" />
                </h2>
                <div class="flexDescHeader">
                    <span class="plays">0</span> Plays Selected
                    <small>(<asp:Literal runat="server" ID="ltrMinimum" /> play minimum)</small>
                </div>
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
                                        Play
                                    </div>
                                </td>
                                <td>
                                    <div class="headerLine" style="display:none;">
                                        Date
                                    </div>
                                </td>
                                <td>
                                    <div class="headerLine" style="display:none;">
                                        Tickets
                                    </div>
                                </td>
                            </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr style="display: none" class="perfRow">                    
                        <td><a title="Remove from subscription" class="remove btn btn-mini btn-danger removePlayBtn">X</a></td>
                        <asp:HiddenField runat="server" ID="performanceID" />
                        <asp:HiddenField runat="server" ID="priceTypeQuantity" />
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
                    </div>
                </FooterTemplate>
        </asp:Repeater>
        <div class="continueBtn pull-right">
            <asp:LinkButton runat="server" ID="btnContinueToSeating" Text="Continue to Seating" CssClass="btn btnStandOut" />
        </div>
    </div>
    <asp:HiddenField runat="server" ID="selectedPerformanceIDs" ClientIDMode="Static" />
</div>


<%--<div id="packageDisplay">
    <EPiServer:Property runat="server" ID="tessituraHeader" PropertyName="Header" CssClass="packageDescription" />
    <div class="packageArea">
        <div class="flexGuidelines">
            <h3>
                <asp:Literal runat="server" ID="ltrPackageName" /></h3>
                <p><span class="plays">0</span> Plays Selected <small>(<asp:Literal runat="server" ID="ltrMinimum" /> play minimum)</small></p>
        </div>
        <div class="miniCart">
            <asp:Repeater runat="server" ID="rptMiniCart">
                <HeaderTemplate>
                    <table border="0" cellspacing="0" class="packageCart table table-bordered">
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr style="display: none">
                        <td><a title="Remove from subscription" class="remove btn btn-mini btn-danger">X</a></td>
                        <asp:HiddenField runat="server" ID="performanceID" />
                        <asp:HiddenField runat="server" ID="priceTypeQuantity" />
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </tbody>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </div>
    <div class="clearfix">
        <asp:Repeater runat="server" ID="rptPriceTypes">
            <HeaderTemplate>
                <div class="flexPackagePriceType pull-left">
                    <h4>Quantity:
                    </h4>
                    <ul class="unstyled">
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
                        </div>
            </FooterTemplate>
        </asp:Repeater>
        <div class="continueBtn pull-right">
            <asp:LinkButton runat="server" ID="btnContinueToSeating" Text="Continue to Seating" CssClass="btn btn-primary pull-right" />
        </div>
        <br />
        <br />
        <br />
    </div>
    <asp:HiddenField runat="server" ID="selectedPerformanceIDs" ClientIDMode="Static" />
</div>--%>
