<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddressDisplayControl.ascx.cs" Inherits="PublicTheater.Web.Controls.Checkout.AddressDisplayControl" %>
<asp:Panel runat="server" ID="nameContainer" CssClass="name" Visible="false"><asp:Literal runat="server" ID="ltrName" /></asp:Panel>
    <address>
        <asp:Literal runat="server" ID="ltrAddress" /><br />
        <asp:Literal runat="server" ID="ltrCity" />,
        <asp:Literal runat="server" ID="ltrState" />
        <br />
        <asp:Literal runat="server" ID="ltrCountry" />
        <asp:Literal runat="server" ID="ltrZip" />
        <br />
        <asp:Literal runat="server" ID="ltrEmailAddress" />
        <br />        
    </address>