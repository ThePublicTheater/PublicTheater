<%@ Page Language="C#" AutoEventWireup="false" CodeBehind="OptimizingBlockPreviewPage.aspx.cs" Inherits="EPiServer.Cms.AddOns.Blocks.Web.OptimizingBlockPreviewPage, EPiServer.Cms.AddOns.Blocks" %>
<%@ Register TagPrefix="EPiServer" Namespace="EPiServer.Web.WebControls" Assembly="EPiServer.Web.WebControls, Version=7.0.586.1, Culture=neutral, PublicKeyToken=8fe83dea738b45b7" %>
<!DOCTYPE html>

<html>
    <head runat="server">
        <title>Optimizing Block preview</title>
    
        <style type="text/css">
            
            body {
	            color: #333;
	            background-color: #fbfbfb;
	            font-family: Arial, Helvetica, Verdana, sans-serif;
	            font-size: 13px;
            }
            
            form  {
                padding: 2em 3em;
            }
            
            fieldset 
            {
                padding: 1em;
                margin-bottom: 25px;
            }
                        
            h1 {
                font-size: 1.1em;  
                font-weight: bold;    
                line-height: 1.4;	
                font-family: Helvetica, Arial, Sans-Serif;
                margin: 0;
            }
            
            h2 {
                font-size: 1.5em;  
                font-weight: normal;    
                line-height: 1.5;	
                font-family: Helvetica, Arial, Sans-Serif;
                text-align: center;
                border: 1px solid dimgray;
                padding: 4em 0;
            }
            
            p {
                margin: 0 0 1em 0;
            }
            
            .warningMessage {
                background-color: #fff8aa;
                border: 1px solid #858585;
                padding: 15px 15px 0 15px;
                margin-bottom: 25px;
                overflow: hidden;
            }
            
        </style>

    </head>
    <body>
        <form runat="server">
            
            <asp:Panel ID="WarningMessage" CssClass="warningMessage" runat="server" Visible="False">
                <EPiServer:Translate runat="server" Text="/episerver/cms/addons/optimizingblock/preview/warning" Language="<%# Language %>"/>
                <pre>
    &lt;system.web&gt;
        &lt;sessionState mode="inProc|StateServer|SQLServer" /&gt;
    &lt;/system.web&gt;
                </pre>
            </asp:Panel>

            <asp:PlaceHolder runat="server" ID="PreviewPlaceHolder"></asp:PlaceHolder>
        </form>
    </body>
</html>
