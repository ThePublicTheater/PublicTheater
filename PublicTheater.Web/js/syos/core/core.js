(function ($) {
	SYOSCore = Backbone.Model.extend({
		initialize : function () {
			var _this = this;

			window.$syos = function () {
				return _this;
			}

			_this.Utility = new SYOSUtility();
			_this.Keyboard = new SYOSKeyboard();
			// _this.Legacy = new SYOSLegacy();
			_this.Display = new SYOSDisplay();

			syosDispatch.trigger('core:didFinishInit');

		}
	});
})(jQuery);