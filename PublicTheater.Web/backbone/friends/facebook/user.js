

define([

    'friends/base/model',
    'friends/facebook/friendCollection',
    'friends/tessitura/performanceCollection'


], function (BaseModel, FriendCollection, PerformanceCollection) {

    return BaseModel.extend({

        initialize: function () {
            this.friends = new FriendCollection();
            this.performances = new PerformanceCollection();
        },

        hasLoaded: function () {
            return (typeof this.get('id') !== 'undefined');
        },

        toJSON: function () {
            var json = BaseModel.prototype.toJSON.apply(this, arguments);
            _(json).extend({
                userId: json.id,
                friendIds: this.friends.pluck('id')
            });
            return json;
        },

        getFriendIds: function () {
            return this.friends.pluck('id');
        }

    });

});