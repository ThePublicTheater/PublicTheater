(function ($) {
	SYOSDisplay = Backbone.Model.extend({
		initialize : function () {
			var _this = this;
			_this.Control = new SYOSControl();

			syosDispatch.on('control:zoom', function (ShouldZoomIn) {
				if(ShouldZoomIn)
					syos.zoomSeatmapIn();
				else
					syos.zoomSeatmapOut();
			});
		}
	});
})(jQuery);