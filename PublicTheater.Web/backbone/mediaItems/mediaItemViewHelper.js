define([

    "base/view",
    "mediaItems/mediaItems"

], function (BaseView, MediaItems) {

    return BaseView.extend({}, {
        getMediaItemType: function (model) {
            if (model instanceof MediaItems.YoutubeMediaItem)
                return MediaItems.YoutubeMediaItem;
            else if (model instanceof MediaItems.AudioMediaItem)
                return MediaItems.AudioMediaItem;
            else if (model instanceof MediaItems.SoundCloudMediaItem)
                return MediaItems.SoundCloudMediaItem;
            else if (model instanceof MediaItems.ImageMediaItem)
                return MediaItems.ImageMediaItem;
            else if (model instanceof MediaItems.VimeoMediaItem)
                return MediaItems.VimeoMediaItem;
            console.log("WARNING -- INCORRECT MEDIA TYPE '" + model + "' FOUND. CANNOT FIND ITEM CLASS");
            return MediaItems.BaseMediaItem;
        },

        getGridTemplate: function (model) {
            return model.gridTemplate;
        },

        getPlayerTemplate: function (model) {
            return model.playerTemplate;
        },

        
        stripYoutubeVideoId: function (url) {
            return MediaItems.YoutubeMediaItem.stripYoutubeVideoId(url);
        }
    });

});