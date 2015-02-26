<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PrintTicketsControl.ascx.cs" Inherits="TheaterTemplate.Web.Controls.Account.PrintTicketsControl" %>


<asp:Panel runat="server" id="pnlPrintWarning" Visible="false">
These tickets contain some unprinted items. If you view these items they will not be printed for you at the box office. Are you sure you want to proceed?
<asp:LinkButton runat="server" id="lbConfirmView" Text="Continue" />
</asp:Panel>

<asp:ListView runat="server" Id="lvTickets" Visible="false">
<LayoutTemplate>
    <ul>
        <li runat="server" id="itemPlaceholder" />
    </ul>
</LayoutTemplate>
<ItemTemplate>
    <li>                
        <asp:Label ID="lblPerformanceTitle" runat="server"><%# Eval("PerformanceTitle") %></asp:Label>
        <asp:Label ID="lblPerformanceDate" runat="server"><%# Eval("PerformanceDate") %></asp:Label>
        <asp:Label ID="lblSection" runat="server"><%# Eval("Section") %></asp:Label>
        <asp:Label ID="lblSeatRow" runat="server"><%# Eval("SeatRow") %></asp:Label>
        <asp:Label ID="lblSeatNumber" runat="server"><%# Eval("SeatNumber") %></asp:Label>        
        <asp:Image ID="Image1" runat="server" ImageUrl='<%# string.Format("~/account/history/BarcodePrinter.ashx?sli={0}&index=0", Eval("SubLineItemId")) %>' />
        <%--<asp:Image ID="Image1" runat="server" ImageUrl='<%# string.Format("~/account/history/BarcodePrinter.ashx?sli={0}&index=1", Eval("SubLineItemId")) %>' />--%>
    </li>
</ItemTemplate>
</asp:ListView>