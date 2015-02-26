TFNode = Backbone.Model.extend({

});

TFState = TFNode.extend({
	defaults : {
		status : "unknown",
		loggedIn : false
	}
});

TFUser = TFNode.extend({
	defaults : {
		first_name : "..."
	},

	initialize : function () {
		this.friends = new TFFriendCollection();
		return this;
	}
});

TFFriend = TFNode.extend({

});

TFFriendCollection = Backbone.Collection.extend({
	model : TFFriend
});