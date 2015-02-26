<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<CriteriaPack.TimeOnSiteCriterion.TimeOnSiteCriterionModel>" %>
<%@ Import Namespace="EPiServer.Personalization.VisitorGroups" %>
<%@ Import Namespace="EPiServer.Cms.Shell" %>
<div>
    <span>User has spent:
        <%= Html.DojoEditorFor(p => p.TimeOnSite, null, Html.Translate("/shell/cms/visitorgroups/criteria/timeperiod/time"), "")%>
    </span>
    <span>
        <%= Html.DojoEditorFor(p => p.DurationUnit, null, Html.Translate("/shell/cms/visitorgroups/criteria/timeperiod/time"), "")%>
    </span>
</div>
