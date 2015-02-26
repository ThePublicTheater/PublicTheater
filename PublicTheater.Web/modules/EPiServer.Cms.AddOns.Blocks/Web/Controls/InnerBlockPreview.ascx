<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="InnerBlockPreview.ascx.cs" Inherits="EPiServer.Cms.AddOns.Blocks.Web.Controls.InnerBlockPreview, EPiServer.Cms.AddOns.Blocks" %>
<%@ Import Namespace="EPiServer.Core" %>

<h2><%: ((IContent)CurrentData).Name%></h2>