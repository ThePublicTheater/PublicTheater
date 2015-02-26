<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PdpFromTess.ascx.cs" Inherits="PublicTheater.Web.modules.PdpFromTess.PdpFromTess" %>
<asp:UpdatePanel runat="server" ID="UpPanel">
    <ContentTemplate>
        <asp:HiddenField runat="server" ID="createPdpHF" ClientIDMode="Static"></asp:HiddenField>
        <asp:Button runat="server" ID="createButton" OnClick="CreatePdp" Style="display: none;" />
    </ContentTemplate>
</asp:UpdatePanel>
<div id="ProdDiv" runat="server">
</div>
