<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GenericPropertyDisplay.aspx.cs" Inherits="TheaterTemplate.Web.Views.Pages.GenericPropertyDisplay" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

    <table width="100%" cellpadding="2">
        <asp:Repeater runat="server" ID="propertyInfo">
            <ItemTemplate>
                <tr>
                    <td class="keyName" style="width:200px;">
                        <asp:Label runat="server" ID="propertyTitle" Font-Bold="true" />
                    </td>
                    <td runat="server" id="tdWrapper">
                        <EPiServer:Property runat="server" ID="epiProp"/>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
    </form>
</body>
</html>
