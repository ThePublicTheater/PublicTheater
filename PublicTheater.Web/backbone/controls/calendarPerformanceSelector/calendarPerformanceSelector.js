

define([

    "base/baseControl",
    "controls/calendarPerformanceSelector/templates"

], function (BaseBBControl, Templates) {

    return BaseBBControl.extend({
        template: Templates.calendarPerformanceSelector,

        selector: "[data-bb-control='calendarPerformanceSelector']",

        momentDateFormat: "MM/DD/YYYY",

        initialize: function (options) {
            this.reservePageURL = options.reservePageURL;
        },

        events: {
            "click [data-bb-control='calendarPerformanceSelector']": "didClickTicketsBtn",
            "click [data-bb='performanceTimeSlot']": "didSelectPerformanceTime",
            "click .closeControl": "closeControl"
        },

        closeControl: function (evt) {
            this.removeOtherControlsOnPage();
            this.buttonTarget = undefined;
        },

        didClickTicketsBtn: function (evt) {

            if (!!this.buttonTarget && this.buttonTarget[0] == $(evt.currentTarget)[0]) {
                this.removeOtherControlsOnPage();
                this.buttonTarget = undefined;
                evt.preventDefault();
                this.closeControl();
                return;
            }

            this.buttonTarget = $(evt.currentTarget);

            this.currentSelectedDate = undefined;

            this.style = this.buttonTarget.data("bb-control-style");

            this.performanceData = this.buildPerformanceAvailability();

            if (!this.performanceData || this.performanceData.length <= 0) {
                console.error("Ticket block calendar not shown, no performance data found.");
            } else {
                evt.preventDefault();
                this.displayCalendarWithDataNearTarget();
            }
        },

        didSelectPerformanceTime: function (evt) {
            evt.preventDefault();
            var $target = $(evt.currentTarget);

            // Trimmed string. Need to extract it.
            window.location = this.reservePageURL + $target.data("id");
        },

        buildPerformanceAvailability: function () {
            var dataDiv = this.buttonTarget.siblings("[data-bb='popupData']");

            if (dataDiv[0].innerText == undefined) {
                dataDiv[0].innerText = dataDiv[0].textContent;
            }

            if (dataDiv.length > 0 && dataDiv[0].innerText != "") {
                return JSON.parse(dataDiv[0].innerText);
            }

            // Returns undefined if no siblings data is found.
        },

        displayCalendarWithDataNearTarget: function () {
            var $parent = this.buttonTarget.parent();

            this.addControlElement();
        },

        addControlElement: function () {
            // There's a:
            // bb-control-style tag on same element with bb-control on it.
            // Other style could be "floating".
            this.removeOtherControlsOnPage();
            var data = this.viewModel();
            if (data.dateSelected && data.arrayOfTimes.length === 1) {
                // only one performance on that day, redirect to performance page directly
                window.location = this.reservePageURL + data.arrayOfTimes[0].PerformanceId;
                return;
            }

            this.currentElement = $(this.template(data));

            $("body").append(this.currentElement);

            this.doDatepicker();

            this.repositionElement();
        },

        repositionElement: function () {
            if (this.style === "inline") {
                this.currentElement.position({
                    "my": "left top",
                    "at": "left bottom",
                    "of": this.buttonTarget
                });
            } else {
                this.currentElement.position({
                    "my": "left bottom",
                    "at": "right top",
                    "of": this.buttonTarget
                });
            }
        },

        removeOtherControlsOnPage: function () {
            $(this.currentElement).remove();
        },

        doDatepicker: function () {
            // Do Datepicker
            var dateContext = this;

            $("[data-bb='datepickerMe']", this.currentElement).datepicker({

                minDate: moment(this.performanceData[0].PerformanceDate).format(this.momentDateFormat),

                maxDate: moment(this.performanceData[this.performanceData.length - 1].PerformanceDate).format(this.momentDateFormat),

                dateFormat: 'mm/dd/yy',

                beforeShowDay: _.bind(dateContext.isValidPerformanceDate, dateContext),

                inline: true,

                onSelect: function (date, inst) {
                    dateContext.showPerformanceView(date);
                }
            });
        },

        getPerformanceTimesForDate: function (datestr) {
            return _.filter(this.performanceData, function (perf) {
                return moment(perf.PerformanceDate).format(this.momentDateFormat) === moment(datestr).format(this.momentDateFormat);
            }, this)
        },

        isValidPerformanceDate: function (datestr) {
            if (this.getPerformanceTimesForDate(datestr).length > 0) {
                return [true, ""];
            }

            return [false, ""];
        },

        showPerformanceView: function (dateString) {
            this.currentSelectedDate = dateString;
            this.addControlElement();
        },

        viewModel: function () {

            if (this.currentSelectedDate === undefined && this.performanceData.length > 0) {
                //skip the date pick if all shows are on the same date
                var firstPerfDate = moment(this.performanceData[0].PerformanceDate).format(this.momentDateFormat);

                if (this.getPerformanceTimesForDate(firstPerfDate).length === this.performanceData.length) {
                    this.currentSelectedDate = firstPerfDate;
                }
            }

            var dateSelected = (this.currentSelectedDate === undefined ? false : true);

            var arrayOfTimes = [];

            if (dateSelected) {
                arrayOfTimes = this.getPerformanceTimesForDate(this.currentSelectedDate);
            }

            return {
                currentSelectedDate: this.currentSelectedDate,
                dateSelected: dateSelected,
                arrayOfTimes: arrayOfTimes
            };
        }
    });

});