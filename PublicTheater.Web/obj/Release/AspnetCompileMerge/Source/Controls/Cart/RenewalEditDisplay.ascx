<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RenewalEditDisplay.ascx.cs" Inherits="TheaterTemplate.Web.Controls.CartControls.RenewalEditDisplay" %>


<div class="renewalEdit">
    <asp:HiddenField runat="server" ID="packageItemIdValue" />
    <h3 runat="server" id="packageRequestContainer" visible="false">
        <asp:Literal runat="server" ID="ltrPackageRequestText" />: <asp:Literal runat="server" ID="ltrPackageRequest" />
    </h3>
    <h4 runat="server" id="originalSeatsContainer" visible="false">
        <asp:Literal runat="server" ID="ltrOriginalSeatsText" />: <asp:Literal runat="server" ID="ltrOriginalSeats" />
    </h4>
    <asp:Repeater runat="server" ID="rptAdditionalChangeRequests">
        <HeaderTemplate>
            <div class="additionalItems">
                <h4>
                    Upgrades Requested:
                </h4>
                <ul>
        </HeaderTemplate>            
        <ItemTemplate>
            <li>
                <asp:Literal runat="server" ID="ltrAdditionalChangeKey" />:
                <asp:Literal runat="server" ID="ltrAdditionalChangeValue" />
            </li>
        </ItemTemplate>
        <FooterTemplate>
                </ul>
            </div>
        </FooterTemplate>
    </asp:Repeater>
    <asp:PlaceHolder runat="server" ID="phAdditionalNotes" Visible="false">
        <h4>
            Additional Notes:
        </h4>
        <ul>
            <li>
                <asp:Literal runat="server" ID="ltrAdditionalNotes" />
            </li>
        </ul>
    </asp:PlaceHolder>
</div>