<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<CriteriaPack.TimePeriodCriterion.TimePeriodCriterionModel>" %>
<%@ Import Namespace="EPiServer.Personalization.VisitorGroups" %>
<%@ Import Namespace="EPiServer.Cms.Shell" %>
<div>
    <span><%=Html.Translate("/shell/cms/visitorgroups/criteria/timeperiod/from")%>
        <%= Html.DojoEditorFor(p => p.StartTime, null, Html.Translate("/shell/cms/visitorgroups/criteria/timeperiod/time"), "")%>
        <%= Html.DojoEditorFor(p => p.StartDate, null, Html.Translate("/shell/cms/visitorgroups/criteria/timeperiod/date"), "")%>
    </span><span style="padding-left:20px;"><%=Html.Translate("/shell/cms/visitorgroups/criteria/timeperiod/to")%>
        <%= Html.DojoEditorFor(p => p.EndTime, null, Html.Translate("/shell/cms/visitorgroups/criteria/timeperiod/time"), "")%>
        <%= Html.DojoEditorFor(p => p.EndDate, null, Html.Translate("/shell/cms/visitorgroups/criteria/timeperiod/date"), "")%>
    </span>
</div>
