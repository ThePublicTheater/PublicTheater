

define([

    'backbone'

], function (Backbone) {

    return Backbone.Model.extend({

        defaults: {
            text: null,
            createdAt: null
        }

    });

});