

define([

    'backbone',
    './tweet'

], function (Backbone, Tweet) {

    return Backbone.Collection.extend({

        model: Tweet

    });

});