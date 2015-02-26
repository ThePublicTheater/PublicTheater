

define([

    'moment',
    'friends/base/model'

], function (moment, BaseModel) {

    return BaseModel.extend({

        defaults: {
            name: "Default Event",
            start: new Date(),
            end: new Date(),
            description: "default description",
            location: "default location",
            privacy: "OPEN"
        },

        toFacebookData: function () {
            var atts = this.toJSON();
            return {
                name: atts.name,
                start_time: moment(atts.start).toISOString(),
                end_time: moment(atts.end).toISOString(),
                description: atts.description,
                location: atts.location,
                privacy_type: atts.privacy
            };
        },

        toJSON: function () {
            var json = BaseModel.prototype.toJSON.apply(this, arguments);
            _(json).extend({
                onFacebook: this.has('eventId')
            });

            return json;
        }

    });

});