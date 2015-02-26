syosConfig = {
    //**** initialization options for SYOSCore
    initOptions: {

        // Array<SYOSPlugin> - plugin models to load into the SYOS
        plugins: ["SYOSZoneHighlight"]

    },

    //**** settings map for the SYOSSettings instance
    settings: {

        // number - minimum zoom level
        ZoomMin: 0.4,

        // number - default zoom if auto-fit fails
        CurrentZoom: 0.4,

        // bool - used internally for ie 8 fallbacks
        IE8: !(typeof IsIE8 === "undefined"),

        // number - because IE 8 is bad at math
        IE8ZoomModifier: 1.4,

        // number - maximum zoom level
        ZoomMax: 1.2,

        // number - amount that is zoomed on mousewheel or control click
        ZoomIncrement: 0.1,

        // numbers - ie fallback for initial x/y position
        ieX: 40,
        ieY: 40,

        // numbers - width of the seatview modal
        seatviewWidth: 36,
        seatviewHeight: 50

    },

    //**** regex settings for Underscore templating
    templateOptions: {
        interpolate: /\{\{(.+?)\}\}/g,
        evaluate: /\<\[([\s\S]+?)\]\>/g,
        escape: /\<\[-([\s\S]+?)\]\>/g
    }

}