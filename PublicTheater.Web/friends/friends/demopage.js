(function ($) {
	TFDemoPage = Backbone.View.extend({
		initialize : function () {
			this.loadAccountModules();
			this.loadFriendPickers();
		},

		loadAccountModules : function () {
			this.accountModules = [];
			var _this = this;
			this.$el.find('.friends-account').each(function (index, element) {
				var accountModule = new TFAccount();
				accountModule.setElement(element).render();
				_this.accountModules.push(accountModule);
			});
			return this;
		},

		loadFriendPickers : function () {
			this.friendPickers = [];
			var _this = this;
			this.$el.find(".friends-picker").each(function (index, element) {
				var friendPicker = new TFPicker();
				friendPicker.setElement(element).render();
				_this.friendPickers.push(friendPicker);
			});
			return this;
		}
	});
})(jQuery);