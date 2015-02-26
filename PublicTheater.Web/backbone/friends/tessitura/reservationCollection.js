

define([

    'friends/base/collection',
    'friends/tessitura/reservation'

], function (BaseCollection, Reservation) {

    return BaseCollection.extend({

        model: Reservation,

        addManyWithSeats: function (infoArr) {
            _(infoArr).each(this.addWithSeats, this);
        },

        addWithSeats: function (info) {
            var atts = _(info).omit('seats');
            var reservation = new Reservation(atts);
            reservation.seats.add(info.seats);
            this.add(reservation);
        },

        matchWithFriends: function (friends) {
            this.invoke('matchWithFriends', friends);
        }

    });

});