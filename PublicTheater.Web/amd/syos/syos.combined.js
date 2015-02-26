/* combined: Mon Feb 10 2014 13:41:07 GMT-0600 (Central Standard Time) */

var SYOS = {};

(function () {

    SYOSView = Backbone.View.extend({

        __bbType: "SYOSView",

        initialize: function () {
            this.on('didRender', this.didRender, this);
        },

        addSubview: function (subview) {
            this.el.appendChild(subview.el);
        },

        removeSubview: function (subview) {
            this.el.removeChild(subview.el);
        },

        didRender: function () { },

        hide: function () {
            this.$el.hide();
        },

        show: function () {
            this.$el.show();
        },

        viewModel: function () {
            return this;
        },

        render: function () {
            if (this.el) {
                this.el.innerHTML = this.template(this.viewModel());
            }
            else {
                throw "Could not find element for " + this._bbType;
            }

            if (syosLogRender) {
                console.groupCollapsed(this.__bbType + " rendered");
                var test = document.createElement('div');
                var htmlString = this.template(this.viewModel());
                test.innerHTML = htmlString;
                console.log("dom:", test);
                console.log("string:", htmlString);
                console.groupEnd();
            }

            this.trigger('didRender', this);
            return this;
        },

        stopProp: function (evt) {
            evt.stopPropagation();
        },

        getBBElement: function (name) {
            return this.el.querySelector('[data-bb="' + name + '"]');
        },

        getBBElementAll: function (name) {
            return this.el.querySelectorAll('[data-bb="' + name + '"]');
        }

    });

    SYOSModel = Backbone.Model.extend();

    SYOSCollection = Backbone.Collection.extend({
        logName: "defaultCollectionName",

        logEvent: function (eventName) {
            console.groupEnd();
            return this;
        }
    });

}());


SYOS.Core = (function () {

    // #region App

    SYOSCore = Backbone.View.extend({

        version: "1.6.3",

        plugins: {},

        initialize: function () {
            this.config = this.options.config;
            console.groupCollapsed('SYOS init');
            console.log('config', this.config);
            console.log('settings', this.config.settings);
            console.groupEnd();

            this.styleSheet = document.styleSheets[0];

            SYOSView.prototype.syos = this;
            SYOSModel.prototype.syos = this;
            SYOSCollection.prototype.syos = this;

            this.readDOMFields();

            this.services = new SYOS.Data.Services();
            this.settings = new SYOSSettings();
            this.performance = new SYOSPerformance();
            this.keyboard = new SYOS.Keyboard.Controller({ el: this.el });

            this.performance.on("change:id", this.loadRootData, this);

            this.statusView = new SYOSStatusView();
            this.addSubview(this.statusView);

            return this;
        },

        loadRootData: function () {
            var ajaxData = JSON.stringify({
                itemId: this.performance.id,
                syosType: this.syosType
            });
            this.services.getRootConfigData({
                data: ajaxData,
                success: _(this.parseRootData).bind(this),
                error: _(SYOS.Errors.rootDataAjax).bind(this)
            });
        },

        parseRootData: function (response) {
            this.rootData = response.d;
            this.checkRootData();

            console.groupCollapsed("Recieved Root Data")
            console.log("response: %o", response.d);
            console.groupEnd();

            this.settings.set('RootConfigId', this.rootData.DataSourceId);

            this.performance.venue.set('name', this.rootData.VenueName || "");

            this.initTest();
            this.levels = new SYOSLevelCollection();

            this.utility = new SYOSUtility();

            syosDispatch.on("levelDidOpen", this.levelDidOpen, this);

            this.start();
            this.registerPlugins();
            syosDispatch.trigger('core:didFinishInit', this);
            syosDispatch.trigger('didFinishInit', this);

            this.settings.set(this.config.settings);

            this.initDisplay();
            this.initMap();
            this.initPurchasing();

            this.singleLevelCheck();

            this.trigger('didFinishInit');
        },

        checkRootData: function () {
            if (this.config.settings.useLocalHouseImage) {
                var houseImages = this.config.settings.useLocalHouseImage;
                _(this.rootData.LevelViews).each(function (levelView, index) {
                    levelView.ImageUrl = houseImages[index];
                }, this);
            }
            if (this.rootData.LevelViews.length === 0) {
                console.warn("RootData LevelView count is 0");
            }
            if (this.rootData.SeatTypeInfos.length === 0) {
                console.warn("RootData SeatTypeInfos count is 0");
            }
        },

        initMap: _(function () {
            this.map = this.canvasMap = new SYOS.Canvas.Map();
            syosDispatch.trigger("map:didFinishInit");
        }).once(),

        initDisplay: _(function () {
            this.display = new SYOSDisplay();
        }).once(),

        initPurchasing: _(function () {
            this.reserveDialog = new SYOS.Buymode.ReserveDialog();
            this.el.appendChild(this.reserveDialog.el);

            var exchangeMode = this.settings.get('exchangeMode');
            var externalMode = this.settings.get('purchaseExternalMode');

            if (externalMode) {
                return;
            }
            if (exchangeMode) {
                this.exchangeMode = new SYOS.Exchange.Purchasing({ el: this.el });
                this.primaryCart = this.exchangeMode.cart;
            }
            else {
                this.buymode = new SYOS.Buymode.Controller({ el: this.el });
                this.primaryCart = this.buymode.cart;
            }

        }).once(),

        singleLevelCheck: function () {
            if (this.levels.length === 1) {
                if (this.display.view.levelSelection) this.display.view.levelSelection.dismiss();
                if (this.display.view.levelSwitch) this.display.view.levelSwitch.deactivate();
                this.display.shouldOpenLevel(this.levels.first());
            }
        },

        openLevelAtIndex: function (levelIndex) {
            var indexMax = this.levels.length,
                level = this.levels.at(levelIndex);

            if (levelIndex < 0 || levelIndex > indexMax) {
                throw new SYOS.Error('Invalid level index. Choose an index from 0 to ' + indexMax);
            } else {
                this.display.shouldOpenLevel(level);
            }
        },

        loadPerformance: function (performanceId) {
            this.performance.set("id", performanceId);
        },

        loadPackage: function (packageId) {
            // todo: add package object
            this.performance.set("id", packageId);
        },

        restart: function () {
            this.performance.trigger("change:id");
            return this;
        },

        levelDidOpen: function (level) {
            this.activeLevel = level;
            this.trigger("didChangeActiveLevel", level);
            return this;
        },

        changePerformance: _(function (performanceNumber) {
            console.group("SYOS: Change Performance");
            this.clearActiveLevel();


            if (performanceNumber !== this.performance.id) {
                console.log("New Performance Number:", performanceNumber);

                syosDispatch.trigger("showLoadingMessage", { text: "Changing Performance..." });

                syosDispatch.trigger("buymode:clearCart");

                this.levels.each(function (level) {
                    level.Seats.reset();
                });

                this.performance.set("id", performanceNumber);

                if (this.activeLevel)
                    this.activeLevel.showLevel();
                else
                    setTimeout(function () { syosDispatch.trigger('hideLoadingMessage') }, 1000);

            }
            else {
                SYOS.Errors.alreadyOnPerformance(this.performance);
            }
            console.groupEnd();
            return this;
        }).throttle(500),

        start: function () {
            _.each(this.rootData.pageDictionary, function (LevelData) {
                var NewLevel = new SYOSLevel(LevelData);
                this.levels.add(NewLevel);
            }, this);
            return this;
        },

        clearActiveLevel: function () {
            if (this.activeLevel) {
                this.activeLevel.seats.reset();
            }
            this.canvasMap.seatMap.circles.reset();
            this.canvasMap.seatMap.renderAll();
        },

        addSubview: SYOSView.prototype.addSubview,

        removeSubview: SYOSView.prototype.removeSubview,

        registerPlugins: function () {
            var plugins = this.options.config.initOptions.plugins;
            if (plugins) {
                _.each(plugins, function (pluginKey) {
                    var PluginModel = window[pluginKey];

                    if (_(PluginModel).isUndefined()) {
                        throw "Plugin " + pluginKey + " Model was not defined...";
                    }
                    else {
                        var pluginName = PluginModel.prototype.pluginName;
                        this.plugins[pluginName] = new PluginModel();
                    }

                }, this);
            }
            syosDispatch.trigger('RegisterPluginComplete');
            return this;
        },

        initTest: function () {
            this.testKeydownCount = 0;
            $(window).on('keydown', _(this.testKeydownHandler).bind(this));
        },

        readDOMFields: function () {
            this.syosType = this.$el.find('.HfSyosType').val();
        },

        testKeydownHandler: function (evt) {
            if (evt.keyCode === 84 && evt.shiftKey) {
                this.testKeydownCount++;
                if (this.testKeydownCount === 3) {
                    this.openTest();
                }
            }
        },

        openTest: function () {
            var testDir = "http://cdn.adagetechnologies.com/syos/shared/160/test/";
            if (location.href.match(/localhost/)) {
                testDir = "http://nmartin.adage.adagetechnologies.com:4646/160/test/";
            }
            var testScript = document.createElement('script');
            testScript.setAttribute('src', testDir + 'run.js');
            document.head.appendChild(testScript);
        }

    });

    SYOSSettings = SYOSModel.extend({

        defaults: {
            CurrentZoom: 0.3,
            AutoZoom: true,
            allowZeroPrice: false,
            defaultModalHeight: 50,
            defaultModalWidth: 50,
            defaultCanvasHeight: 600,
            exchangeMode: false,
            embeddedZoomModifier: 2,
            InFullscreen: false,
            IE8: false,
            showFullscreen: false,
            levelSelectDropdown: true,
            levelSelectMap: true,
            levelShowSeatNumbers: false,
            toolsMenu: true,
            treeMode: false,
            MapX: 0,
            MapY: 0,
            mobileViewportHeight: 0.6,
            mobilePanFactor: 0.8,
            PerformanceNumber: null,
            maxCartLength: 10,
            seatRadius: 7,
            PanIncrement: 20,
            ZoomIncrement: 0.2,
            initialZoomAdjustment: 0,
            initialXOffset: 0,
            initialYOffset: 0,
            reserveConfirm: true,
            simplifiedMap: false,
            showSeatNumbers: false,
            seatviewWidth: 80,
            seatviewHeight: 80,
            seatviewIconHeight: 45,
            seatviewIconWidth: 45,
            seatTextLimit: 0.8,
            levelImageWidth: 950,
            levelImageHeight: 610,
            levelImagePadding: 20,
            seatViewIconPath: "/syos/img/iconPhoto.png",
            touchZoomFactor: 0.2,

            useAutoSizing: true,
            circleTextBaseline: "bottom",
            circleTextFont: "Arial",
            circleTextOffset: 1,

            offsetXMin: -Infinity,
            offsetYMin: -Infinity,
            offsetXMax: Infinity,
            offsetYMax: Infinity,

            boundaryOffsetX: 100,
            boundaryOffsetY: 100,

            seatTypeWarningModalCancelText: "Cancel",
            seatTypeWarningModalConfirmText: "Ok",
            seatTypeWarningRemoveSeatsOnCancel: false,

            zoomMultiplier: 2,
            showGAZone: true
        },

        initialize: function () {
            syosDispatch.on("didChangeFullscreen", this.didChangeFullscreen);
            this.syos.on('didFinishInit', this.syosReady, this);
        },

        syosReady: function () {
            this.set({ Colors: this.syos.rootData.SeatColors });
        },

        didChangeFullscreen: function (InFullscreen) {
            this.set("InFullscreen", InFullscreen);
            return this;
        },

        adjustZoom: function (amt) {
            // amt should be an integer, meaning how many levels the map should zoom in or out

            var currentZoom = this.get('CurrentZoom'),
                zoomMax = this.get('ZoomMax'),
                zoomMin = this.get('ZoomMin');

            // multiplier is a positive integer
            var newZoom = currentZoom * Math.pow(this.get('zoomMultiplier'), amt);
            //var newZoom = currentZoom + amt;

            if (newZoom > zoomMax) {
                newZoom = zoomMax;
            }
            else if (newZoom < zoomMin) {
                newZoom = zoomMin;
            }

            this.set('CurrentZoom', newZoom);
        },

        goToZoom: function (newZoom) {
            if (newZoom > this.get('ZoomMax')) {
                newZoom = this.get('ZoomMax');
            }
            else if (newZoom < this.get('ZoomMin')) {
                newZoom = this.get('ZoomMin');
            }
            this.set('CurrentZoom', newZoom);
        }

    });

    SYOSUtility = SYOSView.extend({

        cleanObject: function (Dirty) {
            var Clean = {};

            for (var key in Dirty) {
                var DirtyVal = Dirty[key];
                var CleanVal = DirtyVal;

                if (!isNaN(parseInt(DirtyVal, 10)))
                    CleanVal = parseInt(DirtyVal, 10);

                Clean[key] = CleanVal;
            }

            return Clean;
        },

        getUrlVars: function () {
            var vars = [], hash;
            var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).replace("#", "").split('&');
            for (var i = 0; i < hashes.length; i++) {
                hash = hashes[i].split('=');
                vars.push(hash[0]);
                vars[hash[0]] = hash[1];
            }
            return vars;
        },

        getUrlVar: function (name) {
            var rVal = this.getUrlVars()[name];
            if (typeof rVal != 'undefined')
                return rVal;
            else
                return '';
        },

        formatPrice: function (priceInt) {
            priceInt = isNaN(priceInt) || priceInt === '' || priceInt === null ? 0.00 : priceInt;
            return parseFloat(priceInt).toFixed(2);
        },

        // int: 1  float: 1.2000 (digit = 4)
        formatPrice2: function (price, digit) {
            price = isNaN(price) || price === '' || price === null ? 0 : price;
            var digit = price == parseInt(price, 10) ? 0 : Math.min(20, digit);
            return price.toFixed(digit);
        },

        isInArray: function (arry, intVal) {
            var currentVal = intVal;
            if ($.is_int(currentVal) === false)
                currentVal = parseInt(currentVal, 10);

            if (arry.indexOf)
                return arry.indexOf(currentVal) > -1;

            for (arryIdx = 0; arryIdx < arry.length; arryIdx++) {
                if (arry[arryIdx] == currentVal)
                    return true;
            }
            return false;
        },

        is_int: function (input) {
            return typeof (input) == 'number' && parseInt(input, 10) == input;
        },

        getCurrentZoom: function () {
            var currentZoom = this.syos.settings.get('CurrentZoom');
            return currentZoom;
        },

        getZoomIncrement: function () {
            return this.get('ZoomIncrement');
        },

        getDistance: function (x1, x2, y1, y2) {
            var xs = 0;
            var ys = 0;
            xs = x2 - x1;
            xs = xs * xs;
            ys = y2 - y1;
            ys = ys * ys;
            return Math.sqrt(xs + ys);
        },

        getMidpoint: function (x1, x2, y1, y2) {
            return {
                x: (x1 + x2) / 2,
                y: (y1 + y2) / 2
            };
        },
        // Huiyuan
        // canvas -> div
        scaleCoordinates: function (x, y) {
            var currentZoom = this.getCurrentZoom();
            if (!(x instanceof Array)) {
                x = [x];
            }
            if (!(y instanceof Array)) {
                y = [y];
            }
            // x,y are arrays for polygons
            x = _(x).map(function (ele) { return ele * currentZoom; }, this);
            y = _(y).map(function (ele) { return ele * currentZoom; }, this);

            var coord = {
                x: (x.length > 1 ? x : x[0]),
                y: (y.length > 1 ? y : y[0])
            };

            return coord;
        },

        // canvas -> div
        scaleScalar: function (x) {
            var currentZoom = this.getCurrentZoom();
            return x * currentZoom;
        },

        // div -> canvas
        descendCoordinates: function (x, y) {
            var currentZoom = this.getCurrentZoom();
            var coord = {
                x: x / currentZoom,
                y: y / currentZoom
            };

            return coord;
        },

        getZoomPointCoordinate: function (initialScale, targetScale, mouse, offset) {
            return (offset - mouse) * targetScale / initialScale + mouse;
        },

        getTouchAverageForKey: function (touches, key) {
            var total = _(touches).reduce(function (m, t) { return m + t[key]; }, 0);
            return total / touches.length;
        },

        getDOMVar: function (selector) {
            return $(selector).val();
        },

        getCanvasHeight: function () {
            var canvasHeight;
            var size = {
                w: $(window).width(),
                h: $(window).height()
            };

            if (size.w < 640) {
                canvasHeight = size.h * this.syos.settings.get('mobileViewportHeight');
            }
            else {
                canvasHeight = this.syos.settings.get('defaultCanvasHeight');
            }

            return canvasHeight;
        },

        isMobileWidth: function () {
            var windowWidth = $(window).width();
            return (windowWidth < 641);
        },

        isTouch: function () {
            if (!!location.href.match(/preventtouch/ig)) return false;
            return "ontouchstart" in document.documentElement;
        },
        // TODO: TEST FUNCTION
        getCenterPointForCircle: function (opts) {
            var startPoint = opts.startPoint;
            var endPoint = opts.endPoint;

            var nextPoint = opts.nextPoint;
            var radius = opts.radius;
            var reverse = opts.reverse;
            var curveDirection = 1;

            if (radius < 0) {
                // curve in
                curveDirection = 0;
            }
            var up = this.getSquare(startPoint.x) + this.getSquare(startPoint.y)
                   - this.getSquare(endPoint.x) - this.getSquare(endPoint.y);
            var down = 2 * (startPoint.x - endPoint.x);

            // p, the constant part of centerPoint.y 's representation of centerPoint.x
            var p = up / down;

            // t, the factor part of centerPoint.y 's representation of centerPoint.x
            var t = (startPoint.y - endPoint.y) / (startPoint.x - endPoint.x);

            // d, modify startPoint.x to d = startPoint.x - p for easier calculation.
            var d = startPoint.x - p;

            // calculate delta = b*b - 4a*c
            var b = 2 * (t * d - startPoint.y);
            var a = this.getSquare(t) + 1;
            var c = this.getSquare(d) + this.getSquare(startPoint.y) - this.getSquare(radius);

            // there will be 2 centerPoints
            var centerPointPair = [{ x: '', y: '' }, { x: '', y: '' }];
            delta = Math.sqrt(this.getSquare(b) - 4 * a * c);

            centerPointPair[0].y = (-1 * b + delta) / (2 * a);
            centerPointPair[0].x = p - t * centerPointPair[0].y;

            centerPointPair[1].y = (-1 * b - delta) / (2 * a);
            centerPointPair[1].x = p - t * centerPointPair[1].y;

            // sin* = |vector1| * |vector2| * sin(angle of vector1 and vector2). 
            var sin0 = this.getSinValueForPoints(startPoint, endPoint, centerPointPair[0]);
            var sin1 = this.getSinValueForPoints(startPoint, endPoint, centerPointPair[1]);

            if ((sin0 > sin1) == curveDirection) {
                return centerPointPair[0];
            }
            else {
                return centerPointPair[1];
            }
        },

        getSquare: function (x) {
            // more efficient than Math.pow(x, 2)
            return x * x;
        },

        // Calculate the cross-product of vec1(startPoint - centerPoint) and vec2(endPoint - centerPoint)
        getSinValueForPoints: function (startPoint, endPoint, centerPoint) {
            var simpleStartPoint = {
                x: startPoint.x - centerPoint.x,
                y: startPoint.y - centerPoint.y
            }
            var simpleEndPoint = {
                x: endPoint.x - centerPoint.x,
                y: endPoint.y - centerPoint.y
            }
            var sin = simpleStartPoint.x * simpleEndPoint.y - simpleStartPoint.y * simpleEndPoint.x;

            return sin;
        },

        getDistFromTwoPoints: function (centerPoint, nextPoint) {
            return Math.sqrt(this.getSquare(centerPoint.x - nextPoint.x) + this.getSquare(centerPoint.y - nextPoint.y));
        }
        /*
        getControlPoint: function (opts) {
            var centerPoint = opts.centerPoint;
            var startPoint = opts.startPoint;
            var endPoint = opts.endPoint;
            var nextPoint = opts.nextPoint;
            var radius = opts.radius;
            var reverse = opts.reverse;

            var curveDirection = 1;
            if (radius < 0 && !reverse) {
                // curve in
                curveDirection = 0;
            }
            var middlePoint = {};
            middlePoint.x = (startPoint.x + endPoint.x)/2;
            middlePoint.y = (startPoint.y + endPoint.y)/2;

            var distCenterToMiddle = this.getDistFromTwoPoints(centerPoint, middlePoint);
            var distControlToMiddle = this.getSquare(radius) / distCenterToMiddle - distCenterToMiddle;

            var up = this.getSquare(startPoint.x) + this.getSquare(startPoint.y)
                   - this.getSquare(endPoint.x) - this.getSquare(endPoint.y);
            var down = 2 * (startPoint.x - endPoint.x);
            // p, the constant part of centerPoint.y 's representation of centerPoint.x
            var p = up / down;

            // t, the factor part of centerPoint.y 's representation of centerPoint.x
            var t = (startPoint.y - endPoint.y) / (startPoint.x - endPoint.x);

            // d, modify startPoint.x to d = startPoint.x - p for easier calculation.
            var d = middlePoint.x - p;

            // calculate delta = b*b - 4a*c
            var b = 2 * (t * d - middlePoint.y);
            var a = this.getSquare(t) + 1;
            var c = this.getSquare(d) + this.getSquare(middlePoint.y) - this.getSquare(distControlToMiddle);

            // there will be 2 centerPoints
            var controlPointPair = [{ x: '', y: '' }, { x: '', y: '' }];
            delta = Math.sqrt(this.getSquare(b) - 4 * a * c);

            controlPointPair[0].y = (-1 * b + delta) / (2 * a);
            controlPointPair[0].x = p - t * controlPointPair[0].y;

            controlPointPair[1].y = (-1 * b - delta) / (2 * a);
            controlPointPair[1].x = p - t * controlPointPair[1].y;


            var dist0 = this.getDistFromTwoPoints(controlPointPair[0], nextPoint);
            var dist1 = this.getDistFromTwoPoints(controlPointPair[1], nextPoint);

            if ((dist0 > dist1) == curveDirection) {
                return controlPointPair[0];
            }
            else {
                return controlPointPair[1];
            }
        }*/
    });

    // #endregion

    // #region Tessitura

    SYOSPricedItem = SYOSModel.extend({

        initialize: function () {
            this.hasPricing = false;
        },

        maxPrice: function () {
            console.error("method not overwritten!");
            return Infinity;
        },

        minPrice: function () {
            console.error("method not overwritten!");
            return -Infinity;
        },

        priceRangeString: function () {
            var rangeString;
            var maxPrice = this.maxPrice();
            var minPrice = this.minPrice();

            if (this.hasPricing === false) {
                rangeString = "Loading...";
            }
            else if (!isFinite(maxPrice) && !isFinite(minPrice)) {
                rangeString = "";
            }
            else if (maxPrice === 0 && minPrice === 0) {
                if (this.syos.settings.get('allowZeroPrice')) {
                    rangeString = "$0";
                }
                else {
                    rangeString = this.syos.settings.get('emptyPriceString');
                }
            }
            else if (maxPrice === minPrice) {
                rangeString = "$" + this.syos.utility.formatPrice(maxPrice);
            }
            else {
                rangeString = "$" + this.syos.utility.formatPrice(minPrice) + " - $" + this.syos.utility.formatPrice(maxPrice);
            }

            return rangeString;
        }

    });

    SYOSLevel = SYOSPricedItem.extend({
        zonePrices: [],

        idAttribute: "LevelNumber",

        defaults: {
            LevelName: "unknown"
        },

        initialize: function () {
            SYOSPricedItem.prototype.initialize.apply(this, arguments);

            this.SeatTypeInfos = [];
            this.PriceTypeCollections = this.priceTypeCollections = {};
            this.Seats = this.seats = new SYOSSeatCollection();
            this.GAZones = this.gaZones = new SYOSGAZoneCollection();
            this.seatViews = this.SeatViews = new SYOSSeatViewCollection();
            this.seatTypes = new SYOSSeatTypeCollection();
            this.seatColors = new SYOSSeatColorCollection();

            this.seatViews.add(this.get("SeatViews"));

            this.set({ LevelName: this.get("LevelHeading") });
            this.getLevelPricing();
        },

        getLevelPricing: function () {
            var ajaxData = {
                performanceNumber: this.syos.performance.id,
                zoneIds: this.get('ZoneIDs'),
                configNumber: this.get("DataSourceId"),
                syosType: this.syos.syosType
            };

            $.ajax({
                url: '/SYOSService/SYOSService.asmx/GetZonesPrices',
                contentType: "application/json",
                responseType: "application/json",
                type: "post",
                data: JSON.stringify(ajaxData),
                success: _(this.parseLevelPricing).bind(this, this.get('LevelName'), ajaxData),
                error: _(SYOS.Errors.zonePriceAjax).bind(this)
            });
            return this;
        },

        parseLevelPricing: function (levelName, request, response) {
            console.groupCollapsed("%s Pricing Request Summary", levelName);
            console.log('Request: %o', request);
            console.log('Response: %o', response.d);
            console.groupEnd();

            this.hasPricing = true;
            this.set('seatCountText', response.d.seatCountDisplayText);
            this.zonePrices = _(response.d.zonePrices).compact();
            this.trigger('change');
            this.trigger('didChangeLevelPricing');

            syosDispatch.trigger('core:didChangeLevelPricing');

            return this;
        },

        errorLevelPricing: function (response) {
            console.group('Error Level Pricing');
            console.log(response);
            console.groupEnd();
            return this;
        },

        maxPrice: function () {
            var maxPrice;
            if (this.zonePrices.length === 0)
                maxPrice = 0;
            else
                maxPrice = Math.max.apply(null, this.zonePrices);
            return maxPrice;
        },

        minPrice: function () {
            var minPrice;
            if (this.zonePrices.length === 0)
                minPrice = 0;
            else
                minPrice = Math.min.apply(null, this.zonePrices);
            return minPrice;
        },

        getPriceTypeCollectionForZone: function (ZoneId) {
            return this.PriceTypeCollections[ZoneId.toString()];
        },

        addLevelMeta: function (LevelInfo) {
            var _this = this;
            var CurrentPriority = 0;

            _.each(LevelInfo.SeatTypes, function (seatType) {
                $.extend(seatType, { Priority: CurrentPriority });
                this.SeatTypeInfos[seatType.ID] = seatType;
                CurrentPriority++;
            }, this);

            _.each(LevelInfo.AllSeatPricing, function (seatPricing) {
                var ZoneId = seatPricing.Key;
                var PriceCollection = new SYOSPriceCollection();
                _.each(seatPricing.Value, function (seatPricingAttributes) {
                    var Price = new SYOSPrice(seatPricingAttributes);
                    PriceCollection.add(Price);
                });
                _this.PriceTypeCollections[ZoneId.toString()] = PriceCollection;
            });

            _.each(LevelInfo.SeatColors, function (seatColor, seatColorKey) {
                _(seatColor).extend({ key: seatColorKey });
                this.seatColors.add(seatColor);
            }, this);

        },

        addBeforeOpen: function (fn) {
            this.beforeOpen = fn;
        },

        getBeforeOpen: function () {
            if (this.beforeOpen) {
                return this.beforeOpen();
            } else {
                //var def = Q.defer();
                //def.resolve();
                //return def.promise;
            }
        },

        showLevel: function () {
            var beforeOpen = this.getBeforeOpen();

            //beforeOpen.then(_(function () {
            //    if (this.get("HasSeats")) {
            //        console.log("Level Seats cached");
            //        syosDispatch.trigger("levelDidOpen", this);
            //    }
            //    else {
            //        this.getSeats();
            //    }
            //}).bind(this));

            if (this.get("HasSeats")) {
                console.log("Level Seats cached");
                syosDispatch.trigger("levelDidOpen", this);
            }
            else {
                this.getSeats();
            }
            return this;
        },

        getSeats: function () {
            var _this = this;
            console.group("Level -> seats webservice call");

            var ajaxData = {
                rootConfigNumber: this.syos.settings.get("RootConfigId"),
                itemNumber: this.syos.performance.id,
                configNumber: this.get("DataSourceId"),
                showAll: false,
                syosType: this.syos.syosType
            };

            $.ajax({
                url: "/SYOSService/SYOSService.asmx/GetLevelInformation",
                contentType: "application/json",
                responseType: "application/json",
                type: "post",
                data: JSON.stringify(ajaxData),
                success: _(this.parseSeats).bind(this),
                error: _(this.errorSeats).bind(this)
            });

            return _this;
        },

        parseSeats: function (response) {
            console.groupCollapsed("Recieved Level Data");
            console.log("Available: %o", response.d.SeatLists[0].length);
            console.log("Unavailable: %o", response.d.SeatLists[1].length);
            console.log("Object: %o", response.d);
            console.groupEnd();

            this.addLevelMeta(response.d);
            this.seatTypes.add(response.d.SeatTypes);
            this.loadSeatLists(response.d.SeatLists);
            this.loadGA(response.d.GAZones);

            if (this.syos.settings.get('exchangeMode')) {
                this.syos.exchangeMode.loadLevelData(response.d);
            }

            syosDispatch.trigger("levelDidOpen", this);
            console.groupEnd();
        },

        getBackgroundImageUrl: function () {
            var url = "/SYOSService/SyosBackgroundImage.ashx?" +
                "performanceNumber=" + this.syos.performance.id +
                "&syosType=" + this.syos.syosType +
                "&seatRadius=" + this.syos.settings.get("seatRadius") +
                "&pageid=" + this.get("DataSourceId") +
                "&hideSeatOutlines=true";
            return url;
        },

        errorSeats: function (response) {
            throw "Error with the get Level Information service call";
        },

        loadSeatLists: function (SeatLists) {
            var _this = this;
            this.trigger('willLoadSeatLists', this);

            _.each(SeatLists[0], function (SeatData) {
                _.extend(SeatData, {
                    IsAvailable: true,
                    LevelName: _this.get("LevelName")
                });

                var NewSeat = new SYOSSeat(SeatData);
                NewSeat.seatType = this.seatTypes.get(NewSeat.get('SeatType'));

                NewSeat.PriceCollection = _this.getPriceTypeCollectionForZone(SeatData.ZoneId);
                if (!NewSeat.PriceCollection) {
                    console.warn("%c WARNING: Unable to assign prices to Seat in Zone %s", "color:red;font-weight:bold;", SeatData.ZoneId);
                    NewSeat.PriceCollection = new SYOSPriceCollection();
                }

                NewSeat.SeatTypeInfo = _this.SeatTypeInfos[NewSeat.get("SeatType").toString()];

                _this.Seats.add(NewSeat);
            }, this);

            _.each(SeatLists[1], function (SeatData) {
                _.extend(SeatData, {
                    LevelName: _this.get("LevelName")
                });
                var NewSeat = new SYOSSeat(SeatData);
                NewSeat.PriceCollection = _this.getPriceTypeCollectionForZone(SeatData.ZoneId);

                NewSeat.SeatTypeInfo = _this.SeatTypeInfos[NewSeat.get("SeatType").toString()];

                _this.Seats.add(NewSeat);
            }, this);

            this.trigger('didLoadSeatLists', this);
            return _this;
        },

        loadGA: function (GAZones) {
            _(GAZones).each(function (gaZone, idx) {

                var newGAZone = new SYOSGAZone(_(gaZone).extend({ IsAvailable: true }));

                // PriceCollection for zone
                newGAZone.PriceCollection = this.getPriceTypeCollectionForZone(gaZone.ZoneId);
                if (!newGAZone.PriceCollection) {
                    console.warn("%c WARNING: Unable to assign prices to GAZone %s", "color:red;font-weight:bold;", gaZone.ZoneId);
                    newGAZone.PriceCollection = new SYOSPriceCollection();
                }

                // Create seatCollection for GAzone
                _(newGAZone.get('TotalSeats')).chain().range().each(function (i) {

                    var available = i < newGAZone.get('AvailableSeats') ? true : false;
                    var id = newGAZone.id + "-" + (i + 1);
                    var gaSeat = new SYOSGASeat({
                        IsAvailable: available,
                        ZoneId: newGAZone.id,
                        id: id,
                        SeatType: -1
                    });

                    // priceCollection for each gaSeat
                    gaSeat.PriceCollection = this.getPriceTypeCollectionForZone(newGAZone.id);
                    gaSeat.seatType = this.seatTypes.get(gaSeat.get('SeatType'));
                    gaSeat.SeatTypeInfo = this.SeatTypeInfos[gaSeat.get("SeatType").toString()];

                    newGAZone.gaSeats.add(gaSeat);

                }, this).value();

                this.GAZones.add(newGAZone);

            }, this);
        }
    });

    SYOSLevelCollection = SYOSCollection.extend({
        model: SYOSLevel,

        initialize: function () {
            this.on('add', this.didAddLevel, this);
            return this;
        },

        didAddLevel: function (level) {
            level.set("levelIndex", this.length - 1);
            return this;
        }
    });

    SYOSVenue = SYOSModel.extend();

    SYOSPerformance = SYOSModel.extend({

        initialize: function () {
            this.venue = new SYOSVenue();
        }

    });

    SYOSSeatView = SYOSModel.extend();

    SYOSSeatViewCollection = SYOSCollection.extend({
        logName: "SYOSSeatViewCollection",
        model: SYOSSeatView,

        initialize: function () {
            this.on('all', this.logEvent, this);
            return this;
        }
    });

    SYOSPrice = SYOSModel.extend({
        defaults: {
            DefaultPrice: 0,
            Price: 0,
            customAction: false
        },

        initialize: function () {
            this.formatPrices();
            this.set('IsDiscounted', this.getIsDiscounted());
            this.on('change:Price', this.formatPrices, this);
            return this;
        },

        getIsDiscounted: function () {
            return (this.get('DefaultPrice') !== this.get('Price'));
        },

        formatPrices: function () {
            this.set({
                defaultPriceFormatted: this.syos.utility.formatPrice(this.get('DefaultPrice')),
                priceFormatted: this.syos.utility.formatPrice(this.get('Price'))
            });
            return this;
        }
    });

    SYOSPriceCollection = SYOSCollection.extend({
        model: SYOSPrice,

        getMax: function () {
            var prices = this.pluck('Price');
            return Math.max.apply(null, prices);
        },

        getMin: function () {
            var prices = this.pluck('Price');
            return Math.min.apply(null, prices);
        },

        comparator: function (model) {
            return (-1 * model.get('Price'));
        }
    });

    SYOSSeat = SYOSPricedItem.extend({

        defaults: {
            IsAvailable: false,
            IsSelected: false,
            IsHighlighted: false,
            IsInCart: false
        },

        maxPrice: function () {
            if (this.PriceCollection.length !== 0)
                return this.PriceCollection.getMax();
            else
                return 0;
        },

        minPrice: function () {
            if (this.PriceCollection.length !== 0)
                return this.PriceCollection.getMin();
            else
                return 0;
        },

        initialize: function () {
            SYOSPricedItem.prototype.initialize.apply(this, arguments);
            this.hasPricing = true;

            var _this = this;

            if ('zoneNames' in this.syos.config) {
                this.getZoneName();
            }

            if (_this.has("SeatNumber")) {
                _this.id = _this.get("SeatNumber");
            }

            _this.on('seatPricingRecieved', function (PricingInfo) {
                $.each(PricingInfo, function () {
                    _this.Prices.add(new SYOSPrice(this));
                });
                _this.trigger('seatPricingUpdated');
            });

            _this.on('setActivePrice', function (PriceTypeIndex) {
                _this.activePrice = _this.PriceCollection.at(PriceTypeIndex);
            });

        },

        getZoneName: function () {
            var zoneId = parseFloat(this.get('ZoneId'));
            var zoneDescription = this.syos.config.zoneNames[zoneId] || "Zone " + zoneId;
            this.set('ZoneDescription', zoneDescription);
        },

        loadPriceInfo: function (PricingInfo) {
            var _this = this;
            $.each(PricingInfo, function () {
                _this.Prices.add(new SYOSPrice(this));
            });
            _this.trigger('seatPricingUpdated');
        },

        getActivePrice: function () {
            return this.activePrice;
        },

        didSelectSeat: function () {
            this.set({ IsSelected: true });
        },

        templateJSON: function () {
            var _this = this;
            var json = _this.toJSON();

            _.extend(json, { PriceTypes: _this.PriceCollection.toJSON() });

            if (typeof _this.ActivePrice !== "undefined")
                _.extend(json, { ActivePrice: _this.ActivePrice.toJSON() });
            if (typeof _this.SeatTypeInfo !== "undefined")
                _.extend(json, { SeatTypeInfo: _this.SeatTypeInfo });

            return json;
        }

    });

    SYOSSeatCollection = SYOSCollection.extend({
        logName: "SYOSSeatCollection",
        model: SYOSSeat,

        getSeatsWithType: function (seatTypeId) {
            return this.filter(function (seat) {
                return (seat.seatType.id === seatTypeId);
            });
        },

        getSeatsWithZoneId: function (zoneId) {
            return this.select(function (seat) {
                return seat.get('ZoneId') === zoneId;
            }, this);
        },

        getAllZoneIds: function () {
            var zoneIds = this.pluck('ZoneId');
            var uniqueZoneIds = _(zoneIds).unique();
            return uniqueZoneIds;
        },

        getAllSectionIds: function () {
            var sectionIds = this.pluck('SectionId');
            var uniqueSectionIds = _(sectionIds).unique();
            return sectionIds;
        },

        getAvailableSeats: function () {
            return this.select(function (seat) {
                return seat.get('IsAvailable');
            });
        },

        getUnavailableSeats: function () {
            return this.select(function (seat) {
                return !seat.get('IsAvailable');
            });
        }

    });

    SYOSGAZone = SYOSPricedItem.extend({

        initialize: function () {
            SYOSPricedItem.prototype.initialize.apply(this, arguments);
            this.hasPricing = true;

            var _this = this;

            if ('zoneNames' in this.syos.config) {
                this.getZoneName();
            }

            if (_this.has("ZoneId")) {
                _this.id = _this.get("ZoneId");
            }

            _this.on('seatPricingRecieved', function (PricingInfo) {
                $.each(PricingInfo, function () {
                    _this.Prices.add(new SYOSPrice(this));
                });
                _this.trigger('seatPricingUpdated');
            });

            _this.on('setActivePrice', function (PriceTypeIndex) {
                _this.activePrice = _this.PriceCollection.at(PriceTypeIndex);
            });
            this.formatAttr();

            this.gaSeats = new SYOSGASeatCollection();
            this.gaSeats.on('change', this.updateAvailability, this);
            this.tickets = [];

            this.set('CurrentAvailableSeats', this.get('AvailableSeats'))
        },

        formatAttr: function () {
            var pointCollection = this.get('Polygon').Points;
            pointCollection = _(pointCollection).map(function (point) {
                return {
                    x: point.X,
                    y: point.Y,
                    r: point.R,
                    reverse: point.Reverse
                }

            }, this);
            this.get('Polygon').Points = pointCollection;
        },

        updateAvailability: function () {
            var availableSeats = this.gaSeats.where({ IsAvailable: true, IsInCart: false }).length;
            this.set('CurrentAvailableSeats', availableSeats);
            this.set('IsAvailable', this.isAvailable());
            if (this.mapPolygon) {
                this.mapPolygon.set('active', this.isAvailable());
            }
        },

        isAvailable: function () {
            return this.get('CurrentAvailableSeats') > 0;
        },

        getAvailableSeat: function () {
            var seat = this.gaSeats.where({ IsAvailable: true })[0];
            return seat || null;
        },

        getSeatsByQuantity: function (quantity) {
            if (this.get('CurrentAvailableSeats') < quantity) {
                console.warn("Not enough available seats");
                return null;
            }
            else {
                return this.gaSeats.where({ IsAvailable: true }).slice(0, quantity);
            }
        }
    });

    SYOSGAZoneCollection = SYOSCollection.extend({
        model: SYOSGAZone
    });

    SYOSGASeat = SYOSModel.extend({
        defaults: {
            IsInCart: false,
            IsAvailable: false
        },

        initialize: function () {
            this.id = this.get('id');

            this.on('setActivePrice', _(function (PriceTypeIndex) {
                this.activePrice = this.PriceCollection.at(PriceTypeIndex);
            }).bind(this));
        },

        getActivePrice: function () {
            return this.activePrice;
        }
    });

    SYOSGASeatCollection = SYOSCollection.extend({
        model: SYOSGASeat
    });

    SYOSSeatType = SYOSModel.extend({

        idAttribute: "ID"

    });

    SYOSSeatTypeCollection = SYOSCollection.extend({

        model: SYOSSeatType,

        comparator: function (seatType) {
            return seatType.get('SeatTypeIndex');
        }

    });

    SYOSSeatColor = SYOSModel.extend({

    });

    SYOSSeatColorCollection = SYOSCollection.extend({

        __bbType: "SYOSSeatColorCollection",

        model: SYOSSeatColor,

        getColorForKey: function (colorKey) {
            return this.detect(function (seatColor) {
                return seatColor.get('key') === colorKey;
            });
        }

    });

    // #endregion

    // #region Plugin

    SYOSPlugin = SYOSView.extend({
        enabled: true,

        initialize: function () {
            syosDispatch.on('didFinishInit', this.syosReady, this);
            if (this.enabled) {
                this.initPlugin();
            }
            else {
                console.warn('Plugin ' + this.pluginName + ' disabled');
            }
            return this;
        },

        initPlugin: function () {
            return this;
        }
    });

    // plugin extend override
    SYOSPlugin.extend = _(SYOSPlugin.extend).wrap(function (fn, extension) {
        var name = extension.pluginName;
        if (typeof extension.pluginName !== "string")
            throw "SYOSPlugin " + name + " must have a string for pluginName";
        if (typeof extension.initPlugin !== 'function')
            throw "SYOSPlugin " + name + " must have a function for initPlugin";

        var args = _(arguments).rest(1);
        return fn.apply(this, args);
    });

    // #endregion

}());


// Adage SYOS

var syosDispatch = _(Backbone.Events).clone();

//#region Logging

(function () {
    var eventLogging = (location.href.match(/logevents/) || (localStorage && localStorage.syosLogEvents === "on"))
    if (eventLogging) {
        console.warn('EVENT LOGGING ON');
        syosDispatch.on('all', function (eventName) {
            console.groupCollapsed("syosDisptach triggered %o", eventName);
            console.log("Arguments: %o", arguments);
            console.trace();
            console.groupEnd();
        });
    }

    var syosLogRender = window.syosLogRender = !!location.href.match(/logrender/);
    if (syosLogRender) {
        console.warn('RENDER LOGGING ON');
    }

}());

//#endregion

//#region Flash Canvas Config

if (typeof FlashCanvas != "undefined") {
    FlashCanvas.setOptions({
        turbo: false,
        delay: 30
    });
}

// #endregion

var syosTestModeActive;

(function ($, Backbone, _) {

    syosTestModeActive = false;

    var SYOS = function (opts) {
        return new SYOSCore(opts);
    };

    var passFn = function () { return this; };

    var check = function (subject, message) {
        var message = message || "(no message provided)";

        if (!subject) {
            throw "SYOS: Failed BoolCheck! " + message;
        }
    };

    //#region Errors

    SYOS.Error = function (message) {
        this.name = "SYOSError";
        this.message = message;
    };

    SYOS.Error.prototype = Error.prototype;

    SYOS.Error.printXHRInfo = function () {
        console.group("%cSYOS XHR Error Details", "color:red;");
        var errorDetail = arguments[0];
        var responseText = JSON.parse(errorDetail.responseText);
        _(responseText).each(function (value, key) {
            console.group(key);
            console.log(value);
            console.groupEnd();
        });
        console.log('error detail: %o', errorDetail);
        console.groupEnd();
    };

    SYOS.Errors = {

        rootDataAjax: function () {
            SYOS.Error.printXHRInfo.apply(null, arguments);
            throw new SYOS.Error("service call for root data failed");
        },

        reserveSeatsAjax: function () {
            SYOS.Error.printXHRInfo.apply(null, arguments);
            throw new SYOS.Error("service call for reserve seats failed!");
        },

        zonePriceAjax: function () {
            SYOS.Error.printXHRInfo.apply(null, arguments);
            throw new SYOS.Error("service call for zone prices failed");
        },

        alreadyOnPerformance: function (performance) {
            console.warn("Aready on performance!");
            console.log("Current Performance: %o", performance.toJSON());
        }

    };

    //#endregion

    //#region Geometry

    SYOS.Geometry = (function () {

        var Coordinates = SYOSModel.extend({

            defaults: {
                x: 0,
                y: 0
            },

            addX: function (val) {
                var target = this.get('x');
                target += val;
                this.set('x', target);
            },

            addY: function (val) {
                var target = this.get('y');
                target += val;
                this.set('y', target);
            }

        });

        return {
            Coordinates: Coordinates
        };

    }());

    // #endregion

    //#region Elements

    SYOS.Elements = (function () {

        //#region View

        var View = SYOSView.extend({

            initialize: function () {
                this.on('didRender', this.didRender, this);

                this.didLoadView && this.didLoadView();
            },

            didRender: passFn

        });

        //#endregion

        //#region Cart

        var Cart = View.extend({

            className: "syos-cart-wrapper",

            initialize: function () {
                this.isExpanded = false;

                this.seats = new SYOSSeatCollection();

                this.seats.on('add', this.didAddSeat, this);
                this.seats.on('remove', this.didRemoveSeat, this);

                syosDispatch.on('buymode:addSeatToCart', this.didAddSeatToCart, this);
                syosDispatch.on('buymode:removeSeatFromCart', this.didRemoveSeatFromCart, this);
                syosDispatch.on('buymode:clearCart', this.shouldClearCart, this);
                syosDispatch.on('buymode:shouldRemoveSeatsOfType', this.shouldRemoveSeatsOfType, this);


                this.gaSeats = new SYOSGASeatCollection();
                this.gaSeats.on('add', this.didAddGASeat, this);
                this.gaSeats.on('remove', this.didRemoveGASeat, this);
                syosDispatch.on('buymode:addGASeatsToCart', this.didAddGASeatsToCart, this);
                syosDispatch.on('buymode:removeGASeatFromCart', this.didRemoveGASeatFromCart, this);

                this.$el.bind('mousewheel', _(this.stopProp).bind(this));

                SYOS.Elements.View.prototype.initialize.apply(this, arguments);
            },

            events: {
                "click [data-bb-event='remove']": "didClickRemove",
                "click [data-bb='expandButton']": "didClickExpand",
                "click [data-bb='reserveButton']": "didClickReserve",
                "click [data-bb-event='info']": "didClickInfo"
            },

            didAddSeatToCart: function (seat) {
                this.seats.add(seat);
                syosDispatch.trigger('buymode:didChangeCart', this);
            },

            didAddGASeatsToCart: function (seats) {
                this.gaSeats.add(seats.models);
                syosDispatch.trigger('buymode:didChangeCart', this);
            },

            didRemoveSeatFromCart: function (seat) {
                this.seats.remove(seat);
                seat.set('IsInCart', false);
                seat.mapCircle.set('inCart', false);
                seat.mapCircle.set('underCursor', false);
                syosDispatch.trigger('buymode:didChangeCart', this);
            },

            didRemoveGASeatFromCart: function (seat) {
                this.gaSeats.remove(seat);
                seat.set('IsInCart', false);
                seat.set('IsAvailable', true);
                syosDispatch.trigger('buymode:didChangeCart', this);
            },

            didAddSeat: function (seat) {
                console.log("Cart - adding seat: %o", seat);

                seat.set('IsInCart', true);
                seat.set('IsSelected', false);
                this.render();
            },

            didAddGASeat: function (gaSeat) {
                console.log("Cart - adding GA seat: %o", gaSeat);
                this.render();
            },

            didClickInfo: function (evt) {
                if (this.modal) {
                    this.modal.close();
                }

                var seatId = this.getChildElementSeatId(evt.currentTarget);
                var seat = this.seats.get(seatId) || this.exchangeSeats.get(seatId);

                this.modal = new SYOSModal({
                    CanDismiss: true,
                    Title: seat.seatType.get('Description'),
                    Content: seat.seatType.get('FullPrompt')
                });
            },

            didRemoveSeat: function (seat) {
                console.log("Cart - removing seat: %o", seat);

                seat.set('IsInCart', false);
                seat.set('IsSelected', false);
                this.render();
            },

            didRemoveGASeat: function (seat) {
                console.log("Cart - removing GA seat: %o", seat);

                seat.set('IsInCart', false);
                seat.set('IsSelected', false);
                this.render();
            },

            didClickRemove: function (evt) {
                var isGAZone = typeof $(evt.currentTarget).parents('[data-zone-id]')[0] !== 'undefined';
                if (!isGAZone) {
                    var seatId = this.getChildElementSeatId(evt.currentTarget);
                    var seat = this.seats.get(seatId) || this.exchangeSeats.get(seatId);
                    syosDispatch.trigger('buymode:removeSeatFromCart', seat);
                }
                else {
                    var seatId = this.getChildElementGAZoneInfo(evt.currentTarget);
                    var seat = this.gaSeats.get(seatId);
                    syosDispatch.trigger('buymode:removeGASeatFromCart', seat);
                }
            },

            didClickExpand: function () {
                this.isExpanded = !this.isExpanded;
                this.render();
            },

            didClickReserve: function (evt) {
                evt.preventDefault();
                syosDispatch.trigger('buymode:reserveSeats');
            },

            didRender: function () {
                this.determineVisibility();
            },

            shouldClearCart: function () {
                this.seats.reset();
                syosDispatch.trigger('buymode:didChangeCart', this);
                this.render();
            },

            shouldRemoveSeatsOfType: function (seatTypeId) {
                this.seats.each(function (seat) {
                    if (seat.seatType.id === seatTypeId)
                        syosDispatch.trigger('buymode:removeSeatFromCart', seat);
                }, this);
            },

            getTotalSeats: function () {
                return this.seats.length + this.gaSeats.length;
            },

            getAllSeats: function () {
                return this.seats.models.concat(this.gaSeats.models);
            },

            getChildElementSeatId: function (element) {
                var parentElement = $(element).parents('[data-seat-id]')[0];
                if (parentElement) {
                    var attributeString = parentElement.getAttribute('data-seat-id');
                }
                var seatId = parseInt(attributeString, 10) || 0;
                return seatId;
            },

            getChildElementGAZoneInfo: function (element) {
                var parentElement = $(element).parents('[data-seat-id]')[0];
                if (parentElement) {
                    var attributeString = parentElement.getAttribute('data-seat-id');
                }
                var seatId = attributeString || 0;
                return seatId;
            },

            determineVisibility: function () {
                var totalSeats = this.getTotalSeats();
                if (totalSeats === 0)
                    this.$el.fadeOut();
                else
                    this.$el.show();
            }

        });

        // #endregion

        return {
            View: View,
            Cart: Cart
        };

    }());

    // #endregion

    //#region Exchange

    SYOS.Exchange = (function () {

        // #region Purchasing

        var Purchasing = SYOSView.extend({

            initialize: function () {
                this.cart = new ExchangeCart();
                this.el.querySelector('.syos-root').appendChild(this.cart.el);

                this.exchangeZones = new ZoneCollection();
                this.addonZones = new ZoneCollection();

                syosDispatch.on('map:seatSelected', this.didSelectSeat, this);
                syosDispatch.on('canvas:didClickSeat', this.didSelectSeat, this);

            },

            didSelectSeat: function (seat) {
                var seatPriceCount = seat.PriceCollection.length;
                if (seatPriceCount !== 1) {
                    throw "SYOS Error: exchange mode does not support multiple price types";
                }
                else {
                    seat.activePrice = seat.PriceCollection.at(0);
                    syosDispatch.trigger('buymode:addSeatToCart', seat);
                }
            },

            loadLevelData: function (levelData) {
                var cleanZones = _(levelData.AllSeatPricing).map(function (seatPricing) {
                    var obj = _(seatPricing.Value["0"]).clone();
                    obj.ZoneId = parseInt(seatPricing.Key);
                    return obj;
                });

                var addonZoneInfo = _(levelData.AddOnSections).map(function (seatPricing) {
                    var obj = _(seatPricing).omit('SectionId');
                    obj.ZoneId = seatPricing.SectionId;
                    return obj;
                });

                this.exchangeZones.add(cleanZones);
                this.addonZones.add(addonZoneInfo);
            }

        });

        // #endregion

        // #region Zone Models

        var Zone = SYOSModel.extend({

            idAttribute: "ZoneId"

        });

        var ZoneCollection = SYOSCollection.extend({

            model: Zone

        });

        // #endregion

        // #region ExchangeCart

        var ExchangeCart = SYOS.Elements.Cart.extend({

            template: Handlebars.templates["exchangeCart.html"],

            // #region events

            didLoadView: function () {
                this.exchangeSeats = new SYOSSeatCollection();
                this.exchangeSeats.on('add', this.didAddSeat, this);
                this.exchangeSeats.on('remove', this.didRemoveExchangeSeat, this);

                this.seatElements = {};
                this.$el.hide();
                this.render();
            },

            didAddSeatToCart: function (seat) {
                if (this.isExchangeCartFull()) {
                    this.seats.add(seat);
                }
                else {
                    this.exchangeSeats.add(seat);
                }
            },

            didRemoveExchangeSeat: function (seat) {
                if (this.seats.length !== 0) {
                    var transferSeat = this.seats.first();
                    this.exchangeSeats.add(transferSeat);
                    this.seats.remove(transferSeat);
                }
                this.didRemoveSeat(seat);
            },

            didRemoveSeatFromCart: function (seat) {
                console.log("Exchange Cart: removing seat: %o", seat);
                this.seats.remove(seat);
                this.exchangeSeats.remove(seat);
            },

            shouldClearCart: function () {
                this.seats.reset();
                this.exchangeSeats.reset();
                this.render();
            },

            // #endregion

            // #region return fn

            getAllSeats: function () {
                return _.union(this.exchangeSeats.models, this.seats.models);
            },

            getExchangeCount: function () {
                return this.syos.settings.get('exchangeCount') || 0;
            },

            getTotalSeats: function () {
                return this.exchangeSeats.length + this.seats.length;
            },

            getExchangePricing: function (seat) {
                var zoneId = parseInt(seat.get('ZoneId'), 10);
                var zone = this.syos.exchangeMode.exchangeZones.get(zoneId);
                if (zone)
                    return zone.toJSON();
                else
                    return undefined;
            },

            getAddonPricing: function (seat) {
                var zoneId = parseInt(seat.get('ZoneId'), 10);
                var zone = this.syos.exchangeMode.addonZones.get(zoneId);
                if (zone)
                    return zone.toJSON();
                else
                    return undefined;
            },

            isExchangeCartFull: function () {
                return (this.exchangeSeats.length > (this.getExchangeCount() - 1));
            },

            hasDowngradeSeat: function () {
                return this.exchangeSeats.any(function (seat) {
                    return (seat.activePrice.get('Price') < 0);
                });
            },

            // #endregion

            // #region view

            viewModel: function () {
                var showExpandButton = !this.isExpanded && (this.getTotalSeats() > 5);
                var exchangeSeats = this.exchangeSeats.map(this.viewModelForExchangeSeat, this);
                var seats = this.seats.map(this.viewModelForAddonSeat, this);

                var sumFn = function (memo, seat) {
                    var price = seat.pricing.Price;
                    if (price < 0) {
                        price = 0;
                    }
                    return memo + price;
                };
                var total = _(exchangeSeats).reduce(sumFn, 0) + _(seats).reduce(sumFn, 0);

                exchangeSeats.reverse();
                seats.reverse();

                return {
                    showExpandButton: showExpandButton,
                    isExpanded: this.isExpanded,
                    exchangeSeats: exchangeSeats,
                    seats: seats,
                    total: total
                };
            },

            viewModelForAddonSeat: function (seat) {
                var model = seat.toJSON();
                _(model).extend({ exchange: false });
                _(model).extend({ pricing: this.getAddonPricing(seat), info: seat.seatType.toJSON() });
                return model;
            },

            viewModelForExchangeSeat: function (seat) {
                var model = seat.toJSON();
                _(model).extend({ exchange: true });
                _(model).extend({ pricing: this.getExchangePricing(seat), info: seat.seatType.toJSON() });
                return model;
            }

            // #endregion

        });

        // #endregion

        return {
            Purchasing: Purchasing,
            Zone: Zone,
            ZoneCollection: ZoneCollection,
            ExchangeCart: ExchangeCart
        };

    }());

    // #endregion

    //#region Data

    SYOS.Data = {};

    SYOS.Data.Services = SYOSView.extend({

        serviceRoot: "/SYOSService/SYOSService.asmx",

        defaultOptions: _(function () {
            return {
                contentType: "application/json",
                responseType: "application/json",
                type: "post",
                success: _(this.defaultSuccess).bind(this),
                error: _(this.defaultError).bind(this)
            };
        }).memoize(),

        defaultSuccess: function (response) {
            console.log(response);
        },

        defaultError: function (response) {
            console.log(response)
        },

        makeRequest: function (url, options) {
            var opts = _(options).defaults(this.defaultOptions());
            _(opts).extend({ url: url });
            console.groupCollapsed("%cData request to %s with options", "color:#670099; font-weight:bold", url);
            console.log("XHR Options: %o", opts);
            console.groupEnd();
            $.ajax(opts);
        },

        reserveSeats: function (options) {
            this.makeRequest(this.serviceRoot + "/ReserveSeats", options);
        },

        getRootConfigData: function (options) {
            this.makeRequest(this.serviceRoot + "/GetRootConfigData", options);
        }


    });

    //#endregion

    //#region Buymode

    SYOS.Buymode = (function () {

        //#region Controller

        var Controller = SYOSView.extend({

            initialize: function () {
                this.initCart();

                //this.seatPopup = new SeatPopup();
                this.gaZonePopup = new GAZonePopup();

                syosDispatch.on('global:levelInformationLoaded', this.didLoadLevelInfo, this);
            },

            initCart: function () {
                this.cart = new Cart();
                this.getBBElement('syosRoot').appendChild(this.cart.el);
            },

            didLoadLevelInfo: function (LevelInfo) {
                this.Level = new SYOSLevel();
                this.Level.loadLevel(LevelInfo);

                syosDispatch.trigger('buymode:ensureMapAccuracy', _this);
                return this;
            },

            didChangeExchangeSeatCount: function (count) {
                console.log("SYOS: Changing exchange count to %s", count);
                this.exchangeCount = count;
            },

            reserveSeatResults: function (response) {
                console.log("reserve seats results", response);
                //window.location.href = response.d;
                return this;
            },

            reserveSeatError: function (Response) {
                var _this = this;
                syosDispatch.trigger("hideLoadingMessage");
                if (Response._message) {
                    var ErrorTemplates = $("script[type='text/reserve-error-template']");
                    var Template = _.detect(ErrorTemplates, function (ErrorTemplate) {
                        var ErrorString = $(ErrorTemplate).attr("data-error-string");
                        var MatchIndex = Response._message.indexOf(ErrorString);
                        if (MatchIndex !== -1)
                            return true;
                    });

                    console.groupEnd();

                    if (!Template)
                        Template = $("#reserve-error-general");
                    else
                        Template = $(Template);

                    console.log("Error Template", Template);

                    // determine template action
                    var templateAction = Template.attr("data-action");

                    // default action
                    var modalFunction = function (modal) {
                        HtmlSyos.restart();
                    };

                    if (templateAction) {
                        // dismiss action
                        if (templateAction === "dismiss") {
                            templateAction = function (modal) {
                                modal.close();
                            }
                        }
                    }

                    // open modal
                    var ViewSeatModal = new SYOSModal({
                        CanDismiss: false,
                        Title: Template.attr("data-error-title"),
                        Content: Template.html(),
                        Width: 60,
                        Height: 35,
                        Actions: [
                            { ButtonText: Template.attr("data-button-text"), Fn: templateAction }
                        ]
                    });

                }

                console.log(Response);
                return _this;
            }

        });

        //#endregion

        //#region Cart

        var Cart = SYOS.Elements.Cart.extend({

            template: Handlebars.templates["cart.html"],

            viewModel: function () {
                var showExpandButton = this.seats.length > this.syos.settings.get('maxCartLength');
                var seats = this.seats.map(this.viewModelForSeat, this);
                var gaSeats = this.gaSeats.map(this.viewModelForSeat, this);

                var total = _(seats).reduce(function (memo, seat) { return memo + seat.pricing.Price; }, 0) +
                            _(gaSeats).reduce(function (memo, seat) { return memo + seat.pricing.Price; }, 0);

                return {
                    isExpanded: this.isExpanded,
                    showExpandButton: showExpandButton,
                    seats: seats,
                    gaSeats: gaSeats,
                    total: total
                };
            },

            viewModelForSeat: function (seat) {
                var model = seat.toJSON();
                _(model).extend({ pricing: seat.getActivePrice().toJSON() });
                _(model).extend({ info: seat.seatType ? seat.seatType.toJSON() : null });
                return model;
            }

        });

        //#endregion

        //#region Popups

        var SYOSPopup = SYOSView.extend({
            initialize: function () {
                SYOSView.prototype.initialize.apply(this, arguments);
                this.position = new SYOS.Geometry.Coordinates();
                this.faded = false;
                this.shown = false;

                $("#syos").prepend(this.$el);
                this.$el.hide();

                syosDispatch.on('keyboard:escape', this.closePopup, this);
                syosDispatch.on("map:willDrag", this.willDragMap, this);
                syosDispatch.on("map:dragged", this.didDragMap, this);
                syosDispatch.on("control:smartZoom", this.movePopupToActiveObject, this);

                this.mapObject = null;
            },

            movePopupToActiveObject: function () {
                var position = this.getActiveObjectPosition();

                var targetCSS = {
                    top: position.y + 'px',
                    left: position.x + 'px'
                };

                var animationOptions = {
                    duration: 300,
                    easing: 'easeOutSine'
                };

                if (this.shown)
                    this.$el.animate(targetCSS, animationOptions);
                else
                    this.$el.css(targetCSS);
            },

            getActiveObjectPosition: function () {
                console.warn("didn't override getActiveObjectPosition!");
            },

            closePopup: function () {
                this.clearObjectSelectedState();
                this.shown = false;
                // todo :
                syosDispatch.trigger('buymode:seatInfoClosed', this.activeObject);
                var animationOptions = {
                    duration: 300,
                    easing: 'easeInSine',
                    direction: 'down'
                };

                this.$el.hide('drop', animationOptions);

            },

            didDragMap: function () {
                this.movePopupToActiveObject();
                this.unfadePopup();
            },

            willDragMap: function () {
                this.fadePopup();
            },

            clearObjectSelectedState: function () {
                if (this.activeObject) {
                    this.activeObject.set({ IsSelected: false });
                    if (this.mapObject === null) {
                        console.warn("Didn't specify mapObject!");
                    }
                    else {
                        this.activeObject[this.mapObject].set('selected', false);
                    }
                }
            },

            render: function () {
                this.el.innerHTML = this.template(this);
                this.openPopup();
                this.movePopupToActiveObject();
                this.trigger('didRender', this);
            },

            getCurrentPosition: function () {
                return {
                    x: parseFloat(this.$el.css('left')),
                    y: parseFloat(this.$el.css('top'))
                };
            },
            openPopup: function () {
                this.shown = true;
                this.$el.show();
                if (this.faded)
                    this.unfadePopup();
            },

            closePopupImmeadiate: function () {
                this.$el.css('display', 'none');
                this.closePopup();
            },

            fadePopup: function () {
                this.faded = true;
                var targetCSS = { opacity: 0.5 };
                if (this.shown)
                    this.$el.animate(targetCSS);
                else
                    this.$el.css(targetCSS);
            },

            unfadePopup: function () {
                this.faded = false;
                var targetCSS = { opacity: 1 };
                if (this.shown)
                    this.$el.animate(targetCSS);
                else
                    this.$el.css(targetCSS);
            }
        });

        var SeatPopup = SYOSPopup.extend({
            template: Handlebars.templates["popup.html"],
            id: "seatPopupView",

            className: "syos-seat-popup-view",

            initialize: function () {
                SYOSPopup.prototype.initialize.apply(this, arguments);
                syosDispatch.on('canvas:didClickSeat', this.didSelectSeat, this);
                this.mapObject = "mapCircle";
            },
            events: {
                "click .syos-popup-addToCart": "addButtonClicked",
                "click .syos-popup-closeButton": "closePopup"
            },

            didSelectSeat: function (seat) {
                if (this.activeObject) {
                    this.clearObjectSelectedState();
                }

                if (seat.get('IsInCart')) {
                    syosDispatch.trigger('buymode:removeSeatFromCart', seat);
                    return true;
                }
                else {
                    this.activeObject = seat;
                    this.activeObject.mapCircle.set('selected', true);
                    this.render();
                }

                var priceCount = this.activeObject.PriceCollection.length;
                if (priceCount === 1) {
                    this.addActiveSeatWithPriceIndex(0);
                    this.closePopupImmeadiate();
                }
                else if (priceCount === 0) {
                    this.closePopupImmeadiate();
                    alert('No Pricing Available for this Seat');
                }
            },

            addActiveSeatWithPriceIndex: function (priceIndex) {
                this.activeObject.trigger('setActivePrice', priceIndex);
                this.activeObject.set({ IsInCart: true });
                syosDispatch.trigger('buymode:addSeatToCart', this.activeObject);
            },


            addButtonClicked: function (evt) {
                var $button = $(evt.currentTarget);
                var priceIndex = $button.parents('tr').index();

                this.addActiveSeatWithPriceIndex(priceIndex);

                this.closePopup();
            },

            getActiveObjectPosition: function () {

                var coords;
                if (this.activeObject) {
                    var circle = this.activeObject.mapCircle;
                    var circleRadius = this.activeObject.mapCircle.scaledAttributes().radius;
                    coords = this.syos.canvasMap.getPositionForCircle(circle);
                    coords.x += circleRadius;
                    coords.y += circleRadius;
                }
                else {
                    coords = { x: 0, y: 0 };
                }
                return coords;
            }
        });

        var GAZonePopup = SYOSPopup.extend({
            template: Handlebars.templates["popupGA.html"],

            id: "gaPopupView",

            className: "syos-seat-popup-view",

            initialize: function () {
                SYOSPopup.prototype.initialize.apply(this, arguments);
                syosDispatch.on('canvas:didClickGAZone', this.didSelectGAZone, this);
                this.mapObject = "mapPolygon";
            },
            events: {
                "click .syos-popup-addToCart": "addButtonClicked",
                "click .syos-popup-closeButton": "closePopup"
            },

            didSelectGAZone: function (gaZone) {
                if (this.activeObject) {
                    this.clearObjectSelectedState();
                }

                this.activeObject = gaZone;
                this.activeObject.mapPolygon.set('selected', true);

                this.render();
            },

            addActiveSeatWithPriceIndex: function (quantityArray) {
                var selectedSeats = new SYOSGASeatCollection();
                _(quantityArray).each(function (quantity, i) {
                    var seatModels = this.activeObject.getSeatsByQuantity(quantity);
                    _(seatModels).each(function (gaSeat) {
                        gaSeat.trigger('setActivePrice', i);
                        gaSeat.set('IsAvailable', false);
                        gaSeat.set('IsInCart', true);
                    }, this);
                    selectedSeats.add(seatModels);
                }, this);

                syosDispatch.trigger('buymode:addGASeatsToCart', selectedSeats);
                this.closePopup();
            },

            didRender: function () {
                var gaSeatsInCart = this.activeObject.gaSeats.where({ IsAvailable: false, IsInCart: true });
                var inCartMarkup = this.activeObject.PriceCollection.map(function (price) {

                    var gaSeats = gaSeatsInCart.filter(function (seat) {
                        return seat.activePrice.get('PriceTypeId') === price.get('PriceTypeId');
                    }, this);
                    return gaSeats.length;

                }, this);
                //
                this.$el.find(".number-in-cart").each(function (i, ele) {
                    $(ele).html(inCartMarkup[i]);
                });
            },

            addButtonClicked: function (evt) {

                var $button = $(evt.currentTarget);
                var priceIndex = $button.parents('tr').index();

                // get quantities for all price type 
                var selectDomArray = $button.parents('tr').siblings().find('select');
                var quantityArray = _(selectDomArray).map(function (ele) { return parseInt(ele.value, 10); }, this);

                var totalSeats = _(selectDomArray).reduce(function (sum, ele) { return sum + parseInt(ele.value, 10); }, 0);

                this.addActiveSeatWithPriceIndex(quantityArray);
            },

            getActiveObjectPosition: function () {

                var coords = { x: 0, y: 0 };
                //if (this.activeObject) {
                //    var polygon = this.activeObject.mapPolygon;
                //    coords = this.syos.canvasMap.getPositionForPolygon(polygon);
                //}

                // Always show in the middle of the screen
                var elWidth = this.$el.width();
                var elHeight = this.$el.height();

                var syosCanvasHeight = $('#syosCanvas').height();
                var syosCanvasWidth = $('#syosCanvas').width();

                coords.x = (syosCanvasWidth - elWidth) / 2;
                coords.y = (syosCanvasHeight - elHeight) / 2;

                return coords;
            }
        });

        //#endregion

        //#region ReserveDialog

        var ReserveDialog = SYOSView.extend({

            className: "syos-reserve-dialog-wrap",

            template: Handlebars.templates["reserveDialog.html"],

            initialize: function () {
                SYOSView.prototype.initialize.apply(this, arguments);
                this.seats = new SYOSSeatCollection();
                syosDispatch.on('buymode:reserveSeats', this.shouldReserveSeats, this);

                this.postReserveActions = [];

                this.close();
                this.render();

                syosDispatch.trigger('reserveDialogReady', this);
            },

            events: {
                "click [data-bb='reserveCancel']": "didClickReserveCancel",
                "click [data-bb='reserveConfirm']": "didClickReserveConfirm"
            },

            addPostReserveAction: function (promise) {
                this.postReserveActions.push(promise);
            },

            didClickReserveCancel: function () {
                if (this.syos.settings.get('seatTypeWarningRemoveSeatsOnCancel')) {
                    syosDispatch.trigger('buymode:shouldRemoveSeatsOfType', this.currentWarningSeatType.get('ID'));
                }

                this.close();
            },

            didClickReserveConfirm: function () {
                this.confirmPrompt = null;
                this.render();
                this.sendReserveRequest();
            },

            shouldReserveSeats: function () {
                this.reset();

                this.seats.add(this.syos.primaryCart.getAllSeats());
                console.log("Reserving with seats: %o", this.seats);

                this.open();

                if (this.syos.settings.get('reserveConfirm')) {
                    this.showSeatTypeWarning();
                }
                else {
                    this.sendReserveRequest();
                }
            },

            getReserveString: function () {
                var string = '';

                this.seats.each(function (seat) {
                    string += (seat.get("SeatNumber") ? seat.get("SeatNumber") : 'z' + seat.get('ZoneId')) + ',' + seat.activePrice.get("PriceTypeId") + ',' + seat.get("SeatType") + ';';
                });

                return string;
            },

            setAjaxString: _(function (string) {
                this.getBBElement('reserveAjax').innerText = string;
            }).throttle(1000),

            showSeatTypeWarning: function () {
                var seatTypes = new SYOSSeatTypeCollection();
                this.seats.each(function (seat) {
                    seatTypes.add(seat.seatType);
                });

                var firstType = seatTypes.first();
                var confirmPromptText = firstType.get('ConfirmPrompt');

                if (confirmPromptText) {
                    this.currentWarningSeatType = firstType;
                    this.confirmPrompt = {
                        dialogText: confirmPromptText,
                        cancelButtonText: this.syos.settings.get('seatTypeWarningModalCancelText'),
                        confirmButtonText: this.syos.settings.get('seatTypeWarningModalConfirmText')
                    };

                    this.render();
                }
                else {
                    this.sendReserveRequest();
                }

            },

            open: function () {
                this.dialogEl.style.display = "none";
                this.el.style.display = "block";
                $(this.dialogEl).show('clip');
            },

            close: function () {
                this.el.style.display = "none";
            },

            didRender: function () {
                this.dialogEl = this.getBBElement('reserveDialog');
            },

            reset: function () {
                this.seats.reset();

                this.confirmPrompt = null;
                this.errorPrompt = null;

                this.render();
            },

            viewModel: function () {
                var showWait = true;
                if (this.confirmPrompt || this.errorPrompt) {
                    showWait = false;
                }

                return {
                    seats: this.seats,
                    showWait: showWait,
                    confirmPrompt: this.confirmPrompt,
                    errorPrompt: this.errorPrompt
                };
            },

            showErrorForMessage: function (message) {
                var message = message.toLowerCase();
                var templateId = "reserve-error-general";

                _($('script[type="text/reserve-error-template"]')).each(function (errorTemplate) {
                    var errorString = errorTemplate.getAttribute('data-error-string').toLowerCase();
                    var pattern = new RegExp(message);
                    var matches = !!errorString.match(pattern);
                    if (matches) {
                        templateId = errorTemplate.id;
                    }
                });

                console.log('selected error template: ' + templateId);

                var selectedTemplate = document.querySelector('#' + templateId);
                var templateContent = $(selectedTemplate).html();

                this.errorPrompt = {
                    title: selectedTemplate.getAttribute('data-error-title'),
                    content: templateContent
                };

                this.render();
            },

            sendReserveRequest: function () {
                var reserveString = this.getReserveString();

                var ajaxData = {
                    seatString: reserveString,
                    itemNumber: this.syos.performance.id,
                    syosType: this.syos.syosType
                };
                var ajaxOpts = {
                    data: JSON.stringify(ajaxData),
                    beforeSend: _(this.reserveSend).bind(this),
                    error: _(this.reserveError).bind(this),
                    success: _(this.reserveSuccess).bind(this)
                };

                this.syos.services.reserveSeats(ajaxOpts);

                return this;
            },

            reserveSend: function () {
                this.setAjaxString('Sending...');
                this.blockRedirect = false;
            },

            reserveError: function (response) {
                var responseText = JSON.parse(response.responseText);
                this.setAjaxString('Error reserving seats.');

                console.log("Error reserving seats: %o", responseText);

                this.showErrorForMessage(responseText['Message']);

                _(SYOS.Errors.reserveSeatsAjax).bind(this);
            },

            reserveSuccess: function (response) {
                syosDispatch.trigger('buymode:reserveSuccess');
                this.redirect = response.d;

                var onResolve = _.bind(function (args) {
                    console.log("SYOS PostReserveAction Complete, Redirecting...");
                    this.setAjaxString('Redirecting...');
                    this.doRedirect();
                }, this);

                var responder = _.after(this.postReserveActions.length, onResolve);

                if (this.postReserveActions.length > 0) {
					_(this.postReserveActions).invoke('then', responder);
				} else {
					responder();
				}

            },

            doRedirect: function () {
                if (this.windowObject) {			// override
                    this.pageRedirect(this.windowObject);
                }
                else if (parent !== window) {		// iframe breakout
                    this.pageRedirect(parent);
                }
                else {								// default behavior
                    this.pageRedirect(window);
                }
            },

            pageRedirect: function (target) {
                _(function () {
                    target.location.href = this.redirect;
                }).chain().bind(this).delay(500);
            },

            useWindowObject: function (windowObject) {
                this.windowObject = windowObject;
            }

        });

        // #endregion

        return {
            Controller: Controller,
            Cart: Cart,
            SeatPopup: SeatPopup,
            GAZonePopup: GAZonePopup,
            ReserveDialog: ReserveDialog
        };


    })();

    //#endregion

    //#region Touch

    SYOS.Touch = function () {

        var Touch = function (opts) {
            this.el = opts.el;
            this.syos = SYOSView.prototype.syos;
            this.initListeners();
        };

        Touch.prototype = {

            initListeners: function () {
                Hammer(this.el).on('pinch', _(this.didPinch).bind(this));
                Hammer(this.el).on('doubletap', _(this.didDoubleTap).bind(this));
                Hammer(this.el).on('drag', _(this.didDrag).bind(this));
                Hammer(this.el).on('dragstart', _(this.didDragStart).bind(this));
                Hammer(this.el).on('dragend', _(this.didDragEnd).bind(this));
            },

            didPinch: _(function (evt) {
                evt.preventDefault();
                evt.stopPropagation();
                evt.gesture.preventDefault();
                evt.gesture.stopPropagation();

                var gesture = evt.gesture;

                syosDispatch.trigger('touch:didPinch', evt.gesture, evt);
                var currZoom = this.syos.utility.getCurrentZoom();
                var targetZoom = currZoom * gesture.scale;

                var dZoom = targetZoom - currZoom;
                dZoom *= this.syos.settings.get('touchZoomFactor');

                var center = {
                    x: this.syos.utility.getTouchAverageForKey(gesture.touches, 'clientX'),
                    y: this.syos.utility.getTouchAverageForKey(gesture.touches, 'clientY')
                };

                syosDispatch.trigger('control:smartZoom', dZoom, center.x, center.y);

                this.lastPinchCenter = gesture.center;
            }).debounce(500),

            didDoubleTap: function (evt) {
                evt.preventDefault();
                evt.stopPropagation();

                var gesture = evt.gesture;
                syosDispatch.trigger('touch:didDoubleTap', evt.gesture);
            },

            didDrag: function (evt) {
                evt.preventDefault();
                evt.stopPropagation();
                evt.gesture.preventDefault();
                evt.gesture.stopPropagation();

                var gesture = evt.gesture;

                syosDispatch.trigger('touch:didDrag', evt.gesture);

                var dx = this.lastDragCenter.pageX - gesture.center.pageX;
                var dy = this.lastDragCenter.pageY - gesture.center.pageY;

                syosDispatch.trigger('control:smartMove', dx, dy);

                this.lastDragCenter = gesture.center;

                return false;
            },

            didDragStart: function (evt) {
                evt.preventDefault();
                evt.stopPropagation();
                evt.gesture.preventDefault();
                evt.gesture.stopPropagation();

                var gesture = evt.gesture;
                syosDispatch.trigger('touch:didDragStart', evt.gesture);

                this.lastDragCenter = gesture.center;

                return false;
            },

            didDragEnd: function (evt) {
                evt.preventDefault();
                evt.stopPropagation();
                evt.gesture.preventDefault();
                evt.gesture.stopPropagation();

                var gesture = evt.gesture;
                syosDispatch.trigger('touch:didDragEnd', gesture);

                return false;
            },


            getDefaultCenter: function () {
                return {
                    pageX: 0,
                    pageY: 0
                };
            }

        };

        return Touch;
    }();

    //#endregion

    //#region Keyboard

    SYOS.Keyboard = (function () {

        var Controller = SYOSView.extend({

            initialize: function () {
                this.active = false;
                $(document).on('keydown', _(this.didKeyup).bind(this));
            },

            events: {
                "mouseover": "didMouseOver",
                "mouseout": "didMouseOut"
            },

            didMouseOver: function () {
                this.active = true;
            },

            didMouseOut: function () {
                this.active = false;
            },

            didKeyup: function (evt) {
                if (this.active) {
                    var key = evt.keyCode.toString();

                    if (key in this.keycodeAliases) {
                        key = this.keycodeAliases[key];
                    }

                    if (key in this.keycodeActions) {
                        this.keycodeActions[key].apply(this, [evt]);
                    }
                }
            },

            keycodeAliases: {
                "107": "187",
                "109": "189"
            },

            keycodeActions: {

                // #region arrow keys

                "37": function (evt) {
                    evt.preventDefault();
                    evt.returnValue = false;
                    syosDispatch.trigger('control:moveLeft');
                },

                "38": function (evt) {
                    evt.preventDefault();
                    evt.returnValue = false;
                    syosDispatch.trigger('control:moveUp');
                },

                "39": function (evt) {
                    evt.preventDefault();
                    evt.returnValue = false;
                    syosDispatch.trigger('control:moveRight');
                },

                "40": function (evt) {
                    evt.preventDefault();
                    evt.returnValue = false;
                    syosDispatch.trigger('control:moveDown');
                },

                // #endregion

                // #region zoom

                "187": function (evt) {
                    var zoom = this.syos.settings.get('ZoomIncrement');
                    syosDispatch.trigger('control:smartZoom', zoom, null, null);
                },

                "189": function (evt) {
                    var zoom = -1 * this.syos.settings.get('ZoomIncrement');
                    syosDispatch.trigger('control:smartZoom', zoom, null, null);
                }

                // #endregion

            }

        });

        return {
            Controller: Controller
        };

    }());

    // #endregion

    //#region Display

    SYOS.Display = (function () {

        SYOSDisplay = SYOSModel.extend({

            defaults: {
                InFullscreen: false
            },

            initialize: function () {
                this.syos.display = this;

                this.Control = this.control = new SYOSControl();
                this.Keyboard = this.keyboard = new SYOSKeyboard();
                this.View = this.view = new SYOSDisplayView({ model: this });

                this.LoadingView = this.loadingView = new SYOSLoadingView({ model: this });

                syosDispatch.on('control:zoom', _(function (ShouldZoomIn) {
                    if (ShouldZoomIn)
                        this.zoomIn();
                    else
                        this.zoomOut();
                }).bind(this));
                syosDispatch.on('control:shouldOpenLevel', this.shouldOpenLevel, this);

                this.on("change:InFullscreen", _(function () {
                    this.syos.settings.set({
                        InFullscreen: this.get("InFullscreen")
                    });
                }).bind(this));

                syosDispatch.on("levelDidOpen", this.levelDidOpen, this);
            },

            shouldOpenLevel: function (level) {
                syosDispatch.trigger("showLoadingMessage", { text: "Loading Level..." });
                level.showLevel();
            },

            levelDidOpen: function () {
                syosDispatch.trigger('display:willShowMap');
                this.view.toolsView && this.view.toolsView.present();
                return this;
            },

            shouldChangeLevels: function () {
                syosDispatch.trigger('display:willShowLevelSelection');
                this.view.toolsView && this.view.toolsView.dismiss();
                this.view.levelSelection && this.view.levelSelection.present();
                return this;
            },

            zoomIn: function () {
                //var zInc = this.syos.settings.get("ZoomIncrement");
                //syosDispatch.trigger('control:smartZoom', zInc);
                syosDispatch.trigger('control:smartZoom', 1);
            },

            zoomOut: function () {
                //var zInc = this.syos.settings.get("ZoomIncrement");
                //syosDispatch.trigger('control:smartZoom', -zInc);
                syosDispatch.trigger('control:smartZoom', -1);
            }

        });

        SYOSDisplayView = SYOSView.extend({
            __bbType: "SYOSDisplayView",

            template: Handlebars.templates["display.html"],

            initialize: function () {
                var _this = this;
                this.setElement(document.getElementById('syos'));

                this.initColors();
                this.initSeatViews();
                this.initLegend();
                this.initLevelMap();

                this.HasAutoscrolled = false;

                if (this.syos.config.touchMode) {
                    this.initTouch();
                }

                this.levelSwitch = new SYOSLevelSwitchView();

                syosDispatch.on('control:zoom', function (ShouldZoomIn) {
                    _this.attemptAutoscroll();
                });
                syosDispatch.on('map:seatSelected', this.attemptAutoscroll, this);

                syosDispatch.on('control:toggleFullscreen', this.toggleFullscreen, this);

                syosDispatch.trigger('display:didInit', this);

                this.render();
                var onWindowResize = _(this.sizeViewport).bind(this);
                $(window).resize(onWindowResize);

                if (this.syos.settings.get('useAutoSizing'))
                    $(window).off('resize');
            },

            toggleFullscreen: function () {
                var _this = this;
                $('#syos').fadeOut(600, function () {
                    $('#syos').toggleClass("fixedscreen");
                    $('#syos').toggleClass("fullscreen");
                    $("#syos").fadeIn(600, function () {
                        if (_this.model.get("InFullscreen"))
                            _this.$el.find("[data-bb-event='toggle-fullscreen']").html("Close Fullscreen");
                        else
                            _this.$el.find("[data-bb-event='toggle-fullscreen']").html("Fullscreen");
                    });
                });

                _this.model.set({
                    InFullscreen: !_this.model.get("InFullscreen")
                });


                return _this;
            },

            initSeatViews: function () {
                this.seatViews = new SYOSSeatViewLayer();
            },

            initColors: function () {
                this.colors = new SYOSSeatColorCollection();
                this.colors.add(_(this.syos.rootData.SeatColors).values());
                return this;
            },

            initTouch: function () {
                this.touch = new SYOS.Touch({ el: this.el });
            },

            initLevelMap: function () {
                var levelMapEl = this.$el.find('#chooseLevel')[0];
                if (this.syos.settings.get('levelSelectMap')) {
                    this.levelSelection = new SYOSLevelSelectionView({ el: levelMapEl });
                    this.initLevelSummary();
                }
            },

            initLevelSummary: function () {
                this.levelSummary = new SYOSLevelSummary();
            },

            initToolsView: function () {
                if (this.toolsView) {
                    this.toolsView.remove();
                }
                this.toolsView = new SYOSToolsView({ el: this.$el.find('#syosTools')[0] });
            },

            events: {
                "selectstart": "didStartSelect"
            },

            didStartSelect: function (evt) {
                evt.preventDefault();
                return this;
            },

            initLegend: function () {
                this.legend = new SYOSLegend();
                this.legend.addSeatColors(this.colors);
                this.el.appendChild(this.legend.render().el);
                return this;
            },

            attemptAutoscroll: function () {
                var _this = this;
                var $window = $(window);
                var threshold = this.$el.offset().top + this.$el.height();
                var windowBottom = $window.scrollTop() + $window.height();

                if (windowBottom < threshold && _this.HasAutoscrolled === false) {
                    var scrollPos = _this.$el.offset().top * 0.9;
                    $('body').animate({
                        'scrollTop': scrollPos + 'px'
                    });
                    _this.HasAutoscrolled = true;
                }
            },

            centerOnScreen: function () {
                this.$el.css("position", "absolute");
                this.$el.css("top", Math.max(0, (($(window).height() - this.outerHeight()) / 2) + $(window).scrollTop()) + "px");
                this.$el.css("left", Math.max(0, (($(window).width() - this.outerWidth()) / 2) + $(window).scrollLeft()) + "px");
            },

            loadLevelDropdown: function () {
                this.levelDropdown = new SYOSLevelDropdown();
            },

            sizeViewport: function () {
                var canvasHeight = this.syos.utility.getCanvasHeight();
                this.$el.find('.syos-canvas').css('height', canvasHeight + 'px');
            },

            viewModel: function () {
                return {
                    showLevelMap: this.syos.settings.get('levelSelectMap'),
                    showToolsView: this.syos.settings.get('toolsMenu'),
                    levelViews: this.syos.rootData.LevelViews,
                    levelClass: this.getLevelClassName()
                };
            },

            getLevelClassName: function () {
                var venueName = this.syos.performance.venue.get('name');
                venueName = venueName.split(" ")[0];
                venueName = venueName.toLowerCase();
                return venueName;
            },

            render: function () {
                SYOSView.prototype.render.apply(this, arguments);

                this.el.appendChild(this.legend.render().el);
                this.seatViews.setElement(this.$el.find('#seatsWrap')[0]);

                this.levelSwitch.setElement(this.$el.find('#changeLevel')[0]);
                this.levelSwitch.render();

                var levelSelectionElement = this.$el.find('#chooseLevel')[0];
                if (levelSelectionElement) {
                    this.levelSelection.setElement(levelSelectionElement);
                }

                this.initToolsView();

                if (this.levelSummary) {
                    this.levelSummary.setElement(this.$el.find('#levelSummary')[0]);
                    this.levelSummary.render();
                }

                if (this.levelDropdown) {
                    this.$el.find('#changeLevels').hide().after(this.levelDropdown.el);
                }

                this.sizeViewport();

                return this;
            }
        });

        SYOSDisplaySubview = SYOSView.extend({

            present: function () {
                if (!this.deactivated) {
                    this.$el.fadeIn();
                }
                return this;
            },

            dismiss: function () {
                this.$el.fadeOut();
                return this;
            },

            openLevel: function (level) {
                syosDispatch.trigger("showLoadingMessage", { text: "Loading Level..." });
                level.showLevel();
                return this;
            },

            deactivate: function () {
                this.deactivated = true;
                this.dismiss();
            }

        });

        SYOSControl = Backbone.Model.extend({
            initialize: function () {
                // scroll events
//                syosDispatch.on('scroll:up', function () {
//                    syosDispatch.trigger('control:zoom', true);
//                });
//                syosDispatch.on('scroll:down', function () {
//                    syosDispatch.trigger('control:zoom', false);
//                });
            }
        });

        SYOSKeyboard = SYOSModel.extend({

            initialize: function () {
                var _this = this;

                $(window).on('keyup', function (event) {
                    _this.sendEventForKeycode(event.keyCode);
                });

                //if (!this.syos.settings.get("IE8")) {
                $(document).on('mousewheel', '.syos-canvas', _(function (evt) {
                    var zoom = this.syos.settings.get('ZoomIncrement');
                    zoom = 1;
                    evt.preventDefault();
                    //Huiyuan
                    var delta = evt.originalEvent.wheelDelta || -(evt.originalEvent.detail);
                    if (delta && delta <= 0)
                        zoom = -zoom;

                    var mousex = evt.offsetX || evt.originalEvent.layerX;
                    var mousey = evt.offsetY || evt.originalEvent.layerY;
                    console.log("mouse xy not tap: " + mousex + ", " + mousey);
                    syosDispatch.trigger('control:smartZoom', zoom, mousex, mousey);
                    //end

                }).bind(this));
                //}
            },

            sendEventForKeycode: function (KeyCode) {
                var key;

                switch (KeyCode) {
                    case 27:
                        key = 'escape';
                        break;
                    case 37:
                        key = 'arrowLeft';
                        break;
                    case 38:
                        key = 'arrowUp';
                        break;
                    case 39:
                        key = 'arrowRight';
                        break;
                    case 40:
                        key = 'arrowDown';
                        break;
                }

                if (key)
                    syosDispatch.trigger('keyboard:' + key);
            }
        });

        SYOSToolsView = SYOSDisplaySubview.extend({

            initialize: function () {
                var _this = this;
                this.setElement($('#syosTools'));

                if (!this.syos.settings.get('showFullscreen')) {
                    this.$el.find('[data-bb-event="toggle-fullscreen"]').hide();
                }

                this.$el.find("#zoomSlider").slider({
                    value: this.syos.utility.getCurrentZoom(),
                    orientation: 'vertical',
                    max: this.syos.settings.get("ZoomMax"),
                    min: this.syos.settings.get("ZoomMin"),
                    step: this.syos.settings.get("ZoomIncrement"),
                    slide: function (event, ui) {
                        this.syos.settings.set({ CurrentZoom: ui.value });
                    }
                });

                this.$el.find("#zoomSlider").slider("value", this.syos.utility.getCurrentZoom());

                syosDispatch.on("levelDidOpen", function (Level) {
                    if (Level.SeatViews.length === 0)
                        $("#toggleView").hide();
                    else
                        $("#toggleView").show();
                });

                this.syos.settings.on("change:CurrentZoom", function () {
                    _this.$el.find("#zoomSlider").slider("value", this.syos.utility.getCurrentZoom());
                });

                return _this;
            },

            events: {
                "click .zoomIn": "zoomIn",
                "click .zoomOut": "zoomOut",
                "tap .zoomIn": "zoomIn",
                "tap .zoomOut": "zoomOut",
                "click [data-bb-event='toggle-fullscreen']": "toggleFullscreen",
                "click [data-bb-event='toggle-seat-view']": "toggleSeatViews",
                "click #moveN": "moveUp",
                "click #moveE": "moveRight",
                "click #moveS": "moveDown",
                "click #moveW": "moveLeft"
            },

            moveUp: function () {
                syosDispatch.trigger("control:moveUp");
            },

            moveRight: function () {
                syosDispatch.trigger("control:moveRight");
            },

            moveDown: function () {
                syosDispatch.trigger("control:moveDown");
            },

            moveLeft: function () {
                syosDispatch.trigger("control:moveLeft");
            },

            zoomIn: function () {
                syosDispatch.trigger("control:zoom", true);
            },

            zoomOut: function () {
                syosDispatch.trigger("control:zoom", false);
            },

            toggleFullscreen: function () {
                syosDispatch.trigger('control:toggleFullscreen');
            },

            toggleSeatViews: function (event) {
                syosDispatch.trigger('toggleSeatViews');
                event.preventDefault();
                return this;
            }
        });

        SYOSLevelSwitchView = SYOSDisplaySubview.extend({
            __bbType: "SYOSLevelSwitchView",

            template: Handlebars.templates["levelSwitch.html"],

            initialize: function () {

                this.listenTo(syosDispatch, 'core:didChangeLevelPricing', this.didChangePricing, this);

                syosDispatch.on('display:willShowLevelSelection', this.dismiss, this);
                syosDispatch.on('display:willShowMap', this.present, this);
                syosDispatch.on("levelDidOpen", this.render, this);
            },

            events: {
                "click [data-bb-event='change-levels']": 'didClickChangeLevels',
                "change [data-bb='changeLevelSelect']": "didChangeLevelSelect"
            },

            didChangeLevelSelect: function (evt) {
                evt.preventDefault();
                var levelName = evt.currentTarget.value;
                var targetLevel = this.syos.levels.detect(function (level) {
                    return level.get('LevelName') === levelName;
                });
                targetLevel && this.openLevel(targetLevel);
            },

            didChangePricing: function () {
                console.log("Updating level selection prices...");
                this.syos.display.view.levelSummary.render();
            },

            didClickChangeLevels: function (evt) {
                evt.preventDefault();
                this.syos.display.shouldChangeLevels();
            },

            render: function () {
                this.el.innerHTML = this.template(this);
                var activeLevelName = this.syos.activeLevel && this.syos.activeLevel.get('LevelName');
                this.$el.find('#changeLevelSelect').val((activeLevelName || ""));
                syosDispatch.trigger('display:finishLevelSwitch');
                return this;
            }

        });

        SYOSLevelSelectionView = SYOSDisplaySubview.extend({

            initialize: function () {
                syosDispatch.on('display:levelDidOpen', this.didOpenLevel, this);

                this.$el.bind('mousewheel', function (event) {
                    event.stopPropagation();
                });
            },

            events: {
                "mouseenter area": "highlightLevel",
                "click area": "showLevel"
            },

            didOpenLevel: function () {
                this.dismiss();
            },

            highlightLevel: function (event) {
                var _this = this;

                var thisLevel = $(event.target).attr('href').replace('#level', '');
                $('#chooseCommand').fadeOut(150);
                $('.houseView_level' + thisLevel).show().siblings('img:not(#houseView)').hide();
                _this.$el.find('li').eq(thisLevel - 1).fadeIn(150).siblings().fadeOut(150);

                return _this;
            },

            showLevel: function (event) {
                var _this = this;
                event.preventDefault();
                syosDispatch.trigger("willShowLevel");
                syosDispatch.trigger("showLoadingMessage", { text: "Loading Level..." });
                var LevelIndex = $(event.currentTarget).index();
                var levelId = this.syos.rootData.LevelViews[LevelIndex].LevelId;

                console.log("Showing level...");
                var SelectedLevel = this.syos.levels.detect(function (Level) {
                    return parseInt(Level.get("LevelNumber"), 10) === levelId;
                });
                _this.dismiss();
                SelectedLevel.showLevel();
                return _this;
            }

        });

        SYOSLoadingView = SYOSDisplaySubview.extend({

            defaultOptions: {
                loadingText: "Loading...",
                showSeats: true
            },

            initialize: function () {
                this.setElement(document.getElementById('syosLoading'));
                this.$el.hide();

                syosDispatch.on("showLoadingMessage", this.handleLoadingMessageRequest, this);

                syosDispatch.on("hideLoadingMessage", this.hideLoadingMessage, this);

                return this;
            },

            hideLoadingMessage: function () {
                this.dismiss();
                $('#syos-full-loading').each(function () {
                    $(this).hide();
                });
                return this;
            },

            // Responds to requests sent through the dispatcher
            handleLoadingMessageRequest: function (options) {
                _(options).defaults(this.defaultOptions);

                this.$el.find("#syos-loading-text").text(options.text);

                this.present();

                if (options.showSeats) {
                    this.$el.find('img').show();
                }
                else {
                    this.$el.find('img').hide();
                }

                $('#syos-full-loading').each(function () {
                    $(this).show();
                });

                return this;
            }
        });

        SYOSModal = SYOSView.extend({
            template: Handlebars.templates["modal.html"],

            defaultOptions: function () {
                return {
                    CanDismiss: true,
                    Height: this.syos.settings.get('modalDefaultHeight'),
                    Width: this.syos.settings.get('modalDefaultWidth')
                }
            },

            initialize: function () {
                _.defaults(this.options, this.defaultOptions());

                this.setElement(document.getElementById('syos-modal'));
                this.$overlay = $('#syos-modal-overlay');

                this.render().show();

                syosDispatch.on('keyboard:escape', this.close, this);

                return this;
            },

            events: {
                "click [data-bb-event='close']": "close",
                "click [data-bb-event='action']": "actionHandler",
                "mousewheel": "stopProp"
            },

            /**
            @method actionHandler
            **/
            actionHandler: function (event) {
                var ActionIndex = $(event.currentTarget).index();
                if (this.options.Actions)
                    this.options.Actions[ActionIndex].Fn.call(null, this);
                else
                    console.error("Actions are undefined for modal!");
                return this;
            },

            show: function () {
                this.$overlay.fadeIn();
                this.$el.fadeIn();
                return this;
            },

            close: function () {
                this.$el.fadeOut();
                this.$overlay.fadeOut();
                return this;
            },

            render: function () {
                this.el.innerHTML = this.template(this.options);

                var LSMargin = (100 - this.options.Height) / 2;
                var PTMargin = (100 - this.options.Width) / 2;

                this.$el.css({
                    width: this.options.Width + "%",
                    height: this.options.Height + "%",
                    top: LSMargin + "%",
                    left: PTMargin + "%"
                });

                return this;
            }
        });

        SYOSSeatTooltip = SYOSView.extend({

            className: 'syos-seat-tooltip',

            positionAdjustment: {
                x: 0,
                y: 0
            },

            adjustCoordinates: function (coords) {
                var newCoords = _(coords).clone();
                newCoords.x += this.positionAdjustment.x;
                newCoords.y += this.positionAdjustment.y;

                return newCoords;
            },

            positionToCircle: function (circle) {
                var circlePosition = this.syos.canvasMap.getPositionForCircle(circle);
                var position = this.adjustCoordinates(circlePosition);
                this.$el.css({
                    top: position.y,
                    left: position.x
                });
            },

            positionToSeat: function (seat) {
                var circle = seat.mapCircle;
                this.positionToCircle(circle);
            }

        });

        SYOSLegend = SYOSView.extend({
            className: "syos-legend",

            template: Handlebars.templates["legend.html"],

            initialize: function () {
                this.legendItems = new SYOSLegendItemCollection();
                this.legendItems.on('add remove change', this.render, this);
            },

            addLegendItemWithColorAndText: function (color, text) {
                var color = color || '#000';
                var text = text || 'legend item text';
                this.legendItems.add({
                    color: color,
                    text: text
                });
            },

            addSeatColors: function (seatColors) {
                var defaultItems = seatColors.map(this.seatColorMapping, this);
                defaultItems = _(defaultItems).compact();
                this.legendItems.add(defaultItems);
            },

            viewModel: function () {
                return {
                    items: this.legendItems.toJSON()
                };
            },

            seatColorMapping: function (seatColor) {
                var legendItem = new SYOSLegendItem({
                    color: seatColor.get('Color'),
                    text: seatColor.get('Description')
                });
                if (!!seatColor.get('Description')) {
                    return legendItem;
                }
            },

            reset: function () {
                this.legendItems.reset();
                this.render();
            },

            render: function () {
                this.el.innerHTML = this.template(this.viewModel());
                return this;
            }

        });

        SYOSLegendItem = SYOSModel.extend({
        });

        SYOSLegendItemCollection = SYOSCollection.extend({

            model: SYOSLegendItem

        });

        SYOSLevelSummary = SYOSView.extend({
            __bbType: "SYOSLevelSummary",

            template: Handlebars.templates["levelSummary.html"],

            initialize: function () {
                this.syos.levels.on('change', this.render, this);
                this.render();
                return this;
            },

            viewModel: function () {
                return this.syos.levels.map(function (level) {
                    var levelInfo = level.toJSON();
                    var levelClassName = level.get('LevelName').replace(/ /g, "");
                    _(levelInfo).extend({
                        levelClassName: levelClassName,
                        priceRangeString: level.priceRangeString()
                    });
                    return levelInfo;
                });
            }

        });

        SYOSStatusView = SYOSView.extend({
            __bbType: "SYOSStatusView",

            className: 'syos-status-view',

            initialize: function () {
                this.shown = false;
                this.hide();

                this.statusElement = document.createElement('span');
                this.el.appendChild(this.statusElement);
                this.statusElement.innerText = "Status Message";

                syosDispatch.on('display:shouldUpdateStatus', this.shouldUpdateStatus, this);
            },

            shouldUpdateStatus: function (statusText, visibleTime) {
                var visibleTime = visibleTime || 1000;

                this.showStatus();
                this.statusElement.innerText = statusText;
                this.setDelayedHide(visibleTime);
            },

            setDelayedHide: function (visibleTime) {
                this.hideTimeout = setTimeout(_(this.hideStatus).bind(this), visibleTime);
            },

            showStatus: function () {
                if (!this.shown) {
                    this.$el.show();
                    this.shown = true;
                }
            },

            hideStatus: function () {
                if (this.shown) {
                    this.$el.hide();
                    this.shown = false;
                }
            }

        });

        SYOSSeatViewLayer = SYOSView.extend({

            initialize: function () {
                this.seatViewIcons = [];

                this.showing = false;

                syosDispatch.on('levelDidOpen', this.hideSeatViews, this);
                syosDispatch.on('toggleSeatViews', this.toggleSeatViews, this);
            },

            toggleSeatViews: function () {
                this.showing ? this.hideSeatViews() : this.showSeatViews();
                return this;
            },

            showSeatViews: function () {
                this.showing = true;
                this.syos.activeLevel.seatViews.each(function (seatView) {
                    this.el.appendChild(this._createIconFromView(seatView).el);
                }, this);
                return this;
            },

            hideSeatViews: function () {
                this.showing = false;
                _(this.seatViewIcons).each(this._removeSeatViewIcon, this);
                return this;
            },

            _removeSeatViewIcon: function (seatViewIcon) {
                this.seatViewIcons = _(this.seatViewIcons).without(seatViewIcon);
                seatViewIcon.remove();
            },

            _createIconFromView: function (seatView) {
                var icon = new SYOSSeatViewIcon({ model: seatView });
                this.seatViewIcons.push(icon)
                return icon;
            }
        });

        SYOSSeatViewIcon = SYOSView.extend({
            tagName: "img",
            className: "syos-seat-view-icon",

            events: {
                "click": "showSeatView"
            },

            initialize: function () {
                var currentZoom = this.syos.utility.getCurrentZoom();
                this._position();
                syosDispatch.on('map:didUpdateState', this._position, this);
                syosDispatch.on("control:smartZoom", this._position, this);
                syosDispatch.on("control:smartMove", this._position, this);
                syosDispatch.on("map:dragged", this._position, this);
                syosDispatch.on("map:willDrag", this.fadeOut, this);
            },

            _position: function () {
                var pos = this.syos.utility.scaleCoordinates(this.model.get('X'), this.model.get('Y'));
                var offset = this.syos.canvasMap.getCircleMapOffset();
                pos.x += offset.x;
                pos.y += offset.y;

                this.el.setAttribute('src', this.syos.settings.get('seatViewIconPath'));
                this.el.style.top = pos.y + "px";
                this.el.style.left = pos.x + "px";
            },

            fadeOut: function () {
                this.$el.hide();
                syosDispatch.on("map:dragged", this.fadeIn, this);
            },

            fadeIn: function () {
                this.$el.show('drop', { duration: 150, direction: 'down' });
                syosDispatch.off("map:dragged", this.fadeIn, this);
            },

            showSeatView: function (evt) {
                console.log("SeatViewIcon: showing seat view modal...");
                this.modal = new SYOSModal({
                    Title: "View from Seats",
                    Width: this.syos.settings.get('seatviewWidth'),
                    Height: this.syos.settings.get('seatviewHeight'),
                    Content: "<img src='" + this.model.get("ImageUrl") + "' />"
                })
                return this;
            }

        });

        SYOSEmbeddedZoom = SYOSView.extend({

            className: 'syos-embedded-zoom-wrap',

            template: Handlebars.templates['embeddedZoom.html'],

            initialize: function () {
                this.render();
            },

            events: {
                "click [data-bb=embeddedZoomIn]": "didClickZoomIn",
                "click [data-bb=embeddedZoomOut]": "didClickZoomOut",
                "tap [data-bb=embeddedZoomIn]": "didClickZoomIn",
                "tap [data-bb=embeddedZoomOut]": "didClickZoomOut"
            },

            getZoomIncrement: function () {
                var zoom = this.syos.settings.get('ZoomIncrement');
                var embedModifier = this.syos.settings.get('embeddedZoomModifier');
                return zoom * embedModifier;
            },

            didClickZoomIn: function () {
                //var zoom = this.getZoomIncrement();
                //syosDispatch.trigger('control:smartZoom', -zoom);
                // use zoom level not absolute zoom amount
                var amt = 1;
                syosDispatch.trigger('control:smartZoom', amt);
            },

            didClickZoomOut: function () {
                //var zoom = this.getZoomIncrement();
                //syosDispatch.trigger('control:smartZoom', zoom);

                var amt = -1;
                syosDispatch.trigger('control:smartZoom', amt);
            }

        });

        SYOSPopover = SYOSView.extend({
            template: Handlebars.templates['popover.html'],

            className: 'syos-popover',

            config: {
                overrideColor: '#3B5998',
                positionAdjustment: {
                    x: -15,
                    y: -40
                }
            },

            initialize: function () {
                _(this.config).extend(this.options.object);
                this.seat = this.config.seat;
                this.circle = this.seat.mapCircle;
                this.img = this.config.img !== '' ? this.config.img : '/syos/img/cute_ghost.png';
                this.bindEvents(this.config.events);

                this.updateMapCircleColor(this.circle);
                this.showPopover();
                this.positionToCircle(this.circle);
                syosDispatch.on('display:shouldUpdateStatus', this.updatePopoverPosition, this);
                syosDispatch.on('levelDidOpen', this.hidePopover, this);
            },

            updateMapCircleColor: function (circle) {
                circle.set('fillOverride', this.config.overrideColor);
            },

            bindEvents: function (events) {
                _(events).each(function (event) {
                    this.$el.on(event.name, event.fn);
                }, this);
            },

            updatePopoverPosition: function () {
                this.positionToCircle(this.circle);
            },

            showPopover: function () {
                this.render();
                this.syos.addSubview(this);
            },

            hidePopover: function () {
                this.syos.removeSubview(this);
            },

            //showPopoverEvent: function () {
            //    this.$el.show();
            //},

            //hidePopoverEvent: function () {
            //    this.$el.hide();
            //},

            adjustCoordinates: function (coords) {
                var newCoords = _(coords).clone();
                newCoords.x += this.config.positionAdjustment.x;
                newCoords.y += this.config.positionAdjustment.y;
                return newCoords;
            },

            positionToCircle: function (circle) {
                var circlePosition = this.syos.canvasMap.getPositionForCircle(circle);
                var position = this.adjustCoordinates(circlePosition);
                this.$el.css({
                    top: position.y,
                    left: position.x
                });
            },

            viewModel: function () {
                return _.extend({ img: this.img });
            }

        });

    }());

    //#endregion

    //#region Facebook

    SYOS.Facebook = (function () {
        var Controller = SYOSView.extend({
            defaults: {
                UserId: 0
            },

            initialize: function () {
                this.on('change:UserId', this.loadFacebookData, this);
                syosDispatch.on('map:didFinishLevelInit', this.loadFacebookData, this);
                syosDispatch.on('display:shouldUpdateStatus', this.updatePopoverPosition, this);
            },


            overrideColor: '#3B5998',

            loadFacebookData: function () {
                this.removeAllPopover();
                this.sharedSeats = new SYOSSeatCollection();
                this.sharedSeats.add(this.syos.activeLevel.seats.first());
                this.sharedSeats.add(this.syos.activeLevel.seats.last());

                // make service call and get seats info
                // if success, call check seats
                this.checkSeats();
            },

            removeAllPopover: function () {
                if (this.sharedSeats) {
                    this.sharedSeats.each(function (sharedSeat) {
                        if (sharedSeat.popover) {
                            sharedSeat.popover.hidePopover();
                        }
                    }, this);
                }
            },

            updatePopoverPosition: function () {
                if (this.sharedSeats) {
                    // make a popover for each seats 
                    this.sharedSeats.each(function (sharedSeat) {
                        sharedSeat.popover.positionToCircle(sharedSeat.mapCircle);
                    }, this);
                }
            },

            checkSeats: function () {
                if (this.sharedSeats) {
                    // make a popover for each seats 
                    this.sharedSeats.each(function (sharedSeat) {
                        sharedSeat.mapCircle.set('fillOverride', this.overrideColor);
                        sharedSeat.popover = new SYOSPopover();
                        sharedSeat.popover.positionToCircle(sharedSeat.mapCircle);
                    }, this);
                }
            }
        });

        return {
            Controller: Controller
        }

    }());

    // #endregion

    window.SYOS = SYOS;


})(jQuery, Backbone, _);


//#region Console fix

if (typeof console === "undefined") {
    console = {};
    console.log = function () { return this; };
}
if (typeof chrome === "undefined") {
    console.info = function () { return this; };
    console.warn = function () { return this; };
    console.error = function () { return this; };
    console.group = function () { return this; };
    console.groupCollapsed = function () { return this; };
    console.groupEnd = function () { return this; };
    console.time = function () { return this; };
    console.timeEnd = function () { return this; };
    console.trace = function () { return this; };
    console.count = function () { return this; };
    console.dirxml = function () { return this; }
}

console.style = {
    majorWarn: "color:white; background:rgba(255,0,0,0.6); font-weight:bold;"
};

//#endregion

//#region Handlebars Extension

Handlebars.registerHelper('log', function (obj) {
    console.log("Handlebars", obj);
});

Handlebars.registerHelper('call', function (method, context) {
    return method.call(context);
});

Handlebars.registerHelper('formattedPrice', function (priceInt) {
    priceInt = isNaN(priceInt) || priceInt === '' || priceInt === null ? 0.00 : priceInt;
    var formatted = parseFloat(priceInt).toFixed(2);
    return formatted;
});

Handlebars.registerHelper('paraFormattedPrice', function (priceInt) {
    priceInt = isNaN(priceInt) || priceInt === '' || priceInt === null ? 0.00 : priceInt;
    var priceFloat = parseFloat(priceInt);
    var isNegative = (priceFloat < 0);
    var baseValue = Math.abs(priceFloat).toFixed(2);
    if (isNegative) {
        return "($" + baseValue + ")";
    }
    else {
        return "$" + baseValue;
    }

});

//#endregion

// #region SYOS Ready

if (typeof syosOnReady === "undefined") {
    syosOnReady = function () {
        console.log('syosOnReady not overwritten \n');
    }
}

syosOnReady();

// #endregion


SYOS.Canvas = (function () {

    var canvasDebug = location.href.match(/canvasdebug/);

    var Map = SYOSView.extend({

        initialize: function () {
            this.setElement(document.getElementById('seatsWrap'));
            this.mousePosition = new SYOS.Geometry.Coordinates();
            this.mapOffset = new SYOS.Geometry.Coordinates();

            this.$circleMap = this.$el.find('#circleMap');

            this.initSeatMap();
            this.initEmbeddedZoom();

            if (this.syos.config.touchMode) {
                try {
                    this.$circleMap.draggableTouch();
                    //this.touch = new SYOS.Touch({ el: this.el });
                }
                catch (err) { console.error("Error with draggable touch - is the library included?"); }
            } else {
                this.$circleMap.draggable();
            }


            this.zoomIncrement = this.syos.settings.get('ZoomIncrement');
            this.initMetrics();

            if (canvasDebug) {
                this.initDebugLayer();
            }

            this.initEvents();
        },

        initEvents: function () {
            $(window).resize(_(this.initMetrics).bind(this));

            syosDispatch.on('levelDidOpen', this.levelDidOpen, this);
            syosDispatch.on('buymode:addSeatToCart', this.didAddSeatToCart, this);
            syosDispatch.on('buymode:removeSeatFromCart', this.didRemoveSeatFromCart, this);

            syosDispatch.on("control:moveUp", this.moveUp, this);
            syosDispatch.on("control:moveLeft", this.moveLeft, this);
            syosDispatch.on("control:moveDown", this.moveDown, this);
            syosDispatch.on("control:moveRight", this.moveRight, this);

            syosDispatch.on("control:smartZoom", this.smartZoom, this);
            syosDispatch.on("control:smartMove", this.smartMove, this);

            //syosDispatch.on("touch:didDrag", this.didTouchDrag, this);
            //syosDispatch.on("touch:didDragEnd", this.didTouchDragEnd, this);
        },

        initMetrics: function () {
            this.width = this.$el.width();
            this.height = this.$el.height();
        },

        initDebugLayer: function () {
            var canvas = document.createElement('canvas');
            canvas.setAttribute('class', 'syos-debug-layer');
            this.debugLayer = new DebugLayer({ el: canvas });
            this.el.appendChild(this.debugLayer.el);
        },

        initSeats: function () {
            this.seatMap.circles.reset();
            this.syos.activeLevel.seats.each(function (seat) {
                seat.mapCircle = new Circle(this.getCirclePropertiesForSeat(seat));
                this.seatMap.circles.add(seat.mapCircle);
            }, this);
        },

        initGAZones: function () {
            this.seatMap.gaPolygons.reset();
            this.syos.activeLevel.gaZones.each(function (gaZone) {
                gaZone.mapPolygon = new Polygon(this.getPolygonPropertiesForGAZone(gaZone));
                this.seatMap.gaPolygons.add(gaZone.mapPolygon);
            }, this);
        },

        initEmbeddedZoom: function () {
            this.embeddedZoom = new SYOSEmbeddedZoom();
            this.addSubview(this.embeddedZoom);
        },

        initBounds: function () {
            var xArr = this.seatMap.circles.pluck('x');
            var yArr = this.seatMap.circles.pluck('y');

            this.maxX = Math.max.apply(null, xArr);
            this.maxY = Math.max.apply(null, yArr);

            this.minX = Math.min.apply(null, xArr);
            this.minY = Math.min.apply(null, yArr);

            this.boundWidth = this.maxX + this.minX;
            this.boundHeight = this.maxY + this.minY;
        },

        initSeatMap: function () {
            var canvasLayer = this.createCanvasLayer('syos-seat-canvas');
            this.seatMap = new SeatMap({ el: canvasLayer, parent: this });
            this.$circleMap.append(canvasLayer);
        },

        createCanvasLayer: function (className) {
            try {
                console.groupCollapsed('Canvas Init');
                var flashCanvas = "G_vmlCanvasManager" in window;
                var size = this.getCanvasSize();

                var canvasLayer = (function () {
                    var el = document.createElement('canvas');
                    el.setAttribute('class', className);
                    el.setAttribute('width', size.width);
                    el.setAttribute('height', size.height);
                    return el;
                }());

                if (flashCanvas) {
                    G_vmlCanvasManager.initElement(canvasLayer);
                }

                console.log('flashCanvas', flashCanvas);
                console.log('size.width', size.width);
                console.log('size.height', size.height);
                console.groupEnd();
                return canvasLayer;
            }
            catch (err) {
                if (isDevEnvironment) alert('Error creating canvas element');
                throw err;
            }
        },

        clear: function () {
            this.seatMap.circles.reset();
            //this.seatMap.renderAll();
        },

        events: {
            "mousemove": "didMousemove",
            "dragstop .syos-circle-map": "didDrag",
            "dragstart .syos-circle-map": "willDrag"
        },

        didMousemove: function (evt) {
            var offset = this.$el.offset();

            var page = {
                x: evt.pageX,
                y: evt.pageY
            };

            var mouse = {
                x: page.x - offset.left,
                y: page.y - offset.top
            };

            this.mousePosition.set({
                x: mouse.x,
                y: mouse.y
            });
        },

        willDrag: function (evt) {
            syosDispatch.trigger("map:willDrag");
        },

        didDrag: function (evt) {
            this.updateOffsetFromDrag();
            syosDispatch.trigger("map:dragged");
        },

        didTouchDrag: function (touchEvent) {
            var factor = this.syos.settings.get('mobileTouchFactor');
            var x = touchEvent.deltaX * factor;
            var y = touchEvent.deltaY * factor;
            this.$circleMap.css({
                left: "+=" + x + "px",
                top: "+=" + y + "px"
            });
        },

        didTouchDragEnd: function () {
            this.updateOffsetFromDrag();
        },

        updateOffsetFromDrag: function () {
            var $circleMap = this.$circleMap;
            var drag = $circleMap.position();
            $circleMap.fadeTo(0, 0);

            this.mapOffset.addX(drag.left);
            this.mapOffset.addY(drag.top);

            _(function () {
                $circleMap.css({ top: "0", left: "0" });
                $circleMap.fadeTo(0, 1);
            }).delay(100);
        },

        moveUp: function (amt) {
            var amt = amt || this.syos.settings.get('PanIncrement');
            this.mapOffset.addY(-amt);
            this.checkMap();
        },

        moveLeft: function (amt) {
            var amt = amt || this.syos.settings.get('PanIncrement');
            this.mapOffset.addX(amt);
            this.checkMap();
        },

        moveDown: function (amt) {
            var amt = amt || this.syos.settings.get('PanIncrement');
            this.mapOffset.addY(amt);
            this.checkMap();
        },

        moveRight: function (amt) {
            var amt = amt || this.syos.settings.get('PanIncrement');
            this.mapOffset.addX(-amt);
            this.checkMap();
        },

        smartZoom: function (amt, x, y) {
            var x = x || this.getCanvasSize().width / 2;
            var y = y || this.getCanvasSize().height / 2;
            var amount = (amt || this.zoomIncrement);

            var oldZoom = this.syos.utility.getCurrentZoom();
            this.syos.settings.adjustZoom(amount);
            var currentZoom = this.syos.utility.getCurrentZoom();

            var tx = this.syos.utility.getZoomPointCoordinate(oldZoom, currentZoom, x, 1 * this.mapOffset.get('x'));
            var ty = this.syos.utility.getZoomPointCoordinate(oldZoom, currentZoom, y, 1 * this.mapOffset.get('y'));

            this.mapOffset.set({ x: tx, y: ty });

            this.checkMap();
        },

        centerOnPoint: function (x, y) {
            var ut = this.syos.utility;
            var levelHeight = this.syos.settings.get('levelImageHeight');
            var levelWidth = this.syos.settings.get('levelImageWidth');

            var target = {
                x: x || levelWidth * 0.5,
                y: y || levelHeight * 0.5
            };
            var center = {
                x: this.$el.width() * 0.5,
                y: this.$el.height() * 0.5
            };

            var delta = {
                x: center.x - target.x,
                y: center.y - target.y
            };

            console.log(target, center, delta);

            this.smartMove(delta.x, delta.y);

        },

        centerOnPoint2: function (x, y) {
            var bgHeight = this.syos.settings.get('levelImageHeight');
            var bgWidth = this.syos.settings.get('levelImageWidth');


            var target = {
                x: x || bgWidth * 0.5 * this.syos.utility.getCurrentZoom(),
                y: y || bgHeight * 0.5 * this.syos.utility.getCurrentZoom()
            };
            var center = {
                x: this.$el.width() * 0.5,
                y: this.$el.height() * 0.5
            };

            var delta = {
                x: center.x - target.x,
                y: center.y - target.y
            };

            var mapOffset = this.syos.canvasMap.mapOffset;

            this.defaultMapOffset = {
                x: mapOffset.get('x') + delta.x,
                y: mapOffset.get('y') + delta.y
            };

            console.log(target, center, delta);
            this.syos.canvasMap.mapOffset.set({ x: this.defaultMapOffset.x, y: this.defaultMapOffset.y });
        },

        smartMove: function (dx, dy) {
            var target = this.mapOffset.toJSON();
            target.x -= dx;
            target.y -= dy;
            this.mapOffset.set(target);

            this.checkMap();
        },

        sizeToElement: function () {
            var levelPadding = this.syos.settings.get('levelImagePadding');

            var targetWidth = this.$el.width();
            var bgWidth = this.syos.settings.get('levelImageWidth');
            //bgWidth = bgWidth + (levelPadding * 2);
            var targetXScale = targetWidth / bgWidth;

            var targetHeight = this.$el.height();
            var bgHeight = this.syos.settings.get('levelImageHeight');
            //bgHeight = bgHeight + (levelPadding * 2);
            var targetYScale = targetHeight / bgHeight;

            var targetScale = Math.min(targetXScale, targetYScale);

            //this.$circleMap[0].style.top = levelPadding + "px";
            //this.$circleMap[0].style.left = levelPadding + "px";
            this.syos.settings.set("CurrentZoom", targetScale);
            // test for getting initial zoom for reset button Huiyuan

            syosDispatch.trigger('didFinishCurrentZoom');
        },

        levelDidOpen: function () {
            this.seatMap.allowRendering = false;
            this.initSeats();
            this.initGAZones();
            this.initBounds();
            this.seatMap.clear();
            this.mapOffset.set({ x: 0, y: 0 });
            this.seatMap.resetPosition();
            this.sizeToElement();
            this.seatMap.allowRendering = true;
            this.seatMap.renderAll();

            var newBackground = this.syos.activeLevel.getBackgroundImageUrl();
            this.seatMap.updateBackground(newBackground);

            syosDispatch.trigger('hideLoadingMessage');
            syosDispatch.trigger('map:didFinishLevelInit');
        },

        didAddSeatToCart: function (seat) {
            seat.mapCircle.set('inCart', true);
        },

        didRemoveSeatFromCart: function (seat) {
            seat.set('IsInCart', false);
            seat.mapCircle.set('inCart', false);
        },

        checkMap: function () {
            var zoom = this.syos.utility.getCurrentZoom();

            var maxX = zoom * this.boundWidth;
            var maxY = zoom * this.boundHeight;

            var canvasX = parseInt(this.seatMap.el.style.left.replace(/px/, ''));
            var canvasY = parseInt(this.seatMap.el.style.top.replace(/px/, ''));

            if (canvasX < -maxX) {
                this.seatMap.el.style.left = "-" + maxX + "px";
            }
            else if (canvasX > maxX) {
                this.seatMap.el.style.left = maxX + "px";
            }
            if (canvasY < -maxY) {
                this.seatMap.el.style.top = "-" + maxY + "px";
            }
            else if (canvasY > maxY) {
                this.seatMap.el.style.top = maxY + "px";
            }
        },

        getCircleMapOffset: function () {
            this.adjustMapOffset();
            return this.mapOffset.toJSON();
        },

        adjustMapOffset: function () {
            var currentZoom = this.syos.utility.getCurrentZoom();

            var mapOffsetX = this.mapOffset.get('x');
            var mapOffsetY = this.mapOffset.get('y');

            var maxX = this.maxX * currentZoom + mapOffsetX;
            var maxY = this.maxY * currentZoom + mapOffsetY;
            var minX = this.minX * currentZoom + mapOffsetX;
            var minY = this.minY * currentZoom + mapOffsetY;

            var canvasSize = this.getCanvasSize();
            var offsetX = this.syos.settings.get('boundaryOffsetX');
            var offsetY = this.syos.settings.get('boundaryOffsetY');

            this.mapOffset.set('x', Math.min(Math.max(mapOffsetX, -(this.maxX * currentZoom) + offsetX),
                canvasSize.width - (this.minX * currentZoom) - offsetX));

            this.mapOffset.set('y', Math.min(Math.max(mapOffsetY, -(this.maxY * currentZoom) + offsetY),
                canvasSize.height - (this.minY * currentZoom) - offsetY));

        },

        getCanvasSize: function () {
            return {
                width: this.$el.width(),
                height: this.$el.height()
            };
        },

        getPositionForCircle: function (circle) {
            var circleAttrs = circle.scaledAttributes();
            var offset = this.getCircleMapOffset();
            return {
                x: circleAttrs.x + offset.x,
                y: circleAttrs.y + offset.y
            };
        },

        getPositionForPolygon: function (polygon) {
            // pointing to the first point
            var polygonAttrs = polygon.scaledAttributes();
            var offset = this.getCircleMapOffset();
            return {
                x: polygonAttrs.vertx[0] + offset.x,
                y: polygonAttrs.verty[0] + offset.y
            };
        },

        getCirclePropertiesForSeat: function (seat) {
            var overlay = "";

            var seatRadius = this.syos.settings.get('seatRadius');
            var seatText = null;

            if (seat.seatType) {
                overlay = seat.seatType.get('Overlay');
            }

            if (this.syos.settings.get('showSeatNumbers')) {
                seatText = seat.get('NumberText');
            }

            return {
                id: seat.id,
                active: seat.get('IsAvailable'),
                inCart: seat.get('IsInCart'),
                overlay: overlay,
                x: seat.attributes.Circle.X,
                y: seat.attributes.Circle.Y,
                radius: seatRadius,
                text: seatText
            };
        },

        getPolygonPropertiesForGAZone: function (gaZone) {
            var polygonSize = _(gaZone.get('Polygon').Points).size();

            var polygonPointsCollection = _(gaZone.get('Polygon').Points).map(function (startPoint, idx) {
                var endPointIdx = (idx + 1) % polygonSize;

                var endPoint = gaZone.get('Polygon').Points[endPointIdx];

                startPoint.middlePoint = {
                    x: (startPoint.x + endPoint.x) / 2,
                    y: (startPoint.y + endPoint.y) / 2
                };

                if (startPoint.r !== 0) {
                    var idx_next = (idx + 2) % polygonSize;
                    var nextPoint = gaZone.get('Polygon').Points[idx_next];
                    var radius = startPoint.r;

                    var opts = {
                        startPoint: startPoint,
                        endPoint: endPoint,
                        nextPoint: nextPoint,
                        radius: radius,
                        reverse: startPoint.reverse
                    }
                    var centerPoint = this.syos.utility.getCenterPointForCircle(opts);
                    opts.centerPoint = centerPoint;

                    startPoint.centerPoint = centerPoint;
                }
                return startPoint;

            }, this);

            // get vertx and verty to calculate polygon boundary
            var vertx = _(polygonPointsCollection).map(function (point) {
                return point.x;
            }, this);
            var verty = _(polygonPointsCollection).map(function (point) {
                return point.y;
            }, this);

            return {
                id: gaZone.id,
                active: gaZone.get('IsAvailable'),
                polygonPointsCollection: polygonPointsCollection,
                availableSeats: gaZone.get('AvailableSeats'),
                totalSeats: gaZone.get('TotalSeats'),
                zoneId: gaZone.get('ZoneId'),
                vertx: vertx,
                verty: verty
            }
        },

        addSeatPopover: function (object) {
            if (object.seat && object.seat.mapCircle) {
                object.seat.popover = new SYOSPopover({ object: object });
            }
            else
                console.warn("invalid seat object.");
        }

    });

    var DebugLayer = SYOSView.extend({

        initialize: function () {
            this.ctx = this.el.getContext('2d');

            (function (style) {
                style.position = "absolute";
                style.left = 0;
                style.top = 0;
            }(this.el.style))

            syosDispatch.on('map:didFinishInit', this.mapDidFinishInit, this);
        },

        mapDidFinishInit: function () {
            this.updateSize();
            this.syos.canvasMap.mousePosition.on('change', this.draw, this);
        },

        updateSize: function () {
            var width = this.syos.canvasMap.$el.width();
            var height = this.syos.canvasMap.$el.height();
            this.el.width = width;
            this.el.height = height;
            this.draw();
        },

        draw: function () {
            this.ctx.save();

            this.drawClear();
            this.drawMouseCrosshair();

            this.ctx.restore();
        },

        drawClear: function () {
            this.el.width = this.el.width;
        },

        drawMouseCrosshair: function () {
            this.ctx.save();
            var mouse = this.syos.canvasMap.mousePosition.toJSON();

            this.ctx.lineWidth = 1;
            this.ctx.globalAlpha = 0.3;
            this.ctx.strokeStyle = "red";

            this.ctx.moveTo(mouse.x, 0);
            this.ctx.lineTo(mouse.x, this.el.height);

            this.ctx.moveTo(0, mouse.y);
            this.ctx.lineTo(this.el.width, mouse.y);

            this.ctx.stroke();
            this.ctx.restore();
        }

    });

    var Circle = SYOSModel.extend({

        defaults: {
            fillOverride: null,
            underCursor: false,
            inCart: false,
            selected: false,
            text: null
        },

        initialize: function () {
            this.overlayLoaded = false;
            var img = this.img = new Image();
            if (this.get('overlay') || !("G_vmlCanvasManager" in window)) {
                this.initOverlay();
            }
        },

        initOverlay: function () {
            this.img.onload = _(this.didLoadOverlay).bind(this);
            this.img.src = this.get('overlay');
        },

        didLoadOverlay: function () {
            this.overlayLoaded = true;
            this.trigger('didLoadOverlay');
        },

        getFill: function () {
            var colorKey = "Main";

            if (!this.get('active')) {
                if (!this.syos.settings.get('allowUnavailableOverride') || !this.get('unavailableFillOverride')) {
                    colorKey = "Unavailable";
                }
                else {
                    return this.get('unavailableFillOverride');
                }
            }
            else if (this.get('inCart')) {
                colorKey = 'Highlight';
            }
            else if (this.get('selected')) {
                colorKey = 'ZoneHighlight';
            }
            else if (this.get('underCursor')) {
                colorKey = 'ZoneHighlight';
            }
            else {
                if (this.get('fillOverride')) {
                    return this.get('fillOverride');
                }
            }

            return this.syos.activeLevel.seatColors.getColorForKey(colorKey).get('Color');
        },

        scaledAttributes: function () {
            var scaledCoords = this.syos.utility.scaleCoordinates(this.get('x'), this.get('y'));
            var scaledRadius = this.getRadius();
            return {
                x: scaledCoords.x,
                y: scaledCoords.y,
                radius: scaledRadius
            };
        },

        getRadius: function () {
            return this.syos.utility.scaleScalar(this.get('radius'));
        },

        distanceFromPoint: function (x, y) {
            var c = this.scaledAttributes();
            var horizontal = Math.pow(x - c.x, 2);
            var vertical = Math.pow(y - c.y, 2);
            return Math.sqrt(horizontal + vertical);
        },

        isPointInCircle: function (x, y, padding) {
            var padding = padding || 0;
            var radius = this.getRadius() + padding;
            return (this.distanceFromPoint(x, y) < radius);
        }

    });

    var CircleCollection = SYOSCollection.extend({

        model: Circle,

        getActiveCircles: function () {
            return this.select(function (circle) {
                return circle.get('active');
            });
        },

        getCircleForCoordinatesAndRadius: function (coords, radius) {
            var radius = radius || this.syos.settings.get('seatRadius');

            var distance = Infinity;
            var nearestCircle = null;

            var x = this.syos.utility.scaleScalar(coords.x);
            var y = this.syos.utility.scaleScalar(coords.y);

            this.each(function (circle) {
                var currentDistance = circle.distanceFromPoint(x, y);
                if (currentDistance < distance) {
                    nearestCircle = circle;
                    distance = currentDistance;
                }
            }, this);

            var adjustedRadius = this.syos.utility.scaleScalar(radius);

            if (distance < adjustedRadius) {
                return nearestCircle;
            } else {
                return null;
            }
        }

    });

    var Polygon = SYOSModel.extend({
        defaults: {
            fillOverride: null,
            underCursor: false,
            inCart: false,
            selected: false,
            text: null
        },

        initialize: function () {
            this.overlayLoaded = false;
            var img = this.img = new Image();
            if (this.get('overlay') || !("G_vmlCanvasManager" in window)) {
                this.initOverlay();
            }
        },

        initOverlay: function () {
            this.img.onload = _(this.didLoadOverlay).bind(this);
            this.img.src = this.get('overlay');
        },

        didLoadOverlay: function () {
            this.overlayLoaded = true;
            this.trigger('didLoadOverlay');
        },

        getFill: function () {
            var colorKey = "Main";

            if (!this.get('active')) {
                colorKey = "Unavailable";
            }
            else if (this.get('inCart')) {
                colorKey = 'Highlight';
            }
            else if (this.get('selected')) {
                colorKey = 'ZoneHighlight';
            }
            else if (this.get('underCursor')) {
                colorKey = 'ZoneHighlight';
            }
            else {
                if (this.get('fillOverride')) {
                    return this.get('fillOverride');
                }
            }

            return this.syos.activeLevel.seatColors.getColorForKey(colorKey).get('Color');
        },

        scaledAttributes: function () {
            var scaledCoords = this.syos.utility.scaleCoordinates(this.get('vertx'), this.get('verty'));
            return {
                vertx: scaledCoords.x,
                verty: scaledCoords.y
            };
        },

        getRadius: function () {
            return this.syos.utility.scaleScalar(this.get('radius'));
        },

        distanceFromPoint: function (x, y) {
            var c = this.scaledAttributes();
            var horizontal = Math.pow(x - c.x, 2);
            var vertical = Math.pow(y - c.y, 2);
            return Math.sqrt(horizontal + vertical);
        },

        isPointInCircle: function (x, y, padding) {
            var padding = padding || 0;
            var radius = this.getRadius() + padding;
            return (this.distanceFromPoint(x, y) < radius);
        },

        // Given mouse position, decide if the mouse position is inside of the polygon
        // http://en.wikipedia.org/wiki/Point_in_polygon
        // http://stackoverflow.com/questions/11716268/point-in-polygon-algorithm
        pointInPolygon: function (mousex, mousey) {
            var data = this.scaledAttributes();
            var vertx = data.vertx;
            var verty = data.verty;

            var nvert = vertx.length;
            var j = nvert - 1;
            var c = false;
            _(nvert).chain().range().each(function (i) {
                if ((verty[i] > mousey) !== (verty[j] > mousey) &&
                    (mousex < (vertx[j] - vertx[i]) * (mousey - verty[i]) / (verty[j] - verty[i]) + vertx[i])) {
                    c = !c;
                }
                j = i;
            }, this).value();
            return c;
        },
        // Given mouse position, decide if the mouse position is inside of the part circle
        pointInPartCircle: function (mousex, mousey, checkReverse) {
            checkReverse = checkReverse || false;
            var scaledMouse = this.syos.utility.descendCoordinates(mousex, mousey);
            mousex = scaledMouse.x;
            mousey = scaledMouse.y;
            // only deal with area < semi-circle case
            var points = this.get('polygonPointsCollection');

            var inFullCircle = false;
            var inEmptyCircle = false;
            _(points).each(function (point, i) {
                if (point.r !== 0) {
                    var idx = (i + 1) % points.length;
                    var endPoint = checkReverse ? point : points[idx];

                    var simpleTestPoint = {
                        x: mousex - point.middlePoint.x,
                        y: mousey - point.middlePoint.y
                    }
                    var simpleEndPoint = {
                        x: endPoint.x - point.middlePoint.x,
                        y: endPoint.y - point.middlePoint.y
                    }
                    var sin = simpleTestPoint.x * simpleEndPoint.y - simpleTestPoint.y * simpleEndPoint.x;
                    var dist = this.syos.utility.getDistance(mousex, point.centerPoint.x, mousey, point.centerPoint.y);
                    // reversed canvas coordinates
                    if (sin >= 0 && dist <= Math.abs(point.r)) {
                        if (point.r < 0) {
                            inEmptyCircle = true;
                        }
                        else {
                            inFullCircle = true;
                        }
                    }
                }
            }, this);
            if (checkReverse) {
                return inEmptyCircle;
            }
            else {
                return inFullCircle;
            }
        }
    });

    var PolygonCollection = SYOSCollection.extend({

        model: Polygon,

        getActivePolygon: function () {
            return this.select(function (polygon) {
                return polygon.get('active');
            });
        }
    });

    var SeatMap = SYOSView.extend({

        initialize: function () {
            console.log("CanvasMap: initializing SeatMap...");
            this.canvasMap = this.options.parent;
            this.ctx = this.getRenderContext();

            this.allowRendering = true;
            this.renderConditions = [];

            this.determineMode();
            this.initBackgroundImage();

            this.circles = new CircleCollection();
            this.circles.on('change', this.didChangeCircle, this);

            this.hoverCircle = null;
            this.hoverPolygon = null;
            this.clickCircle = null;

            this.gaPolygons = new PolygonCollection();
            this.gaPolygons.on('change', this.didChangePolygon, this);

            $(window).resize(_(this.didSizeWindow).bind(this));

            this.syos.settings.on('change:CurrentZoom', this.didChangeZoom, this);
            this.canvasMap.mapOffset.on('change', this.renderAll, this);

        },

        initBackgroundImage: function () {
            var width = this.syos.settings.get('levelImageWidth');
            var height = this.syos.settings.get('levelImageHeight');

            this.background = document.createElement('img');
            this.background.setAttribute('width', width);
            this.background.setAttribute('height', height);
        },

        initTimedRender: function () {
            setInterval(_(function () {
                this.fullRender();
            }).bind(this), 1000);
        },

        updateBackground: function (imgUrl) {
            this.background.onload = _(this.renderAll).bind(this);

            this.background.src = imgUrl;

            if (this.syos.settings.get('syosBackgroundImage')) {
                this.background.src = this.syos.settings.get('syosBackgroundImage');
            }
        },

        events: {
            "mousemove": "didMousemove",
            "click": "didMousedown",
            "tap": "didTap"
        },

        didMousemove: function (evt) {
            if (this.syos.activeLevel && !this.syos.config.touchMode) {
                this.updateMousePosition(evt);
                this.determineHoverObject();
                //this.determineCircle();
                //this.determinePolygon();

                if (syosTestModeActive) {
                    this.clear();
                    this.renderAll();
                    this.drawLineToHoverCircle();
                }
            }
            return this;
        },

        didMousedown: function (evt) {
            // TODO: CHECK hoverPolygon 
            if (this.hoverPolygon && !this.syos.config.touchMode) {
                evt.preventDefault();
                evt.stopPropagation();
                evt.returnValue = false;
                syosDispatch.trigger('canvas:didClickGAZone', this.syos.activeLevel.gaZones.get(this.hoverPolygon.id));
            }
            if (this.hoverCircle && !this.syos.config.touchMode) {
                evt.preventDefault();
                evt.stopPropagation();
                evt.returnValue = false;
                syosDispatch.trigger('canvas:didClickSeat', this.syos.activeLevel.seats.get(this.hoverCircle.id));
            }
        },

        didTap: function (evt) {
            var elOffset = $(evt.currentTarget).offset();
            evt.offsetX = this.syos.utility.getTouchAverageForKey(evt.originalEvent.gesture.touches, 'pageX');
            evt.offsetX -= elOffset.left;
            evt.offsetY = this.syos.utility.getTouchAverageForKey(evt.originalEvent.gesture.touches, 'pageY');
            evt.offsetY -= elOffset.top;


            var circleSize = this.syos.utility.scaleScalar(this.syos.settings.get('seatRadius'));

            if (circleSize >= 10 || !this.syos.config.touchMode) {
                this.updateMousePosition(evt);
                this.determineHoverObject();
                //this.determineCircle();
                //this.determinePolygon();
                if (this.hoverCircle) {
                    syosDispatch.trigger('canvas:didClickSeat', this.syos.activeLevel.seats.get(this.hoverCircle.id));
                }
                if (this.hoverPolygon) {
                    syosDispatch.trigger('canvas:didClickGAZone', this.syos.activeLevel.gaZones.get(this.hoverPolygon.id));
                }
            }
            else {
                // zoom in 
                //var zoom = this.syos.settings.get('ZoomIncrement') * this.syos.settings.get('embeddedZoomModifier');
                syosDispatch.trigger('control:smartZoom', 1, evt.offsetX, evt.offsetY);
            }
            console.log("tap");
        },

        didTapEmpty: function (evt) {

        },

        didSizeWindow: _(function () {
            this.determineSimplified();
            this.renderAll();
        }).debounce(500),

        didChangeCircle: function (circle) {
            this.ctx.save();

            var currentZoom = this.syos.utility.getCurrentZoom();
            var offset = this.canvasMap.getCircleMapOffset();

            this.ctx.translate(offset.x, offset.y);
            this.ctx.scale(currentZoom, currentZoom);
            this.renderCircle(this.ctx, circle);

            this.ctx.restore();
        },

        didChangePolygon: function (polygon) {
            this.ctx.save();

            var currentZoom = this.syos.utility.getCurrentZoom();
            var offset = this.canvasMap.getCircleMapOffset();

            this.ctx.translate(offset.x, offset.y);
            this.ctx.scale(currentZoom, currentZoom);
            this.renderPolygon(this.ctx, polygon);

            this.ctx.restore();
        },

        didChangeZoom: function () {
            this.renderAll();
        },

        determineMode: function () {
            var testContext = this.getContext('2d');

            var mode = this.mode = {};
            mode.shadows = !_(window.chrome).isUndefined();
            mode.mozSmooth = _(testContext).has('mozImageSmoothingEnabled');
            mode.webkitSmooth = _(testContext).has('webkitImageSmoothingEnabled');

            this.determineSimplified();
        },

        determineSimplified: function () {
            var isMobileWidth = this.syos.utility.isMobileWidth();
            if (isMobileWidth) {
                console.log('SeatMap: forcing to simplified mode due to small viewport');
                this.mode.simplified = true;
            } else {
                this.mode.simplified = this.syos.settings.get('simplifiedMap');
            }
        },

        determineHoverObject: function () {
            this.determineCircle();
            if (this.hoverCircle === null) {
                this.determinePolygon();
            }
        },


        determineCircle: function () {
            var distance = Infinity;
            var nearestCircle = null;

            this.circles.each(function (circle) {
                var currentDistance = circle.distanceFromPoint(this.mouseX, this.mouseY);
                if (currentDistance < distance) {
                    nearestCircle = circle;
                    distance = currentDistance;
                }
            }, this);

            if (nearestCircle && this.mouseIsInCircle(nearestCircle) && nearestCircle.get('active')) {
                this.setHoverCircle(nearestCircle);
                this.setCursor('pointer');
            }
            else {
                this.setHoverCircle(null);
                this.setCursor('');
            }

            return this;
        },

        determinePolygon: function () {
            // this.mouseX, this.mouseY

            var hoverPolygon = this.gaPolygons.filter(function (polygon) {
                var inPolygonPart = polygon.pointInPolygon(this.mouseX, this.mouseY);
                if (inPolygonPart) {
                    return !polygon.pointInPartCircle(this.mouseX, this.mouseY, true);
                }
                else {
                    return polygon.pointInPartCircle(this.mouseX, this.mouseY);
                }

            }, this)[0];

            if (typeof hoverPolygon !== 'undefined' && hoverPolygon.get('active')) {
                this.setHoverPolygon(hoverPolygon);
                this.setCursor('pointer');
            }
            else {
                this.setHoverPolygon(null);
                this.setCursor('');
            }

            return this;
        },

        resetPosition: function () {
            this.$el.css({ top: "0px", left: "0px" });
        },

        updateMousePosition: function (evt) {
            var x = evt.offsetX || evt.originalEvent.layerX;
            var y = evt.offsetY || evt.originalEvent.layerY;

            this.mouseX = x - this.canvasMap.mapOffset.get('x');
            this.mouseY = y - this.canvasMap.mapOffset.get('y');

        },

        updateCanvasSize: function () {
            this.el.width = this.canvasMap.$el.width();
            this.el.height = this.canvasMap.$el.height();
        },

        setCursor: function (value) {
            this.$el.css('cursor', value);
        },

        setHoverCircle: function (newCircle) {
            if (this.hoverCircle !== null) {
                this.hoverCircle.set('underCursor', false);
            }

            this.hoverCircle = newCircle;

            if (this.hoverCircle !== null) {
                this.hoverCircle.set('underCursor', true);
            }
        },

        setHoverPolygon: function (newPolygon) {
            if (this.hoverPolygon !== null) {
                this.hoverPolygon.set('underCursor', false);
            }

            this.hoverPolygon = newPolygon;

            if (this.hoverPolygon !== null) {
                this.hoverPolygon.set('underCursor', true);
            }
        },

        mouseIsInCircle: function (circle) {
            var circlePadding = 2;
            return circle.isPointInCircle(this.mouseX, this.mouseY, circlePadding);
        },

        clear: function () {
            this.el.width = this.el.width;
        },

        addRenderCondition: function (fn) {
            this.renderConditions.push(fn);
            return fn;
        },

        getShouldRender: function () {
            var allowed = this.allowRendering;
            if (this.renderConditions) {
                var results = _(this.renderConditions).map(function (fn) {
                    return fn();
                });
                var negativeResults = _(results).select(function (val) { return !val; });
                if (negativeResults.length > 0) {
                    return false;
                }
            }
            return allowed;
        },

        renderAll: _(function () {
            var shouldRender = this.getShouldRender();
            if (shouldRender) {
                this.updateCanvasSize();

                console.groupCollapsed('Full Map Render');
                console.log('offset:', this.canvasMap.mapOffset.toJSON());
                console.log('zoom:', this.syos.utility.getCurrentZoom());

                var currentZoom = this.syos.utility.getCurrentZoom();
                var offset = this.canvasMap.getCircleMapOffset();

                this.clear();

                console.log("current zoom: " + this.syos.utility.getCurrentZoom());
                this.ctx.save();

                this.ctx.translate(offset.x, offset.y);
                this.ctx.scale(currentZoom, currentZoom);

                this.renderBackground(this.ctx);

                syosDispatch.trigger('display:shouldUpdateStatus', 'rendering...');

                this.circles.each(function (circle) {
                    var fn = _(this.renderCircle).partial(this.ctx, circle);
                    if (this.mode.simplifiedMap) _(fn).chain().bind(this).delay(10);
                    else _(fn).bind(this)();
                }, this);

                if (this.syos.settings.get('showGAZone')) {
                    this.gaPolygons.each(function (polygon) {
                        var fn = _(this.renderPolygon).partial(this.ctx, polygon);
                        if (this.mode.simplifiedMap) _(fn).chain().bind(this).delay(10);
                        else _(fn).bind(this)();
                    }, this);
                }

                this.ctx.restore();
                console.groupEnd();
            }
        }).debounce(100),

        renderBackground: function (ctx) {
            ctx.save();

            ctx.drawImage(this.background, 0, 0);

            ctx.restore();
        },

        renderPolygon: function (ctx, polygon) {
            this.clearPolygon(ctx, polygon);
            var polygonPointsCollection = polygon.get('polygonPointsCollection');
            ctx.save();
            ctx.fillStyle = polygon.getFill();
            ctx.beginPath();
            _(polygonPointsCollection).each(function (startPoint, idx) {
                var endPointIdx = (idx + 1) % polygonPointsCollection.length;

                var endPoint = polygonPointsCollection[endPointIdx];
                if (idx === 0) {
                    ctx.moveTo(startPoint.x, startPoint.y);
                }
                if (startPoint.r === 0) {
                    ctx.quadraticCurveTo(startPoint.middlePoint.x, startPoint.middlePoint.y, endPoint.x, endPoint.y);
                }
                else {
                    var idx_next = (idx + 2) % polygonPointsCollection.length;
                    var nextPoint = polygonPointsCollection[idx_next];
                    var radius = startPoint.r;

                    var opts = {
                        startPoint: startPoint,
                        endPoint: endPoint,
                        nextPoint: nextPoint,
                        radius: radius
                    }

                    var centerPoint = startPoint.centerPoint;
                    // calculate the start and end angle. since canvas's coordinate's y axis is reversed, use centerPoint.y - startPoint.y
                    var startAngle = -Math.atan2(centerPoint.y - startPoint.y, startPoint.x - centerPoint.x);
                    if (startAngle < 0) {
                        startAngle += Math.PI * 2;
                    }
                    var endAngle = -Math.atan2(centerPoint.y - endPoint.y, endPoint.x - centerPoint.x);
                    if (endAngle < 0) {
                        endAngle += Math.PI * 2;
                    }
                    // DRAW arc

                    if (radius > 0) {
                        ctx.arc(centerPoint.x, centerPoint.y, Math.abs(radius), startAngle, endAngle);
                    }
                    else {
                        ctx.arc(centerPoint.x, centerPoint.y, Math.abs(radius), startAngle, endAngle, true);
                    }
                }
            }, this);

            ctx.fill();
            ctx.restore();
        },

        clearPolygon: function (ctx, polygon) {
            var polygonPointsCollection = polygon.get('polygonPointsCollection');
            ctx.save();
            ctx.fillStyle = '#fff';
            ctx.strokeStyle = '#fff';
            ctx.beginPath();
            _(polygonPointsCollection).each(function (startPoint, idx) {
                var endPointIdx = (idx + 1) % polygonPointsCollection.length;

                var endPoint = polygonPointsCollection[endPointIdx];
                if (idx === 0) {
                    ctx.moveTo(startPoint.x, startPoint.y);
                }
                if (startPoint.r === 0) {
                    ctx.moveTo(startPoint.x, startPoint.y);
                    ctx.lineWidth = 1;
                    ctx.lineTo(endPoint.x, endPoint.y);
                    ctx.stroke();
                }
                else {
                    var idx_next = (idx + 2) % polygonPointsCollection.length;
                    var nextPoint = polygonPointsCollection[idx_next];
                    var radius = startPoint.r;

                    var opts = {
                        startPoint: startPoint,
                        endPoint: endPoint,
                        nextPoint: nextPoint,
                        radius: radius
                    }

                    var centerPoint = startPoint.centerPoint;
                    // calculate the start and end angle. since canvas's coordinate's y axis is reversed, use centerPoint.y - startPoint.y
                    var startAngle = -Math.atan2(centerPoint.y - startPoint.y, startPoint.x - centerPoint.x);
                    if (startAngle < 0) {
                        startAngle += Math.PI * 2;
                    }
                    var endAngle = -Math.atan2(centerPoint.y - endPoint.y, endPoint.x - centerPoint.x);
                    if (endAngle < 0) {
                        endAngle += Math.PI * 2;
                    }
                    // DRAW arc

                    if (radius > 0) {
                        ctx.arc(centerPoint.x, centerPoint.y, Math.abs(radius), startAngle, endAngle);
                    }
                    else {
                        ctx.arc(centerPoint.x, centerPoint.y, Math.abs(radius), startAngle, endAngle, true);
                    }

                    ctx.stroke();
                }
            }, this);
            ctx.restore();
        },

        renderCircle: function (ctx, circle) {
            var shouldRender = this.getShouldRender();
            if (shouldRender) {
                ctx.save();

                var attr = circle.toJSON();
                var currentZoom = this.syos.utility.getCurrentZoom();
                var isAboveTextThreshold = currentZoom > this.syos.settings.get('seatTextLimit');

                //ctx.scale(currentZoom, currentZoom);

                var tX = attr.x;
                var tY = attr.y;

                ctx.translate(tX, tY);

                if (!this.mode.simplified) {
                    ctx.fillStyle = "#FFFFFF";
                    this.renderCirclePalette(ctx, attr.radius);

                    ctx.fillStyle = "#333333";
                    this.renderCircleShadow(ctx, attr.radius);
                }

                ctx.fillStyle = circle.getFill();
                ctx.strokeStyle = "#DDD";
                this.renderCircleBackground(ctx, attr.radius);

                if (circle.overlayLoaded) {
                    this.renderCircleOverlay(ctx, attr.radius, circle.img);
                }
                else if (attr.text && isAboveTextThreshold && !this.mode.simplified) {
                    ctx.fillStyle = "#FFF";
                    this.renderCircleText(ctx, attr.radius, attr.text);
                }

                ctx.restore();
            }
        },

        renderCirclePalette: function (ctx, radius) {
            var paletteRadius = radius + 1.5;
            ctx.beginPath();
            ctx.arc(0, 0, paletteRadius, 0, Math.PI * 2, false);
            ctx.fill();
        },

        renderCircleShadow: function (ctx, radius) {
            var shadowRadius = radius + 0.5;
            ctx.beginPath();
            ctx.arc(-0.5, 0.5, shadowRadius, 0, Math.PI * 2, false);
            ctx.fill();
        },

        renderCircleBackground: function (ctx, radius) {
            ctx.beginPath();
            ctx.arc(0, 0, radius, 0, Math.PI * 2, false);
            ctx.fill();
            ctx.stroke();
        },

        renderCircleOverlay: function (ctx, radius, img) {
            var imgPad = 2;
            var imgMtr = {
                x: 0 - radius + imgPad,
                y: 0 - radius + imgPad,
                w: (radius - imgPad) * 2
            };
            ctx.drawImage(img, imgMtr.x, imgMtr.y, imgMtr.w, imgMtr.w);
        },
        //Huiyuan
        renderCircleText: function (ctx, radius, text) {
            ctx.save();
            ctx.textBaseline = this.syos.settings.get('circleTextBaseline');
            ctx.font = radius + "px " + this.syos.settings.get('circleTextFont');

            var textLength = ctx.measureText(text).width;

            var offset = this.syos.settings.get('circleTextOffset');
            var labelLength = radius * Math.sqrt(3);
            var textX = textLength > labelLength ? 0 - labelLength / 2 : 0 - textLength / 2;
            var textY = radius / 2;
            var textW = labelLength;

            ctx.fillText(text, textX, textY, labelLength - offset);
            ctx.restore();
        },
        //End
        getRenderContext: function () {
            var ctx = this.el.getContext('2d');
            return ctx;
        },

        getContext: function () {
            var ctx = this.el.getContext('2d');
            var offset = this.canvasMap.getCircleMapOffset();

            ctx.translate(offset.x, offset.y);

            if (_(ctx).has('mozImageSmoothingEnabled')) {
                ctx.mozImageSmoothingEnabled = true;
            }
            else if (_(ctx).has('webkitImageSmoothingEnabled')) {
                ctx.webkitImageSmoothingEnabled = true;
            }

            return ctx;
        }

    });

    return {
        Map: Map
    };

}());