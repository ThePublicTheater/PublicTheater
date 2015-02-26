

require(['/backbone/require.config.js'], function () {

    var should = chai.should();
    
    mocha.setup('bdd');
    mocha.timeout(4000);

    require([

        'q',
        'underscore',
        'errorHelpers',
        'friends/app/context',
        'friends/model'

    ], function (Q, _, ErrorHelpers, Context, Model) {

        window.Q = Q;

        var context = new Context();
        window.context = context;

        context.on('change', function () {
            if (!context.get('sdkReady')) {
                console.log('waiting for sdk...');
                return;
            }
            if (context.get('fbStatus') !== 'connected') {
                console.log('waiting for logged in...');
                return;
            }

            runMocha();
        });

        var runMocha = _(function () {
            console.log('starting test');
            mocha.run();
        }).once();

        describe("Friends Context", function () {

            it("should return the current user with loadUser", function (done) {
                context.loadUser().then(function (user) {
                    console.log('user:', user);
                    user.should.be.an.instanceOf(Model.User);
                    done();
                });
            });

            it("should fetch the user's friends", function (done) {
                context.loadUserFriends().then(function (friends) {
                    console.log('friends:', friends);
                    friends.should.be.an.instanceOf(Model.FriendCollection);
                    done();
                });
            });

            it("should fetch the user's upcoming performances", function (done) {
                context.loadUserPerformances().then(function (performances) {
                    console.log('performances:', performances);
                    performances.should.be.an('object');
                    performances.each(function (perf) {
                        perf.reservation.should.be.an('object');
                        perf.reservation.seats.should.be.an('object');
                        perf.friendReservations.should.be.an('object');
                    });
                    done();
                });
            });

            it("should fetch events on a performance", function (done) {
                context.loadUserPerformances().then(function (performances) {
                    context.loadEventsForPerformance(performances.first()).then(function (events) {
                        console.log('performance events:', events);
                        events.should.be.an('object');
                        done();
                    });
                });
            });

            it("should fetch reservations on a performance", function (done) {
                context.loadUserPerformances().then(function (performances) {
                    context.loadReservationsForPerformance(performances.first()).then(function (reservations) {
                        console.log("performance reservations:", reservations);
                        reservations.should.be.an('object');
                        done();
                    });
                });
            });


        });


    });

});