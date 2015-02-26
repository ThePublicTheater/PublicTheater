define([
    "backbone"
], function (Backbone) {

    return Backbone.Router.extend({
        routes: {
            "play/:id" : "playItemById"
        },

        playItemById: function () {

        }
    });

});