define([

    "base/collection",
    "mediaItems/mediaItems"

], function (BaseCollection, MediaItems) {

    return BaseCollection.extend({
        model: function (attrs, options) {
            if (!!attrs.MediaType.match(/youtube/ig)) {
                return new MediaItems.YoutubeMediaItem(attrs, options);
            } else if (!!attrs.MediaType.match(/video/ig)) {
                return new MediaItems.VideoMediaItem(attrs, options);
            } else if (!!attrs.MediaType.match(/audio/ig)) {
                return new MediaItems.AudioMediaItem(attrs, options);
            } else if (!!attrs.MediaType.match(/soundCloud/ig)) {
                return new MediaItems.SoundCloudMediaItem(attrs, options);
            } else if (!!attrs.MediaType.match(/image/ig)) {
                return new MediaItems.ImageMediaItem(attrs, options);
            } else if (!!attrs.MediaType.match(/vimeo/ig)) {
                return new MediaItems.VimeoMediaItem(attrs, options);
            }

            console.log("WARNING -- INCORRECT MEDIA TYPE '" + attrs.MediaType + "' FOUND. CANNOT FIND ITEM CLASS");
            return new MediaItems.BaseMediaItem(attrs, options);
        },

        url: "/",

        initialize: function (models, opts) {
            this.context = opts.context;
        }
    });

});