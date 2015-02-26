<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CartDonationControl.ascx.cs" Inherits="TheaterTemplate.Web.Controls.ContributeControls.CartDonationControl" %>

<div id="packageDisplay" style="display: none;">
    <div id="cartExpire">
        <asp:Literal runat="server" ID="ltrTimerText" />
    </div>
</div>

<%--<div class="donationHeader">
    <h1>Donation</h1>
</div>--%>

<div id="considerDonation">
    
    <div id="donationCallout">
        <asp:Image runat="server" ID="imgMain" Visible="false" />
        <EPiServer:Property ID="Property1" runat="server" PropertyName="MainBody" />
    </div>

    <div id="donationEffect">
        <ul>
            <li>
                <label>
                    Current Order Total:
                </label>
                <div>
                    <%-- 
                        Use Balance to charge because if a change request is in the cart, the total can reflect an incorrect amount which will throw off the calculation
                        -AJM 7/3/13
                    --%>
                    $<span id="CurrentOrderTotal" class="currentOrderTotal"><%= string.Format("{0:n2}",CurrentCart.BalanceToCharge) %></span>
                </div>
            </li>
            <li>
                <asp:Label ID="lblSuggestedDonation" runat="server" Text="Suggested Contribution:"
                    AssociatedControlID="SuggestedDonation" />
                <div>
                    <asp:TextBox runat="server" ID="SuggestedDonation" ClientIDMode="Static" CssClass="suggestedDonation" />
                </div>
                <asp:PlaceHolder runat="server" ID="phDonationExplanation" Visible="false">
                    <p class="explanation">
                        <asp:Literal runat="server" ID="ltrSuggestedDonationExpl" />
                    </p>
                </asp:PlaceHolder>
            </li>
            <%--<li class="recognition">               
                <asp:Label ID="lblRecognitionName" runat="server" Text="Recognition Name:" AssociatedControlID="txtRecognitionName" />
                <div>
                    <asp:TextBox runat="server" ID="txtRecognitionName" ClientIDMode="Static" />
                </div>
               <p>If your donation is $100 or more please indicate how you wish to be acknowledged in our playbills.</p>
            </li>--%>
            <li class="total">
                <label>
                    Total With Donation:
                </label>
                <div id="DonationTotal" class="donationTotal">
                </div>
            </li>
        </ul>
        <div id="donationDecision">
            <asp:LinkButton runat="server" ID="addDonation" Text="Add Donation" CssClass="btn solid btnStandOut" />
            <asp:HyperLink runat="server" ID="noDonation" Text="No Thanks" CssClass="textLink" />           
        </div>
    </div>
</div>
<asp:HiddenField runat="server" ID="RoundUpFormula" ClientIDMode="Static" />
<asp:Panel runat="server" ID="pnlError" CssClass="errorBox" Visible="false">
    <asp:Literal runat="server" ID="ltrError" />
</asp:Panel>

<%--<div id="packageDisplay" class="row-fluid">
    <div class="span6">
        <h2>Donation</h2>
    </div>
    <div class="span6 clearfix">
        <div id="cartExpire" class="span6 pull-right alert alert-warning">
        <asp:Literal runat="server" ID="ltrTimerText" />
    </div>
</div>
</div>
<div id="considerDonation">
    <div class="row-fluid clearfix">
        <div id="donationCallout" class="span6">
        <asp:Image runat="server" ID="imgMain" />
        <EPiServer:Property ID="Property1" runat="server" PropertyName="MainBody" />
    </div>
        <div id="donationEffect" class="span5 pull-right">
            <ul class="unstyled">
                <li class="clearfix">
                    <label class="pull-left">
                    Current Order Total:
                </label>
                    <div class="pull-right">
                        $<span id="CurrentOrderTotal" class="currentOrderTotal"><%= CurrentCart.Total %></span>
                    </div>
            </li>
                <li class="clearfix">
                    <asp:Label CssClass="pull-left" ID="lblSuggestedDonation" runat="server" Text="Suggested Contribution" AssociatedControlID="SuggestedDonation" />
                    <div class="pull-right">
                    <asp:TextBox runat="server" ID="SuggestedDonation" ClientIDMode="Static" CssClass="suggestedDonation" />
                </div>
                <asp:PlaceHolder runat="server" ID="phDonationExplanation" Visible="false">
                    <p class="explanation">
                        <asp:Literal runat="server" ID="ltrSuggestedDonationExpl" />
                    </p>
                </asp:PlaceHolder>
            </li>
                <li class="total clearfix">
                    <label class="pull-left">
                    Total With Donation:
                </label>
                    <div id="DonationTotal" class="donationTotal pull-right">
                </div>
            </li>
        </ul>
        <div id="donationDecision">
                <asp:HyperLink runat="server" ID="noDonation" Text="No Thanks" CssClass="btn" />
                <asp:LinkButton runat="server" ID="addDonation" Text="Add Donation" CssClass="btn btn-primary" />
            </div>
        </div>
    </div>
</div>
<asp:HiddenField runat="server" ID="RoundUpFormula" ClientIDMode="Static" />
<asp:Panel runat="server" ID="pnlError" CssClass="errorBox" Visible="false">
    <asp:Literal runat="server" ID="ltrError" />
</asp:Panel>--%>