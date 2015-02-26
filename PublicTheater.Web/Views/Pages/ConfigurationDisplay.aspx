<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConfigurationDisplay.aspx.cs" Inherits="TheaterTemplate.Web.Views.Pages.ConfigurationDisplay" %>
<%@ Register Src="~/Views/Controls/ExcelToKVPLUploader.ascx" TagPrefix="templ" TagName="ExcelUploader" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <EPiServer:FullRefreshPropertiesMetaData ID="FullRefreshPropertiesMetaData1" runat="server" />
    <%--<div style="display:none">
        <asp:GridView runat="server" ID="displayOptions" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField ControlStyle-Font-Bold="true" HeaderText="Key" DataField="Key" SortExpression="Key" />
                <asp:BoundField HeaderText="Value" DataField="ValueOfKey" />
                <asp:TemplateField Visible="false" HeaderText="Published" SortExpression="Published" />
                <asp:TemplateField Visible="false" />
            </Columns> 
        </asp:GridView>
    </div>--%>
    <div runat="server" id="divTest">
        <asp:Repeater runat="server" ID="rptTest">
            <HeaderTemplate>
                <table cellspacing="0" rules="all" border="1" style="border-collapse:collapse;">
                    <tr>
                        <th scope="col">Key</th>
                        <th scope="col">Value</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td><%# Eval("Key") %></td>
                    <td><%# Eval("ValueOfKey")%></td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
    
    <templ:ExcelUploader runat="server" ID="excelUploader" PropertyName="ConfigProperty" Visible="false" />
    
    </form>
</body>
</html>
