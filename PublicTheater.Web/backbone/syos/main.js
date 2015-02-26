

require([

    "handlebars",
    "syos/config",

    "syostemplate",
    "syoscore"


], function (Handlebars, syosConfig, Template, CombinedSyos) {

    console.log('Loading Plugins...');

    require([
        // add plugins here
        //"./syos/plugins/syos.zoneHighlight"
        'syosZoneHighlight',
        'syos/plugins/syos.popup'

    ], function (SYOSZoneHighlight, SYOSPopup) {

        // window.pluginName = PluginName;
        window.SYOSZoneHighlight = SYOSZoneHighlight;

        window.SYOSPopup = SYOSPopup;

        console.log('Loading SYOS...');
        var syos = window.htmlSyos = new SYOS({
            el: $('[data-bb="syos"]')[0],
            config: syosConfig
        });
        syos.loadPerformance(window.cfs.urlVars["performanceNumber"]);
    });

});