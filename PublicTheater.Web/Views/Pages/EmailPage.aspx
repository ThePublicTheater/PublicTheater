<%@ Page Title="" Language="C#"
    AutoEventWireup="true" CodeBehind="EmailPage.aspx.cs" Inherits="TheaterTemplate.Web.Views.Pages.EmailPage" %>
<asp:Literal runat="server" id="htmlEmailBody" />
<asp:PlaceHolder runat="server" id="textEmailBody" visible="false">
    <h2>
        Subject: <asp:Literal runat="server" id="textSubject" />
    </h2>
    <pre>
        <asp:Literal runat="server" id="textBody" />
    </pre>
</asp:PlaceHolder>