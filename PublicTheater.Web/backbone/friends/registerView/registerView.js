

define([

    'friends/base/view',
    'friends/registerView/templates'

], function (BaseView, Templates) {

    return BaseView.extend({

        className: 'fb-register',

        template: Templates.registerView,

        initialize: function (opts) {
            this.context = opts.context;
        },

        events: {
            "click [data-action=register]": "didClickRegister"
        },

        didClickRegister: function (evt) {
            evt.preventDefault();

            this.context.attemptRegister().then(_(function (response) {
                if (response.errorText) {
                    alert(response.errorText);
                    return;
                }

                location.reload();
            }).bind(this));

        }

    });

});