

define([

    'backbone',
    'performanceService/performanceCollection',
    'asmxHelpers'

], function (Backbone, PerformanceCollection, AsmxHelpers) {

    return Backbone.View.extend({

        rootUrl: '/Services/PerformanceService.asmx/',

        getPerformancesForCalendar: function (opts) {
            var def = new $.Deferred();

            if (!_.isDate(opts.startDate))
                throw new TypeError("startDate must be of type date");
            if (!_.isDate(opts.endDate))
                throw new TypeError("endDate must be of type date");

            this.request('GetPerformancesForCalendar', opts).done(function (asmxResponse) {
                var response = AsmxHelpers.clean(asmxResponse);
                var performances = new PerformanceCollection();
                performances.add(response);
                def.resolveWith(null, [performances]);
            });

            return def;
        },

        request: function (method, data) {
            var url = this.rootUrl + method;
            var json = JSON.stringify(data);

            return $.ajax({
                url: url,
                type: 'POST',
                data: json,
                dataType: 'json',
                contentType: 'application/json'
            });
        }


    });

});