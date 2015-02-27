<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PaymentDonationAsk.ascx.cs" Inherits="PublicTheater.Web.Controls.Contribute.PaymentDonationAsk" %>

<ul>
    <asp:HiddenField runat="server" ID="RoundUpFormula" ClientIDMode="Static" />
    <li class="suggestedDonation">
        <asp:Label runat="server" ID="lblDonationDescription" Text="Consider Rounding up your donation:" AssociatedControlID="txtDonationAmount" />
        <asp:TextBox runat="server" ID="txtDonationAmount" CssClass="suggestedDonation" />
        <asp:LinkButton runat="server" ID="lbAddNewDonation" CssClass="btn solid" Text="Add" ValidationGroup="PaymentPageAsk" />
        <a href="#" id="noDonation" class="textLink">No Thanks</a>
    </li>
    <li runat="server" id="descriptionContainer" visible="false" class="donationDescription">
        <asp:Label runat="server" ID="lblDescriptionArea" />
    </li>
    <li class="donationTotalWrap">
        <div class="totalBoxWrap">
            <asp:Label runat="server" ID="lblTotalWithDonation" Text="Total with donation:" CssClass="totalDonationlbl" />
            <asp:Label runat="server" ID="lblNewTotal" CssClass="donationTotal" />
        </div>
    </li>
    
</ul>

<asp:Panel runat="server" ID="pnlError" CssClass="errorBox" Visible="false">
    <asp:Literal runat="server" ID="ltrError" />
</asp:Panel>