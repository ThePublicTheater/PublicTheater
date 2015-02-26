

define([

    'backbone',
    'performanceService/performance'

], function (Backbone, Performance) {

    return Backbone.Collection.extend({

        model: Performance

    });

});