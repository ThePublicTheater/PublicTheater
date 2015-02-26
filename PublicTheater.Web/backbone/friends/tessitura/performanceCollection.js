

define([

    'friends/base/collection',
    'friends/tessitura/performance'

], function (BaseCollection, Performance) {

    return BaseCollection.extend({

        model: Performance,

        addWithInfo: function (infoArr) {
            this.add(_(infoArr).map(function (info) {
                var perf = new Performance();
                perf.addInfo(info);
                return perf;
            }));
        }


    });

});