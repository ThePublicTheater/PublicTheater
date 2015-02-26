

define([

    'friends/base/view',
    'friends/base/modal',
    'friends/reservationView/reservationPanel',
    'friends/tessitura/performance',
    'friends/reservationView/templates'

], function (BaseView, Modal, ReservationPanel, Performance, Templates) {
    "use strict";

    return BaseView.extend({

        className: 'fb-reservation-view fb-action-button',

        template: Templates.reservationView,

        initialize: function (opts) {
            this.context = opts.context;
            this.performanceId = opts.data.pid;

            this.listenTo(this.context, 'change:loggedIn', this.render, this);

            this.context.loadUserPerformances()
                .then(_(this.attachPerformance).bind(this))
                .fail(_(this.didError).bind(this));
        },

        attachPerformance: function () {
            this.performance = this.context.user.performances.get(this.performanceId);
            this.render();
        },

        events: {
            "click [data-bb=shareAction]": "didClickShare"
        },

        didClickShare: function (evt) {
            evt.preventDefault();

            var panel = new ReservationPanel({
                context: this.context,
                performance: this.performance
            });
            panel.render();

            var modal = new Modal({
                content: panel,
                title: this.performance.get('title')
            });
            modal.render();
            modal.open();
        },

        templateModel: function () {
            return {
                ready: (typeof this.performance === 'object'),
                context: this.context.toJSON()
            };
        }

    });

});