

define([

    'moment',
    'friends/base/model',
    'friends/tessitura/reservation',
    'friends/tessitura/reservationCollection',
    'friends/facebook/eventCollection',
    'friends/facebook/event'

], function (moment, BaseModel, Reservation, ReservationCollection, EventCollection, FriendEvent) {

    return BaseModel.extend({

        idAttribute: 'performanceId',

        initialize: function () {
            this.reservation = new Reservation();
            this.event = new FriendEvent();

            this.friendEvents = new EventCollection();
            this.friendReservations = new ReservationCollection();

            this.on('change:performanceId', this.updatePerformanceId, this);
            this.updatePerformanceId();
        },

        toJSON: function () {
            var base = BaseModel.prototype.toJSON.apply(this, arguments);
            return base;
        },

        updatePerformanceId: function () {
            this.reservation.set('performanceId', this.get('performanceId'));
            this.event.set('performanceId', this.get('performanceId'));
        },

        addInfo: function (info) {
            info.performanceDate = moment(info.performanceDate).toDate();

            var atts = _(info).omit('friendReservations', 'reservation', 'event', 'friendEvents');

            this.set(atts);
            
            this.event.addInfo(info.event);
            this.reservation.addInfo(info.reservation);

            this.friendReservations.addManyWithSeats(info.friendReservations);
        }

    });

});