

define([

    'friends/base/model',
    'friends/tessitura/seatCollection',
    'friends/facebook/friend'

], function (BaseModel, SeatCollection, Friend) {

    return BaseModel.extend({

        initialize: function () {
            this.seats = new SeatCollection();
            this.friend = new Friend();
        },

        addInfo: function (info) {
            var atts = _(info).omit('seats', 'friend');
            this.set(atts);
            this.seats.add(info.seats);
        },

        matchWithFriends: function (friends) {
            this.friend = friends.findWhere({ id: this.get('friendId') }) || this.friend;
        },

        toJSON: function () {
            var base = BaseModel.prototype.toJSON.apply(this, arguments);

            _(base).extend({
                seats: this.seats.toJSON(),
                friend: this.friend.toJSON()
            });

            return base;
        }
        

    });

});