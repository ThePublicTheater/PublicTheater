

define(['backbone'], function (Backbone) {

    return Backbone.Collection.extend({

        toggle: function (model) {
            if (this.contains(model))
                this.remove(model);
            else
                this.add(model);
        }

    });

});