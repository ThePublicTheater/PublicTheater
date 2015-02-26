

require(['/backbone/require.config.js'], function () {

    var should = chai.should();
    
    mocha.setup('bdd');
    mocha.timeout(10 * 1000);

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

        var hasStarted = false;

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
            hasStarted = true;
            console.log('starting test');
            mocha.run();
        }).once();

        setTimeout(function () {
            if (!hasStarted) alert("Hasn't started? Are you logged into facebook & logged in as a tessitura user?");
        }, 3000);

        describe("Friends - Performance Service", function () {

            it("should return a performance populated with info", function (done) {

                var testPerformance = new Model.Performance({ performanceId: 2047 });

                context.loadPerformance(testPerformance).then(function (performance) {

                    performance.should.be.an.instanceOf(Model.Performance);
                    performance.reservation.should.be.an.instanceOf(Model.Reservation);
                    performance.friendReservations.should.be.an.instanceOf(Model.ReservationCollection);
                    performance.friendEvents.should.be.an.instanceOf(Model.EventCollection);

                    done();
                });

            });


        });


    });

});