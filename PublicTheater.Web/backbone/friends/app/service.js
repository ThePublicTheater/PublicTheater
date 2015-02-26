

define([
    'q',
    'backbone',
    'errorHelpers',
    'asmxHelpers'

], function (Q, Backbone, ErrorHelpers, AsmxHelpers) {

    return Backbone.View.extend({

        getPerformances: function (opts) {
            console.log('Friends: Getting performances', opts);
            var def = Q.defer();

            this.request('GetPerformances', opts).done(function (asmxResponse) {
                var response = AsmxHelpers.clean(asmxResponse);
                def.resolve(response);
            });

            return def.promise;
        },

        getPerformance: function (opts) {
            console.log('Friends: Getting single performance', opts);
            var def = Q.defer();

            this.request('GetPerformance', opts).done(function (asmxResponse) {
                var response = AsmxHelpers.clean(asmxResponse);
                def.resolve(response);
            });

            return def.promise;
        },

        getReservations: function (opts) {
            console.log('Friends: Getting reservations', opts);
            var def = Q.defer();

            this.request('GetReservations', opts).done(function (asmxResponse) {
                var response = AsmxHelpers.clean(asmxResponse);
                def.resolve(response);
            });

            return def.promise;
        },

        saveReservation: function (opts) {
            console.log('Friends: Saving reservation', opts);

            var def = Q.defer();

            this.request('SaveReservation', opts).done(function (asmxResponse) {
                var response = AsmxHelpers.clean(asmxResponse);
                def.resolve(response);
            });

            return def.promise;
        },

        getEvents: function (opts) {
            console.log('Friends: Getting events', opts);
            var def = Q.defer();
            this.request('GetEvents', opts).done(function (asmxResponse) {
                var response = AsmxHelpers.clean(asmxResponse);
                def.resolve(response);
            });
            return def.promise;
        },

        saveEvent: function (opts) {
            console.log('Friends: Saving event', opts);

            var def = Q.defer();

            this.request('SaveEvent', opts)
                .done(function (asmxResponse) {
                    var response = AsmxHelpers.clean(asmxResponse);
                    def.resolve(response);
                });

            return def.promise;
        },

        attemptRegister: function (opts) {
            console.log('Friends: Attempting Register', opts);

            var def = Q.defer();

            this.request('Register', opts)
                .done(function (asmxResponse) {
                    var response = AsmxHelpers.clean(asmxResponse);
                    def.resolve(response);
                });

            return def.promise;
        },

        request: function (method, data) {
            var url = '/Services/FriendService.asmx/' + method;
            var json = JSON.stringify(data);
            return $.ajax({
                url: url,
                type: 'POST',
                data: json,
                error: ErrorHelpers.printErrorToConsole,
                dataType: 'json',
                contentType: 'application/json'
            });
        }

    });

});