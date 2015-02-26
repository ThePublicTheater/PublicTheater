

define([

    'friends/base/popover',
    'friends/base/view',
    'friends/tessitura/performance',
    'friends/reservationList/reservationList',
    'friends/charmView/templates'

], function (Popover, BaseView, Performance, ReservationList, Templates) {

    return BaseView.extend({

        className: 'fb-charm',

        template: Templates.charmView,

        initialize: function (opts) {
            this.context = opts.context;
            this.listenTo(this.context, 'change:fbStatus', this.render, this);

            var performanceId = parseInt(opts.data.pid, 10);
            this.performance = new Performance({
                id: performanceId
            });

            this.context
                .loadReservationsForPerformance(this.performance)
                .then(_(this.render).bind(this));
        },

        events: {
            "click [data-bb=charmCount]": "didClickCharmCount"
        },

        didClickCharmCount: function (evt) {
            evt.stopPropagation();
            var reservationList = new ReservationList({
                model: this.performance.friendReservations
            });
            reservationList.render();

            var popover = new Popover({
                content: reservationList,
                parent: this
            });
            popover.show();
        },

        templateModel: function () {
            return {
                context: this.context.toJSON(),
                user: this.context.user.toJSON(),
                reservations: this.performance.friendReservations.toJSON()
            };
        }

    });

});