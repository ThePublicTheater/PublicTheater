define([

    "mediaItems/mediaItem/mediaItem",
    "mediaItems/ytMediaItem/templates"

], function (BaseModel, Templates) {

    var YOUTUBE_VIDEO_PREVIEW_IMAGE_FORMAT = "http://i1.ytimg.com/vi/<%= ytVideoId %>/mqdefault.jpg";

    return BaseModel.extend({
        gridTemplate: Templates.gridTemplate,

        playerTemplate: Templates.playerTemplate,
        
        viewModel: function () {
            var imageURL = this.getThumbnail();
            var hasImage = (!!imageURL);
            var audioURL = this.getAudioURL();
            var hasTopSection = hasImage;

            return _.defaults({
                title: this.get("MediaItemTitle"),
                subtitle: this.get("MediaItemSubTitle"),
                mediaType: this.get("MediaType"),
                mediaId: this.get("MediaId"),
                hasTopSection: hasTopSection,
                hasImage: hasImage,
                imageURL: imageURL,
                mediaURL: audioURL,
                description: this.get("MediaDescriptionHTML")
            }, this.viewModelDefaults);
        },

        getAudioURL: function () {
            if (this.isValidURL(this.get("MediaURL")))
                return this.get("MediaURL");
        },

        getYoutubeVideoId: function () {
            return this.constructor.stripYoutubeVideoId(this.getAudioURL());
        },

        getThumbnail: function () {
            if (this.get("CoverURL") == null || this.get("CoverURL") == "")
                return this.constructor.getYoutubeThumbnailFromURL(this.getAudioURL());
            else
                return this.get("CoverURL");
        }        
    }, {
        gridTemplate: Templates.gridTemplate,
        playerTemplate: Templates.playerTemplate,


        YOUTUBE_VIDEO_PREVIEW_IMAGE_FORMAT: "http://i1.ytimg.com/vi/<%= ytVideoId %>/mqdefault.jpg",

        stripYoutubeVideoId: function (url) {
            var ytURLBeforeVideoId = "?v=";

            var fullYoutubeURL = url;

            var endPosition = fullYoutubeURL.indexOf("&");
            if (endPosition == -1)
                endPosition = fullYoutubeURL.length;

            return fullYoutubeURL.substring(fullYoutubeURL.indexOf(ytURLBeforeVideoId) + ytURLBeforeVideoId.length, endPosition);
        },

        getYoutubeThumbnailFromURL: function (url) {
            return _.template(this.YOUTUBE_VIDEO_PREVIEW_IMAGE_FORMAT, { ytVideoId: this.stripYoutubeVideoId(url) });
        }
        
    });

});