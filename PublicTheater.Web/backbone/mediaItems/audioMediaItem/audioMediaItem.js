define([

    "mediaItems/mediaItem/mediaItem",
    "mediaItems/audioMediaItem/templates"

], function (BaseModel, Templates) {

    return BaseModel.extend({
        gridTemplate: Templates.gridTemplate,

        playerTemplate: Templates.playerTemplate,

        viewModel: function () {
            var hasImage = false;
            var hasTopSection = true;

            return _.defaults({
                title: this.get("MediaItemTitle"),
                subtitle: this.get("MediaItemSubTitle"),
                mediaType: this.get("MediaType"),
                mediaId: this.get("MediaId"),
                hasTopSection: hasTopSection,
                hasImage: hasImage,
                mediaURL: this.getAudioURL()
            }, this.viewModelDefaults);
        },

        getAudioURL: function () {
            if (this.isValidURL(this.get("MediaURL")))
                return this.get("MediaURL");

            return "";
        },

        showsInMediaPlayer: false
    });

});