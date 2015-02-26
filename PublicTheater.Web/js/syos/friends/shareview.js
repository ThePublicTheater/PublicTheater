(function ($) {
	ShareView = Backbone.View.extend({
		initialize : function () {
			var _this = this;

			this.$el = $('#syosFriendsShare');

			this.dict = new Backbone.Model();
			this.dict.set("showLoginButton",true);
			this.dict.set("showLoading",false);
			this.dict.set("friendCount",0);
			this.dict.set("selectedFriends",[]);
			this.dict.set("shareWithAllFriends",false);

			this.friendSelect = new FriendSelect();
			this.friendSelect.parentView = this;

			// this.performance = new Performance({ parentView: this });
			this.facebook = new Facebook({ parentView: this });
			
			this.advancedDisplayed = false;


			this.template = Handlebars.compile($('#friendsShare').html());
			this.render(this);

			// When Friend Select is ready, get saved friends from server
			this.on('friendSelectReady', function () {
				this.dict.set("performanceId", $("#fakePerformanceId").val());
				syosFriends.bridge.getPerformanceFriends(this,this.dict.get("performanceId"));
			});

			// Bridge Events
			this.on('recievedPerformanceFriends', function (response) {
				_this.seatingInfo = response.SeatingInfo;
				$.each(_this.seatingInfo, function () {
					_this.friendSelect.trigger('selectSavedFriend',this.FriendId);
				});
			});

			// Friend Selection Events
			this.on('friendSelected', function (response) {
				_this.dict.get("selectedFriends").push(response);
				_this.render(_this);
			});

			// Global Events
			syosFriendsDispatch.on('selectFriends:seatingDataUpdated', function () {
				var _this = window.syosFriends.shareView;

				var selectedIds = '';
				$.each(_this.dict.get('selectedFriends'), function () {
					selectedIds += this.id;
					selectedIds += ','; 
				});
				selectedIds = selectedIds.substring(0, selectedIds.length - 1);

				var seatingData = {
					facebookIds : selectedIds,
					shareWithAllFriends : _this.dict.get('shareWithAllFriends'),
					performanceId : _this.dict.get('performanceId')
				}
				syosFriendsDispatch.trigger("shareView:seatingDataChanged", seatingData);
			});

			syosFriendsDispatch.on('facebook:statusChanged', function (response) {
				var isConnected = _this.facebook.get('status') === "connected";
				var hasRecievedFriends = _this.facebook.get('hasRecievedFriends');

				if(isConnected) {
					_this.dict.set('showLoginButton', false);
				}

				if(isConnected && !hasRecievedFriends) {
					_this.facebook.getFriends();
				}

				_this.render();
			});

		},

		events : {
			'click .syosFriendsLogin' : function () {
				this.facebook.login();
			},

			'click .shareButton': function () {
				this.$el.find('.shareDialog').slideToggle(400,'easeInOutSine');
				this.dialogOpen = !this.dialogOpen;
			},

			'click .shareAdvancedToggle' : function (e) {
				var advanced = this.$el.find('.shareAdvanced');
				advanced.slideToggle(400,'easeInOutSine');
				this.advancedDisplayed = !this.advancedDisplayed;

				if(this.advancedDisplayed)
					this.$el.find('.shareAdvancedToggle').html('Hide advanced');
				else
					this.$el.find('.shareAdvancedToggle').html('Show advanced');

				e.preventDefault();
			},

			'click .shareSaveButton' : function (e) {
				syosFriendsDispatch.trigger('selectFriends:seatingDataUpdated');
				this.friendSelect.trigger('closeFriendSelect');
				e.preventDefault();
			},

			'click .friendsEditButton' : function () {
				this.friendSelect.trigger('openFriendSelect');
			},

			'change .shareSelection input[type="checkbox"]' : function (e) {
				var targetBox = $(e.currentTarget);
				var isChecked = (targetBox.attr("checked") === "checked");

				targetBox.parent().toggleClass("selected");

				if(isChecked) {
					this.selectedFriendIds.push(targetBox.val());
				}

				this.dict.set('selectedFriendCount', this.selectedFriendIds.length);
				this.render();
			},

			'click .removeSelectedFriend' : function (e) {
				var targetIndex = $(e.currentTarget).parent('li').index();
				this.dict.get("selectedFriends").splice(targetIndex,1);
				this.render();
				e.preventDefault();
			}
		},

		render : function () {
			var html = this.template(this.dict.toJSON());
			this.$el.html(html);

			// if(!this.advancedDisplayed)
				// this.$el.find('.shareAdvanced').hide();
			


			return this;
		},

		facebookDidChange : function () {

			var isConnected = this.facebook.get('status') === "connected";
			var hasRecievedFriends = this.facebook.get('hasRecievedFriends');

			if(isConnected) {
				this.dict.set('showLoginButton', false);
			}

			if(isConnected && !hasRecievedFriends) {
				this.facebook.getFriends();
			}

			this.render();
		}
	});
})(jQuery);