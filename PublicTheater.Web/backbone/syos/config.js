define({

    //**** initialization options for SYOSCore
    initOptions: {

        // Array<SYOSPlugin> - plugin models to load into the SYOS
        plugins: ["SYOSZoneHighlight", "SYOSPopup"]
        //plugkkins:[]

    },

    touchMode: function () {
        return false;
    }(),

    mapMode: "legacy",

    //**** settings map for the SYOSSettings instance
    settings: {

        // external purchasing mode
        purchaseExternalMode: false,

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
        ZoomMax: 1.4,

        // number - minimum zoom level
        ZoomMin: 0.1,

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