

define(['backbone'], function (Backbone) {

    return Backbone.View.extend({

        templateModel: function () {
            return {};
        },
        
        willRender: function () { },

        render: function () {
            this.willRender();

            var contents = this.template(this.templateModel());
            this.el.innerHTML = contents;

            this.trigger('didRender');
            this.didRender();

            return this;
        },

        didRender: function () { },

        didError: function (error) {
            console.log("View Error", arguments);
            this.el.innerHTML = "";
            this.$el.addClass('fb-error');
            this.$el.attr('title', 'Error: ' + error.message);
        },

        replaceWithView: function (tag, view) {
            var target = this.el.querySelector('[data-replace=' + tag + ']');
            target.parentElement.replaceChild(view.el, target);
        }

    });

});