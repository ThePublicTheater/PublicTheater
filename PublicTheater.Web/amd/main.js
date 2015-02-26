// This main.js is required by /static/backbone/main.js which defined baseUrl as /static.

var Paths = Paths || {};
Paths.amdBaseUrl = '../amd/';

requirejs.config({

    // If override baseUrl here, general libraries which are included in /static/backbone/main.js can't be used by name directly (like 'jquery')

    // require modules that are defined in paths can't use ./filename for including files that are in the same directory
    // use real path instead
    urlArgs: 'bust=20141105',
    paths: {
        'syos.main' : 'htmlsyos',
        syoscore:		Paths.amdBaseUrl + "syos/syos.combined",
        syostemplate:	Paths.amdBaseUrl + "syos/syos.templates",
        cfs:			Paths.amdBaseUrl + 'CFS/app',
        cfsConfig:		Paths.amdBaseUrl + 'CFS/app.config'
    }   
});

require([

    Paths.amdBaseUrl + 'CFS/main'

], function () {
    "use strict";
    
    var syosWrap = $('[data-bb=syos]');
    if (syosWrap.length > 0) {
        require([Paths.amdBaseUrl + "syos/main"]);
    }

});