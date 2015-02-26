/* CFS Mobile Calendar
-----------------------------------*/

define(['cfs', 'handlebars'], function (CFS, Handlebars) {


    // #region Error Handling

    var Errors = {

        ajaxError: function (response) {
            console.error(response);
            throw "MobileCalendar error loading performances";
        },

        cfsObject: function () {
            throw "Attempted to load CFS Mobile Calendar before CFS App";
        },

        momentDependency: function () {
            throw "Mobile Calendar requires moment.js library";
        }

    };

    // #endregion


    // #region Pre-init Checks

    if (typeof CFS === "undefined") Errors.cfsObject();
    if (typeof moment === "undefined") Errors.momentDependency();

    // #endregion

    // #region MobileCalendar

    var MobileCalendar = CFS.View.extend({

        // #region init

        initialize: function () {
            this.initTemplate();
            this.initModels();
            this.initFields();

            this.loadCalendarMonths();

            this.render();

            this.loadPerformances();

            this.on('didRender', this.didRender, this);
            this.on('willLoadPerformances', this.willLoadPerformances, this);
            this.on('didLoadPerformances', this.didLoadPerformances, this);
        },

        initTemplate: function () {
            this.template = Handlebars.compile(this.el.innerHTML);
        },

        initModels: function () {
            this.performances = new PerformanceList();
            this.allCalendarMonths = new CFS.CFSList();
        },

        initFields: function () {
            this.selectedDate = moment();
            this.selectedDate.startOf('month');
            this.suppressSelectChanges = false;
        },

        // #endregion

        // #region events

        events: {
            "change [data-bb='fullMonthSelect']": "didChangeFullMonth"
        },

        didRender: function () {
            var currentMonth = this.selectedDate.month();
            var currentYear = this.selectedDate.year();
            var fullMonth = this.selectedDate.format('M/1/YYYY')

            this.suppressSelectChanges = true;
            $(this.getBBElement('fullMonthSelect')).val(fullMonth);
            this.suppressSelectChanges = false;

            this.$el.show();
        },

        didChangeFullMonth: function (evt) {
            if (!this.suppressSelectChanges) {
                var selectedMonthValue = $(evt.currentTarget).val();
                this.selectedDate = moment(selectedMonthValue);
                this.loadPerformances();
            }
        },

        didLoadPerformances: function () {
            this.render();
        },

        willLoadPerformances: function () {
            this.performances.reset();
            this.render();
        },

        // #endregion

        // #region view model

        viewModel: function () {
            return {
                performances: this.getPerformancesVM(),
                allCalendarMonths: this.getMonthsVM(),
                loading: this.loading
            };
        },

        getPerformancesVM: function () {
            var performances = this.performances.map(function (performance) {
                var perfInfo = performance.toJSON();

                var date = moment(perfInfo.date);
                var now = moment();

                if (date.isAfter(now)) {
                    _(perfInfo).extend({
                        dateString: date.format('D MMM'),
                        timeString: date.format('h:mma')
                    });

                    return perfInfo;
                }
                else {
                    return false;
                }
            });

            performances = _(performances).compact();

            return performances;
        },

        getMonthsVM: function () {
            var allCalendarMonths = this.allCalendarMonths.map(function (month) {
                var monthObject = moment(month.get("dateString"));

                month.set({
                    monthValue: monthObject.format('M/1/YYYY'),
                    monthText: monthObject.format('MMMM YYYY')
                });

                return month.toJSON();
            });

            allCalendarMonths = _(allCalendarMonths).compact();
            return allCalendarMonths;
        },

        // #endregion

        // #region performance data

        loadPerformances: function () {
            this.loading = true;
            this.trigger('willLoadPerformances');

            var startDate = this.selectedDate;
            var endDate = this.selectedDate.clone();
            endDate.add('months', 1).startOf('month');

            this.cfs.service.getPerformancesForCalendar({
                data: { startDate: startDate, endDate: endDate },
                success: _(this.parsePerformances).bind(this),
                error: _(Errors.ajaxError).bind(this)
            });
        },

        parsePerformances: function (response) {
            this.loading = false;
            var data = response.d;

            console.groupCollapsed('Recieved %s Performances', data.length);
            console.log("Performances: %o", data);
            console.groupEnd();

            this.performances.add(data);
            this.trigger('didLoadPerformances');
        },

        // #endregion

        // #region calendar data

        loadCalendarMonths: function () {
            this.cfs.service.getCalendarMonths({
                success: _(this.parseCalendar).bind(this),
                error: _(Errors.ajaxError).bind(this)
            });
        },

        parseCalendar: function (response) {
            var data = response.d;

            console.groupCollapsed('Recieved %s Months', data.length);
            console.log("Months: %o", data);
            console.groupEnd();

            var parseData = _(data).map(function (dataObject) {
                return { dateString: dataObject };
            });

            this.allCalendarMonths.add(parseData);
        }

        // #endregion

    });

    // #endregion


    // #region Custom Performance List

    var PerformanceList = CFS.PerformanceList.extend({

        comparator: function (performance) {
            var date = moment(performance.get('date'));
            return date.valueOf();
        }

    });

    // #endregion


    // #region Namespacing

    //CFS.MobileCalendar = MobileCalendar;
    //CFS.MobileCalendar.PerformanceList = PerformanceList;

    MobileCalendar.PerformanceList = PerformanceList;

    // #endregion
    return MobileCalendar;

});
