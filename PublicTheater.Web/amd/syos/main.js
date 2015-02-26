require([

    "hammer",
    "draggableTouch",
     Paths.amdBaseUrl + "syos/config",
     Paths.amdBaseUrl + "htmlsyos"

], function (Hammer, DraggableTouch, syosConfig) {

    console.log('Loading Plugins...');
    window.syosConfig = syosConfig;
    require([
    'syos.main'
    ], function (syosMain) {

        require([
            // add plugins here
            'syos.main',
            Paths.amdBaseUrl + 'syos/plugins/syos.zoneHighlight',
            Paths.amdBaseUrl + 'syos/plugins/syos.popup',
            Paths.amdBaseUrl + 'syos/plugins/syos.limitTickets',
            Paths.amdBaseUrl + 'syos/plugins/syos.adaseatColor'

        ], function (syosMain, SYOSZoneHighlight, SYOSPopup, SYOSLimitTickets, SYOSAdaseatColor) {

            // window.pluginName = PluginName;
            window.SYOSZoneHighlight = SYOSZoneHighlight;
            window.Hammer = Hammer;
            window.DraggableTouch = DraggableTouch;
            window.SYOSLimitTickets = SYOSLimitTickets;
            window.SYOSAdaseatColor = SYOSAdaseatColor;

            console.log('Loading SYOS...');
            var syos = window.htmlSyos = syosMain;
          
            syos.loadPerformance(window.cfs.urlVars["performanceNumber"]);

        });

    });

});