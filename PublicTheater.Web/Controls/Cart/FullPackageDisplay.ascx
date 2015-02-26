<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FullPackageDisplay.ascx.cs" Inherits="TheaterTemplate.Web.Controls.CartControls.FullPackageDisplay" %>

<%-- WE ONLY WANT TO DISPLAY THIS PORTION WHEN THE PACKAGE ITEM ISN'T A PART OF A MASTER PACKAGE --%>
<asp:PlaceHolder runat="server" ID="phPackageCartHeader">
    <div class="flexGuidelines">
        <h3>
            <asp:LinkButton runat="server" ID="lbRemove" Text="X" CommandName="Remove" CssClass="remove" ToolTip="Remove this from your cart" />
            <asp:Literal runat="server" ID="ltrPackageDescription" />  
        </h3>
    </div>
    <div class="miniCart">
        <table cellspacing="0" border="0" class="packageCart">
            <tbody>
</asp:PlaceHolder>

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
            <asp:Literal runat="server" ID="ltrSection" />
        </span>
        <br />
        <span>
            <asp:Literal runat="server" ID="ltrPriceTypes" />
        </span>
        <br />
        <span>
            <asp:Literal runat="server" ID="ltrTotalPrice" />                                        
        </span>
        <asp:HiddenField runat="server" ID="priceTypeQuantity" />                                
    </td>
</tr>

<%-- WE ONLY WANT TO DISPLAY THIS PORTION WHEN THE PACKAGE ITEM ISN'T A PART OF A MASTER PACKAGE --%>
<asp:PlaceHolder runat="server" ID="phPackageCartFooter">
            </tbody>
        </table>
    </div>
</asp:PlaceHolder>