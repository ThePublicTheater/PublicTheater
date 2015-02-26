define([

    "base/view",
    "controls/calendar/mobile/templates"

], function (BaseView, Templates) {

    return BaseView.extend({
        template: Templates.mobileCalendar,

        events: {
            "touchstart [data-bb='previousDayArrow']": "didClickPreviousDayArrow",
            "touchstart [data-bb='nextDayArrow']": "didClickNextDayArrow",
            "change [data-bb='mobileVenueFilterSelect']": "didClickMobileFilter",
            "click [data-bb='daySelectDropdown']": "toggleDatepickerVisibility"
        },

        selector: "[data-bb-control='mobileCalendar']",

        initialize: function (opts) {
            this.desktopCalendar = opts.desktopCalendar;

            this.setElement($(this.selector)[0]);

            var calendarArea = $("#calendarArea");

            if (calendarArea.length > 0)
                this.$desktopCalendar = $(calendarArea[0]);

            var activeDate = this.getDateFromURL();
            this.calendarConfig = {
                onSelect: _(this.showDay).bind(this),
                minDate: new Date(),
                defaultDate: activeDate.format('L')
            };

            this.loadInitialDay();


        },

        getCurrentDate: function () {
            return this.currentDate;
        },

        getDateFromURL: function () {
            var monthUrlParameter = this.getUrlParameterByName("month");

            if (monthUrlParameter != null && moment(monthUrlParameter).isValid()) {
                return moment(monthUrlParameter);
            } else {
                return moment();
            }
        },

        loadInitialDay: function () {
            this.currentDate = this.getDateFromURL();

            this.showDay(this.currentDate.format("L"));
        },

        didClickNextDayArrow: function (evt) {
            var tomorrow = this.getCurrentDate().clone().add('d', 1);
            this.showDay(tomorrow);
        },

        didClickPreviousDayArrow: function (evt) {
            var yesterday = this.getCurrentDate().clone().subtract('d', 1);
            this.showDay(yesterday);
        },

        showDay: function (dayString) {
            var viewDate = moment(dayString);
            if (viewDate.month() === this.getCurrentDate().month()) {
                this.currentDate = moment(dayString);

                var dayToShow = moment(dayString).date();

                var dayDisplayDiv = this.$desktopCalendar.find('td:not(.offDay) span.day').filter(function (i, span) {
                    return span.innerHTML == dayToShow;
                }, this);


                if (dayDisplayDiv.length > 0) {
                    this.displayDiv = $(dayDisplayDiv[0]);

                    this.render();
                }
            } else {
                this.desktopCalendar.redirectToDate(viewDate.format('L'));
            }
        },

        buildDisplayDataFromDiv: function () {
            var $spanInsideDay = this.displayDiv;

            var $venueDivs = $spanInsideDay.siblings("div:not(.inactive)"); // Filtered Items are Hidden

            return _.map($venueDivs, function (venueDiv) {
                var $venueNameSpan = $("span.venueName", venueDiv)[0];
                var venueName = $venueNameSpan.innerText;

                var $performanceDivs = $(".performanceRow", venueDiv)

                var performanceData = _.map($performanceDivs, function (performanceDiv) {
                    var $performanceDataSpans = $("a span", performanceDiv);

                    var performanceImage = undefined;
                    var performanceName = $performanceDataSpans[0].innerText;
                    var performanceTime = $performanceDataSpans[1].innerText;

                    var hasImage = false;
                    var $perfImg = $("[data-bb='popoverContent'] img", performanceDiv);

                    if ($perfImg.length > 0) {
                        if ($perfImg.attr("src") != "") {
                            hasImage = true;
                            var performanceImage = $perfImg.attr("src");
                        } else {
                            // Opportunity to set a default image.
                        }
                    }
                    var $links = $("[data-bb='popoverContent'] .buttons a", performanceDiv);
                    var performanceLink = $links.length > 0 ? $links[0].href : "#";

                    return {
                        Name: performanceName,
                        Time: performanceTime,
                        ImageURL: performanceImage,
                        ReserveURL: performanceLink
                    };
                });

                return {
                    ShowVenue: venueName !== "",
                    Venue: venueName,
                    Performances: performanceData
                };
            }).filter(function (venueConfig) {
                return venueConfig !== undefined
            });
        },

        getUrlParameterByName: function (name) {
            name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
            var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
                results = regex.exec(location.search);
            return results == null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
        },

        viewModel: function () {
            this.displayData = this.buildDisplayDataFromDiv();
            console.log("Rendering with display data:");
            console.log(this.displayData);

            return {
                currentDate: this.getCurrentDate().format("ddd MMM D"),
                venueFilters: this.buildVenueFilters(),
                displayModels: this.displayData
            };
        },

        didRender: function () {
            this.createDayChangeDatepicker();
        },

        createDayChangeDatepicker: function () {
            var $datepicker = this.$(".chooseDateArea");

            $datepicker.datepicker(this.calendarConfig);
        },

        didClickMobileFilter: function (evt) {
            var $target = $(evt.currentTarget);

            var desktopVenueFilterSelector = "ul.venueFilters li a." + $target.val();

            $(desktopVenueFilterSelector).click();

            this.render();
        },

        buildVenueFilters: function () {
            var $venueFilterListItems = $("ul.venueFilters li");

            var arrayOfVenues = _.map($venueFilterListItems, function (listItem) {
                var $listItem = $(listItem);

                var $innerLink = $($listItem.children("a")[0]);

                var isActive = $innerLink.parent("li").hasClass("selected");

                var value = $innerLink.attr("class").replace("selected", "").trim();
                var venueText = $innerLink.html();

                return {
                    IsActive: isActive,
                    Venue: value,
                    VenueText: venueText
                };
            });

            return _(arrayOfVenues).sortBy("IsActive");
        },

        toggleDatepickerVisibility: function () {
            var $datepicker = this.$(".chooseDateArea");

            if ($datepicker.is(":visible")) {
                $datepicker.slideUp();
            } else {
                $datepicker.slideDown();
            }
        }
    });

});