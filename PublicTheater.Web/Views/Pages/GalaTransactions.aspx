<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPages/Interior.master"
    AutoEventWireup="true" CodeBehind="GalaTransactions.aspx.cs" Inherits="PublicTheater.Web.Views.Pages.GalaTransactions" %>

<asp:content id="Content1" contentplaceholderid="HeadContent" runat="server">
</asp:content>
<asp:content id="Content3" contentplaceholderid="PrimaryContent" runat="server">
    <div class="indvGiveWrap">
        <div class="large-8 medium-6 small-12 indvImgArea">
            <asp:image runat="server" id="imgBannerImage">
            </asp:image>
            <EPiServer:Property CssClass="donationSmallBannerText" runat="server" ID="epiBannerText"
                PropertyName="BannerText">
            </EPiServer:Property>
        </div>
        <div class="large-4 medium-6 small-12">
            <EPiServer:Property runat="server" ID="epiSideHeading" CssClass="galaDateInfo" PropertyName="SideHeading">
            </EPiServer:Property>
        </div>
    </div>
</asp:content>
<asp:content id="Content4" contentplaceholderid="PrimaryContentBottomSection" runat="server">
    <div class="indvGiveWrap">
        <div class="interiorPage">
            <div class="large-12 medium-12 small-12">
                <EPiServer:Property runat="server" CssClass="errorBox" Visible="False" ID="propInvalidLevelError"
                    PropertyName="InvalidLevelError" DisplayMissingMessage="false" />
                <asp:validationsummary runat="server" id="vsDonation" cssclass="errorBox" />
            </div>
            <asp:repeater runat="server" id="rptGalaLevelGroups">
                <itemtemplate>
                   <div class="ticketSection">
                    <h2><asp:Literal runat="server" ID="litGroupTitle"></asp:Literal></h2>
                    <asp:Repeater runat="server" ID="rptGalaLevels">
                        <ItemTemplate>
                            <div class="large-6 medium-12 small-12">
                                <asp:DropDownList runat="server" ID="ddlQuantity">
                                </asp:DropDownList>
                                <div class="ticketDetails">
                                    <h3><asp:Literal runat="server" ID="litTitle"></asp:Literal></h3>
                                    <h4><asp:Literal runat="server" ID="litAmountInfo"></asp:Literal></h4>
                                    <asp:Literal runat="server" ID="litDescription"></asp:Literal>
                                </div>
                            </div>    
                        </ItemTemplate>
                    </asp:Repeater>
                   </div>
                </itemtemplate>
            </asp:repeater>
            <div class="large-12 medium-12 small-12">
                <div class="giftOptions galaDonate">
                    <h2>
                        Make a donation to the Annual Gala</h2>
                    <asp:label id="Label1" runat="server">Donation Amount:</asp:label>
                    <asp:textbox runat="server" id="txtDonationAmount">
                    </asp:textbox>
                    <span class="taxNote">Your donation is fully tax deductible</span>
                </div>
                <div class="giftOptions galaAck">
                    <h2>
                        Acknowledgement</h2>
                    <ul>
                        <div class="leftAckBox">
                            <li class="checkbox">
                                <asp:radiobutton runat="server" id="cbListedDonation" clientidmode="Static" groupname="Acknowledgement"
                                    text="Please list me in the Annual Gala Program" />
                                    
                            </li>
                            <li class="donorName">
                                <label>
                                    How would you like to be listed?</label>
                                <asp:requiredfieldvalidator runat="server" id="rqdDonorName" controltovalidate="txtDonorName"
                                    errormessage="Please choose how you would like to be acknowledged" cssclass="donorError">*</asp:requiredfieldvalidator>
                                <asp:textbox runat="server" id="txtDonorName" clientidmode="Static">
                                </asp:textbox>
                                <span>Examples: John &amp; Jane Smith </span>
                                
                                <EPiServer:Property runat="server" ID="propAcknowledgementNotes" PropertyName="AcknowledgementNotes" DisplayMissingMessage="false" CustomTagName="span" />
                                </li>
                        </div>
                        <li class="checkbox anonCheckbox">
                            <asp:radiobutton runat="server" id="cbAnonDonor" clientidmode="Static" groupname="Acknowledgement"
                                text="I wish to make this donation anonymously" />
                        </li>
                    </ul>
                </div>
                <asp:button runat="server" id="btnAddToCart" text="Add to Cart" cssclass="btn solid" />
            </div>
        </div>
    </div>
</asp:content>
<asp:content id="Content5" contentplaceholderid="BeforeCloseBodyContent" runat="server">
</asp:content>