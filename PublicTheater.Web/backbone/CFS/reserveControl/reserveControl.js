define([
    "cfs",
    "./calendar"

], function (CFS, CFSCalendar) {

    var ReserveControl = CFS.View.extend({
        openPanel: "syos",

        promoActive: false,
        promoApplies: false,

        performanceNumber: null,

        bestPanel: null,
        syosPanel: null,
        bestBtn: null,
        syosBtn: null,
        promoPos: null,
        promoNeg: null,
        promoSelect: null,
        dateText: null,

        initialize: function () {
            this.loadCustom();
            this.performanceNumber = this.cfs.urlVars.performanceNumber;
            this.bindElements();

            this.calendar = new CFSCalendar.CFSCalendar();
            if (this.$el.find('#syos').length !== 0) {
                this.openPanel = "syos";
            }
            else {
                this.openPanel = "best";
            }
            this.initPanels().determinePromoState().syncView();
            this.getCalendarInfo();

            return this;
        },

        loadCustom: function () {
            if (CFS.cfsConfig.reserveControl.customScript) {
                head.js(CFS.cfsConfig.reserveControl.customScript);
            }
        },

        bindElements: function () {
            this.promoDisplay = this.$el.find('.promoDisplay');
            this.bestPanel = this.$el.find('#best-tab');
            this.syosPanel = this.$el.find('#syos-tab');
            this.bestBtn = this.$el.find('#chooseBest');
            this.syosBtn = this.$el.find('#chooseOwn');
            this.promoPos = this.$el.find('.promoMessage.PromoApplicableMessage');
            this.promoNeg = this.$el.find('.promoMessage.PromoNotApplicableMessage');
            this.promoSelect = this.$el.find('.PromoPerfDropDown');
            this.dateText = this.$el.find('.dateSelected');
            return this;
        },

        events: {
            "click #chooseBest": "openBest",
            "click #chooseOwn": "openSYOS",
            "change .PromoPerfDropDown": "didChangePromoSelect"
        },

        getCalendarInfo: function () {
            this.cfs.service.getCalendarInfo({ perfId: parseInt(this.performanceNumber) }, this.parseCalendarInfo, this.errorCalendarInfo, this);
            return this;
        },

        parseCalendarInfo: function (response) {
            console.log("Calendar info recieved: ", response.d);
            this.calendar.parseInfo(response.d);
            this.syncView();
            return this;
        },

        errorCalendarInfo: function (response) {
            console.warn("Error loading Calendar info!!!", response);
            return this;
        },

        didChangePromoSelect: function (evt) {
            this.changePerformance(evt.currentTarget.value);
            return this;
        },

        changePerformance: function (performanceNumber) {
            console.log("ReserveControl: change performance called");
            this.performanceNumber = performanceNumber;
            $("#HfSelectedPerformanceId").val(performanceNumber);
            if (typeof htmlSyos !== "undefined") {
                htmlSyos.changePerformance(performanceNumber);
            }
            this.determinePromoState().syncView();
            return this;
        },

        initPanels: function () {
            if (this.openPanel === "syos")
                this.openSYOS().closeBest();
            else
                this.openBest().closeSYOS();
            return this;
        },

        openBest: function (evt) {
            evt && evt.preventDefault();
            this.bestBtn.addClass('selected', 400);
            this.bestPanel.slideDown();
            this.closeSYOS();
            return this;
        },

        closeBest: function () {
            this.bestPanel.slideUp();
            this.bestBtn.removeClass('selected', 400);
            return this;
        },

        openSYOS: function (evt) {
            evt && evt.preventDefault();
            this.syosPanel.slideDown();
            this.closeBest();
            this.syosBtn.addClass('selected', 400);
            return this;
        },

        closeSYOS: function () {
            this.syosPanel.slideUp();
            this.syosBtn.removeClass('selected', 400);
            return this;
        },

        determinePromoState: function () {
            if (this.$el.find('.promoDesc').length)
                this.promoActive = true;
            else
                this.promoActive = false;

            if (this.promoSelect.length !== 0) {
                this.promoApplies = _(this.promoSelect.children('option')).any(function (el) {
                    return parseInt($(el).val()) === parseInt(this.performanceNumber);
                }, this);
            }
            else
                this.promoApplies = false;
            return this;
        },

        syncView: function () {
            this.syncPromoMessage();
            this.syncPromoSelect();
            this.syncText();
            this.updateBestAvailable();
            return this;
        },

        syncPromoMessage: function () {
            this.promoNeg.hide();
            this.promoPos.hide();
            if (!this.promoApplies && this.promoActive)
                this.promoNeg.show();
            else if (this.promoActive)
                this.promoPos.show();
            else
                this.promoDisplay.hide();
            return this;
        },

        syncPromoSelect: function () {
            if (this.promoSelect.val() !== this.performanceNumber)
                this.promoSelect.val(this.performanceNumber);
            return this;
        },

        syncText: function () {
            var performanceInfo = this.calendar.performances.get(this.performanceNumber);
            if (typeof performanceInfo !== 'undefined')
                this.dateText.html(performanceInfo.get('LongDateString') + " " + performanceInfo.get('Time'));
            return this;
        },

        updateBestAvailable: function () {
            console.log("ReserveControl: updating best available");
            if (this.openPanel === "best") {
                this.bestPanel.fadeTo(500, 0.2, 'easeInOutCirc', function () {
                    $(this).fadeTo(500, 1, 'easeInOutCirc');
                });
            }
            this.$el.find('#bestAvailablePanelControls input[type="submit"]').trigger('click');
            return this;
        },

        initSYOS: function () {
            var syosLoaded = typeof SYOS !== "undefined";

            try {
                var _initSYOS = _(function() {
                    HtmlSyos = htmlSyos = new SYOS({
                        config: syosConfig,
                        el: $('[data-bb="syos"]')[0]
                    });
                    HtmlSyos.loadPerformance(this.cfs.urlVars["performanceNumber"]);
                }).bind(this);

                if (syosLoaded)
                    _initSYOS();
                else
                    window.syosOnReady = _initSYOS;
            } catch(err) {
                alert("Select Your Own Seats is currently unavailable.\n\n" + err);
                this.disableSYOS();
            }
        },
    
        disableSYOS: function () {
            this.openPanel = "best";
            this.syosPanel.hide();
            this.syosBtn.hide();
            this.openBest();
        },

        test: function () {
            console.group("Reserve Control Test");
            var domElements = {
                bestPanel: this.bestPanel,
                syosPanel: this.syosPanel,
                bestBtn: this.bestBtn,
                syosBtn: this.syosBtn,
                promoPos: this.promoPos,
                promoNeg: this.promoNeg,
                promoSelect: this.promoSelect,
                dateText: this.dateText
            };
            console.group("DOM Test");
            _(domElements).each(this.testElement, this);
            console.groupEnd();
            console.groupEnd();
        },

        testElement: function (elementArray, name) {
            console.group("Element " + name);
            console.assert(elementArray.length !== 0, "No dom element!");
            console.dirxml(elementArray);
            console.groupEnd();
        }

    });

    return ReserveControl;
});

