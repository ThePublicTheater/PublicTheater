<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<CriteriaPack.QueryStringCriterion.QueryStringParameterModel>" %>
<%@ Import Namespace="EPiServer.Personalization.VisitorGroups" %>
<%@ Import Namespace="EPiServer.Cms.Shell" %>

<div>
    <span class="epi-criteria-inlineblock">
        <%= Html.DojoEditorFor(p => p.Key, new { }, Html.Translate("/shell/cms/visitorgroups/criteria/querystring/key"), "epi-criteria-label")%> 
    </span>
    <span class="epi-criteria-inlineblock">
        <%= Html.DojoEditorFor(model => model.Condition)%> 
    </span>
    <span class="epi-criteria-inlineblock">
        <%= Html.DojoEditorFor(p => p.Value)%> 
    </span>
</div>