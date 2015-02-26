

define([

    'q',
    'friends/base/model'

], function (Q, BaseModel) {

    return BaseModel.extend({

        defaults: {
            sdkReady: false
        },

        initialize: function () {
            this.loadSDK().done(_(function () {
                this.trigger('ready');
                this.set('sdkReady', true);
                console.log('Friends: SDK ready');
            }).bind(this));
        },

        loadSDK: function () {
            var def = new Q.defer();
            if (this.get('sdkReady')) def.resolve();
            else {
                console.log('Friends: loading SDK...');
                window.fbAsyncInit = _(this.initFacebook).bind(this, def);
                (function (d, s, id) {
                    var js, fjs = d.getElementsByTagName(s)[0];
                    if (d.getElementById(id)) { return; }
                    js = d.createElement(s); js.id = id;
                    js.src = "//connect.facebook.net/en_US/all.js";
                    fjs.parentNode.insertBefore(js, fjs);
                }(document, 'script', 'facebook-jssdk'));
            }

            return def.promise;
        },

        initFacebook: function (def) {
            this.FB = window.FB;
            this.FB.Event.subscribe('auth.authResponseChange', _(this.onAuthResponseChange).bind(this));
            this.FB.init({
                appId: '332501403523272',
                channelUrl: location.origin + '/channel.html',
                status: true,
                xfbml: true
            });
            this.sdkLoaded = true;

            def.resolve();
        },

        onAuthResponseChange: function (response) {
            console.log('Friends: FB Auth Response changed', response);
            this.trigger('authChange', response);
        },

        withLoginStatus: function () {
            var def = new $.Deferred();

            this.FB.getLoginStatus(_(function (response) {
                def.resolveWith(null, [response]);
            }).bind(this));

            return def;
        },

        attemptLogin: function () {
            var def = new Q.defer();
            this.FB.login(def.resolve);
            return def.promise;
        },

        attemptLogout: function () {
            var def = new Q.defer();
            this.FB.logout(def.resolve);
            return def.promise;
        },
        
        loadUser: function () {
            var def = Q.defer();
            this.FB.api('/me', def.resolve);
            return def.promise;
        },

        loadFriends: function () {
            var def = new Q.defer();
            this.FB.api('/me/friends', def.resolve);
            return def.promise;
        },

        createEvent: function (event, user) {
            var def = Q.defer();

            var url = ["https://graph.facebook.com/", user.get('id'), "/events"].join("");
            var data = event.toFacebookData();

            this.FB.api('/me/events', 'post', data, def.resolve);

            return def.promise;
        },

        //#region Permissions

        checkForPermission: function (permissionKey) {
            console.log("Friends: checking %o permission", permissionKey);

            var def = Q.defer();

            this.FB.api('/me/permissions', function (res) {
                var permissions = res.data[0];
                var permissionValue = permissions[permissionKey];
                var hasPermission = (permissionValue === 1);

                console.log("Friends: permission %o status: %o", permissionKey, permissionValue);

                def.resolve(hasPermission);
            });

            return def.promise;
        },

        requestPermission: function (permissionKey) {
            console.log("Friends: requesting %o permission", permissionKey);

            var def = Q.defer();

            this.FB.login(function (res) {
                console.log('permission response', res);
                console.log("Friends: requesting %o permission", permissionKey);
            }, { scope: permissionKey });

            return def.promise;
        }

        //#endregion

    });

});