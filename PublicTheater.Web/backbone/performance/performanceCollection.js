

define([

    'base/collection',
    'performance/performance'

], function (BaseCollection, Performance) {

    return BaseCollection.extend({
        model: Performance

    });

});