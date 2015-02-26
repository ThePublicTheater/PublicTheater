<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="cache.aspx.cs" Inherits="TheaterTemplate.Web.Pages._monitor.cache" %>

<%@ Register Src="~/controls/monitor/CacheItems.ascx" TagPrefix="monitor" TagName="CacheItems" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/_monitor/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
    <script src="/_monitor/bootstrap/js/bootstrap.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container-fluid">
        <monitor:CacheItems runat="server" ID="monitorCache" />
    </div>
    </form>
</body>
</html>
