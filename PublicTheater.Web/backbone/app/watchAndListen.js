

define([

    'base/view',
    "mediaPlayer/mediaPlayer",
    "mediaGrid/mediaGrid"

], function (BaseView, MediaPlayer, MediaGrid) {

    return BaseView.extend({
        initialize: function (opts) {
            this.context = opts.context;

            this.mediaPlayer = new MediaPlayer({ el: $("#playerArea",this.$el), context: this.context });
            this.mediaGrid = new MediaGrid({ el: $("#gridArea",this.$el), context: this.context, player: this.mediaPlayer });
        },

        render: function () {
            this.mediaGrid.render();
        }
    });

});