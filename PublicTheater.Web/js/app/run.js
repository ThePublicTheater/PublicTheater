(function () {

    $(document).ready(function () {
        //$.ajax({
        //    url: '/js/app/app.config.js',
        //    error: errorConfig,
        //    success: parseConfig
        //});
        head.js('/js/app/app.config.js', startCFS);
    });

    function startCFS() {
        head.js(cfsConfig.dir + "main.js", function () {
            cfs = new CFSApp();
        });
    }

    function errorConfig() {
        console.error("Error loading CFS config");
        console.log(arguments);
    }

}).call(this);