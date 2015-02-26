<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VtixCustomerLookup.ascx.cs" Inherits="PublicTheater.Web.modules.VTixCustomerLookup.VtixCustomerLookup" %>
<table style="float:left;">
    <tr>
        <td>
            <asp:Label ID="Label1" runat="server" Text="Customer Number: "></asp:Label></td><td>
                <asp:TextBox ID="tbCustNo" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label2" runat="server" Text="Email Address: "></asp:Label></td><td>
                <asp:TextBox ID="tbEmailAddress" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label3" runat="server" Text="Date: "></asp:Label></td><td>
                <asp:TextBox ID="tbDate" runat="server"></asp:TextBox></td>

    </tr>
</table>
 <asp:Button ID="Button1" runat="server" Text="Search" OnClick="Button1_Click" />
<asp:GridView ID="GridView1" runat="server" GridLines="Horizontal" style="clear:both" >
    <HeaderStyle CssClass="header"  />
    <RowStyle HorizontalAlign="Center" CssClass="rows"/>
 
</asp:GridView>
<style type="text/css">
    
     th{
         font-size: 20px;
         font-weight: bold;
         padding-right: 8px;
         padding-left: 8px;
     }
    td {
        padding-right: 8px;
        padding-left: 8px;
        text-align: center;
    }
    .header {
        border-bottom: 1px;
    }
</style>
