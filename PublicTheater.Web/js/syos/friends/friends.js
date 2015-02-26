(function ($) {

	// Show View
	ShowView = Backbone.View.extend({
		initialize : function () {
			this.$el = $('#syosFriendsShow');

			this.performance = new Performance({ parentView: this });
			this.dict = new ViewDict({ parentView: this });
			this.facebook = new Facebook({ parentView: this });
			
			this.template = Handlebars.compile($('#friendsShow').html());
			this.render(this);
		},

		events : {
			'click .syosFriendsLogin' : function () {
				this.facebook.login();
			},
			'click .showButton' : function () {
				this.$el.find('.friendsList').slideToggle(400,'easeInOutSine');
			}
		},

		render : function () {
			var html = this.template(this.dict.toJSON());
			this.$el.html(html);

			return this;
		},

		facebookDidChange : function () {
			var _this = this;

			var isConnected = this.facebook.get('status') === "connected";

			if(isConnected) {
				this.dict.set('showLoginButton', false);
				window.syosFriends.bridge.getPerformanceFriends(function (response) {
					var friendIDs = response;

					_this.dict.set('friendCount', friendIDs.length);

					_this.performance.get('friends').reset();

					$.each(friendIDs, function () {
						_this.performance.get('friends').add(new Friend({ parentView: _this, facebookID: this }));
					});

					_this.friendDidChange();
				});
			}

		},

		friendDidChange : function () {
			var _this = this;

			// Update view dict with friends from performance
			var friends = [];

			$.each(_this.performance.get('friends').models, function () {
				friends.push(this.toJSON());
			});

			_this.dict.set('friends',friends);

			_this.render();

		}

	}),

	Friend = Backbone.Model.extend({
		defaults : {
			facebookID : "0",
			name : "Default Person"
		},

		initialize : function () {
			var _this = this;
			var fbID = _this.get('facebookID');

			FB.api('/' + fbID, function (response) {
				_this.set('name', response.name);
				_this.set('link', response.link);
				_this.get('parentView').friendDidChange();
			});
		}
	});


})(jQuery);






