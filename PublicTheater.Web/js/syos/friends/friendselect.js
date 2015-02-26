(function ($) {
	FriendSelect = Backbone.View.extend({
		initialize : function () {
			var _this = this;
			this.$el = $('#syosFriendSelect')
			this.template = Handlebars.compile($('#friendSelectTemplate').html());

			syosFriendsDispatch.on('facebook:recievedFriendData', function(response) {
				response.sort(function(a, b) {
					var nameA = a.name.toLowerCase(), nameB = b.name.toLowerCase();
					if(nameA < nameB) return -1;
					if(nameA > nameB) return 1;
					return 0;
				});
				_this.friendList = response;
				_this.render();

				_this.parentView.trigger("friendSelectReady");
			});

			this.on('openFriendSelect', function () {
				_this.$el.fadeIn();
			});

			this.on('closeFriendSelect', function () {
				_this.$el.fadeOut();
			});

			this.on('selectSavedFriend', function (friendID) {
				$.each(_this.friendList, function () {
					if(this.id == friendID)
						_this.parentView.trigger('friendSelected', this);
				});
			});

			this.render();
			this.$el.hide();
		},

		events : {
			'click .closeSelectFriends' : function () {
				this.trigger('closeFriendSelect');
			},
			'click li' : function (e) {
				var targetIndex = $(e.currentTarget).index();
				this.parentView.trigger('friendSelected',this.friendList[targetIndex])
			}
		},

		render : function () {
			var html = this.template({
				friendList : this.friendList
			});
			this.$el.html(html);
		}
	});
})(jQuery);