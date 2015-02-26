(function ($) {
	SYOSLegacyMapView = Backbone.View.extend({
		id : "syos",

		initialize : function () {
			var _this = this;
			_this.$el = $('#syos');
		},

		changeLevelClicked : function () {
			log('changelevelclicked');
			alert('hi');
			syosDispatch.trigger('map:levelSelectionOpened');
		},

		levelSelected : function () {
			syosDispatch.trigger('map:levelSelected');
		}
	});
})(jQuery);