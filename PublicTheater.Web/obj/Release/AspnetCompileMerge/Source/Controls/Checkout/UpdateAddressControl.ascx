<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UpdateAddressControl.ascx.cs"
    Inherits="PublicTheater.Web.Controls.Checkout.UpdateAddressControl" %>
<asp:panel id="NewAddress" runat="server" cssclass="newAddress form">
    <fieldset>
        <h3>
            <asp:literal id="ltrAddressControlTitle" text="New Address" runat="server" /></h3>
        <asp:label runat="server" id="lbPlaceHolderAddressMessage"></asp:label>
        <ul class="unstyled">
            <asp:panel runat="server" id="missingClientNamePanel" visible="false">
                <li class="field">
                    <asp:label id="lblFirstName" runat="server" text="First Name" associatedcontrolid="txtFirstName" />
                    <asp:textbox runat="server" id="txtFirstName">
                    </asp:textbox>
                    <asp:requiredfieldvalidator runat="server" id="reqFirstName" controltovalidate="txtFirstName"
                        text="First name required" display="Dynamic" cssclass="errorMsg" />
                </li>
                <li class="field">
                    <asp:label id="lblLastName" runat="server" text="Last Name" associatedcontrolid="txtLastName" />
                    <asp:textbox runat="server" id="txtLastName">
                    </asp:textbox>
                    <asp:requiredfieldvalidator runat="server" id="reqLastName" controltovalidate="txtLastName"
                        text="Last name required" display="Dynamic" cssclass="errorMsg" />
                </li>
            </asp:panel>
            <li class="field">
                <asp:label id="lbltxtStreetAddress" runat="server" text="Address 1" associatedcontrolid="txtStreetAddress1" />
                <asp:textbox runat="server" id="txtStreetAddress1">
                </asp:textbox>
                <asp:requiredfieldvalidator runat="server" id="reqStreetAddress1" controltovalidate="txtStreetAddress1"
                    text="Address required" display="Dynamic" cssclass="errorMsg" />
            </li>
            <li>
                <asp:label id="lbltxtStreetAddress2" runat="server" text="Address 2" associatedcontrolid="txtStreetAddress2" />
                <asp:textbox runat="server" id="txtStreetAddress2">
                </asp:textbox>
            </li>
            <li class="field">
                <div class="leftCol">
                    <asp:label id="lbltxtCity" runat="server" text="City" associatedcontrolid="txtCity" />
                    <asp:textbox runat="server" id="txtCity">
                    </asp:textbox>
                    <asp:requiredfieldvalidator runat="server" id="reqCity" controltovalidate="txtCity"
                        text="City required" display="Dynamic" cssclass="errorMsg" />
                </div>
                <div class="rightCol">
                    <asp:label id="lblddlState" runat="server" text="State" associatedcontrolid="ddlState" />
                    <asp:dropdownlist runat="server" id="ddlState">
                    </asp:dropdownlist>
                </div>
            </li>
            <li class="field">
                <div class="leftCol">
                    <asp:label id="lbltxtPostalCode" runat="server" text="Postal Code" associatedcontrolid="txtPostalCode" />
                    <asp:textbox runat="server" id="txtPostalCode">
                    </asp:textbox>
                    <asp:requiredfieldvalidator runat="server" id="reqPostalCode" controltovalidate="txtPostalCode"
                        text="Postal Code required" display="Dynamic" cssclass="errorMsg" />
                    <asp:regularexpressionvalidator runat="server" id="regexPostalCode" controltovalidate="txtPostalCode"
                        validationexpression="^(\d{5}|\d{5}\-\d{4})$" text="Postal code must be numeric (#####) or (#####-####)"
                        display="Dynamic" cssclass="errorMsg" />
                </div>
                <div class="rightCol">
                    <asp:label id="lblddlCountry" runat="server" text="Country" associatedcontrolid="ddlCountry" />
                    <asp:dropdownlist runat="server" id="ddlCountry" autopostback="true">
                    </asp:dropdownlist>
                </div>
            </li>
        </ul>
    </fieldset>
</asp:panel>
