define({

    //**** initialization options for SYOSCore
    initOptions: {

        // Array<SYOSPlugin> - plugin models to load into the SYOS
        plugins: ["SYOSZoneHighlight", "SYOSLimitTickets", "SYOSAdaseatColor"]
        //plugkkins:[]

    },

    touchMode: function () {
        return 'ontouchstart' in window // works on most browsers 
            || navigator.msMaxTouchPoints;
        //'onmsgesturechange' in window; // works on ie10 and then broke in ie11
    } (),

    mapMode: "legacy",

    //**** settings map for the SYOSSettings instance
    settings: {

        // external purchasing mode
        purchaseExternalMode: false,

        //disable popup
        disableDefaultSeatPopup: false,

        // use level select dropdown
        levelSelectDropdown: true,

        // use level select map
        levelSelectMap: true,

        // use fullscreen button
        showFullscreen: false,

        // confirm
        reserveConfirm: true,

        // number - default zoom if auto-fit fails
        CurrentZoom: 0.6,

        // bool - used internally for ie 8 fallbacks
        IE8: !(!!document.createElement("canvas").getContext),

        // number - because IE 8 is bad at math
        IE8ZoomModifier: 1.4,

        // number - maximum zoom level
        ZoomMax: 2,

        // number - minimum zoom level
        ZoomMin: 0.125,

        // number - amount that is zoomed on mousewheel or control click
        ZoomIncrement: 0.2,

        // number - adjustment for the map after auto zooming has occured
        initialZoomAdjustment: 0.15,
        initialXOffset: 270,
        initialYOffset: 80,


        // numbers - ie fallback for initial x/y position
        ieX: 320,
        ieY: 40,

        // numbers - width of the seatview modal
        seatviewWidth: 60,
        seatviewHeight: 80,

        // numbers - dimensions of the modal
        defaultModalHeight: 30,
        defaultModalWidth: 50,

        levelImageWidth: 1900,
        levelImageHeight: 1200,

        // string - path to the seatview icon
        seatViewIconPath: "/syos/img/iconPhoto.png",

        // string - text to show when there's a 0 - 0 price range
        emptyPriceString: "Currently Sold Out",

        defaultZoomForFullSite: 0.4,

        seatTypeWarningModalCancelText: "No – Cancel",
        seatTypeWarningModalConfirmText: "Yes – Continue",
        
        boundaryOffsetX: 0,
        boundaryOffsetY: 0

    },

    //**** regex settings for Underscore templating
    templateOptions: {
        interpolate: /\{\{(.+?)\}\}/g,
        evaluate: /\<\[([\s\S]+?)\]\>/g,
        escape: /\<\[-([\s\S]+?)\]\>/g
    }

});
