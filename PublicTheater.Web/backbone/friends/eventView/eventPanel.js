

define([

    'friends/base/view',
    'friends/base/viewBag',
    'friends/facebook/friendCollection',
    'friends/facebook/event',
    'friends/friendSelection/friendSelection',
    'friends/eventView/templates'

], function (BaseView, ViewBag, FriendCollection, FriendEvent, FriendSelection, Templates) {

    return BaseView.extend({

        template: Templates.eventPanel,

        initialize: function (opts) {
            this.context = opts.context;
            this.performance = opts.performance;

            this.bag = new ViewBag();

            this.allFriends = this.context.user.friends;
            this.selectedFriends = new FriendCollection();

            this.event = this.performance.event;
            this.event.set({
                name: this.performance.get('title'),
                location: this.performance.get('venueName'),
                start: moment().toDate(),
                end: moment().add('hours', 1).toDate()
            });

            this.bag.on('change', this.render, this);

            this.listenTo(this.context, 'change:hasEventsPermission', this.render, this);
            this.listenTo(this.event, 'change:eventId', this.render, this);

            this.bag.raise('loadingFriends');
            this.context.loadUserFriends()
                .then(_(this.context.checkEventPermission).bind(this.context))
                .then(_(this.initSelectionView).bind(this));
        },

        initSelectionView: function () {
            this.bag.lower('loadingFriends');

            this.selectionView = new FriendSelection({
                selected: this.selectedFriends,
                available: this.allFriends
            });
            this.selectionView.render();
            this.render();
        },

        events: {
            "click [data-action=authorizeApp]": "didClickAuthorize",
            "click [data-action=createEvent]": "didClickCreateEvent"
        },

        didClickAuthorize: function () {
            this.context.requestEventPermission();
        },

        didClickCreateEvent: function () {
            this.bag.raise('creatingEvent');
            this.context.createEvent(this.event).then(_(function () {
                this.bag.lower('creatingEvent');
                this.render();
            }).bind(this));
        },

        didRender: function () {
            this.selectionView && this.replaceWithView('friendSelection', this.selectionView);
            this.setBindings();
        },

        setBindings: function () {
            var boundElements = this.el.querySelectorAll('[data-bind]');
            _(boundElements).each(function (boundEl) {
                var $boundEl = $(boundEl);
                var attr = $boundEl.attr('data-bind');

                $boundEl.on('change', _(function () {
                    this.event.set(attr, $boundEl.val());
                }).bind(this));

            }, this);
        },

        templateModel: function () {
            return {
                context: this.context.toJSON(),
                event: this.event.toJSON(),
                bag: this.bag.toJSON(),
                performance: this.performance.toJSON()
            };
        }

    });

});