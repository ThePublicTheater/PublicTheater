<%@ Assembly Name="EPiServer.Cms.AddOns.Blocks" %>
<%@ Import Namespace="System.Web.Mvc" %>
<%@ Import Namespace="EPiServer.Web.Mvc.Html" %>
<%@ Import Namespace="EPiServer.Cms.AddOns.Blocks" %>
<%@ Control Language="C#" Inherits="ViewUserControl<OptimizingBlock>" %>

<%: Html.PropertyFor(m => m.TargetGoal) %>

<%: Html.PropertyFor(m => m.Variations) %>