define([
    'cfs',
    'CFS/reserveControl/seatSelection'

], function (CFS, SeatSelection) {

    var ExchangeControl = CFS.View.extend({
        __bbType: "ExchangeControl",

        initialize: function () {
            this.initSelection();
            this.initSeatList();
            this.initPerformanceList();

            var syosCount = this.$el.find('[data-bb="syos"]').length;
            console.log("%s SYOS elements found on page", syosCount);

            if (syosCount !== 0) {
                this.initSYOS();
            }
        },

        didChangeSeatSelection: function () {
            if (this.syos) {
                $(this.syos.el).slideUp();
            }
        },

        initSYOS: function () {
            if (_(syosConfig).isUndefined()) throw "CFSError: syosConfig was not defined...";

            _(syosConfig.settings).extend({
                allowZeroPrice: true,
                exchangeMode: true
            });

            _(this.$el.find('[data-bb="syos"]')).each(function (syosEl) {
                this.syos = new SYOS({ config: syosConfig, el: syosEl });
            }, this);

            try {
                var checkedBoxes = this.seatList.getCheckedCheckboxes();
                var exchangePrice = parseFloat(this.seatList.getCheckboxPrice(checkedBoxes[0]));

                this.syos.settings.set('exchangeCount', checkedBoxes.length);
                this.syos.settings.set('exchangePrice', exchangePrice);

                this.syos.loadPerformance(this.performanceList.getSelectedPerformance());
            }
            catch (err) {
                console.warn("\n CFS: Error loading SYOS");
                throw err;
            }
        },

        initSelection: function () {
            var selectionEl = this.el.querySelector('[data-bb="exchangeSelection"]');

            if (_(selectionEl).isUndefined()) {
                throw "CFSError: could not find the exchangeSelection element";
            }

            this.selection = new SeatSelection({ el: selectionEl });
        },

        initPerformanceList: function () {
            var performanceListEl = this.el.querySelector('[data-bb="exchangePerformanceList"]');

            if (_(performanceListEl).isUndefined()) {
                throw "CFSError: could not find the exchangePerformanceList element";
            }

            this.performanceList = new ExchangePerformanceList({ el: performanceListEl });
        },

        initSeatList: function () {
            var listEl = this.getBBElement('exchangeSeatList');
            if (!listEl) throw "CFSError: could not find the exchangeSeatList element";
            
            this.seatList = new ExchangeSeatList({ el: listEl });
            this.seatList.on('didChangeSelection', this.didChangeSeatSelection, this);
        }

    });

    var ExchangeSeatList = CFS.View.extend({

        initialize: function () {
            this.activePriceType = null;
            this.activePrice = null;
            this.checkboxes = this.$el.find('input[type="checkbox"]');
            this.updateActive();
        },

        events: {
            "change input": "didChangeInput"
        },

        didChangeInput: function (evt) {
            this.updateActive();
            this.trigger('didChangeSelection');
        },

        getCheckboxPriceType: function (checkbox) {
            var priceType = $(checkbox).parent('[data-price-type]').attr('data-price-type');
            if (priceType) return priceType;
            else throw "CFSError: could not get price type for checkbox";
        },

        getCheckboxPrice: function (checkbox) {
            var price = $(checkbox).parent('[data-price]').attr('data-price');
            if (price) return price;
            else throw "CFSError: could not get price for checkbox";
        },

        getCheckboxActive: function (checkbox) {
            var priceTypeMatch = (this.activePriceType === this.getCheckboxPriceType(checkbox));
            var priceMatch = (this.activePrice === this.getCheckboxPrice(checkbox));
            return (priceTypeMatch && priceMatch);
        },

        getCheckedCheckboxes: function () {
            return _(this.checkboxes).filter(function (checkbox) {
                if (checkbox.checked) return checkbox;
            });
        },

        updateActive: function () {
            _(this.getCheckedCheckboxes()).each(function (checkbox) {
                this.activePriceType = this.getCheckboxPriceType(checkbox);
                this.activePrice = this.getCheckboxPrice(checkbox);
            }, this);
            _(this.checkboxes).each(this.enableCheckbox);
            this.disableInactiveCheckboxes();
        },

        enableAllCheckboxes: function () {
            _(this.checkboxes).each(this.enableCheckbox);
        },

        enableCheckbox: function (element) {
            $(element).removeAttr('disabled');
        },
        
        disableCheckbox: function (element) {
            $(element).attr('disabled', 'disabled');
        },

        disableInactiveCheckboxes: function () {
            if (this.getCheckedCheckboxes().length !== 0) {
                _(this.checkboxes).each(function (element) {
                    if (!this.getCheckboxActive(element)) this.disableCheckbox(element);
                }, this);
            }
        }

    });

    var ExchangePerformanceList = CFS.View.extend({

        events: {
            "change input": "didChangeInput"
        },

        didChangeInput: function (evt) {
            if (evt.currentTarget.checked) {
                try {
                    if (!_(window.htmlSyos).isUndefined()) htmlSyos.changePerformance(this.getSelectedPerformance());
                }
                catch (err) {
                    console.warn("\n CFS: Error changing performance with SYOS");
                    throw err;
                }
            }
        },

        getSelectedPerformance: function () {
            var selectedElement = this.$el.find('input[type="radio"]:checked')[0];
            var performanceId = parseInt(selectedElement.value, 10);
            return performanceId;
        }

    });



    return ExchangeControl;
});
