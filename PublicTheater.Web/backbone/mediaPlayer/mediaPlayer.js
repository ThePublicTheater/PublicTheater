define([

    "base/view",
    "mediaItems/mediaItemViewHelper"

], function (BaseView, MediaItemViewHelper) {

    return BaseView.extend({
        initialize: function (opts) {
            this.context = opts.context;
            this.router = this.context.router;

            this.context.router.on("route:playItemById", _.bind(function (itemId) {
                var mediaId = parseInt(itemId);

                this.playMediaItemById(mediaId);
            }, this));
        },
        
        events: {
            "click .closePlayerButton": "hide",
            "click #albumSlider li": "didClickRelatedMediaItem"
        },

        didClickRelatedMediaItem: function (evt) {
            var $target = $(evt.currentTarget);

            this.playMediaItemById($target.find("img").data("id"));
        },        

        template: function (someViewModel) {
            return MediaItemViewHelper.getPlayerTemplate(this.currentMedia)(someViewModel);
        },

        playMediaItemById: function (mediaItemId) {
            this.playMediaItem(this.context.getMediaItemById(mediaItemId));
        },

        playMediaItem: function (mediaItem) {
            if (this.currentMedia === undefined || this.currentMedia.get("MediaId") !== mediaItem.get("MediaId") || !this.isShown()) {
                this.currentMedia = mediaItem;
                this.router.navigate("play/" + mediaItem.get("MediaId"), { replace: true });

                if (this.currentMedia.showsInMediaPlayer) {
                    this.show();
                    this.render();
                } else {
                    this.hide();
                }
            }


        },

        viewModel: function () {
            return this.currentMedia.viewModel();
        },

        didRender: function () {
            if (this.currentMedia.get("MediaType") === "youtube") {
                this.player = this.$el.find("#ytPlayer");

                if (this.player.length > 0) {
                    this.player = this.initYTPlayer();
                }
            } else if (this.currentMedia.get("MediaType") === "image") {
                var albumCarousel = this.$("#albumSlider");

                if (albumCarousel.length) {
                    var mythis = this;
                    t = albumCarousel.bxSlider({
                        maxSlides: 5,
                        slideWidth: 140,
                        slideMargin: 5,
                        nextSelector: '#slider-next',
                        prevSelector: '#slider-prev',
                        nextText: "▶",
                        prevText: "◀",
                        infiniteLoop: false,
                        hideControlOnEnd: true,
                        pager: false,
                        startSlide: ((this.lastAlbumPageIndex != null)
                            && (this.lastAlbumId == this.currentMedia.attributes.ParentMediaItemId)
                            ? this.lastAlbumPageIndex : 0),
                        onSlideBefore: function (slideElement, oldIndex, newIndex) {
                            mythis.lastAlbumPageIndex = newIndex;
                        }
                    });

                    this.lastAlbumId = this.currentMedia.attributes.ParentMediaItemId;

                }
            } else if (this.currentMedia.get("MediaType") === "vimeo") {
                this.player = this.$el.find("#ytPlayer");

                if (this.player.length > 0) {
                    this.player = this.initVimeoPlayer();
                }
            }

            this.animateScrollToTop();
        },

        animateScrollToTop: function () {
            var doc = $("html,body");

            if (doc.attr("scrollTop") === undefined || doc.attr("scrollTop") !== "0")
                $("html,body").animate({ scrollTop: 0 });
        },

        initYTPlayer: function () {
            // YT Init
            var $leftPanel = this.$el.find("#leftPanel");

            var playerWidth = parseInt($leftPanel.css("width"));

            return new YT.Player('ytPlayer', {
                playerVars: {
                    "autoplay": 1
                },

                height: "" + (playerWidth / 16) * 9, // Ratio change?
                width: "" + playerWidth,
                videoId: this.currentMedia.getYoutubeVideoId()
            });
        },
        initVimeoPlayer: function () {
            var $leftPanel = this.$el.find("#leftPanel");

            var playerWidth = parseInt($leftPanel.css("width"));
            this.player.attr("height", "" + (playerWidth / 16) * 9);
            this.player.attr("width","" + playerWidth);

        },
        isShown: function () {
            if (this.$el.attr("display") === "none" || this.$el.attr("display") === undefined) {
                return false;
            }

            return true;
        },
        
        show: function () {
            if (!this.isShown()) {
                this.$el.slideDown();
            }
        },

        hide: function () {
            if (!!this.player)
                this.player.stopVideo();

            if (this.$el.attr("display") !== "none")
                this.$el.slideUp();
        }
    });

});