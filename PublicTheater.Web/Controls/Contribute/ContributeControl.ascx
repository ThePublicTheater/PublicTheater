<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ContributeControl.ascx.cs"
    Inherits="PublicTheater.Web.Controls.Contribute.ContributeControl" %>
<%@ Register Assembly="TheaterTemplate.Shared" TagPrefix="theaterTemplate" Namespace="TheaterTemplate.Shared.WebControls" %>
<asp:updatepanel id="upContributeControl" runat="server">
    <contenttemplate>

        <div class="large-12 medium-12 small-12">
            <asp:Panel runat="server" ID="pnlError" CssClass="errorBox" Visible="false">
                <asp:Literal runat="server" ID="litErrorSummary" />
            </asp:Panel>

            <asp:ValidationSummary runat="server" ID="vsDonation" CssClass="errorBox" ValidationGroup="DonationForm" />

            <EPiServer:Property runat="server" ID="propHeaderImage" PropertyName="HeaderImage" DisplayMissingMessage="false" />
        </div>
        <div class="large-12 medium-12 small-12 sliderZone">
            
            <h2><EPiServer:Property runat="server" ID="propTitle" PropertyName="Title" DisplayMissingMessage="false" /></h2>

            <EPiServer:Property runat="server" ID="propCertificatePageHeader" CssClass="levelDescrip" PropertyName="HeaderText" DisplayMissingMessage="false" />

            <div style="display: none;">
                <div class="supportUs">
                    <fieldset>
                        <asp:TextBox runat="server" ID="txtDonationAmt" data-target="donationAmount" CssClass="donationAmount" />
                        <asp:RequiredFieldValidator runat="server" ID="valDonationAmtRequired" ControlToValidate="txtDonationAmt"
                            ErrorMessage="Please enter a donation amount" ValidationGroup="DonationForm">*</asp:RequiredFieldValidator>
                        <asp:CustomValidator runat="server" ID="valAmtIsNumerical" ValidationGroup="DonationForm" OnServerValidate="txtDonationAmt_ServerValidate"
                            ErrorMessage="Please enter a valid donation amount">*</asp:CustomValidator>
                    </fieldset>
                </div>
            </div>
            
            <div class="shortDonationAmountDescription" data-target="shortDonationAmountDescription"></div>
            <div class="sliderWrap">
                <div class="sliderBarWrapper"><div class="donationSlider" data-target="donationSliderArea"></div></div>     
                <div class="donationSliderAmounts" data-target="donationSliderAmountList"></div>
            </div>
            <div class="longDonationAmountDescription" data-target="longDonationAmountDescription"></div>

        </div>

        <div class="large-12 medium-12 small-12 levelSelectedInformation">
           
            <div class="funds" style="display: none">
                <h2>Funds</h2>
                <asp:Repeater runat="server" ID="rptFund">
                    <HeaderTemplate>
                        <ul class="thumbnails">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <li>
                            <div class="fundLeftCol">
                                <asp:Image runat="server" ID="imgFund" />
                            </div>
                            <theaterTemplate:GroupRadioButton runat="server" GroupName="fundRadios" ID="rdoFund" />
                            <asp:HiddenField runat="server" ID="donationType" />
                            <label>
                                <asp:Literal runat="server" ID="ltFundTitle" />
                            </label>
                            <asp:PlaceHolder runat="server" ID="phFundDescription">
                                <div class="fundDesc">
                                    <asp:Literal runat="server" ID="ltFundDescription" />
                                </div>
                            </asp:PlaceHolder>
                        </li>
                    </ItemTemplate>
                    <FooterTemplate>
                        </ul>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
            <asp:Panel runat="server" ID="pnlAcknowledgement" CssClass="giftOptions">
                <h2>Acknowledgement</h2>

                <div class="initialGiftOptions">
                    <ul class="unstyled">
                        <div class="leftAckBox">
                        <li class="checkbox">
                            <asp:RadioButton GroupName="anonDonorRadios" runat="server" ID="cbListedDonation" ClientIDMode="Static" Text="Please list me in The Public Playbill" Checked="true"/>
                        </li>                                      
                        <li class="donorName">
                            <asp:Label runat="server" ID="lblDonorName" AssociatedControlID="txtDonorName" Text="How would you like to be listed?" />
                            <asp:RequiredFieldValidator runat="server" ID="rqdDonorName" ControlToValidate="txtDonorName" ErrorMessage="Please choose how you would like to be acknowledged" ValidationGroup="DonationForm" CssClass="donorError">*</asp:RequiredFieldValidator>
                            <asp:TextBox runat="server" ID="txtDonorName" ClientIDMode="Static" CssClass="txt" />
                            <span>
                                Examples:
                                John & Jane Smith
                            </span>                    
                        </li>
                        </div>
                        <li class="checkbox anonCheckbox">
                            <asp:RadioButton GroupName="anonDonorRadios" runat="server" ID="cbAnonDonor" ClientIDMode="Static" Text="I wish to make this donation anonymously" />
                            <asp:CheckBox runat="server" ID="cbCommemorativeGift" ClientIDMode="Static" Text="This is a commemorative gift" Visible="False" />
                        </li> 
                    </ul>
                </div>
                <div class="commemorativeGiftContainer">
                    <asp:PlaceHolder runat="server" ID="phHonoreeLetter" Visible="false">
                        <div class="commemorativeInstruction">
                            Should we send a letter to the honoree?
                        </div>
                        <div class="commemorativeFields">
                            <ul>
                                <li>
                                    <asp:Label runat="server" ID="lblName" Text="Name" AssociatedControlID="txtName" />
                                    <asp:TextBox runat="server" ID="txtName" />
                                </li>
                                <li>
                                    <asp:Label runat="server" ID="lblAddress" Text="Address" AssociatedControlID="txtAddress" />
                                    <asp:TextBox runat="server" ID="txtAddress" />
                                </li>
                                <li>
                                    <asp:Label runat="server" ID="lblAddress2" AssociatedControlID="txtAddress2" />
                                    <asp:TextBox runat="server" ID="txtAddress2" />
                                </li>
                                <li>
                                    <asp:Label runat="server" ID="lblCity" Text="City" AssociatedControlID="txtCity" />
                                    <asp:TextBox runat="server" ID="txtCity" />
                                </li>
                                <li>
                                    <asp:Label runat="server" ID="lblState" Text="State" AssociatedControlID="txtState" />
                                    <asp:TextBox runat="server" ID="txtState" />
                                </li>
                                <li>
                                    <asp:Label runat="server" ID="lblZip" Text="Zip" AssociatedControlID="txtZip" />
                                    <asp:TextBox runat="server" ID="txtZip" />
                                </li>
                            </ul>
                        </div>
                    </asp:PlaceHolder>
                </div>
            </asp:Panel>

            <div class="matchingGift">
                <h2>Corporate Matching</h2>
                <EPiServer:Property runat="server" ID="propMatchingGiftText" PropertyName="MatchingGiftText" DisplayMissingMessage="false" />

                <asp:CheckBox runat="server" ID="cbxCompanyMatching" ClientIDMode="Static" />
                <asp:Label runat="server" ID="txtMatchingPrompt" Text="I plan to apply for matching gifts from my company"></asp:Label>
                <asp:Label runat="server" ID="lblCompanyName" Text="Company Name" AssociatedControlID="txtCompanyName" />
                <asp:TextBox runat="server" ID="txtCompanyName" ClientIDMode="Static" />

                <EPiServer:Property runat="server" ID="propCertificatePageContent" PropertyName="FooterText" DisplayMissingMessage="false" />
            </div>

            <div class="matchingGift">
                <h2>Declination of benefits</h2>
                <ul class="unstyled">
                    <li class="checkbox">
                        <asp:CheckBox runat="server" ID="cbNoBenfits" />
                        <asp:Label runat="server" ID="label1">I do not wish to receive benefits for my donation. Make this gift fully tax-deductible.</asp:Label>
                    </li>
                </ul>
            </div>

            <asp:Button runat="server" ID="btnAddDonation" Text="Add to cart" CssClass="btn solid" ValidationGroup="DonationForm" />
        </div>

    </contenttemplate>
</asp:updatepanel>