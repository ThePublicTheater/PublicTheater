(function ($) {
	SYOSModal = Backbone.Model.extend({
		defaults : {
			CanDismiss : true
		},

		initialize : function () {
			var _this = this;
			_this.View = new SYOSModalView({model: _this});

			_this.on('actionSelectedAtIndex', function (ActionIndex) {
				ActionIndex = parseInt(ActionIndex);
				var Action = _this.get("Actions")[ActionIndex];
				Action.Fn.call(null, _this);
			});
		},

		templateJSON : function () {
			return this.toJSON();
		}
	});
})(jQuery);