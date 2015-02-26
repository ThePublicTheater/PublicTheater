

define([

    'friends/base/model'

], function (BaseModel) {

    return BaseModel.extend({

        toJSON: function () {
            var base = BaseModel.prototype.toJSON.apply(this, arguments);
            _(base).extend({
                thumb: this.getThumb()
            });
            return base;
        },

        getThumb: function () {
            return "http://graph.facebook.com/" + this.get('id') + "/picture";
        }
    

    });

});