(function () {

    var SeatSelection = Backbone.View.extend({

        initialize: function () {
            this.hideBestAvailable(0);
        },

        events: {
            "click #chooseBest": "didClickBest",
            "click #chooseOwn": "didClickOwn"
        },

        didClickBest: function (evt) {
            evt.preventDefault();
            evt.returnValue = false;
            this.hideSYOS();
            this.showBestAvailable();
        },

        didClickOwn: function (evt) {
            evt.preventDefault();
            evt.returnValue = false;
            this.showSYOS();
            this.hideBestAvailable();
        },

        hideBestAvailable: function (duration) {
            var duration = duration || 300;
            this.$el.find('#bestAvail').slideUp(duration);
            this.$el.find('#chooseBest').removeClass('on', duration);
        },

        showBestAvailable: function (duration) {
            var duration = duration || 300;
            this.$el.find('#bestAvail').slideDown(duration);
            this.$el.find('#chooseBest').addClass('on', duration);
        },

        hideSYOS: function (duration) {
            var duration = duration || 300;
            this.$el.find('#syosOnPage').slideUp();
            this.$el.find('#chooseOwn').removeClass('on', duration);
        },

        showSYOS: function (duration) {
            var duration = duration || 300;
            this.$el.find('#syosOnPage').slideDown();
            this.$el.find('#chooseOwn').addClass('on', duration);
        }

    });

    CFS.SeatSelection = SeatSelection;

})(CFS);