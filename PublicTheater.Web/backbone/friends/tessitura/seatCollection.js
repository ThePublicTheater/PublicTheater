

define([

    'friends/base/collection',
    'friends/tessitura/seat'

], function (BaseCollection, Seat) {

    return BaseCollection.extend({

        model: Seat

    });

});