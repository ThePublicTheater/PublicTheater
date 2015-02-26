

define([

    'friends/base/view',
    'friends/statusView/templates'

], function (BaseView, Templates) {

    return BaseView.extend({

        className: 'fb-status',

        template: Templates.statusView,

        initialize: function (opts) {
            this.context = opts.context;
            
            this.listenTo(this.context, 'change', this.render, this);
            this.listenTo(this.context.user, 'change', this.render, this);

            this.context.loadUser().then(_(this.render).bind(this));
        },

        events: {
            'click [data-bb=loginButton]': 'didClickLogin',
            'click [data-bb=logoutButton]': 'didClickLogout'
        },

        didClickLogin: function (evt) {
            evt.preventDefault();
            evt.returnValue = false;
            this.context.attemptLogin().then(_(this.render).bind(this));
        },

        didClickLogout: function (evt) {
            evt.preventDefault();
            evt.returnValue = false;
            this.context.attemptLogout().then(_(this.render).bind(this));
        },

        templateModel: function () {
            return {
                context: this.context.toJSON(),
                user: this.context.user.toJSON()
            };
        }

    });

});