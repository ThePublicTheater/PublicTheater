
requirejs.config({
    baseUrl: '/backbone',

    paths: {
        q: '/Static/backbone/lib/q',
        jquery: '/Static/backbone/lib/jquery',
        jqueryui: '/Static/backbone/lib/jqueryui',
        underscore: '/Static/backbone/lib/underscore',
        backbone: '/Static/backbone/lib/backbone',
        handlebars: '/Static/backbone/lib/handlebars.runtime',
        handlebarsHelpers: 'handlebarsHelpers/handlebarsHelpers',
        noext: 'lib/noext',
        bxslider: '/Static/js/lib/jquery.bxslider.min',
        audiojs: 'lib/audiojs/audio.min',
        navbarJS: '/js/navbarJS/navbarJS',
        calendarPerformanceSelector: 'controls/calendarPerformanceSelector/calendarPerformanceSelector',
        mobileCalendar: 'controls/calendar/mobile/mobileCalendar',
        desktopCalendar: 'controls/calendar/calendar',
        soundCloud: ['https://w.soundcloud.com/player/api', 'lib/soundCloudapi'],
        hammer: 'lib/hammer',
        draggableTouch : 'lib/jquery.draggabletouch'
    },

    shim: {
        underscore: {
            exports: '_'
        },
        backbone: {
            deps: ['underscore', 'jquery'],
            exports: 'Backbone'
        },
        handlebars: {
            exports: 'Handlebars'
        },
        handlebarsHelpers: {
            deps: ['handlebars'],
            exports: 'Handlebars'
        },
        bxslider: {
            deps: ['jquery']
        },
        audiojs: {
            deps: ['jquery']
        },
        youtube: {
            deps: ['jquery']
        },
        navbarJS: {
            deps: ['jquery']
        },
        jqueryui: {
            deps: ['jquery']
        }
    }

});

require([

    'jquery',
    'underscore',
    'backbone',
    'handlebarsHelpers',
    
    // BB Controls
    'calendarPerformanceSelector',
    
    // No exports from here on out.
    'noext!https://www.youtube.com/player_api',
    'jqueryui',
    'navbarJS',
    'bxslider',
    'audiojs',
    'soundCloud',
    'app/globalSliders'

], function ($, _, Backbone, Handlebars, CalendarPerformanceSelector, MobileCalendar, DesktopCalendar) {
    "use strict";
    
    var pageTypeName = $("#epiPageType").val();

    if (pageTypeName === "WatchListenPageData") {
        var rootData = JSON.parse($("#mediaCenterData").val());

        require([

            "context/watchAndListenContext",
            "app/watchAndListen"

        ], function (WatchAndListenContext, WatchAndListenApp) {
            var watchAndListenContext = new WatchAndListenContext({ rootData: rootData });

            window.app = window.app = new WatchAndListenApp({ el: $("#secMainContent .row"), context: watchAndListenContext });

            app.render();
            
            Backbone.history.start();
        });
    } else if (pageTypeName === "CalendarPageData") {

        require([

            'mobileCalendar',
            'desktopCalendar'

        ], function (MobileCalendar, DesktopCalendar) {
            window.desktopCalendar = new DesktopCalendar({ el: $("#calendarPage") });
            window.mobileCalendar = new MobileCalendar({ desktopCalendar: desktopCalendar });
        });

    } else if (pageTypeName === "GiftCertificatePageType") {

        require(["/js/gift/preview.js"]);

    } else if (pageTypeName === "StandaloneDonationPageType") {

        require(["/js/contribute/donationFull.js"], function (DonationPage) {
            window.donationPage = new DonationPage({ el: document.body });
            window.donationPage.render();
        });

    } else if (pageTypeName === "GalaTransactionPageData") {

        require(["/js/contribute/gala.js"], function (GalaPage) {
            window.donationPage = new GalaPage({ el: document.body });
            window.donationPage.render();
        });
    }
    else if (pageTypeName === "DonatePageData") {

        require(["epiPageTypes/DonatePageData/donatePageData"], function (DonateNowPage) {
            window.donationPage = new DonateNowPage({ el: document.body });
        });

    }
    else if (pageTypeName === "ArchivePageData") {

        require(["epiPageTypes/archivePageData"], function (ArchivePage) {
            window.archivePage = new ArchivePage({ el: document.body });
        });

    }
    else if (pageTypeName === "TicketHistoryPageType") {

        require(["epiPageTypes/ticketHistoryPageData"], function (TicketHistoryPage) {
            window.ticketHistoryPage = new TicketHistoryPage({ el: document.body });
        });

    }
    

    //var syosWrap = $('[data-bb=syos]');
    //if (syosWrap.length > 0) {
    //    require(['syos/main']);
    //}
    
    // Entire Document Controls (Buy Tickets JS Button)
    _.defer(function () {
        var buyTicketsButtonControl = new CalendarPerformanceSelector({ el: document.body, reservePageURL: $("#reservePageUrl").val() });
    });
});