<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="download.aspx.cs" Inherits="TheaterTemplate.Web.Pages._monitor.download" %>

<%@ Register Src="~/controls/monitor/download.ascx" TagPrefix="monitor" TagName="download" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <monitor:download runat="server" id="downloadControl" />
    </form>
</body>
</html>
