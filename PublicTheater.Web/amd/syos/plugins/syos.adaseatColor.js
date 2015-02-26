define([
    "./syos.adaseatColor"
], function (SYOSAdaseatColor) {

    var adaSeatColor = '#0b07e3';
    var adaCompanionSeatColor = '#1e70f4';

    return SYOSPlugin.extend({
        pluginName: "SYOSAdaseatColor",

        initPlugin: function () {

            syosDispatch.on('map:didFinishLevelInit', this.loadCurrentLevel, this);

            syosDispatch.on('reserveDialogReady', function () {
                
                syosDispatch.on('legend:didRender', _.bind(function () {
                    this.addIcons();
                }, this));

                htmlSyos.display.view.legend.addLegendItemWithColorAndText(adaSeatColor, "Accessible", "syos-accessible-seat");
                htmlSyos.display.view.legend.addLegendItemWithColorAndText(adaCompanionSeatColor, "Accessible/Companion", "syos-accessible-companion-seat");

               
            }, this);

        },

        adaSeatID: [8, 12, 18],
        adaCompanionSeatID: [13],

        loadCurrentLevel: function () {
            this.level = this.syos.activeLevel;
            this.overrideAdaseats();
        },

        overrideAdaseats: function () {

            this.level.seats.each(function (seat) {

                if (this.adaSeatID.indexOf(seat.SeatTypeInfo.ID) !== -1) {
                    seat.mapCircle.set("fillOverride", adaSeatColor);
                    seat.mapCircle.img.src = "";
                }
                if (this.adaCompanionSeatID.indexOf(seat.SeatTypeInfo.ID) !== -1) {
                    seat.mapCircle.set("fillOverride", adaCompanionSeatColor);
                    seat.mapCircle.img.src = "";
                }

            }, this);

        },

        addIcons: function() {
            $('.syos-accessible-seat span').after("<div><i class='fa fa-wheelchair'></i></div>");
            $('.syos-accessible-companion-seat span').after("<div class='icon'>C</div>");
        }
    });
});