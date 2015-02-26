define([

    "base/model",
    "mediaItemCollection/mediaItemCollection",
    "mediaFilterView/mediaFilterView",
    "watchAndListenRouter/watchAndListenRouter"

], function (BaseModel, MediaItemCollection, MediaFilterView, WatchAndListenRouter) {

    return BaseModel.extend({
        defaults: {
            category: "all"
        },

        initialize: function (opts) {
            this.router = new WatchAndListenRouter({ context: this });

            this.filterView = new MediaFilterView({ el: $("div.tabs ul"), context: this });

            this.mediaItems = new MediaItemCollection([], { context: this });

            if (!!opts.rootData) {
                this.rootData = opts.rootData;

                this.loadRootData();
            }

            this.filterView.on("updateFilterEvent", this.throwUpdateFiltersEvent, this);
        },

        throwUpdateFiltersEvent: function () {
            this.trigger("updatedFilters");
        },

        loadRootData: function () {
            _(this.rootData.MediaItems).each(function (mediaItemAttributes) {
                this.mediaItems.add(mediaItemAttributes);
            }, this);
        },

        getMediaItemById: function (id) {
            return this.mediaItems.findWhere({ MediaId: id });
        },

        getMediaItemsByType: function (mediaType) {
            return this.mediaItems.findWhere({ MediaType: mediaType });
        },

        getVisibleMediaItems: function () {
            return this.mediaItems.where({ VisibleOnGrid: true });
        },

        getFilteredMediaItems: function () {
            return _.filter(this.getVisibleMediaItems(), this.filterView.appliesToFilters, this.filterView);
        },

        getRelatedMediaItems: function (parentId) {
            if (parentId === -1)
                return null;

            return this.mediaItems.where({ ParentMediaItemId: parentId });
        }
    });

});