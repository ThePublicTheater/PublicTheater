(function ($) {
	FriendsMapLogin = Backbone.View.extend({
		initialize : function () {
			var _this = this;
			this.$el = $('#mapLogin');
			this.dict = new Backbone.Model({showLogin:true});
			this.template = Handlebars.compile($('#friendsMapLogin').html());

			syosFriendsDispatch.on('facebook:recievedFriendData', function (msg) {
				_this.friendData = msg;
			});

			syosFriendsDispatch.on("facebook:statusChanged", function (msg) {
			});

			this.render();
		},

		render : function () {
			var html = this.template(this.dict.toJSON());
			this.$el.html(html);
		}
	});
})(jQuery);