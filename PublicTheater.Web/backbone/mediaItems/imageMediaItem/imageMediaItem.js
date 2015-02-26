define([

    "mediaItems/mediaItem/mediaItem",
    "mediaItems/imageMediaItem/templates"

], function (BaseModel, Templates) {

    return BaseModel.extend({
        gridTemplate: Templates.gridTemplate,

        playerTemplate: Templates.playerTemplate,

        initialize: function () {
            this.set("playable", false);
        },

        viewModel: function () {
            var imageURL = this.getImageURL();
            var hasImage = (!!imageURL);
            var hasTopSection = hasImage;

            return _.defaults({
                title: this.get("MediaItemTitle"),
                subtitle: this.get("MediaItemSubTitle"),
                description: this.get("MediaDescriptionHTML"),
                mediaType: this.get("MediaType"),
                mediaId: this.get("MediaId"),
                hasTopSection: hasTopSection,
                hasImage: hasImage,
                imageURL: imageURL,
                relatedMediaItems: _(this.getRelatedMediaItems()).pluck("attributes")
            }, this.viewModelDefaults);
        },

        didRender: function () {

        },

        getRelatedMediaItems: function () {
            return this.collection.context.getRelatedMediaItems(this.get("ParentMediaItemId"));
        }
    });

});