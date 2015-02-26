

define([

    'backbone'

], function (Backbone) {

    return Backbone.View.extend({
        willRender: function () {},

        render: function () {
            this.willRender();

            if(!!this.template)
                this.el.innerHTML = this.template(this.viewModel());

            this.didRender();

            return this;
        },

        didRender: function () { },
        viewModel: function () { return {}; }
    });

});