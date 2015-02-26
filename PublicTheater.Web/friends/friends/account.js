(function ($) {
	TFAccount = TFModule.extend({
		template : _.template($('#friends-account-template').html()),

		initModule : function () {
			this.user.on("change", this.render, this);
			this.appState.on("change", this.render, this);
			return this;
		},

		events : {
			"click .friends-login-button" : "facebookLogin",
			"click .friends-logout-button" : "facebookLogout"
		},

		render : function () {
			this.$el.html(this.template());
			return this;
		}
	})
})(jQuery)