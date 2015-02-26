define([

    "mediaItems/mediaItem/mediaItem",
    "mediaItems/vimeoMedia/templates"

], function (BaseModel, Templates) {

    

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
            return "//player.vimeo.com/video/" + this.getVimeoVideoId() + "?portrait=0&byline=0&badge=0&title=0";
        },

        getVimeoVideoId: function () {
            var VIMEO_REGEX = /(http\:\/\/|https\:\/\/)?(www\.)?(vimeo\.com\/)([0-9]+)/;
            var result = VIMEO_REGEX.exec(this.get("MediaURL"));
            if(null != result) {
                return result[4];

            } else {
                return "";
            }
            
        },
        getThumbnail: function () {
            return this.get("CoverURL");
            
        }

        
    }, {
        gridTemplate: Templates.gridTemplate,
        playerTemplate: Templates.playerTemplate,


        

       
        
    });

});