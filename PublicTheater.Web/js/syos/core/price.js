(function ($) {
	SYOSPrice = Backbone.Model.extend({
		defaults : {
			DefaultPrice : 0,
			Price : 0
		},

		initialize : function () {
			var _this = this;

			// determine if price is discounted
			var IsDiscounted;
			_this.get('DefaultPrice') === _this.get('Price') ?
				IsDiscounted = false :
				IsDiscounted = true;

			_this.set('IsDiscounted', IsDiscounted);

		}
	});
})(jQuery)
