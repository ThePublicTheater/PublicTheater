<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RenewalChangeRequest.ascx.cs" Inherits="TheaterTemplate.Web.Controls.Subscriptions.FullControls.RenewalChangeRequest" %>
<%@ Register Assembly="TheaterTemplate.Shared" TagPrefix="theaterTemplate" Namespace="TheaterTemplate.Shared.WebControls" %>

<%-- Need to maintain the current line item after postback --%>
<asp:HiddenField runat="server" ID="currentPackageItemId" />

<h2 class="viewMore">
    <asp:Literal runat="server" ID="ltrChangeHeader" />
</h2>

<div class="venuePanelsDetails collapsedContent">
    <asp:Panel runat="server" ID="pnlChangeQuantity" CssClass="renewalChoiceContainer" Visible="false">
        <h3>
            Change Quantity
        </h3>
        <div class="renewalQuantity">
            <asp:DropDownList runat="server" ID="ddlQuantity">
                <asp:ListItem Value="1" Text="1" Selected="True" />
                <asp:ListItem Value="2" Text="2" />
                <asp:ListItem Value="3" Text="3" />
                <asp:ListItem Value="4" Text="4" />
                <asp:ListItem Value="5" Text="5" />
                <asp:ListItem Value="6" Text="6" />
                <asp:ListItem Value="7" Text="7" />
                <asp:ListItem Value="8" Text="8" />
            </asp:DropDownList>
            <asp:HiddenField runat="server" ID="originalQuantity" />

            <asp:Panel runat="server" ID="displayLowerMessage" CssClass="lowered" style="display:none">
                <asp:Label runat="server" id="lblSeatsToRemove" AssociatedControlID="seatsToRemove" Text="Please note which seats you would like removed:" />
                <asp:TextBox runat="server" ID="seatsToRemove"></asp:TextBox>
            </asp:Panel>
            <asp:Panel runat="server" ID="displayIncreaseMessage" CssClass="increased" Visible="false" style="display:none">
                <asp:Label runat="server" id="lblnewSeatLocations" AssociatedControlID="newSeatLocations" Text="Location of new seats:"  />
                <asp:DropDownList runat="server" ID="newSeatLocations" DataValueField="Key" DataTextField="Value"></asp:DropDownList>
            </asp:Panel>
        </div>
    </asp:Panel>
    <asp:Repeater runat="server" ID="rptAdditionalSectionChoices">
        <HeaderTemplate>
            <div class="renewalChoiceContainer sectionChoices">
                <h3>
                    Change Pricing Section 
                </h3> 
                <ul>
        </HeaderTemplate>
        <ItemTemplate>
                <li>
                    <theaterTemplate:GroupRadioButton runat="server" ID="rdoSection" />
                    <span class="priceDifference"><asp:Literal runat="server" ID="ltrPriceChange" /></span>
                </li>
        </ItemTemplate>
        <FooterTemplate>
                </ul>
            </div>
        </FooterTemplate>
    </asp:Repeater>
    <asp:Repeater runat="server" ID="rptAdditionPackageChoices">
        <HeaderTemplate>
            <div class="renewalChoiceContainer packageChoices">
                <h3>
                    <asp:Literal runat="server" ID="ltrAdditionalPackageHeader" />
                </h3> 
                <ul>
        </HeaderTemplate>
        <ItemTemplate>
                <li>
                    <theaterTemplate:GroupRadioButton runat="server" ID="rdoPackage" />
                    <span class="differences" style="display:none;">
                        <asp:HiddenField runat="server" ID="sectionDifferences" />
                    </span>
                    <asp:Repeater runat="server" ID="rptPackagePerformances">
                    <HeaderTemplate>
                        <div class="packagePerformances" style="display:none;">
                            <ul>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <li>
                            <asp:Label runat="server" ID="lblPerformanceName" />
                            <asp:Label runat="server" ID="lblPerformanceDate" />
                        </li>
                    </ItemTemplate>
                    <FooterTemplate>
                            </ul>
                        </div>
                    </FooterTemplate>
                    </asp:Repeater>
                </li>
        </ItemTemplate>
        <FooterTemplate>
                </ul>
            </div>
        </FooterTemplate>
    </asp:Repeater>
    <asp:Repeater runat="server" ID="rptSeatingUpgradeChoices">
        <ItemTemplate>
            <div class="renewalChoiceContainer">
                <h3>
                    <asp:Literal runat="server" ID="ltrSeatingUpgradeChoicesHeader" />
                </h3>
                <ul>
            <asp:Repeater runat="server" ID="rptIndividualUpgradeSection">
                <ItemTemplate>
                    <li>
                        <theaterTemplate:GroupRadioButton runat="server" ID="rdoAdditionalChoice" />
                    </li>
                </ItemTemplate>
            </asp:Repeater>
                </ul>
            </div>
        </ItemTemplate>
    </asp:Repeater>
    <asp:Panel runat="server" ID="pnlNotes" CssClass="renewalChoiceContainer" Visible="false">
        <h3>
            Additional Notes
        </h3>
        <div>
            <asp:TextBox runat="server" ID="txtAdditionalNotes" TextMode="MultiLine" />
        </div>
    </asp:Panel>
</div>