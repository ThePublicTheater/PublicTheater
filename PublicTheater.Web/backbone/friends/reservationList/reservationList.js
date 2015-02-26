

define([

    'friends/base/view',
    'friends/reservationList/templates'

], function (BaseView, Templates) {
    "use strict";

    return BaseView.extend({

        className: "fb-reservation-list",

        template: Templates.reservationList,

        initialize: function (opts) {
            this.context = opts.context;
            this.render = _(this.render).debounce(500);
        },

        templateModel: function () {
            return {
                reservations: this.model.toJSON()
            };
        }

    });

});