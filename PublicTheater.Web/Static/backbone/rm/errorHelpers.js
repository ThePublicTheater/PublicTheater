

/**
Fancy AJAX error printing to console
@class ErrorHelpers
@requires underscore
**/

define([

    'underscore'

], function (_) {

    return {

        /**
        prints grouped error information to the Chrome console
        @method printErrorToConsole
        **/
        printErrorToConsole: function () {
            console.group("%cAJAX Error Details", "color:red;");
            var errorDetail = arguments[0];
            var responseText = JSON.parse(errorDetail.responseText);
            _(responseText).each(function (value, key) {
                console.group(key);
                console.log(value);
                console.groupEnd();
            });
            console.log('error detail: %o', errorDetail);
            console.groupEnd();
        },

        qError: function (reason) {
            console.error("error in promise chain!");
            throw reason;
        }

    };

});