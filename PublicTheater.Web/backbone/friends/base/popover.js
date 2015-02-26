

define([

    'backbone',
    'friends/base/templates'

], function (Backbone, Templates) {

    return Backbone.View.extend({

        className: 'fb-popover',

        template: Templates.popover,

        initialize: function (opts) {
            this.content = opts.content;
            this.parent = opts.parent;

            if (opts.auxClass) this.$el.addClass(opts.auxClass);

            this.$el.html(this.template());
            this.$el.find('[data-bb=popoverContent]').append(this.content.el);

            $('body').append(this.el);
            this.position();
        },

        position: function () {
            var locus = this.parent.$el.offset();
            locus.left += this.parent.$el.width();

            this.$el.css({
                position: 'absolute',
                top: locus.top,
                left: locus.left
            });
            
        },

        closeForExternalClick: function (evt) {
            var inPopover = $.contains(this.el, evt.target);
            if (!inPopover) this.hide();
        },

        show: function () {
            $('body').on('click', _(this.closeForExternalClick).bind(this));
            this.$el.show();
        },

        hide: function () {
            $('body').off('click', _(this.closeForExternalClick).bind(this));
            this.$el.hide();
        }

    });

});