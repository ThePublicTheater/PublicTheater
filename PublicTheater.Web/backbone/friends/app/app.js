

define([

    'backbone',
    'friends/reservationView/reservationView',
    'friends/statusView/statusView',
    'friends/eventView/eventView',
    'friends/registerView/registerView',
    'friends/charmView/charmView'

], function (Backbone, ReservationView, StatusView, EventView, RegisterView, CharmView) {

    return Backbone.View.extend({

        initialize: function (opts) {
            this.context = opts.context;
            this.listenTo(this.context, 'change', this.didChangeContext, this);
            this.initSubviews = _(this.initSubviews).once();
        },

        didChangeContext: function () {
            if (this.context.isReady()) {
                this.initSubviews();
            }
        },

        initSubviews: function () {
            console.group('Friends View Summary');
            this.subviews = {};
            _(this.viewTypes).each(this.initViewType, this);
            console.groupEnd();
        },

        viewTypes: {
            friendsEvent: {
                View: EventView,
                key: 'event'
            },
            friendsStatus: {
                View: StatusView,
                key: 'status'
            },
            friendsReservation: {
                View: ReservationView,
                key: 'reservation'
            },
            friendsCharm: {
                View: CharmView,
                key: 'charm'
            },
            friendsRegister: {
                View: RegisterView,
                key: 'register'
            }
        },

        initViewType: function (viewType, replaceTag) {
            console.group(viewType.key);
            var targets = this.el.querySelectorAll('[data-replace=' + replaceTag + ']');

            var views = this.subviews[viewType.key] = _(targets).map(function (replaceEl) {
                var view = new viewType.View({
                    context: this.context,
                    data: $(replaceEl).data()
                });
                view.render();

                replaceEl.parentElement.replaceChild(view.el, replaceEl);

                console.log(view.el);
                return view;
            }, this);

            if (views.length === 0) console.log('none');

            console.groupEnd();
        },

        log: function () {
            console.log('context', this.context.toJSON());
            console.log('user', this.context.user.toJSON());
        }



    });

});