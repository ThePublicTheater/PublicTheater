<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="BlockPreview.aspx.cs" Inherits="PublicTheater.Web.Views.Blocks.BlockPreview" %>

<link href="http://fonts.googleapis.com/css?family=Roboto:400,300,300italic,400italic,700,700italic|Roboto+Condensed:300italic,400italic,700italic,400,300,700" rel="stylesheet" type="text/css" />
<link href="/Static/stylesheets/main.css" rel="stylesheet" type="text/css" />
<style>
body { background: #fff; padding: 20px; }
</style>

<form id="Form1" runat="server">
<EPiServer:Property ID="FullWidthPreviewProperty" runat="server">
    <RenderSettings EnableEditFeaturesForChildren="true" />
</EPiServer:Property>
</form>


