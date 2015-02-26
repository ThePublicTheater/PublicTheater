requirejs.config({
    baseUrl: '/static/backbone',

    // if a module is defined here, make sure to always use the assigned 'keyword', not the path
    paths: {
        jquery: 'lib/jquery',
        jqueryui: 'lib/jqueryui',
        underscore: 'lib/underscore',
        backbone: 'lib/backbone',
        bootstrap: 'lib/bootstrap',
        handlebars: 'lib/handlebars',
        head: 'lib/head.load',
        moment: 'lib/moment',
        q: 'lib/q',

        //adage cdn hosted
        asmxHelpers: 'rm/asmxHelpers',
        errorHelpers: '/rm/errorHelpers',

        cfsApp: '../js/app/run'
    },

    // for global-based JS, register the plugin along with the dependencies here
    shim: {
        jquery: {
            exports: 'jQuery'
        },
        jqueryui: {
            deps: ['jquery']
        },
        underscore: {
            exports: '_'
        },
        handlebars: {
            exports: 'Handlebars'
        },
        backbone: {
            deps: ['underscore', 'jquery'],
            exports: 'Backbone'
        },
        bootstrap: {
            deps: ['jquery']
        },
        cfsApp: {
            deps: ['jqueryui', 'backbone', 'head', 'handlebars', 'moment', 'bootstrap'],
            exports: 'cfs'
        },
        syoscore: {
			deps: ['syostemplates']
        }
    }

});