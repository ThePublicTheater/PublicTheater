/* global manager */
define([
    'backbone',
    'cfsConfig'
],

function (Backbone, cfsConfig) {

    // #region CFS Setup

    var CFS = {

        nullFn: function () { return this; }

    };

    //var cfsConfigDefaults = {
    //    dir: "/js/app/",
    //    htmlDir: "/js/app/templates/",
    //    facebook: false,
    //    reserveControl: {
    //        calendarOptions: {},
    //        target: "#siteMain"
    //    }
    //};

    // #endregion


    // #region CFS Error Handling

    CFS.Errors = {
        jqueryDepend: function () {
            throw "CFS JS requires jQuery";
        },

        headDepend: function () {
            throw "CFS JS requires head.js";
        },

        underscoreDepend: function () {
            throw "CFS JS requires underscore";
        },

        handlebarsDepend: function () {
            throw "CFS JS requires handlebars";
        },

        backboneDepend: function () {
            throw "CFS JS requires backbone";
        },

        momentDepend: function () {
            throw "CFS JS requires moment.js";
        },

        genericAjax: function () {
            throw "CFS: An ajax error has occurred!";
        }

    };

    // #endregion


    // #region Dependency Check

    if (typeof jQuery === "undefined") CFS.Errors.jqueryDepend();
    if (typeof head === "undefined") CFS.Errors.headDepend();
    if (typeof _ === "undefined") CFS.Errors.underscoreDepend();
    if (typeof Handlebars === "undefined") CFS.Errors.handlebarsDepend();
    if (typeof Backbone === "undefined") CFS.Errors.backboneDepend();
    if (typeof moment === "undefined") CFS.Errors.momentDepend();

    // #endregion


    // #region CFS Core Models

    var CFSModel = Backbone.Model.extend();

    var CFSList = Backbone.Collection.extend({

        comparator: function () {
            return 0;
        }

    });

    // #endregion


    // #region CFS Core View

    var CFSView = Backbone.View.extend({

        ajaxError: function (response) {
            console.error("Error with an ajax request", arguments);
        },

        cleanTemplate: function (dirty) {
            var clean = dirty.replace(/\<\!\[CDATA\[/g, '').replace(/]]>/, '');
            return clean;
        },

        getBBElement: function (elementString) {
            var element = _(this.$el.find('[data-bb="' + elementString + '"]')).first();
            return element;
        },

        getBBElementAll: function (elementString) {
            var elements = _(this.$el.find('[data-bb="' + elementString + '"]')).map(function (el) { return el; });
            return elements;
        },

        viewModel: function () {
            return this;
        },

        render: function () {
            this.trigger('willRender');
            this.el.innerHTML = this.template(this.viewModel());
            this.trigger('didRender');
            return this;
        }
    });

    // #endregion


    // #region CFS App

    var CFSApp = CFSView.extend({

        urlVars: null,

        // #region init

        initialize: function () {
            console.groupCollapsed('CFS Core JS Info');
            console.log("CFS loaded from " + cfsConfig.dir);
            console.log("Config: ", cfsConfig);
            console.groupEnd();
            CFSView.prototype.cfs = this;
            CFSModel.prototype.cfs = this;
            CFSList.prototype.cfs = this;

            this.service = new Service();

            this.setElement(document.body);
            this.getUrlVars();
            //this.loadConfig();
            this.loadControls();
            return this;
        },

        getUrlVars: function () {
            var vars = {};
            var urlWithoutHash = window.location.href.replace(/#(.*)/, '');
            var parts = urlWithoutHash.replace(/[?&]+([^=&]+)=([^&]*)/gi, function (m, key, value) {
                vars[key] = value.replace(/#/, '');
            });
            this.urlVars = vars;
        },

        loadConfig: function () {
            var url = '/backbone/CFS/app.config.js?q=' + new Date().getTime();
            $.ajax({
                url: url,
                error: this.errorConfig,
                success: this.parseConfig,
                context: this
            });
            return this;
        },

        parseConfig: function (response) {
            eval(response);
            _(cfsConfig).defaults(cfsConfigDefaults);

            //this.loadControls();

            if (cfsConfig.facebook)
                this.loadFBApi();

            return this;
        },

        errorConfig: function () {
            console.error("Error loading CFS config");
            console.log(arguments);
            return this;
        },

        loadControls: function () {
            if (this.$el.find('#selectSeatHeader').length !== 0)
                this.loadReserveControl();
            if (this.$el.find('[data-bb="calendar"]').length !== 0) {
                this.loadCalendarControl();
            }
            if (this.$el.find('[data-bb="mobileCalendar"]').length !== 0) {
                this.loadMobileCalendar();
            }
            if (this.$el.find('[data-bb="exchangeWrap"]').length !== 0) {
                this.loadExchangeControl();
            }
            if (this.$el.find('[data-bb="fullSubscription"]').length !== 0) {
                this.loadFullSubControl();
            }
            return this;
        },

        // #endregion

        facebookReady: function () {
            var url = makeCFSUrl('friends/friends.js');
            head.js(url, _.bind(function () {
                if (this.$el.find('#cfsFriendManager').length !== 0)
                    this.loadFriendManager();
                if (this.$el.find('#cfsFriendSelect').length !== 0)
                    this.loadFriendSelect();
                if (this.$el.find('#cfsFriendLogin').length !== 0)
                    this.loadFriendLogin();
            }, this));
            return this;
        },

        // #region control loading

        loadFriendLogin: function () {
            console.log("loading friend login");

            head.js(makeCFSUrl("friends/login.js"), _.bind(function () {
                this.friendLogin = new FriendsLogin();
            }, this));
            return this;
        },

        loadFriendSelect: function () {
            console.log("loading friend select")
            head.js(makeCFSUrl("friends/select.js"), _.bind(function () {
                this.friendSelect = new FriendsSelect();
            }, this));
            return this;
        },

        loadFriendManager: function () {
            console.log("loading friend manager");
            head.js(makeCFSUrl("friends/manager.js"), _.bind(function () {
                this.friendManager = new FriendsManager();
            }, this));
            return this;
        },

        loadReserveControl: function () {
            console.log("CFS: Loading Reserve Control");
            require(["CFS/reserveControl/reserveControl"], _(function (CFSReserveControl) {
                var targetSelector = cfsConfig.reserveControl.target;
                var targetElement = $(targetSelector)[0];
                if (!targetElement) {
                    console.error('ReserveControl: could not find element matching selector %o (cfsConfig.reserveControl.target)', targetSelector);
                }
                this.reserveControl = new CFSReserveControl({ el: $(cfsConfig.reserveControl.target)[0] });
            }).bind(this));

            return this;
        },

        loadFullSubControl: _(function () {
            console.log("CFS: Loading Full Subscription Control");
            head.js(
                makeCFSUrl("reserveControl/seatSelection.js"),
                makeCFSUrl("subscriptions/full/subFullControl.js"),
                _(function () {
                    this.fullSubControl = new CFS.FullSubControl({ el: this.$el.find('[data-bb="fullSubscription"]')[0] });
                }).bind(this)
            );
        }).once(),

        loadExchangeControl: _(function () {
            //console.log("CFS: Loaded ExchangeControl");
            //head.js(
            //    makeCFSUrl("reserveControl/seatSelection.js"),
            //    makeCFSUrl("exchange/exchangeControl.js"),
            //    _(function () {
            //        this.exchangeControl = new CFS.ExchangeControl({ el: this.$el.find('[data-bb="exchangeWrap"]')[0] });
            //    }).bind(this)
            //);
            require(['CFS/exchange/exchangeControl'], _(function (ExchangeControl) {
                this.exchangeControl = new ExchangeControl({ el: this.$el.find('[data-bb="exchangeWrap"]')[0] });
            }).bind(this));
        }).once(),

        loadCalendarControl: _(function () {
            console.log("CFS: Loading Calendar Control");

            require(['CFS/calendar/calendarControl'], _(function (CalendarControl) {
                this.calendarControl = new CalendarControl({ el: document.getElementById('calendarPage') });
            }).bind(this));
        }).once(),

        loadMobileCalendar: _(function () {
            console.log("CFS: Loading Mobile Calendar");
            //head.js(
            //    makeCFSUrl("calendar/mobileCalendar.js"),
            //    _(function () {
            //        this.mobileCalendar = new CFS.MobileCalendar({ el: document.querySelector('[data-bb="mobileCalendar"]') });
            //    }).bind(this)
            //);
            require(['CFS/calendar/mobileCalendar'], _(function (MobileCalendar) {
                this.mobileCalendar = new MobileCalendar({ el: document.querySelector('[data-bb="mobileCalendar"]') });
            }).bind(this));
        }).once(),

        loadFBApi: function () {
            console.log("loading facebook api");
            (function (self) {
                window.fbAsyncInit = function () { self.facebookReady(); };
            })(this);
            (function (d, debug) {
                var js, id = 'facebook-jssdk', ref = d.getElementsByTagName('script')[0];
                if (d.getElementById(id)) { return; }
                js = d.createElement('script'); js.id = id; js.async = true;
                js.src = "//connect.facebook.net/en_US/all" + (debug ? "/debug" : "") + ".js";
                ref.parentNode.insertBefore(js, ref);
            }(document, true));
            return this;
        }

        // #endregion

    });

    // #endregion


    // #region Service

    var Service = Backbone.View.extend({
        rootUrl: '/SYOSService/SYOSService.asmx/',
        peUrl: '/Services/PEService.asmx/',

        getCalendarInfo: function (data, success, error, context) {
            $.ajax({
                url: this.rootUrl + 'GetCalendarInfo',
                contentType: 'application/json',
                data: JSON.stringify(data),
                type: 'POST',
                success: success,
                error: error,
                context: context
            });
        },

        getPerformances: function (options) {
            _(options).defaults({
                data: {},
                success: CFS.nullFn,
                error: CFS.Errors.genericAjax
            });

            _(options.data).defaults({
                startDate: moment(),
                endDate: moment().add('days', 7)
            });

            options.data.startDate = options.data.startDate.format('M/D/YYYY');
            options.data.endDate = options.data.endDate.format('M/D/YYYY');

            $.ajax({
                url: this.peUrl + 'GetPerformances',
                contentType: 'application/json',
                data: JSON.stringify(options.data),
                type: 'POST',
                success: options.success,
                error: options.error
            });
        },

        getPerformancesForCalendar: function (options) {
            options.data.startDate = options.data.startDate.format('M/D/YYYY');
            options.data.endDate = options.data.endDate.format('M/D/YYYY');

            $.ajax({
                url: this.peUrl + 'GetPerformancesForCalendar',
                contentType: 'application/json',
                data: JSON.stringify(options.data),
                type: 'POST',
                success: options.success,
                error: options.error
            });

        },

        getCalendarMonths: function (options) {
            console.groupCollapsed("Requesting Months");
            console.groupEnd();

            $.ajax({
                url: this.peUrl + 'GetCalendarMonths',
                contentType: 'application/json',
                type: 'POST',
                success: options.success,
                error: options.error
            });

        }

    });

    // #endregion


    // #region Tess Models

    var CFSPerformance = CFSModel.extend({

    });

    var CFSPerformanceList = CFSList.extend({

        model: CFSPerformance

    });

    // #endregion


    // #region console fix & extension

    if (typeof chrome === "undefined") {
        console = {};
        console.log = function () { return this; };
        console.group = function () { return this; };
        console.groupCollapsed = function () { return this; };
        console.groupEnd = function () { return this; };
        console.time = function () { return this; };
        console.timeEnd = function () { return this; };
        console.trace = function () { return this; };
        console.count = function () { return this; };
        console.info = function () { return this; };
        console.warn = function () { return this; };
        console.dirxml = function () { return this; }
        console.error = function () { return this; };
    }

    // #endregion


    // #region Namespacing

    // todo: clean up closure names

    CFS.cfsConfig = cfsConfig;

    CFS.View = CFSView;
    CFS.Model = CFSModel;
    CFS.List = CFSList;

    CFS.Service = Service;

    CFS.Performance = CFSPerformance;
    CFS.PerformanceList = CFSPerformanceList;

    CFS.App = CFSApp;

    // #endregion


    // #region localhost settings




    // #endregion

    //return {
    //    View: CFSView,
    //    Model: CFSModel,
    //    List: CFSList,
    //    Service: Service,
    //    Performance: CFSPerformance,
    //    PerformanceList: CFSPerformanceList
    //}
    return CFS;
});