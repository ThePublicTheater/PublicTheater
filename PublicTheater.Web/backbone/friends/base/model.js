

define(['backbone'], function (Backbone) {

    return Backbone.Model.extend({

        addInfo: function (info) {
            this.set(info);
        }

    });

});