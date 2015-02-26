// configuration for facebook friends

cfsFriendsConfig = {
	facebookAppId : '332501403523272',
	
Url : window.location.domain + '/channel.html'
};

// global dispatch handler

cfsFriendsDispatch = _.clone(Backbone.Events);
cfsFriendsDispatch.on('all', function (eventName) {
	console.group("friends dispatcher");
	console.log("event", eventName);
	console.log("data", arguments);
	console.groupEnd();
	return this;
});