

define([

    'friends/base/collection',
    'friends/facebook/friend'

], function (BaseCollection, Friend) {

    return BaseCollection.extend({

        model: Friend

    });

});