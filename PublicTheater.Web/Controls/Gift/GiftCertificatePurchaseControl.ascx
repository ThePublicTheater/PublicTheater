<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GiftCertificatePurchaseControl.ascx.cs" Inherits="TheaterTemplate.Web.Controls.GiftControls.GiftCertificatePurchaseControl" %>
<%@ Import Namespace="TheaterTemplate.Shared.EpiServerConfig" %>
<EPiServer:Property runat="server" ID="propCertificatePageHeader" PropertyName="Header" DisplayMissingMessage="false" />             
<EPiServer:Property runat="server" ID="propCertificatePageContent" PropertyName="Body" DisplayMissingMessage="false" />

<asp:Repeater ID="rptCertificateDesign" runat="server" OnItemDataBound="rptCertificateDesign_DataBound">
    
    <HeaderTemplate>
        <div class="chooseDesign">
            <h3>Choose a Design</h3>    
            <ul class="thumbnails">
                <!-- placeholder -->
               
    </HeaderTemplate>

    <ItemTemplate>
        <li class="span3">
            <div class="thumbnail">
                <asp:Image ID="imgTemplateThumbnail" runat="server" CssClass="certificateGraphic" />
                <p><asp:Literal ID="ltrDesignDescription" runat="server" /></p>
            </div>
        </li>
    </ItemTemplate>

    <FooterTemplate>
            </ul>
        </div>    
    </FooterTemplate>
</asp:Repeater>

<div class="customizeGiftCert">
    <h3>Customize Gift Certificate</h3>
    <fieldset class="formSection">
        <asp:ValidationSummary ID="valGiftCertificateErrorSummary" runat="server" CssClass="errorMsg" ValidationGroup="CertificateParams" />
        <input type="hidden" id="hdnSelectedDesign" class="selectedDesignId" runat="server" qsparam="DesignPath" value=""/>
        
        <div class="row-fluid amountRow">
            
            <div class="large-6 medium-12 small-12 giftCertcol">
                <label>Amount</label>
                <asp:RequiredFieldValidator CssClass="errorMsg" ID="valAmount" runat="server" ControlToValidate="txtAmount" ErrorMessage="The amount entered was not valid" ValidationGroup="CertificateParams">*</asp:RequiredFieldValidator>
                <asp:TextBox ID="txtAmount" runat="server" qsparam="Amount" CssClass="previewItem" />
                
                <%--<asp:RegularExpressionValidator ID="valAmountIsNumber" ControlToValidate="txtAmount" ErrorMessage="The amount entered was not valid" ValidationExpression="^[0-9]+(\.[0-9][0-9])?$" ValidationGroup="CertificateParams" runat="server">*</asp:RegularExpressionValidator>--%>
                <label>To</label>
                <asp:RequiredFieldValidator CssClass="errorMsg" ID="valTo" runat="server" ControlToValidate="txtTo" ValidationGroup="CertificateParams" ErrorMessage="Please enter name in the To field">*</asp:RequiredFieldValidator>
                <asp:TextBox ID="txtTo" runat="server" qsparam="To" CssClass="previewItem" />
                
                <label>From</label>
                <asp:RequiredFieldValidator ID="valFrom" runat="server" ControlToValidate="txtFrom" ValidationGroup="CertificateParams" CssClass="errorMsg" ErrorMessage="Please enter name in the From field">*</asp:RequiredFieldValidator>
                <asp:TextBox ID="txtFrom" runat="server" qsparam="From" CssClass="previewItem" />
                
            </div>
            
            <div class="large-6 medium-12 small-12 giftCertcol">
                <label>Recipient's Email</label>
                <asp:TextBox ID="txtEmail" runat="server" />
                <asp:RegularExpressionValidator ID="valValidEmail" ControlToValidate="txtEmail" ErrorMessage="The email entered was not valid" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="CertificateParams" runat="server">*</asp:RegularExpressionValidator>
                <label>Verify email</label>
                <asp:TextBox ID="txtVerifyEmail" runat="server" />
                <asp:CompareValidator ID="valEmailsMatch" runat="server" ControlToValidate="txtVerifyEmail" ValidationGroup="CertificateParams" ControlToCompare="txtEmail" Operator="Equal" ErrorMessage="The emails do not match">*</asp:CompareValidator>
            
                <label>Message</label>
                <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine" Rows="8" Columns="31" qsparam="Message" CssClass="previewItem" />
                <asp:RequiredFieldValidator ID="valMessage" CssClass="errorMsg" runat="server" ControlToValidate="txtMessage" ValidationGroup="CertificateParams" ErrorMessage="Please enter a message for the gift certificate">*</asp:RequiredFieldValidator>
            </div>

        </div>
        
        <div class="row-fluid">
            <div class="previewRow">
                <h3>Gift Certificate Preview</h3>
                <img id="imgPreview" alt="Gift Certificate Preview" />
                <input type="hidden" id='imageServicePath' value='<%= TheaterSharedConfig.GetValue("GIFT_CERTIFICATE_IMAGE_SERVICE_URL", "/gift/certificategraphic.aspx") %>'/>
            </div>
        </div>
        <div class="row-fluid clearfix">
            <asp:Button ID="btnAddToCart" runat="server" Text="Add to Cart" OnClick="btnAddToCart_Click" CausesValidation="true" CssClass="btn solid btnStandOut" ValidationGroup="CertificateParams" />
        </div>
    </fieldset>
</div>

<div class="giftCertificatePageFooter">
    <EPiServer:Property runat="server" ID="propCertificatePageFooter" PropertyName="Footer" DisplayMissingMessage="false" />         
</div>

<asp:Panel runat="server" ID="pnlError" CssClass="errorBox" Visible="false">
    <asp:Label ID="lblErrorMessage" runat="server" />            
</asp:Panel>