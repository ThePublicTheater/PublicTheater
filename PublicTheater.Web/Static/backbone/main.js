
requirejs.config({
    baseUrl: '/Static/',

    paths: {
        jquery: '//ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min',
        jqueryui: '//ajax.googleapis.com/ajax/libs/jqueryui/1.10.2/jquery-ui.min',
        underscore: 'backbone/lib/underscore',
        backbone: 'backbone/lib/backbone',
        bootstrap: 'backbone/lib/bootstrap',
        moment: 'backbone/lib/moment',
        handlebars: 'backbone/lib/handlebars.runtime',
        q: 'backbone/lib/q',
        head: 'backbone/lib/head.load',

        respond: 'js/lib/respond',
        bxslider: 'js/lib/jquery.bxslider.min',
        lemmonSlider: 'js/lib/lemmonSlider',
        fitText: 'js/lib/jquery.fittext',
        jcycle: 'js/lib/jquery.cycle.all',
        foundation: 'js/lib/foundation',
        foundationTopbar: 'js/lib/foundation.topbar',
        foundationReveal: 'js/lib/foundation.reveal',
        foundationSection: 'js/lib/foundation.section',
        selectBox: 'js/lib/jquery.selectbox-0.2.min',
        uniform: 'js/lib/jquery.uniform.min',
        
        modernizr: 'js/lib/modernizr',
        equalize: 'js/lib/equalize',
        menuAim: "js/lib/jquery.menu-aim",
        touchSwipe: "js/lib/jquery.touchSwipe.min",
        custom: 'js/custom',
        search: 'js/search',
        asmxHelpers: 'backbone/rm/asmxHelpers',
        errorHelpers: 'backbone/rm/errorHelpers',
        responsiveDispatch: 'backbone/rm/responsiveDispatch',

        NonCFSBackbone: "../backbone/main"
    },

    shim: {
        underscore: {
            exports: '_'
        },
        bxslider: {
            deps: ['jquery']
        },
        lemmonSlider:
            {
                deps: ['jquery']
            },
        jcycle: {
            deps: ['jquery']
        },
        jqueryui: {
            deps: ['jquery']
        },
        foundation: {
            deps: ['jquery']
        },
        foundationTopbar: {
            deps: ['foundation']
        },
        foundationReveal: {
            deps: ['foundation']
        },
        foundationSection: {
            deps: ['foundation']
        },
        backbone: {
            deps: ['underscore', 'jquery'],
            exports: 'Backbone'
        },
        handlebars: {
            exports: 'Handlebars'
        },
        bootstrap: {
            deps: ['jquery']
        },
        fitText: {
            deps: ['jquery']
        },
        uniform: {
            deps: ['jquery']
        },
        selectBox: {
            deps: ['jquery']
        },
        equalize: {
            deps: ['jquery']
        },
        menuAim: {
            deps: ['jquery']
        },
        search: {
            deps: ['jquery']
        },
        jquerytouch: {
            deps: ['jquery']
        }
    }

});


require([

    'jquery',
    'underscore',
    'backbone',
    'responsiveDispatch',
    'backbone/../../twitter/twitter',
    'menuAim',
    'touchSwipe',
    
    'bxslider',
    'lemmonSlider',
    'jcycle',
    'foundationTopbar',
    'foundationReveal',
    'foundationSection',
    'foundation',
    'selectBox',
    'fitText',
    'modernizr',
    'equalize',
    'uniform',
    'jqueryui',
    'bootstrap',
    'head',
    'search'

], function ($, _, Backbone, responsiveDispatch, Twitter) {
    "use strict";

    var ranges = [
        { name: "large", min: 979, max: Infinity },
        { name: "medium", min: 767, max: 979 },
        { name: "small", min: 0, max: 767 }
    ];

    responsiveDispatch.initRanges(ranges);

    window.twitterApp = new Twitter({ el: $('body') });


    var toLoad = ['custom'];

    if ($('.slideshow').length !== 0) {
        toLoad.push('js/views/blocks/rotatorBlockControl');
    }

    require(toLoad, function () {
        responsiveDispatch.forceEmit();
    });
    
    if($('#SearchControl').length!==0) {
        require(['search'], function (SearchControl) {
            window.searchControl = new SearchControl({ el: $('#SearchControl') });
            _.defer(function () {
                window.searchControl.render();
            });
        });
    }

    // This is unsafe, document.write would overwrite the entire page just to be the following line, since its not in markup (paraphrasing Nick)
    //window.jQuery || document.write('<script src="/static/js/lib/jquery.js"><\/script>');
    require(["../amd/main"]);
    require(["NonCFSBackbone"]);
    window.amdShim.execute();
});
