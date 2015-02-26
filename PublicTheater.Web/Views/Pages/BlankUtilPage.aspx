<%@ Page Language="c#" Inherits="PublicTheater.Web.Views.Pages.BlankUtilPage" CodeBehind="BlankUtilPage.aspx.cs" %>
<%@ Import Namespace="EPiServer.Core" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title>BlankUtilPage</title>
    </head>
    <body>
        <form id="Form1" runat="server">
            <div>
                <EPiServer:Property runat="server" PropertyName="MainBody" DisplayMissingMessage="False"/>
                <%= ((PropertyLongString)CurrentPage.Property["Misc"]).Value.ToString() %>
                
                <EPiServer:Property runat="server" PropertyName="xForm" DisplayMissingMessage="False"/>
            </div>
        </form>
    </body>
</html>