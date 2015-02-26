<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="vtixSummaryDynamicContent.ascx.cs" Inherits="PublicTheater.Web.modules.VTixSummary.vtixSummaryDynamicContent" %>
<asp:GridView ID="GridView1" runat="server" GridLines="Horizontal" >
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
