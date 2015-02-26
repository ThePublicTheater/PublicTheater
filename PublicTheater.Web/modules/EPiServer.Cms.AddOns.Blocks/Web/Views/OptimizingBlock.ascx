<%@ Assembly Name="EPiServer" %>
<%@ Import Namespace="System.Web.Mvc" %>
<%@ Import Namespace="EPiServer.Core" %>
<%@ Import Namespace="EPiServer.Web.Mvc.Html" %>
<%@ Control Language="C#" Inherits="ViewUserControl<IContent>" %>

<% if (Model != null) Html.RenderContentData(Model, false); %>
