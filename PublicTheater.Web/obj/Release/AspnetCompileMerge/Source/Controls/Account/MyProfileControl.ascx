<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MyProfileControl.ascx.cs" Inherits="PublicTheater.Web.Controls.Account.MyProfileControl" %>
<%@ Register TagPrefix="uc" Src="~/controls/account/AddressManagerControl.ascx" TagName="AddressManagerControl" %>
<%@ Register TagPrefix="uc" Src="~/controls/account/UserInterests.ascx" TagName="UserInterests" %>
<%@ Register Src="~/controls/checkout/BillingAddressControl.ascx" TagPrefix="uc"
    TagName="ChangeBillingAddress" %>
<%@ Register Src="~/controls/checkout/ShippingAddressControl.ascx" TagPrefix="uc"
    TagName="ChangeShippingAddress" %>
<%@ Register src="~/modules/Mail2SignUp/Mail2SignUpDynamicContent.ascx" tagPrefix="dc" tagName="Mail2Signup" %>

<div class="formHeader accountPages">
    <div class="large-12 medium-12 small-12">
        <div class="ticketHistoryBody">
            <EPiServer:Property runat="server" ID="propHeader" DisplayMissingMessage="false" CustomTagName="p" PropertyName="Header" />
        </div>
    </div>
</div>

<div id="registerControl">

    <div class="row-fluid">
        <div class="small-12 medium-12 large-12">
            
            <div id="Div1">
                <EPiServer:Property runat="server" ID="propDuplicateEmailError" DisplayMissingMessage="false"
                    PropertyName="DuplicateEmailError" Visible="false" CssClass="errorBox" />
                <EPiServer:Property runat="server" ID="propDuplicateLoginError" DisplayMissingMessage="false"
                    PropertyName="DuplicateLoginError" Visible="false" CssClass="errorBox" />
                <EPiServer:Property runat="server" ID="propGenericError" DisplayMissingMessage="false"
                    PropertyName="GenericError" Visible="false" CssClass="errorBox" />
                <EPiServer:Property runat="server" ID="propInvalidPromoError" DisplayMissingMessage="false"
                    PropertyName="InvalidPromoError" Visible="false" CssClass="errorBox" />
                <EPiServer:Property runat="server" ID="propUpdateSuccessMessage" DisplayMissingMessage="false"
                    PropertyName="UpdateSuccessMessage" Visible="false" />
            </div>
        </div>

        <div class="small-12 medium-12 large-12">
            <fieldset class="form">
                <div class="formSection">
                    <ul>
                        <li class="field">
                            <asp:Label ID="lblEmailAddress" runat="server" Text="Email Address:" AssociatedControlID="tbxEmailAddress" />
                            <asp:TextBox runat="server" ID="tbxEmailAddress" />
                            <asp:RequiredFieldValidator ID="rfvEmailAddress" runat="server" ControlToValidate="tbxEmailAddress"
                                Text="Email Address required" ValidationGroup="RegistrationValidation" Display="Dynamic"
                                CssClass="errorMsg" />
                            <asp:RegularExpressionValidator ID="revEmailAddress" ControlToValidate="tbxEmailAddress"
                                Text="Please enter a valid email address." runat="server" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                ValidationGroup="RegistrationValidation" Display="Dynamic" CssClass="errorMsg" />
                        </li>
                        <li class="field">
                            <asp:Label ID="lblPassword" runat="server" Text="New Password:" AssociatedControlID="tbxPassword" />
                            <asp:TextBox runat="server" ID="tbxPassword" TextMode="Password" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="dynamic" ControlToValidate="tbxPassword"
                                Text="Password must be 6-12 nonblank characters." ValidationExpression="[^\s]{6,12}"
                                ValidationGroup="RegistrationValidation" CssClass="errorMsg" />
                        </li>
                        <li class="field">
                            <asp:Label ID="lblConfirmPassword" runat="server" Text="Confirm New Password:" AssociatedControlID="tbxConfirmPassword" />
                            <asp:TextBox runat="server" ID="tbxConfirmPassword" TextMode="Password" />
                            <asp:CompareValidator ID="cmpPassword" runat="server" ControlToValidate="tbxConfirmPassword"
                                ControlToCompare="tbxPassword" Text="Passwords do not match" ValidationGroup="RegistrationValidation"
                                Display="Dynamic" CssClass="errorMsg" />
                            <asp:CompareValidator ID="cmpConfirmPassword" runat="server" ControlToValidate="tbxPassword"
                                ControlToCompare="tbxConfirmPassword" Text="Passwords do not match" ValidationGroup="RegistrationValidation"
                                Display="Dynamic" CssClass="errorMsg" />
                        </li>
                        <li class="field">
                            <asp:Label ID="lblPhone" runat="server" Text="Phone" AssociatedControlID="tbxPhone" />
                            <asp:TextBox runat="server" ID="tbxPhone" />
                            <asp:RequiredFieldValidator ID="rfvPhone" runat="server" ControlToValidate="tbxPhone"
                                Text="Phone required" ValidationGroup="RegistrationValidation" Display="Dynamic"
                                CssClass="errorMsg" />
                            
                        </li>
                    </ul>
                </div>
            </fieldset>
        </div>

        <div class="addressManager">
            <uc:AddressManagerControl runat="server" ID="AddressManagerControl" />
        </div>

        <div class="small-12 medium-12 large-12">
            <uc:UserInterests runat="server" ID="UserInterests" />
        </div>
        <div class="small-12 medium-12 large-12">
            <h3>Manage Email Preferences</h3>
            <p>Use the boxes below to opt in and out of communications about The Public Theater’s programs.</p>
            <dc:Mail2Signup ID="mail2Signup" MatchSubscriptions="True" PublicText="The Public Theater - plays and musicals downtown at Astor Place" JoesPubText="Joe’s Pub at The Public - music and cabaret downtown at Astor Place" ShakespeareText="Free Shakespeare in the Park" SubmitButton="False" SubmitText="" FirstChoice="1"  runat="server"/>
            <asp:HiddenField ClientIDMode="Static" ID="Mail2Public" runat="server" />
            <asp:HiddenField ClientIDMode="Static" ID="Mail2JoesPub" runat="server" />
            <asp:HiddenField ClientIDMode="Static" ID="Mail2Shakespeare"  runat="server" />
            <script type="text/javascript">
                $(document).ready(function() {
                    var Mail2ShakespeareEl = document.getElementById('<%= Mail2Shakespeare.ClientID%>');
                    var Mail2PublicEl = document.getElementById('<%= Mail2Public.ClientID%>');
                    var Mail2JoesPubEl = document.getElementById('<%= Mail2JoesPub.ClientID%>');
                    window.Mail2 = function() {
                        Mail2ShakespeareEl.value = $("#Mail2ShakespeareOption").prop('checked');
                        Mail2PublicEl.value = $("#Mail2PublicOption").prop('checked');
                        Mail2JoesPubEl.value = $("#Mail2JoesPubOption").prop('checked');
                    };
                });
            </script>
        </div>
        <div class="small-12 medium-12 large-12">
            <asp:Button runat="server" OnClientClick="window.Mail2()" ID="btnUpdateInformation" Text="Update Information" ValidationGroup="RegistrationValidation" CssClass="btn btnStandOut" />
            <asp:HyperLink runat="server" ID="lnkEmailPreference" Text="Update Your Email Preferences" CssClass="btn btnStandOut" Visible="False"></asp:HyperLink>
        </div>

    </div>
</div>
