

define([

    './templates'

], function (Templates) {

    return SYOSPlugin.extend({

        template: Templates.syos,

        pluginName: 'SYOSPopup',

        initPlugin: function () {
            //syosDispatch.on('levelDidOpen', this.levelDidOpen, this);
            this.listenTo(syosDispatch, 'levelDidOpen', this.levelDidOpen);
        },

        checkVenue: function(){

            if (this.syos.rootData.VenueImageUrl.toLowerCase().indexOf("joespub") > - 1){
                this.syos.primaryCart.rowDescriptionOverride = "Table";
                this.syos.primaryPopup.rowDescriptionOverride = "Table";
            }

        },

        customMiniCartLineItem: function (seats, info) {

            return Templates.customMiniCartLineItem({
                seats: updatedSeats,
                info: info,
                tableText: this.table
            });
        },


        levelDidOpen: function () {

            this.checkVenue();

            var CustomSeatPopup = SYOS.Buymode.SeatPopup.extend({
                className: "syos-seat-popup-view-custom",
                getActiveSeatPosition: function () {
                    var coords;
                    if (this.activeSeat) {
                        var circle = this.activeSeat.mapCircle;
                        var circleRadius = this.activeSeat.mapCircle.scaledAttributes().radius;
                        coords = this.syos.canvasMap.getPositionForCircle(circle);
                        coords.x += circleRadius;
                        coords.y += circleRadius;

                        // check if popup is still inside of the syos div

                        var popupSize = {
                            width: this.$el.outerWidth(),
                            height: this.$el.outerHeight()
                        };
                        var syosArea = {
                            width: $('[data-bb=syos]').width(),
                            height: $('[data-bb=syos]').height()
                        };
                        if (coords.x + popupSize.width > syosArea.width) {
                            coords.x = coords.x - popupSize.width - 2 * circleRadius;
                        }
                        if (coords.y + popupSize.height > syosArea.height) {
                            coords.y = coords.y - popupSize.height - 2 * circleRadius;
                        }
                        coords.x = Math.max(0, coords.x);
                        coords.y = Math.max(0, coords.y);

                        coords.x = Math.max(coords.x, syosArea.width - popupSize.width);
                        coords.y = Math.max(coords.y, syosArea.height - popupSize.height);
                    }
                    else {
                        coords = { x: 0, y: 0 };
                    }

                    
                    return coords;
                }
            });

            if (!this.seatPopup) {
            	this.seatPopup = new CustomSeatPopup();
            }
        }

    });

});