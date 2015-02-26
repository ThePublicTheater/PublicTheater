<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FlexPackageDisplay.ascx.cs" Inherits="TheaterTemplate.Web.Controls.CartControls.FlexPackageDisplay" %>


<div class="packageArea">
    <div class="subsFlexHeader">
        <div class="flexGuidelines">
            
            <asp:LinkButton runat="server" ID="lbRemove" Text="X" CommandName="Remove" CssClass="remove" ToolTip="Remove this from your cart" />
            <%--<asp:Image runat="server" Visibile="False" ID="AdaSeatImage" ImageUrl="~/Global/Seat%20Type%20Images/FSCT/accessMan.png" />--%>
           <h2> <asp:Literal runat="server" ID="ltrPackageDescription" />  </h2>
            
            <div class="flexDescHeader">
                <span class="plays">
                    <asp:PlaceHolder runat="server" ID="phRequirements" Visible="false">
                        <asp:Literal runat="server" ID="ltrNumberOfPerformances" /> Plays
                        <small>
                            (<asp:Literal runat="server" ID="ltrMinimumPerformances" /> play minimum)
                        </small>
                    </asp:PlaceHolder>
                </span>
            </div>
        </div>
    </div>

    <div class="miniCart flex-cart">
        <asp:Repeater runat="server" ID="rptPackagePerformances">
            <HeaderTemplate>
                <ul>
                    <li class="cart-head">
                        <span><asp:Literal runat="server" ID="ltrPerformanceNameHeader" /></span>
                        <span><asp:Literal runat="server" ID="ltrPerformanceTimeHeader" /></span>
                        <span><asp:Literal runat="server" ID="ltrVenueHeader" /></span>
                        <span><asp:Literal runat="server" ID="ltrPriceTypeHeader" /></span>
                        <span><asp:Literal runat="server" ID="ltrSectionHeader" /></span>
                    </li>
            </HeaderTemplate>
            <ItemTemplate>
                <li>
                    <span><asp:Literal runat="server" ID="ltrPerformanceName" /></span>
                    <span><asp:Literal runat="server" ID="ltrPerformanceTime" /></span>
                    <span><asp:Literal runat="server" ID="ltrVenue" /></span>
                    <span><asp:Literal runat="server" ID="ltrPriceTypes" /></span>
                    <span><asp:Literal runat="server" ID="ltrSection" /></span>
                </li>                   
            </ItemTemplate>
            <FooterTemplate>
                </ul>
            </FooterTemplate>
        </asp:Repeater>
    </div>


</div>


<%--<div class="packageArea">
    <div class="subsFlexHeader">
        <div class="flexGuidelines">
            <h2>
                <asp:LinkButton runat="server" ID="lbRemove" Text="X" CommandName="Remove" CssClass="remove" ToolTip="Remove this from your cart" />
                        <%--<asp:Image runat="server" Visibile="False" ID="AdaSeatImage" ImageUrl="~/Global/Seat%20Type%20Images/FSCT/accessMan.png" />--%>
                <%--<asp:Literal runat="server" ID="ltrPackageDescription" />  
            </h2>
            <div class="flexDescHeader">
                <span class="plays">
                    <asp:Literal runat="server" ID="ltrNumberOfPerformances" /> Plays
                    <small>
                        (<asp:Literal runat="server" ID="ltrMinimumPerformances" /> play minimum)
                    </small>
                </span>
            </div>
        </div>
    </div>
    <div class="miniCart">
        <asp:Repeater runat="server" ID="rptPackagePerformances">
            <HeaderTemplate>
                <table border="0" cellspacing="0" class="packageCart">
                    <thead>
                        <tr class="headerTr">
                            <th><asp:Literal runat="server" ID="ltrPerformanceNameHeader" /></th>
                            <th><asp:Literal runat="server" ID="ltrPerformanceTimeHeader" /></th>
                            <th><asp:Literal runat="server" ID="ltrVenueHeader" /></th>
                            <th><asp:Literal runat="server" ID="ltrPriceTypeHeader" /></th>
                            <th><asp:Literal runat="server" ID="ltrSectionHeader" /></th>
                        </tr>
                    </thead>
                    <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="perfRow">
                    <td><asp:Literal runat="server" ID="ltrPerformanceName" /></td>
                    <td><asp:Literal runat="server" ID="ltrPerformanceTime" /></td>
                    <td><asp:Literal runat="server" ID="ltrVenue" /></td>
                    <td><asp:Literal runat="server" ID="ltrPriceTypes" /></td>
                    <td><asp:Literal runat="server" ID="ltrSection" /></td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                    </tbody>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
</div>--%>