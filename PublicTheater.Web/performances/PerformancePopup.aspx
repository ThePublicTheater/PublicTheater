<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PerformancePopup.aspx.cs" Inherits="TheaterTemplate.Web.Pages.Performances.PerformancePopup" %>

<%@ Register TagPrefix="performance" TagName="PopUpDetails" Src="~/controls/performances/PopUpDetails.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <link href="/css/main.css" rel="stylesheet" type="text/css" media="screen" />
    <!--[if lt IE 9]>
	    <link rel="stylesheet" type="text/css" href="/css/ie.css" media="all" />
    <![endif]-->
    <!--[if lt IE 8]>
	    <link rel="stylesheet" type="text/css" href="/css/ie7.css" media="all" />
    <![endif]-->
    <style type="text/css">
        body  
        {
            background-color: #FFF;
        }
    </style>
</head>

<body>
    <form id="Form1" runat="server">
        <performance:PopUpDetails ID="PopUpDetails1" runat="server" />
    </form>
</body>

</html>