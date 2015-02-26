(function ($) {
	SYOSModalView = Backbone.View.extend({
		initialize : function () {
			var _this = this;
			_this.$el = $('#syos-modal');
			_this.$overlay = $('#syos-modal-overlay');
			_this.Template = Handlebars.compile($('#modalTemplate').html());

			_this.$el.bind('mousewheel', function (event) {
				event.stopPropagation();
			})

			_this.render();
			_this.show();

			syosDispatch.on('keyboard:escape', function () {
				_this.close();
			});
		},

		events : {
			"click [data-bb-event='close']" : "close",
			"click [data-bb-event='action']" : "actionHandler"

		},

		actionHandler : function (event) {
			var ActionIndex = $(event.currentTarget).index();
			this.options.model.trigger("actionSelectedAtIndex", ActionIndex);
		},

		show : function () {
			this.$overlay.fadeIn();
			this.$el.fadeIn();
		},

		close : function () {
			this.$el.fadeOut();
			this.$overlay.fadeOut();
		},

		render : function () {
			var html = this.Template(this.options.model.templateJSON());
			this.$el.html(html);
		}
	});
})(jQuery);