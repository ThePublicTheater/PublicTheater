define([

    "base/model"

], function (BaseModel, Templates) {

    return BaseModel.extend({
        // So there aren't any weird cases with null/undefined values. 
        defaults: {
            playable: true
        },

        viewModelDefaults: {
            title: "",
            subtitle: "",
            mediaType: "",
            mediaId: -1,
            hasTopSection: false,
            hasImage: false,
            imageURL: "",
            hasAudioPlayer: false
        },
        
        // Media Types Should Override This
        viewModel: function () {
            var imageURL = this.getImageURL();
            var hasImage = (!!imageURL);
            var hasTopSection = hasImage;

            return _.defaults({
                title: this.get("MediaItemTitle"),
                subtitle: this.get("MediaItemSubTitle"),
                mediaType: this.get("MediaType"),
                mediaId: this.get("MediaId"),
                hasTopSection: hasTopSection,
                hasImage: hasImage,
                imageURL: imageURL
            }, this.viewModelDefaults);
        },

        getImageURL: function () {
            if (this.get("MediaType").match(/image/)) {
                if (this.isValidURL(this.get("MediaURL")))
                    return this.get("MediaURL");
            }

            return undefined;
        },

        isValidURL: function (url) {
            if (url === null || url === undefined || url === "")
                return false;

            return true;
        },

        isPlayable: function () {
            return this.get("playable") === true;
        },

        showsInMediaPlayer: true
    });

});