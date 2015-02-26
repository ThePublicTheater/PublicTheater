(function ($) {
	SYOSControl = Backbone.Model.extend({
		initialize : function () {
			
			// scroll events
			syosDispatch.on('scroll:up', function () {
				syosDispatch.trigger('control:zoom',true);
			});
			syosDispatch.on('scroll:down', function () {
				syosDispatch.trigger('control:zoom',false);
			});

		}
	});
})(jQuery);