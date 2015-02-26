<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="OptimizingBlockPreview.ascx.cs" Inherits="EPiServer.Cms.AddOns.Blocks.Web.Controls.OptimizingBlockPreview, EPiServer.Cms.AddOns.Blocks" %>
<%@ Register TagPrefix="EPiServer" Namespace="EPiServer.Web.WebControls" Assembly="EPiServer.Web.WebControls" %>


<h1><EPiServer:Translate ID="Translate1" runat="server" Text="/episerver/cms/addons/optimizingblock/preview/goal/title" Language="<%#Language %>"/></h1>
<p><EPiServer:Translate ID="Translate2" runat="server" Text="/episerver/cms/addons/optimizingblock/preview/goal/description" Language="<%#Language %>"/></p>
<fieldset>
    <EPiServer:Translate runat="server" Text="/episerver/cms/addons/optimizingblock/preview/goal/label" Language="<%#Language %>" /><EPiServer:Property ID="Property1" runat="server" PropertyName="TargetGoal"></EPiServer:Property>
</fieldset>

<h1><EPiServer:Translate runat="server" Text="/episerver/cms/addons/optimizingblock/preview/variations/title" Language="<%#Language %>" /></h1>
<p><EPiServer:Translate runat="server" Text="/episerver/cms/addons/optimizingblock/preview/variations/description" Language="<%#Language %>" /></p>
<asp:PlaceHolder runat="server" ID="PlaceHolder" />