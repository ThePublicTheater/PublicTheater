(function ($) {
	Facebook = Backbone.Model.extend({
		defaults : {
			status : "unknown",
			loading : false,
			hasRecievedFriends : false,
			friendsData : null
		},

		initialize : function () {
			var _this = this;

			FB.Event.subscribe('auth.statusChange', function (response) {
				syosFriendsDispatch.trigger("facebook:userIdRecieved", response.authResponse.userID);

				_this.set("status",response.status);

				syosFriendsDispatch.trigger("facebook:statusChanged", response);
			});

			this.on("requestFriendData", function (sender, friendId) {
				FB.api('/' + friendId, function (response) {
					sender.trigger('recievedFriendData', response);
				});
			});
		},

		login : function () {
			var _this = this;

			FB.login( function (response) {
			}, {scope: "read_friendlists"});
		},

		getFriends : function () {
			var _this = this;

			_this.set('loading', true);
			FB.api('/me/friends/', function(response) {
				_this.set('friendsData',response.data);
				_this.set('hasRecievedFriends', true);
				
				syosFriendsDispatch.trigger('facebook:recievedFriendData', response.data);
			});
		},

	});
})(jQuery);