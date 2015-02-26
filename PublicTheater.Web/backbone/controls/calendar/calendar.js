define([

    "base/view"

], function (BaseView) {

    return BaseView.extend({
        initialize: function (opts) {
            var numberOfFilters = $('.venueFilters li').length;
            var liWidth = 100 / numberOfFilters + "%";
            $('.venueFilters li').css('width', liWidth);
            this.loadFilters();
            this.themePerformances = this.getThemePerformanceList();

            var fakeEvent = new Object();
            var $defaultTheme = this.$('#SelectedTheme');
            //if ($defaultTheme.val() != "" && $defaultTheme.val() != "Default") {
            if ($defaultTheme.val() != "" ) {
                fakeEvent.currentTarget = $defaultTheme;
                this.toggleFilter(fakeEvent);
            }
            $('.loadingCal').hide();
            $('.hideTheWrapperCal').show();
        },

        events: {
            "click .venueFilters li a": "toggleFilter"
        },

        loadFilters: function () {
            var filter = window.filterVenue;
            if (filter && $('.' + filter).length > 0) {
                //Hacky, find new solution
                this.manualEnableFilter(filter);
                this.toggleSelectedFilter($("a." + filter).closest('li'));
            }
        },

        redirectToDate: function (dateString) {
            var unhashedSearchString = window.location.search;
            if (window.location.search === "") {
                window.location.search = "month=" + dateString;
            } else {
                var queryString = window.location.search.substring(1);

                var queryStringParts = queryString.split('&');

                var foundMonthParam = false;
                for (var i = 0; i < queryStringParts.length; i++) {
                    var keyValuePair = queryStringParts[i].split('=');

                    if (keyValuePair[0] === "month") {
                        queryStringParts[i] = keyValuePair[0] + "=" + dateString;
                        foundMonthParam = true;
                        break;
                    }
                }

                if (!foundMonthParam) {
                    window.location.search = window.location.search + "&month=" + dateString;
                } else {
                    window.location.search = "?" + queryStringParts.join("&");
                }
            }
        },

        toggleFilter: function (evt) {
            if (typeof evt.target !== "undefined")
                evt.preventDefault();

            var target = evt.currentTarget;

            if (typeof target.className === "undefined") {
                var className = target.val();
                var selectRealTarget = "a." + className;
                target = this.$(selectRealTarget);
                target.className = className;
            }

            var clickedVenue = target.className;

            if (clickedVenue != null) {
                $('#SelectedTheme').val(clickedVenue);
                if (!!clickedVenue.match(/allVenues/ig)) {
                    this.showAllVenues();
                } else {
                    var actualVenueClass = clickedVenue.replace("selected", "").trim();
                    this.manualEnableFilter(actualVenueClass);
                    window.filterVenue = actualVenueClass;
                }

                this.toggleSelectedFilter($(target).closest('li'));
            }
        },

        showAllVenues: function () {
            $('.performanceRow').removeClass('inactive');
            $('#calendarTable td > div > div').removeClass('inactive');
            window.filterVenue = undefined;
        },

        manualEnableFilter: function (venueClassName) {
            
            var themePerfs = this.themePerformances;
            $('.performanceRow').each(function () {
                var perfId = parseInt($(this).attr('data-pid'));
                var exist = !!_.where(themePerfs, { Key: venueClassName, Value: perfId }).length;
                if (exist) {
                    $(this).removeClass("inactive");
                } else {
                    $(this).addClass("inactive");
                }
            });

            
            $('#calendarTable td > div > div').each(function () {
                var allHidden = _.every($(this).find('.performanceRow'), function(row) {
                    return $(row).hasClass('inactive');
                });
                
                if(allHidden){
                    $(this).addClass("inactive");
                }
                else{
                    $(this).removeClass("inactive");
                }
            });

            //var classSelector = "." + venueClassName;

            //var venuePerformances = $(classSelector);

            //venuePerformances.removeClass('inactive');

            //$('#calendarTable td > div > div').not(classSelector).addClass('inactive');
        },

        toggleSelectedFilter: function (activeFilter) {
            $(".venueFilters li").not(activeFilter).removeClass("selected");
            $(activeFilter).addClass("selected");
        },

        getThemePerformanceList: function () {
            return JSON.parse($('#ThemePerformanceIds').val());
        }
    });

});
