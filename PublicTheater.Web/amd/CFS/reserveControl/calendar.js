define([
    'cfs',
    'cfsConfig'
], function (CFS, cfsConfig) {
    CFSCalendar = CFS.View.extend({
        initialize: function () {
            this.setElement($('.calendarWrap')[0]);
            this.performances = new CFSCalendarPerformances();
            this.selectedPerformances = new CFSCalendarPerformances();
            this.performances.on('add change', this.determineCurrent, this);
            this.performances.on('didSelectPerformance', this.hide, this);
            return this;
        },

        parseInfo: function (info) {
            _(info.Performances).each(function (performanceInfo) {
                performanceInfo.id = performanceInfo.ID;
            });
            this.performances.add(info.Performances);
            if (info.CurrentPerformanceId != null) {
                var currentPerf = this.performances.get(info.CurrentPerformanceId);
                if(currentPerf != null){
                   currentPerf.set('active', true);
                }
            }
            if (this.performances.length < 2) //this.hideForSinglePerformance === true && 
                $('#changePerformance').hide();
            this.setupDatepicker();
            return this;
        },

        determineCurrent: function () {
            this.performances.each(function (performance) {
                if (performance.get('active'))
                    this.activePerformance = performance;
            }, this);
            return this;
        },

        setupDatepicker: function () {
            (function (self) {
                var datepickerOpts = {
                    minDate: self.performances.first().getDate(),
                    maxDate: self.performances.last().getDate(),
                    beforeShowDay: function (date) {
                        return self.beforeShowDay.call(self, date);
                    },
                    onSelect: function (dateText, inst) {
                        self.onDateSelect(dateText, inst);
                    },
                    constrainInput: true,
                };

                if (self.activePerformance != null)
                    datepickerOpts.defaultDate = self.activePerformance.getDate();
                else
                    datepickerOpts.defaultDate = new Date();



                self.datepicker = $('#changeDateDatepicker').datepicker(_(datepickerOpts).defaults(cfsConfig.reserveControl.calendarOptions));

                $('#changeDateDatepicker').append('<div id="closeDatePicker">x</div>').on('click', '#closeDatePicker', function () {
                    self.hide();
                });
            })(this);
            return this;
        },

        beforeShowDay: function (date) {
            var targetTime = date.getTime();
            var times = [];
            this.performances.each(function (performance) {
                times.push(performance.getDate().getTime());
            });
            var performanceOnDate = (_.indexOf(times, targetTime) !== -1);
            return [performanceOnDate];
        },

        onDateSelect: function (dateText, inst) {

            var selectedDate = new Date(inst.selectedYear, inst.selectedMonth, parseInt(inst.selectedDay));
            this.selectedPerformances.reset();

            this.performances.each(function (performance) {
                if (performance.getDate().getTime() === selectedDate.getTime()) {
                    this.selectedPerformances.push(performance);
                }
            }, this);
            if (self.activePerformance == null && this.selectedPerformances.length == 1)
                window.location.replace(window.location.href.split('?')[0] + "?performanceNumber=" + this.selectedPerformances.at(0).id);
            this.showSelectedDates();
            return this;
        },

        showSelectedDates: function () {
            $("#changeDateDatepickerTimes").hide().children().remove();
            if (this.selectedPerformances.length !== 1) {
                this.selectedPerformances.each(function (performance) {
                    var performanceView = new CFSCalendarPerformanceView({ model: performance });
                    $("#changeDateDatepickerTimes").append(performanceView.render().el);
                }, this);
                $("#changeDateDatepickerTimes").slideDown();
            }
            else {
                this.cfs.reserveControl.changePerformance(this.selectedPerformances.at(0).id);
                this.hide();
            }
            return this;
        },

        hide: function () {
            this.$el.fadeOut();
            return this;
        }

    });

    CFSCalendarPerformanceView = CFS.View.extend({
        className: "syos-calendar-performance-time",
        tagname: "span",

        events: {
            "click": "didClick"
        },

        didClick: function (evt) {
            this.model.trigger('didSelectPerformance', this.model);
            if (self.activePerformance == null)
                window.location.replace(window.location.href.split('?')[0] + "?performanceNumber=" + this.model.get("ID"));
            this.cfs.reserveControl.changePerformance(this.model.get("ID"));

            return this;
        },

        render: function () {
            this.$el.html(this.model.get('Time'));
            return this;
        }
    });

    CFSCalendarPerformance = Backbone.Model.extend({
        defaults: {
            LongDateString: "No Date sent"
        },

        getDate: function () {
            var dateValues = this.get("Date").split('/');
            var year = parseInt(dateValues[0]);
            var month = parseInt(dateValues[1]);
            var day = parseInt(dateValues[2]);
            var performanceDate = new Date(year, month - 1, day);
            return performanceDate;
        }
    });

    CFSCalendarPerformances = Backbone.Collection.extend({
        model: CFSCalendarPerformance,

        initialize: function () {
            this.on('all', function (eventName) {
            });
            return this;
        },

        performancesOnDate: function (date) {
            var performances = [];
            this.each(function (performance) {
                if (performance.getDate().getTime() === date.getTime())
                    performances.push(performance);
            });
            return performances;
        },

        comparator: function (performance) {
            return performance.getDate().getTime();
        }
    });

    return {
        CFSCalendar: CFSCalendar,
        CFSCalendarPerformanceView: CFSCalendarPerformanceView,
        CFSCalendarPerformance: CFSCalendarPerformance,
        CFSCalendarPerformances: CFSCalendarPerformances
    }
});