(function ($) {
	FriendsMapView = Backbone.View.extend({
		initialize : function () {
			var _this = this;
			this.$el = $('#mapView');
			this.dict = new Backbone.Model();
			this.mapTips = [];
			this.showLogin = true;

			var performanceId = $('#HfSelectedPerformanceId').val();
			this.dict.set('performanceId', performanceId);

			syosFriends.bridge.getPerformanceFriends(this, this.dict.get("performanceId"));

			this.on('recievedPerformanceFriends', function (response) {
				_this.seatingInfo = response.SeatingInfo;
				_this.highlightMap();
				_this.render();
			});

			this.on('loadFriendPopup', function (friendId, event) {
				window.syosFriends.facebook.trigger('requestFriendData', this, friendId);
			});

			this.on('recievedFriendData', function (response) {
				if(!this.mapTips[response.id]) {
					this.mapTips[response.id] = new FriendsMapTip(response);
				}
			});

			this.on('showFriendPopup', function (friendId, event) {
				var targetMapTip = this.mapTips[friendId];
				var tip = {
					x : event.pageX - 13,
					y : event.pageY - 35
				}
				targetMapTip.$el.css({
					top : tip.y + 'px',
					left : tip.x + 'px',
					display : 'block'
				})
			});

			syosFriendsDispatch.trigger("facebook:statusChanged", function (response) {
			});

			this.render();
		},

		highlightMap : function () {
			var _this = this;
			$.each(this.seatingInfo, function () {
				var friendId = this.FriendId;
				$.each(this.Performances, function () {
					$.each(this.Seats, function () {
						var targetSeatId = this.Seats;
						if(unavailableSeatsReferences[targetSeatId]) {
							unavailableSeatsReferences[targetSeatId].attr('fill','#3B5998');
							unavailableSeatsReferences[targetSeatId].attr('path','#A7B4D1');
							unavailableSeatsReferences[targetSeatId].attr('opacity',1);
							unavailableSeatsReferences[targetSeatId].click(function (e) {
								_this.trigger("showFriendPopup", friendId, e);
							});
							_this.trigger("loadFriendPopup", friendId, '');
							// unavailableSeatsReferences[targetSeatId].hover(function (e) {
							// });
						}
						else {
						}
					});
				});
			});
		},

		render : function () {
			var _this = this;

		}
	});
})(jQuery);