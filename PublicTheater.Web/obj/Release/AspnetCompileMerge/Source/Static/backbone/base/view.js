

define(['backbone'], function (Backbone) {

    return Backbone.View.extend({

        templateModel: function () {
            return {};
        },

        willRender: function () { },

        didRender: function () { },

        render: function () {
            this.willRender();

            var contents = this.template(this.templateModel());
            this.el.innerHTML = contents;

            this.trigger('didRender');
            this.didRender();

            return this;
        },

        replaceWithView: function (tag, view) {
            var target = this.el.querySelector('[data-replace=' + tag + ']');
            target.parentElement.replaceChild(view.el, target);
        }

    });

});