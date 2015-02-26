

define([

    'friends/base/view',
    'friends/base/modal',
    'friends/tessitura/performance',
    'friends/facebook/event',
    'friends/eventView/eventPanel',
    'friends/eventView/templates'

], function (BaseView, Modal, Performance, FriendEvent, EventPanel, Templates) {

    return BaseView.extend({

        className: 'fb-event-view fb-action-button',

        template: Templates.eventView,

        initialize: function (opts) {
            this.context = opts.context;
            this.performanceId = opts.data.pid;

            this.event = new FriendEvent();

            this.listenTo(this.context, 'change:loggedIn', this.render, this);

            this.context.loadUserPerformances().then(_(this.attachPerformance).bind(this));
        },

        attachPerformance: function () {
            this.performance = this.context.user.performances.get(this.performanceId);
            this.event = this.performance.event;

            this.listenTo(this.event, 'change:eventId', this.render, this);

            this.render();
        },

        events: {
            "click [data-bb=createAction]": "didClickCreate"
        },

        didClickCreate: function (evt) {
            evt.preventDefault();

            var panel = new EventPanel({
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
            var model = {};

            model.context = this.context.toJSON();
            model.ready = (typeof this.performance === 'object');
            model.user = this.context.user.toJSON();
            model.event = this.event.toJSON();

            return model;
        }

    });

});