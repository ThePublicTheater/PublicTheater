define([

    "base/view",
    "mediaItems/mediaItemViewHelper"

], function (BaseView, MediaItemViewHelper) {

    return BaseView.extend({
        events: {
            "click .mediaItem": "didClickMediaItem",
            "click .mediaItem playButton": "didClickMediaItem"
        },

        initialize: function (opts) {
            this.context = opts.context;

            this.player = opts.player;
            this.mediaCollection = this.context.mediaItems;

            this.context.on("updatedFilters", this.render, this);

            this.$columns = $(".gridColumn", this.$el);
        },


        // Special Render Function
        render: function () {

            // Resets
            this.$columns.html("");
            this.$el.find(".noMediaItemsMessage").remove();

            var gridItems = this.context.getFilteredMediaItems();

            if (gridItems.length > 0) {
                // Splits into columns based on how many div.gridColumn's it can find in the element.
                _.each(gridItems, function (mediaItem, index, collection) {
                    var columnIndex = index % this.$columns.length;

                    if (columnIndex === NaN)
                        columnIndex = 0;
                    try{
                        $(this.$columns[columnIndex]).append(MediaItemViewHelper.getGridTemplate(mediaItem)(mediaItem.viewModel()));
                    } catch (e) {
                        console.log("A media item without a w&l model was ignored. If there is a empty slot somewhere, this is what is going on.");
                    }
                }, this);
            } else {
                this.$el.append("<span class=\"noMediaItemsMessage\">No media items</span>");
            }

            this.didRender();
        },

        didRender: function () {
            console.log("AudioJS Tried to Initialize " + $("audio").length + " instances.");
            _.defer(function () { window.audiojs.createAll(); });

            console.log("Sound Cloud Tried to build events for" + $(".soundCloud").length + " instances.");
            var grid = this;
            _.each($(".soundCloud"), function (soundCloudItem) {
                var iframe = $(soundCloudItem).find("iframe");
                SC.Widget(iframe[0].id).bind(SC.Widget.Events.PLAY, function () {
                    grid.didClickMediaItemTarget(soundCloudItem);
                });
            }, this);


        },


        didClickMediaItem: function (evt) {

            evt.preventDefault();
            var $clickTarget = $(evt.currentTarget);
            this.didClickMediaItemTarget($clickTarget);
        },

        didClickMediaItemTarget: function (currentTarget) {

            var mediaId = $(currentTarget).data("id");

            var mediaItem = this.mediaCollection.findWhere({ MediaId: mediaId });

            this.player.playMediaItem(mediaItem);

            this.pausePlayAllAudioPlayers(currentTarget);

        },

        pausePlayAllAudioPlayers: function (currentTarget) {
            var foundPlayers = $(currentTarget).find("audio");

            if (foundPlayers.length) {
                var instance = _(window.audiojs.instances).findWhere({ element: foundPlayers[0] });
            }

            // If they're pressing a play/pause button, AudioJS already handled it.
            if (!$(currentTarget.parentNode).hasClass("play-pause")) {
                _(window.audiojs.instances).each(function (inst) {
                    if (inst.playing && instance != inst) {
                        inst.pause();
                    }
                });
            }

            var soundCloudPlayers = $(currentTarget).find("iframe");

            var currentPlayingId = 0;
            if (soundCloudPlayers.length) {
                currentPlayingId = soundCloudPlayers[0].id;
            }

            _.each($(".soundCloud"), function (soundCloudItem) {
                var iframe = $(soundCloudItem).find("iframe");
                if (currentPlayingId != iframe[0].id) {
                    SC.Widget(iframe[0].id).pause();
                }
            }, this);

        }
    });

});