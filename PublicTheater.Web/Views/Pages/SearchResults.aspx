<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPages/Interior.master" AutoEventWireup="true" CodeBehind="SearchResults.aspx.cs" Inherits="PublicTheater.Web.Views.Pages.SearchResults" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <h1>Search results for '<span id="searchtext"></span>'</h1>
    <div id="results" class="searchResults">Loading ...</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
    <script src="http://www.google.com/jsapi" type="text/javascript"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
    <script type="text/javascript">
        function getQueryString(name) {
            name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
            var regexS = "[\\?&]" + name + "=([^&#]*)";
            var regex = new RegExp(regexS);
            var results = regex.exec(window.location.href);
            if (results == null)
                return "";
            else
                return decodeURIComponent(results[1].replace(/\+/g, " "));
        }
        google.load('search', '1', { language: 'en', style: google.loader.themes.MINIMALIST });
        function onLoad() {
            var options = { };
            options[google.search.Search.RESTRICT_EXTENDED_ARGS] = { 'as_sitesearch': '<%= RequestUrl %>' };
            var customSearchControl = new google.search.CustomSearchControl('<%= SearchEngineKey %>', options);
            var drawOptions = new google.search.DrawOptions();
            drawOptions.setAutoComplete(true);
            drawOptions.setDrawMode(google.search.SearchControl.DRAW_MODE_LINEAR);
            customSearchControl.draw('results', drawOptions);
            customSearchControl.setSearchStartingCallback(this, function (control, searcher, query) {
                $('.search_input').val(query);
                $('#searchtext').text(query);
            });
            customSearchControl.setLinkTarget(GSearch.LINK_TARGET_SELF);
            customSearchControl.execute(getQueryString('q'));
        }
        google.setOnLoadCallback(onLoad);
</script>
</asp:Content>
