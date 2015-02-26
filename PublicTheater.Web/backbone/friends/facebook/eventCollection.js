

define([

    'friends/base/collection',
    'friends/facebook/event'

], function (BaseCollection, Event) {

    return BaseCollection.extend({

        model: Event,

        addWithInfo: function (infoArr) {
            this.add(_(infoArr).map(function (info) {
                var evt = new Event();
                evt.addInfo(info);
                return evt;
            }));
        }

    });

});