

define([

    'friends/base/view',
    'friends/base/templates'

], function (BaseView, Templates) {

    return BaseView.extend({

        className: 'fb-modal',

        template: Templates.modal,

        initialize: function (opts) {
            this.content = opts.content;
            this.title = opts.title;

            this.closeForOverlayClick_bound = _(this.closeForOverlayClick).bind(this);

            this.render();
        },

        events: {
            "click [data-bb=closeModal]": "close"
        },

        didRender: function () {
            this.replaceWithView('modalContent', this.content);
        },

        templateModel: function () {
            return {
                title: this.title
            };
        },

        closeForOverlayClick: function (evt) {
            evt.preventDefault();
            var isOverlay = $(evt.target).is(this.$el);
            if (isOverlay) this.close();
        },

        open: function () {
            $('body').prepend(this.el);
            $('body').on('click', this.closeForOverlayClick_bound);

            _(this.$el.addClass).chain().bind(this.$el, 'fb-modal-open').delay(100);
        },

        close: function () {
            $('body').off('click', this.closeForOverlayClick_bound);
            this.$el.removeClass('fb-modal-open');

            _(this.remove).chain().bind(this).delay(300);

        }

    });

});