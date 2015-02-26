<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddPerformanceItem.aspx.cs" Inherits="TheaterTemplate.Web.Pages.Cart.AddPerformanceItem" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Literal runat="server" ID="ltrSuccess" />
        <asp:HyperLink runat="server" ID="lnkCart" NavigateUrl="~/cart/index.aspx" Text="Return To Cart" />
    </div>
    </form>
</body>
</html>
