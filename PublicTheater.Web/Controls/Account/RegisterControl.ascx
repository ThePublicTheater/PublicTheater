<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="RegisterControl.ascx.cs"
    Inherits="PublicTheater.Web.Controls.Account.RegisterControl" %>
<%@ Register TagPrefix="uc" Src="~/controls/checkout/UpdateAddressControl.ascx" TagName="UpdateAddressControl" %>
<%@ Register TagPrefix="uc" Src="~/controls/account/UserInterests.ascx" TagName="UserInterests" %>
<%@ Register TagPrefix="recaptcha" Namespace="Recaptcha" Assembly="Recaptcha" %>

<EPiServer:Property runat="server" ID="propHeader" DisplayMissingMessage="false" PropertyName="Header" />
<div id="registerControl">
    <div class="row-fluid">
        <div class="span12">
            <EPiServer:Property runat="server" ID="propDuplicateEmailError" DisplayMissingMessage="false" PropertyName="DuplicateEmailError" Visible="false" CssClass="errorBox" />
            <EPiServer:Property runat="server" ID="propDuplicateLoginError" DisplayMissingMessage="false" PropertyName="DuplicateLoginError" Visible="false" CssClass="errorBox" />
            <EPiServer:Property runat="server" ID="propGenericError" DisplayMissingMessage="false" PropertyName="GenericError" Visible="false" CssClass="errorBox" />
            <EPiServer:Property runat="server" ID="propInvalidPromoError" DisplayMissingMessage="false" PropertyName="InvalidPromoError" Visible="false" CssClass="errorBox" />
            <EPiServer:Property runat="server" ID="propDuplicateAccountEmailError" DisplayMissingMessage="false" PropertyName="DuplicateAccountEmailError" Visible="false" CssClass="errorBox" />
        </div>

        <%-- Left Side --%>
        <div class="leftRegister large-6 medium-12 small-12">
            <div class="formSection form">
                <h3>Your Information</h3>
                <ul class="unstyled">
                    <li class="field">
                        <asp:Label ID="lblEmailAddress" runat="server" Text="Email Address:" AssociatedControlID="tbxEmailAddress" />
                        <asp:TextBox runat="server" ID="tbxEmailAddress" />
                        <asp:RequiredFieldValidator ID="rfvEmailAddress" runat="server" ControlToValidate="tbxEmailAddress"
                            Text="Email address required" ValidationGroup="RegistrationValidation" Display="Dynamic"
                            CssClass="errorMsg" />
                        <asp:RegularExpressionValidator ID="revEmailAddress" ControlToValidate="tbxEmailAddress"
                            Text="Please enter a valid email address." runat="server" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                            ValidationGroup="RegistrationValidation" Display="Dynamic" CssClass="errorMsg" />
                    </li>
                    <li class="field">
                        <asp:Label ID="lblPassword" runat="server" Text="Password:" AssociatedControlID="tbxPassword" />
                        <asp:TextBox runat="server" ID="tbxPassword" TextMode="Password" />
                        <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="tbxPassword"
                            Text="Password required" ValidationGroup="RegistrationValidation" Display="Dynamic"
                            CssClass="errorMsg" />                
                        <asp:RegularExpressionValidator runat="server" Display="dynamic" ControlToValidate="tbxPassword"
                            Text="Password must be 6-12 nonblank characters" ValidationExpression="[^\s]{6,12}"
                            ValidationGroup="RegistrationValidation" CssClass="errorMsg" />
                    </li>
                    <li class="field">
                        <asp:Label ID="lblConfirmPassword" runat="server" Text="Confirm Password:" AssociatedControlID="tbxConfirmPassword" />
                        <asp:TextBox runat="server" ID="tbxConfirmPassword" TextMode="Password" />
                                            <asp:RequiredFieldValidator ID="rfvPasswordConfirmation" runat="server" ControlToValidate="tbxConfirmPassword"
                            Text="Password confirmation required" ValidationGroup="RegistrationValidation" Display="Dynamic"
                            CssClass="errorMsg" />
                        <asp:CompareValidator ID="cmpConfirmPassword" runat="server" ControlToValidate="tbxConfirmPassword"
                            ControlToCompare="tbxPassword" Text="Passwords do not match" ValidationGroup="RegistrationValidation"
                            Display="Dynamic" CssClass="errorMsg" />
                    </li>
                    <li class="field">
                        <asp:Label ID="lblFirstName" runat="server" Text="First Name:" AssociatedControlID="tbxFirstName" />
                        <asp:TextBox runat="server" ID="tbxFirstName" />
                        <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="tbxFirstName"
                            Text="First name required" ValidationGroup="RegistrationValidation" Display="Dynamic"
                            CssClass="errorMsg" />
                    </li>
                    <li class="field">
                        <asp:Label ID="lblLastName" runat="server" Text="Last Name:" AssociatedControlID="tbxLastName" />
                        <asp:TextBox runat="server" ID="tbxLastName" />
                        <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="tbxLastName"
                            Text="Last name required" ValidationGroup="RegistrationValidation" Display="Dynamic"
                            CssClass="errorMsg" />
                    </li>
                    <li class="field">
                        <asp:Label ID="lblPhone" runat="server" Text="Phone" AssociatedControlID="tbxPhone" />
                        <asp:TextBox runat="server" ID="tbxPhone" />
                        <asp:RequiredFieldValidator ID="rfvPhone" runat="server" ControlToValidate="tbxPhone"
                            Text="Phone number required" ValidationGroup="RegistrationValidation" Display="Dynamic"
                            CssClass="errorMsg" />
                            
                    </li>
                </ul>
            </div>
        </div>

        <%-- Right side --%>
        <div class="rightRegister large-6 medium-12 small-12">
            <uc:UpdateAddressControl runat="server" ID="UpdateAddressControl"></uc:UpdateAddressControl>      
        </div>

        <div class="large-12 medium-12 small-12">
            <uc:UserInterests runat="server" ID="UserInterests" />
        </div>

        <div class="submitRegister large-6 medium-12 small-12">
                     <recaptcha:RecaptchaControl ID="Recaptcha1" PublicKey="6Lff_fASAAAAAJK3ebipq26VGepiNkIeOK1xE6Wu" PrivateKey="6Lff_fASAAAAANrgjcUPZoafxHwI-P0B2QDkSDOK" Theme="Clean" runat="server" />
        <asp:CustomValidator ID="CustomValidator1" runat="server" Text="ReCaptcha Failed – Please enter the two words or numbers correctly to help us avoid spam." ValidationGroup="RegistrationValidation" CssClass="errorMsg" ClientValidationFunction="clientValidateCaptcha" OnServerValidate="ValidateCaptcha" style="margin-left:0px;"></asp:CustomValidator>
            <span class="errorMsg" style="margin-left:0px; display:none;" runat="server" ID="captchaSuccessMsg">ReCaptcha Successful – Please complete the form to register.</span>
            <script>
                captchaExists = true;
                function clientValidateCaptcha(src, args) {
                    if (!captchaExists) { args.IsValid = true; return; }

                    var isValid;
                    var captchaInfo =
                    {
                        challengeValue: Recaptcha.get_challenge(),
                        responseValue: Recaptcha.get_response(),
                    };
                    $.ajax({
                        type: "POST",
                        url: "/Services/RecaptchaClient.asmx/ValidateCaptcha",
                        data: JSON.stringify(captchaInfo),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        async: false,
                        success: function (msg) {
                            isValid = msg.d;
                        }
                    });
                    args.IsValid = isValid;
                    if (!isValid) {
                        Recaptcha.reload();
                    } else {
                        Recaptcha.destroy();
                        captchaExists = false;
                    }
                }
                var originalValidationFunction = Page_ClientValidate;
                if (originalValidationFunction && typeof (originalValidationFunction) == "function") {
                    Page_ClientValidate = function (validationGroup) {
                        originalValidationFunction(validationGroup);

                        if (!Page_IsValid && !captchaExists) {
                            $("#<%=captchaSuccessMsg.ClientID%>").show();
                        }
                    };
                }
            </script>
            <script runat="server">
                                                
                void ValidateCaptcha(object source, ServerValidateEventArgs args)
                {
                    args.IsValid = (bool)(Session["reCaptchaValid"] ?? false);
                    
                }

            </script>


            <asp:PlaceHolder ID="phPromoCode" runat="server" Visible="False">
                <div class="formSection">
                    <a href="#" id="havePromo">I have a promo code</a>
                </div>
                <div id="enterPromoCode" class="field formSection">
                    <%--<asp:Label runat="server" ID="lblPromoCode" Text="Enter Your Promo Code:" AssociatedControlID="tbxPromoCode" />--%>
                    <asp:TextBox runat="server" ID="tbxPromoCode" placeholder="Enter Source Code Here" />
                </div>
            </asp:PlaceHolder>
            <div class="registerSubmitBtn">
                <asp:Button runat="server" ID="btnSubmitRegistration" Text="Register" ValidationGroup="RegistrationValidation"
                    CssClass="btn" />
            </div>
        </div>


    </div>
</div>
