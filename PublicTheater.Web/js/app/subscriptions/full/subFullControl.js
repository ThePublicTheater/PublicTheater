(function (CFS) {

    var FullSubControl = Backbone.View.extend({
        __bbType: "ExchangeControl",

        initialize: function () {
            this.initSelection();

            var syosCount = this.$el.find('[data-bb="syos"]').length;
            console.log("%s SYOS elements found on page", syosCount);

            if (syosCount !== 0) {
                this.initSYOS();
            }
        },

        initSYOS: function () {
            if (_(syosConfig).isUndefined()) throw "CFSError: syosConfig was not defined...";


            if (_(window.htmlSyos).isUndefined()) {
                _(this.$el.find('[data-bb="syos"]')).each(function (syosEl) {
                    window.htmlSyos = new SYOS({
                        config: syosConfig,
                        el: syosEl
                    });

                    var packageId = parseInt(this.$el.find('.HfPackageId').val(), 10);
                    htmlSyos.loadPackage(packageId);

                }, this);
            }

        },

        initSelection: function () {
            var selectionEl = this.$el.find('[data-bb="seatSelection"]')[0];

            if (_(selectionEl).isUndefined()) {
                throw "CFSError: could not find the exchangeSelection element";
            }

            this.selection = new CFS.SeatSelection({ el: selectionEl });
        }

    });




    CFS.FullSubControl = FullSubControl;

})(CFS || {});