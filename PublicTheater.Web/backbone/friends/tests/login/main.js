

require(['/backbone/require.config.js'], function () {

    require([

        'friends/app/context'

    ], function (Context) {

        var ctx = new Context();
        $('#login').on('click', function () {
            ctx.attemptLogin();
        });

    });

});