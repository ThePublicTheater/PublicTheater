

define(['backbone'], function (Backbone) {

    return Backbone.Model.extend({

        raise: function (key) {
            this.set(key, true);
        },

        lower: function (key) {
            this.set(key, false);
        }
        
    });

});