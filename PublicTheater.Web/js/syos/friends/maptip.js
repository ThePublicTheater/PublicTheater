(function ($) {
	FriendsMapTip = Backbone.View.extend({
		initialize : function () {
			var elementId = 'maptip-' + this.options.id;

			$('body').append('<div id="' + elementId +'" class="friendMapTip"></div>');
			this.$el = $('#' + elementId);

			this.template = Handlebars.compile($('#friendMapTip').html());

			this.render();
		},

		events : {
			'click .closeTip' : function () {
				this.$el.hide();
			}
		},

		render : function () {
			var html = this.template(this.options);
			this.$el.html(html);
		}
	});
})(jQuery);