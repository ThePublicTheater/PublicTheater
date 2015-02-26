(function ($) {

	// Bridge handles all communication with not-JS (including hidden forms & other dom garbage)
	FriendsBridge = Backbone.Model.extend({
		initialize : function () {
			var _this = this;

			syosFriendsDispatch.on("facebook:userIdRecieved", function (response) {
				_this.set('userFacebookId', response);
			});

			syosFriendsDispatch.on("shareView:seatingDataChanged", function (seatingData) {
				_this.saveFriendSeating(seatingData);
			});

		},

		defaults : {
			userFacebookId : 0
		},

		getPerformanceFriends : function (sender, performanceId) {

			performanceId = parseInt(performanceId);

			Adage.HtmlSyos.Code.SYOSService.GetFriendSeating('',performanceId,
			    function onSuccess(msg) {
			    	sender.trigger('recievedPerformanceFriends', msg);
			    },
			    function onError(msg) {
			        alert('Performance Friends Error:\r\n ' + msg._message);
			    }
			);
		},

		saveFriendSeating : function (seatingData) {
			Adage.HtmlSyos.Code.SYOSService.SaveFriendSeating(this.get("userFacebookId"), seatingData.facebookIds, seatingData.shareWithAllFriends, parseInt(seatingData.performanceId),
				function onSuccess(msg) {
					alert('Save seating success\r\n' + msg._message);
				},
				function onError(msg) {
					alert('Save seating error \r\n' + msg._message);
				}
			);
		}
	});
})(jQuery);
