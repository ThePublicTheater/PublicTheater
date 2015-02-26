<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<CriteriaPack.SelectedLanguageCriterion.SelectedLanguageModel>" %>
<%@ Import Namespace="EPiServer.Personalization.VisitorGroups" %>
<%@ Import Namespace="EPiServer.Cms.Shell" %>

<div>
    <span class="epi-criteria-inlineblock">
        <%= Html.DojoEditorFor(model => model.Condition)%> 
    </span>
    <span class="epi-criteria-inlineblock">
        <%= Html.DojoEditorFor(model => model.SelectedLanguage)%> 
    </span>
</div>