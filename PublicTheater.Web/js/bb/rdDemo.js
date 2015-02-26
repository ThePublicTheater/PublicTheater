(function ($) {

    responsiveDispatch.on('didEnter:medium', function () {
        console.log('med mode');
    });

    responsiveDispatch.on('didExit:medium', function () {
        console.log('med mode bye');
    });
    
    responsiveDispatch.on('didEnter:large', function () {
        console.log('large mode');
    });

    responsiveDispatch.on('didExit:large', function () {
        console.log('large mode bye');
    });

})(jQuery);