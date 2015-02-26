<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchTextBox.ascx.cs"
    Inherits="PublicTheater.Web.Views.Controls.SearchTextBox" %>
<asp:panel runat="server" id="SearchControl" cssclass="search" defaultbutton="btnSearch" ClientIDMode="Static">
    <asp:textbox runat="server" placeholder="Search" id="SearchText" clientidmode="Static"></asp:textbox>
    <asp:linkbutton runat="server" id="btnSearch" cssclass="btnSearch" clientidmode="Static"><i class="icon-search"></i></asp:linkbutton>
    <asp:HiddenField runat="server" ID="hidSearchPageUrl" ClientIDMode="Static"/>
    <asp:HiddenField runat="server" ID="hidSearchPartnerID" ClientIDMode="Static"/>
</asp:panel>