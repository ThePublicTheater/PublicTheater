define([

     "backbone"

], function (Backbone) {

    return Backbone.View.extend(
        {
            el: '#masterBody',

            initialize: function (opts) {
                console.log("Loading Search Scripts");
                this.setupAutoComplete();
            },

            events: {
                "click #btnSearch": "executeSearch",
                "keypress #btnSearch": "executeSearchKeyPress"
            },


            executeSearchkeypress: function (event) {
                if (event.keyCode == 13) {
                    this.executeSearch();
                }
            },

            execSearch: function (source, eventArgs) {
                this.$('#SearchText').val(eventArgs.get_value());
                this.executeSearch();
            },

            executeSearch: function (e) {
                e.preventDefault();
                var searchPage = this.$('#hidSearchPageUrl').val();
                var searchTerm = this.$('#SearchText').val();
                var searchPageWithQuery = searchPage + '?q=' + searchTerm;
                var siteTheme = this.getSiteTheme();
                if (siteTheme != "") {
                    searchPageWithQuery += "&" + siteTheme;
                }
                window.location.replace(searchPageWithQuery);
                return false;
            },

            getSiteTheme: function () {
                var siteThemeQueryString = "";
                if (window.location.search != "") {
                    var queryString = window.location.search.substring(1);
                    var queryStringParts = queryString.split('&');
                    for (var i = 0; i < queryStringParts.length; i++) {
                        var keyValuePair = queryStringParts[i].split('=');
                        if (keyValuePair[0] === "SiteTheme") {
                            siteThemeQueryString = queryStringParts[i];
                            break;
                        }
                    }
                }
                return siteThemeQueryString;
            },

            setupAutoComplete: function () {
                var searchEngineKey = this.$('#hidSearchPartnerID').val();
                this.$('#SearchText')
                .focus(function () { this.select(); })
                .mouseup(function (e) { e.preventDefault(); })
                .autocomplete({
                    position: { my: "left bottom", at: "left top", offset: "0, 5", collision: "none" },
                    source: function (request, response) {
                        $.ajax({
                            url: "http://clients1.google.com/complete/search?client=partner&hl=en&sugexp=gsnos%2Cn%3D13&gs_rn=25&gs_ri=partner&partnerid=" + searchEngineKey + "&types=t&ds=cse&cp=3&gs_id=v&q=" + request.term,
                            dataType: "jsonp",
                            success: function (data) {
                                response($.map(data[1], function (item) {
                                    return {
                                        label: item[0],
                                        value: item[0]
                                    };
                                }));
                            }
                        });
                    },
                    autoFill: true,
                    minChars: 0,
                    select: function (event, ui) {
                        this.$('#SearchText').val(ui.item.value);
                        this.$('#btnSearch').trigger('click');
                    }
                });
            }
        });
});