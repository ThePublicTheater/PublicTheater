define([
    "./syos.limitTickets"
], function (SYOSLimitTickets) {

    return SYOSPlugin.extend({

        pluginName: 'limitTickets',

        initPlugin: function () {
            this.listenTo(syosDispatch, 'levelDidOpen', this.levelDidOpen);
            this.listenTo(this.syos, 'didFinishInit', this.checkCartVisibility);
        },

        checkCartVisibility: function () {
            if (!this.syos.primaryCart.seats.length)
                htmlSyos.primaryCart.hide();
        },

        levelDidOpen: function () {
            syosDispatch.trigger("view:didInit");
            if (this.syos.rootData.VenueImageUrl.toLowerCase().indexOf("joespub") > -1) {
                this.syos.primaryCart.rowDescriptionOverride = "Table";
                this.syos.primarySeatPopup.rowDescriptionOverride = "Table";
            }

            var zonePriceTypes = this.syos.activeLevel.priceTypeCollections;
            this.priceTypes = _.chain(zonePriceTypes)
                .map(function (ptc) { return ptc.models; })
                .flatten().value();

            this.makePredicates();

            syosDispatch.on("buymode:addSeatToCart", this.updatePredicates, this);
            syosDispatch.on("buymode:removeSeatFromCart", this.updatePredicates, this);

            syosDispatch.off('scroll:up');
            syosDispatch.off('scroll:down');
            $(document).off('mousewheel', '.syos-canvas');

            this.updatePredicates();

        },

        makePredicates: function () {
            this.predicates = _.chain(this.priceTypes).map(function (current) {
                function pred(test) {
                    return test.get('PriceTypeId') !== current.get('PriceTypeId');
                }

                return [current.get('PriceTypeId'), pred];
            }).object().value();
        },

        updatePredicates: function () {

            var seats = this.syos.primaryCart.seats.models;
            var popup = this.syos.buymode.seatPopup;

            _.each(this.priceTypes, _.bind(function (priceType) {

                var predicate = this.predicates[priceType.get('PriceTypeId')];
                var maxSeats = priceType.get('MaxSeats');

                var matchingSeats = _.filter(seats, function (seat) {
                    return seat.activePrice.get('PriceTypeId') === priceType.get('PriceTypeId');
                });

                if (matchingSeats.length >= maxSeats) {
                    popup.addPredicate(predicate);
                }

                else if (matchingSeats.length < maxSeats) {
                    popup.removePredicate(predicate);
                }

            }, this));

        }

    });


}
)