<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChangePasswordControl.ascx.cs"
    Inherits="TheaterTemplate.Web.Controls.AccountControls.ChangePasswordControl" %>

<h3>Change Password</h3>

<div id="changePasswordControl" class="formSection">
    <asp:Panel ID="pnlChangeForm" runat="server" DefaultButton="btnSubmit">
        <fieldset>
            <ul>
                <li class="field">
                    <asp:Label runat="server" ID="lblNewUserName" AssociatedControlID="txtNewUserName">
                        User Name</asp:Label>
                    <asp:TextBox ID="txtNewUserName" runat="server" />
                    <asp:RequiredFieldValidator ID="rfvNewUserName" runat="server" Text="User Name required"
                        ControlToValidate="txtNewUserName" ValidationGroup="ChangePasswordValidation"
                        Display="Dynamic" CssClass="errorMsg"></asp:RequiredFieldValidator>
                </li>
                <li class="field">
                    <asp:Label runat="server" ID="lblNewPassword" AssociatedControlID="txtNewPassword">
                        New Password</asp:Label>
                    <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" />
                    <asp:RequiredFieldValidator ID="rfvNewPassword" runat="server" Text="Password required"
                        ControlToValidate="txtNewPassword" ValidationGroup="ChangePasswordValidation"
                        Display="Dynamic" CssClass="errorMsg"></asp:RequiredFieldValidator>
                </li>
                <li class="field">
                    <asp:Label runat="server" ID="lblConfirmPassword" AssociatedControlID="txtConfirmPassword">
                        Confirm New Password</asp:Label>
                    <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" />
                    <asp:RequiredFieldValidator ID="rfvConfirmPassword" runat="server" Text="Password Confirmation required"
                        ControlToValidate="txtConfirmPassword" ValidationGroup="ChangePasswordValidation"
                        Display="Dynamic" CssClass="errorMsg"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="compareValidConfirmPassword" runat="server" Text="Password fields must match"
                        ControlToValidate="txtConfirmPassword" ControlToCompare="txtNewPassword" ValidationGroup="ChangePasswordValidation"
                        Display="Dynamic" CssClass="errorMsg"></asp:CompareValidator>
                </li>
                <asp:Button ID="btnSubmit" CssClass="btn" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                    ValidationGroup="ChangePasswordValidation" />
            </ul>
        </fieldset>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlError" CssClass="errorBox" Visible="false">
        <asp:Literal runat="server" ID="litErrorMessage" />
    </asp:Panel>
</div>


<asp:Panel ID="ModalPanelAddNewEmail" CssClass="simpleModal" runat="server" Visible="false">
    <asp:Button runat="server" ID="btnHiddenSubmit" Style="display:none" />
    <asp:Button runat="server" ID="btnHiddenCancel" Style="display:none" />
    <div id="subscriptionBuilder">
        <div>
            <p>
                There is currently no e-mail address associated with this account. Please enter an e-mail address to associate.
            </p>
        </div>
        <div class="field">
            <asp:Label ID="lblEmailRetrievePassword" AssociatedControlID="txtNewEmail"
                Text="Enter Your Email Address:" runat="server" />
            <asp:TextBox runat="server" ID="txtNewEmail" />
            <asp:RegularExpressionValidator ID="revEmailAddress" ControlToValidate="txtNewEmail"
                Text="Please enter a valid email address." runat="server" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                ValidationGroup="AddNewEmail" Display="Dynamic" CssClass="errorMsg" />
            <div class="actions">
                <asp:Button runat="server" ID="btnAddEmail" CssClass="btn btnStandOut" Text="Add E-Mail" ValidationGroup="AddNewEmail" />
            </div>
        </div>
    </div>
    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender" runat="server" TargetControlID="btnHiddenSubmit"
        BehaviorID="themodals" PopupControlID="ModalPanelAddNewEmail" BackgroundCssClass="bgOverlay" 
        CancelControlID="btnHiddenCancel" />
</asp:Panel>
