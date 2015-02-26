<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DonationTicketBlockControl.ascx.cs" Inherits="PublicTheater.Web.Views.Blocks.DonationTicketBlockControl" %>

<asp:Panel ID="Panel1" runat="server" CssClass="block noHeight">
<h2> <EPiServer:Property runat="server"  ID="header" 
                    PropertyName="Header" DisplayMissingMessage="false" /></h2>
<hr/>
<asp:Panel ID="AvailablePanel" runat="server" >
    <div><EPiServer:Property runat="server"  ID="mainBody"
                    PropertyName="MainBody" DisplayMissingMessage="false" />
        </div>
    <div style="margin-top: 20px; margin-bottom: 20px;">
    Quantity: <asp:DropDownList ID="qtyddl" runat="server"></asp:DropDownList>
        </div>
    <asp:Button ID="reserveButton" runat="server" Text="Reserve" OnClick="reserveButton_Click" />
</asp:Panel>
<asp:Panel ID="SoldOutPanel" runat="server" Visible="False" >
    <EPiServer:Property runat="server"  ID="soldOutHtml"
                    PropertyName="SoldOutHtml" DisplayMissingMessage="false" />
</asp:Panel>
   </asp:Panel>
