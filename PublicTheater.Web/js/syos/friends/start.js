    // SYOS Global
    window.syosDispatch = _.extend({}, Backbone.Events);

    // SYOS Friends
    window.syosFriendsDispatch = _.extend({}, Backbone.Events);

      // Load the SDK Asynchronously
      (function(d){
         var js, id = 'facebook-jssdk', ref = d.getElementsByTagName('script')[0];
         if (d.getElementById(id)) {return;}
         js = d.createElement('script'); js.id = id; js.async = true;
         js.src = "//connect.facebook.net/en_US/all.js";
         ref.parentNode.insertBefore(js, ref);
       }(document));

    window.fbAsyncInit = function() {
        FB.init({
            appId      : '118928038257647', // App ID from the App Dashboard
            channelUrl : window.location.origin + '/channel.html', // Channel File for x-domain communication
            status     : true, // check the login status upon init?
            cookie     : true, // set sessions cookies to allow your server to access the session?
            xfbml      : true  // parse XFBML tags on this page?
        });

        window.syosFriends = {};
        window.syosFriends.bridge = new FriendsBridge();
        window.syosFriends.facebook = new Facebook();
    };

    window.unavailableSeatsReady = function() {
        window.syosFriends.mapView = new FriendsMapView();
        // window.syosFriends.mapLogin = new FriendsMapLogin();
    }