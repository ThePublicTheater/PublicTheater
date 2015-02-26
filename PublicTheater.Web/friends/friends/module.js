(function ($) {
	TFModule = Backbone.View.extend({
		appState : new TFState(),
		user : new TFUser(),

		fbPermissions : {scope : "email"},

		initialize : function () {
			this.initFBListeners();
			this.initModule();

			this.appState.on("change:status", this.populateUserInfo, this);

			return this;
		},

		initModule : function () {
			return this;
		},

		initFBListeners : function () {
			(function (self) {
				FB.Event.subscribe('auth.login', function () {
					self.getFacebookLoginStatus();
				});
				FB.Event.subscribe('auth.logout', function () {
					self.getFacebookLoginStatus();
				});
			})(this);
			return this;
		},

		facebookLogin : function () {
			FB.login();
			return this;
		},

		facebookLogout : function () {
			FB.logout();
			return this;
		},

		getFacebookLoginStatus : function () {
			console.log("getting login status");
			(function (self) {
				FB.getLoginStatus(function (response) {
					console.log("status", response);
					self.appState.set({
						status : response.status,
						loggedIn : (response.status === "connected")
					});
				});
			})(this);
			return this;
		},

		populateUserInfo : function () {
			this.getUser();
			this.getFriends();
			return this;
		},

		getUser : function () {
			if(this.appState.get('loggedIn')) {
				(function (self) {
					FB.api('/me', function (response) {
						self.user.set(response);
					});
				})(this);
			};
			return this;
		},

		getFriends : function () {
			(function (self) {
				FB.api('/me/friends', function (response) {
					self.user.friends.add(response.data);
				});
			})(this);
			return this;
		},

		authChangeHandler : function (response) {
			this.loggedIn = (response.status === "connected");
			this.render();
 			return this;
		},

		render : function () {
			return this;
		}
	});
})(jQuery)