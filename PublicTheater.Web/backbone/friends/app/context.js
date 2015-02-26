

define([

    'q',
    'errorHelpers',
    'friends/base/model',
    'friends/app/service',
    'friends/app/sdk',
    'friends/facebook/user',
    'friends/tessitura/performanceCollection'

], function (Q, ErrorHelpers, BaseModel, Service, FacebookSDK, FacebookUser, PerformanceCollection) {

    return BaseModel.extend({

        defaults: {
            fbStatus: 'loading',
            loggedIn: false,
            sdkReady: false,
            hasEventsPermission: false,
            userLoaded: false,
            eventsLoaded: false,
            performancesLoading: false,
            performancesLoaded: false,
            friendsLoading: false,
            friendsLoaded: false,
            seatingLoaded: false
        },

        initialize: function (opts) {
            this.service = new Service();
            this.sdk = new FacebookSDK();
            this.user = new FacebookUser();

            this.queue = {
                login: [],
                performance: []
            };

            this.initEvents();
        },

        initEvents: function () {
            this.listenTo(this.sdk, 'authChange', this.didChangeAuth, this);
            this.listenTo(this.sdk, 'ready', _(this.set).bind(this, 'sdkReady', true));
        },

        isReady: function () {
            return this.get('sdkReady');
        },

        isLoggedIn: function () {
            return !!this.user.get('id');
        },

        // #region authentication

        attemptLogin: function () {
            var def = Q.defer();

            this.sdk.attemptLogin().done(_(function (response) {
                def.resolve(response);
            }).bind(this));

            return def.promise;
        },

        attemptLogout: function () {
            var def = Q.defer();
            this.sdk.attemptLogout().then(def.resolve);
            return def.promise;
        },

        attemptRegister: function () {
            var def = Q.defer();

            var opts = {
                user: this.user.toJSON(),
                token: this.sdk.FB.getAccessToken()
            };

            this.service.attemptRegister(opts).then(_(function (res) {
                console.log('register response', res);
            }).bind(this));

            return def.promise;
        },

        // #endregion

        // #region events

        checkEventPermission: function () {
            var def = Q.defer();

            this.sdk.checkForPermission('create_event').then(_(function (hasPermission) {
                this.set('hasEventsPermission', hasPermission);
                def.resolve(hasPermission);
            }).bind(this));

            return def.promise;
        },

        requestEventPermission: function () {
            var def = Q.defer();

            this.sdk.requestPermission('create_event').then(_(function (hasPermission) {
                this.set('hasEventsPermission', hasPermission);
                def.resolve(hasPermission);
            }).bind(this));

            return def.promise;
        },

        createEvent: function (event) {
            var def = Q.defer();

            this.sdk.createEvent(event, this.user).then(_(function (res) {
                console.log("Friends: create event response", res);
                event.set({
                    eventId: res.id
                });
                
                this.saveEvent(event).then(def.resolve);

            }).bind(this));

            return def.promise;
        },

        saveEvent: function (event) {
            var def = Q.defer();

            console.log("Friends: saving event %o", event);

            event.set('userId', this.user.get('id'));

            var opts = {
                user: this.user.toJSON(),
                evt: event.toJSON()
            };
            this.service.saveEvent(opts).then(_(function (res) {
                console.log("Friends: saved event %o", res);
                def.resolve(event);
            }).bind(this));

            return def.promise;
        },

        // #endregion

        loadPerformance: function (performance) {
            var def = Q.defer();

            this.onceLoggedIn().then(_(function () {
                var opts = {
                    user: this.user.toJSON(),
                    performance: performance.toJSON()
                };
                this.service.getPerformance(opts).then(function (response) {
                    console.log(response);
                    performance.addInfo(response);
                    def.resolve(performance);
                });
            }).bind(this));

            return def.promise;
        },

        loadUser: function () {
            var def = Q.defer(),
                sdk = this.sdk,
                user = this.user;

            this.onceLoggedIn().then(function () {
                sdk.loadUser().then(function (response) {
                    user.set(response);
                    def.resolve(user);
                });
            });

            return def.promise;
        },

        loadUserFriends: function () {
            var def = Q.defer();

            if (this.get('loadedFriends')) {
                return def.resolve().promise;
            }

            this.onceLoggedIn().then(_(function () {

                this.sdk.loadFriends().then(_(function (response) {

                    this.user.friends.add(response.data);
                    def.resolve(this.user.friends);

                }).bind(this));

            }).bind(this));

            return def.promise;
        },

        loadUserPerformances: function () {
            var def = Q.defer();

            var loading = this.get('performancesLoading');
            var loaded = this.get('performancesLoaded');

            if (loaded) {
                def.resolve(this.user.performances);
                return def.promise;
            }
            else {
                this.queue.performance.push(def);
                if (!loading) {
                    this.set('performancesLoading', true);

                    this.onceLoggedIn().then(_(function () {
                        var opts = {
                            user: this.user.toJSON()
                        };

                        this.service.getPerformances(opts).then(_(function (response) {
                            try {
                                this.user.performances.addWithInfo(response);
                                this.set('performancesLoaded', true);
                                this.resolveQueue('performance', this.user.performances);
                            }
                            catch (err) {
                                this.rejectQueue('performance', err);
                            }
                        }).bind(this));

                    }).bind(this));
                }
            }

            return def.promise;
        },

        loadEventsForPerformance: function (performance) {
            var def = Q.defer(),
                user = this.user,
                service = this.service;
            
            this.onceLoggedIn().then(function () {
                var opts = {
                    user: user.toJSON(),
                    performance: performance.toJSON()
                };
                service.getEvents(opts).then(function (response) {
                    performance.events.add(response);
                    def.resolve(performance.events);
                });
            });

            return def.promise;
        },

        loadReservationsForPerformance: function (performance) {
            var def = Q.defer();

            this.onceLoggedIn()
                .then(_(this.loadUserFriends).bind(this))
                .then(_(function () {
                    var opts = {
                        user: this.user.toJSON(),
                        performance: performance.toJSON()
                    };
                    this.service.getReservations(opts).then(_(function (response) {
                        performance.friendReservations.addManyWithSeats(response);
                        performance.friendReservations.matchWithFriends(this.user.friends);
                        def.resolve(performance);
                    }).bind(this));

                }).bind(this));

            return def.promise;
        },

        saveReservation: function (reservation) {
            var def = Q.defer();

            reservation.set('userId', this.user.get('id'));

            var opts = {
                user: this.user.toJSON(),
                reservation: reservation.toJSON()
            };
            this.service.saveReservation(opts).then(_(function (res) {
                console.log("Friends: saved reservation %o", res);
                def.resolve(reservation);
            }).bind(this));

            return def.promise;

        },

        didChangeAuth: function (response) {
            var loggedIn = (response.status === "connected");

            this.set({
                fbStatus: response.status,
                loggedIn: loggedIn
            });

            if (response.authResponse) {
                this.user.set({
                    id: response.authResponse.userID
                });
                this.resolveQueue('login');
            }

        },

        onceLoggedIn: function () {
            var def = Q.defer();

            if (this.isLoggedIn())
                def.resolve();
            else
                this.queue.login.push(def);

            return def.promise;
        },

        resolveQueue: function (name, param) {
            console.log('Friends: resolving ' + name + ' queue');

            _(this.queue[name]).invoke('resolve', param);
            this.queue[name] = [];
        },

        rejectQueue: function (name, err) {
            console.log('Friends: rejecting ' + name + ' queue');

            _(this.queue[name]).invoke('reject', err);
            this.queue[name] = [];
        }


    });

});