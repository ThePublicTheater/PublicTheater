<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ForgotPasswordControl.ascx.cs"
    Inherits="TheaterTemplate.Web.Controls.AccountControls.ForgotPasswordControl" %>
<div id="forgotPasswordControl" class="forgotPassword">
    <asp:Panel runat="server" ID="pnlLookupByNumber" DefaultButton="btnSubmitNumberLookup" CssClass="formSection">
        <EPiServer:Property runat="server" ID="CustomerNumberHeader" PropertyName="CustomProperty1" />
        <asp:Literal runat="server" ID="litCustomerNumberErrors"></asp:Literal>
        <fieldset>
            <ul>
                <li class="field">
                    <asp:Label runat="server" ID="lblCustomerNumber" AssociatedControlID="txtCustomerNumber">Customer Number</asp:Label>
                    <asp:TextBox runat="server" ID="txtCustomerNumber"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvCustomerNumber" runat="server" ControlToValidate="txtCustomerNumber"
                        ValidationGroup="LookupByNumberValidation" Text="Customer Number required" CssClass="errorMsg"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="compValCustomerNumber" runat="server" Text="Customer Number must be an integer"
                        Operator="DataTypeCheck" Type="Integer" ValidationGroup="LookupByNumberValidation"
                        ControlToValidate="txtCustomerNumber" CssClass="errorMsg"></asp:CompareValidator>
                </li>
                <li class="field">
                    <asp:Label runat="server" ID="lblPhone" AssociatedControlID="txtPhone">Phone Number</asp:Label>
                    <asp:TextBox runat="server" ID="txtPhone"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvPhone" runat="server" ControlToValidate="txtPhone"
                        ValidationGroup="LookupByNumberValidation" Text="Phone Number required" CssClass="errorMsg"></asp:RequiredFieldValidator>
                </li>
                <li class="field">
                    <asp:Label runat="server" ID="lblPostalCode" AssociatedControlID="txtPostalCode">Postal Code</asp:Label>
                    <asp:TextBox runat="server" ID="txtPostalCode"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvPostalCode" runat="server" ControlToValidate="txtPostalCode"
                        ValidationGroup="LookupByNumberValidation" Text="Postal Code required" CssClass="errorMsg"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="regexValPostalCode" runat="server" Text="Postal code must be 5 numbers."
                        ValidationExpression="[\d]{5}" ValidationGroup="LookupByNumberValidation" ControlToValidate="txtPostalCode" CssClass="errorMsg"></asp:RegularExpressionValidator>
                </li>
            </ul>
        </fieldset>
        <asp:Button runat="server" ID="btnSubmitNumberLookup" CssClass="btn" ValidationGroup="LookupByNumberValidation"
            Text="Submit" OnClick="btnSubmitNumberLookup_Click" />
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlLookupByEmail" CssClass="formSection emailLookup" DefaultButton="btnSubmitEmailLookup">
        <EPiServer:Property runat="server" ID="EmailLookupHeader" PropertyName="CustomProperty3" />
        <asp:Literal runat="server" ID="litEmailErrors"></asp:Literal>
        <EPiServer:Property runat="server" ID="EmailLookupContent" PropertyName="CustomProperty4" />
        <ul>
            <li class="field">
                <asp:Label runat="server" ID="lblEmail" AssociatedControlID="txtEmail">Email Address</asp:Label>
                <asp:TextBox runat="server" ID="txtEmail"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEmail" ValidationGroup="LookupByEmailValidation" CssClass="errorMsg"></asp:RequiredFieldValidator>
            </li>
            <li>
                <asp:Button runat="server" ID="btnSubmitEmailLookup" CssClass="btn" ValidationGroup="LookupByEmailValidation"
                Text="Submit" OnClick="btnSubmitEmailLookup_Click" /> 
            </li>
        </ul>
        <EPiServer:Property runat="server" ID="EmailLookupSubcontent" PropertyName="CustomProperty5" />
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlEmailConfirmation" Visible="false">
        <EPiServer:Property runat="server" ID="EmailConfirmationContent" PropertyName="CustomProperty6" />
    </asp:Panel>
</div>
