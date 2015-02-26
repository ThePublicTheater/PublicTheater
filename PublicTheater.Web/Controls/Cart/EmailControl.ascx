<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EmailControl.ascx.cs" Inherits="TheaterTemplate.Web.Controls.CartControls.EmailControl" %>

<h4>Subject</h4>
<asp:TextBox runat="server" ID="SubjectLine"></asp:TextBox>

<h4>Email Body</h4>
<asp:PlaceHolder runat="server" ID="HtmlEmailSection">
    <asp:Literal runat="server" ID="HtmlEmailOutput"></asp:Literal>
</asp:PlaceHolder>
<asp:PlaceHolder runat="server" ID="TextEmailSection">
    <asp:TextBox runat="server" ID="TextEmailOutput" TextMode="MultiLine" Width="500px" Height="500px"></asp:TextBox>
</asp:PlaceHolder>