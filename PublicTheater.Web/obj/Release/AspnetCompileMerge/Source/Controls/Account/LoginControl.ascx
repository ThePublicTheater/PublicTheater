<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoginControl.ascx.cs" Inherits="PublicTheater.Web.Controls.Account.LoginControl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

    <div id="loginContainer">
        
        <div class="large-5 columns">
            <asp:Panel runat="server" ID="loginPanel" DefaultButton="btnLogin" CssClass="login">
                <h2>
                    <EPiServer:Property runat="server" ID="LoginHeader" PropertyName="LoginHeader" />
                </h2>
                <asp:Panel runat="server" ID="pnlError" CssClass="errorBox" Visible="false">
                    <EPiServer:Property runat="server" ID="invalidLogin" PropertyName="LoginFailedText" DisplayMissingMessage="false" Visible="false" />
                    <EPiServer:Property runat="server" ID="invalidPromo" PropertyName="PromoCodeError" DisplayMissingMessage="false" Visible="false" />
                    <EPiServer:Property runat="server" ID="loginError" PropertyName="LoginError" DisplayMissingMessage="false" Visible="false" />
                    <EPiServer:Property runat="server" ID="resetSuccessful" PropertyName="PasswordResetSuccessfully" DisplayMissingMessage="false" Visible="false" />
                    <EPiServer:Property runat="server" ID="resetError" PropertyName="PasswordResetError" DisplayMissingMessage="false" Visible="false" />
                    <EPiServer:Property runat="server" ID="guestContinueFailed" PropertyName="GuestCheckoutAccountExistsError" DisplayMissingMessage="false" Visible="false" />
                </asp:Panel>
                <asp:ValidationSummary runat="server" ID="vsLoginPage" ValidationGroup="vgLogin" CssClass="errorBox" />

                <ul class="loginForm">
                    <li>
                        <asp:Label runat="server" ID="lblUsername" AssociatedControlID="tbxUsername" Text="Email Address:" />
                        <asp:RequiredFieldValidator ID="rfvUsername" CssClass="errorAsterisk" runat="server" ValidationGroup="vgLogin" ControlToValidate="tbxUsername" ErrorMessage="Email Address cannot be blank." Text="*" />
                        <asp:TextBox runat="server" ID="tbxUsername" />                   
                    </li>
                    <li>
                        <asp:Label runat="server" ID="lblPassword" Text="Password:" AssociatedControlID="tbxPassword" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="errorAsterisk" runat="server" ValidationGroup="vgLogin" ControlToValidate="tbxPassword" ErrorMessage="Password cannot be blank." Text="*" />
                        <asp:TextBox runat="server" ID="tbxPassword" TextMode="Password" />                      
                    </li>
                    <li>
                        <asp:PlaceHolder ID="phRememberMe" runat="server">
                            <div class="rememberMe">
                                <asp:CheckBox ID="cbxRememberMe" runat="server" Text="Remember Me" />
                            </div>
                        </asp:PlaceHolder>
                    </li>
                    <li>
                        <a id="forgotPassword" runat="server">Forgot Password?</a>
                    </li>
                    <li class="havePromoCode">
                        <asp:PlaceHolder ID="phPromoCode" runat="server">
                            <a href="#" id="havePromo">I have a promo code</a>
                            <div id="enterPromoCode" class="field">
                                <asp:Label runat="server" ID="lblPromoCode" Text="Enter Your Promo Code:" AssociatedControlID="tbxPromoCode" />
                                <asp:TextBox runat="server" ID="tbxPromoCode" CssClass="loginPromo"/>
                            </div>
                        </asp:PlaceHolder>
                    </li>
                    <li>
                        <asp:Button runat="server" ID="btnLogin" Text="Login" CssClass="btn" ValidationGroup="vgLogin" />
                    </li>
                </ul>
            </asp:Panel>
        </div>
       
        <asp:Panel ID="ModalPanelRetrievePassword" CssClass="simpleModal" DefaultButton="btnRetrievePassword" runat="server">
                <div class="field">
                    <asp:Label ID="lblEmailRetrievePassword" AssociatedControlID="tbxEmailRetrievePassword"
                        Text="Enter Your Email Address:" runat="server" />
                    <asp:TextBox runat="server" ID="tbxEmailRetrievePassword" />
                    <div class="actions">
                        <asp:Button runat="server" ID="btnRetrievePassword" CssClass="btn btnStandOut" Text="Reset Password" />
                        <asp:Button runat="server" ID="btnCancelRetrievePassword" CssClass="btn" Text="Cancel" />
                    </div>
                </div>
            </asp:Panel>
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender" runat="server" TargetControlID="forgotPassword"
                BehaviorID="themodals" PopupControlID="ModalPanelRetrievePassword" BackgroundCssClass="bgOverlay"
                CancelControlID="btnCancelRetrievePassword" />
        
        
        
        <div class="large-4 columns">
            <asp:Panel runat="server" ID="registerPanel" CssClass="register">
                <EPiServer:Property runat="server" ID="SecondaryBody" PropertyName="SecondaryBody" DisplayMissingMessage="False" />
                <div class="submit">
                    <asp:HyperLink runat="server" ID="lnkRegister" Text="Register" CssClass="btn" />
                </div>
            </asp:Panel>

        </div>

        <div class="large-3 columns">
            <asp:Panel runat="server" ID="ContinueAsGuestPanel" DefaultButton="btnContinueAsGuest" CssClass="login">
                <EPiServer:Property runat="server" ID="ContinueAsGuestHeader" PropertyName="ContinueAsGuestHeader" />

                <asp:Panel runat="server" ID="Panel2" CssClass="errorBox" Visible="false">
                    <EPiServer:Property runat="server" ID="Property2" PropertyName="LoginFailedText" DisplayMissingMessage="false" Visible="false" />
                </asp:Panel>

                <asp:ValidationSummary runat="server" ID="ContinueAsGuestValidationSummary" ValidationGroup="vgContinueAsGuest" CssClass="errorBox" />

                <ul class="continueAsGuestForm">
                    <li>
                        <asp:Label runat="server" ID="lblGuestEmailAddress" AssociatedControlID="tbxContinueAsGuestEmail" Text="Username:" />
                        <asp:TextBox runat="server" ID="tbxContinueAsGuestEmail" />
                        <asp:RequiredFieldValidator ID="ContinueAsGuestEmailRequired" runat="server" ValidationGroup="vgContinueAsGuest" ControlToValidate="tbxContinueAsGuestEmail" ErrorMessage="Email address cannot be blank." Text="*" />
                        <asp:RegularExpressionValidator
                            ID="EmailAddressFormatValidation"
                            ControlToValidate="tbxContinueAsGuestEmail"
                            ValidationGroup="vgContinueAsGuest"
                            Text="*"
                            ErrorMessage="Invalid Email Address"
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                            runat="server" />
                    </li>
                    <li>
                        <asp:Button runat="server" ID="btnContinueAsGuest" Text="Continue as Guest" CssClass="btn" ValidationGroup="vgContinueAsGuest" />
                    </li>
                </ul>
            </asp:Panel>
        </div>
    </div>

