<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="XFormControl.ascx.cs"
    Inherits="TheaterTemplate.Web.Views.Controls.XFormControl" %>
<asp:Panel runat="server" ID="FormPanel" CssClass="xForm">
    <XForms:XFormControl ID="FormControl" runat="server" EnableClientScript="false" ValidationGroup="XForm" />
</asp:Panel>
<asp:Panel runat="server" ID="StatisticsPanel" Visible="false">
    <asp:Literal ID="NumberOfVotes" runat="server" />
    <!-- Set StatisticsType to format output: N=numbers only, P=percentage -->
    <EPiServer:XFormStatistics StatisticsType="P" runat="server" ID="Statistics" PropertyName="XForm" />
</asp:Panel>
<br />
<asp:Button runat="server" ID="SwitchButton" CssClass="button" OnClick="SwitchView"
    CausesValidation="false" Text="<%$ Resources: EPiServer, form.showstat %>" />