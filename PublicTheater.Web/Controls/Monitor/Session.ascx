<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Session.ascx.cs" Inherits="TheaterTemplate.Web.Controls.MonitorControls.Session" %>
    <br />
    <div>
        <asp:HyperLink runat="server" ID="lnkCurrentAPI" CssClass="btn" Target="_blank" Text="API Link" />
        <asp:HyperLink runat="server" ID="lnkCartData" CssClass="btn" NavigateUrl="~/_monitor/CartData.aspx" Target="_blank" Text="Cart Link" />
        <asp:HyperLink runat="server" ID="lnkCache" CssClass="btn" Text="Cache Page" NavigateUrl="~/_monitor/cache.aspx" />
    </div>
    <br />
    <table class="table table-condensed">
        <thead>
            <tr>
                <th>Key</th>
                <th>Value</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td>Current MOS</td>
                <td><asp:Literal runat="server" ID="ltrMOS" /></td>
            </tr>
            <tr>
                <td>Current Promo</td>
                <td><asp:Literal runat="server" ID="ltrPromo" /></td>
            </tr>
            <tr>
                <td>Session Key</td>
                <td><asp:Literal runat="server" ID="ltrSessionKey" /></td>
            </tr>
            <tr>
                <td>Customer Number</td>
                <td><asp:Literal runat="server" ID="ltrCustomerNumber" /></td>
            </tr>
        </tbody>
    </table>
    <br />
    <br />
    <div class="row-fluid">
        <div class="span6">
            <label>Get Config Key/Value:</label>
            <div class="input-append">
                <asp:TextBox runat="server" ID="configKey" />
                <asp:Button runat="server" ID="getConfigValue" Text="Get Value" OnClick="getConfigValueText" CssClass="btn" />
            </div>
        </div>
        <div class="span4">
            <asp:Literal runat="server" ID="configValue" EnableViewState="false" />
        </div>
    </div>
    <div class="clearfix">
        <span class="pull-left">
            <asp:Literal runat="server" ID="emailSentMsg" Text="<br />Email Sent" Visible="false" EnableViewState="false" />
        </span>
        <asp:Button runat="server" ID="lnkSendConfirmationEmail" Text="Send Confirmation" OnClick="SendConfirmationEmail" CssClass="pull-right btn" />
    </div>