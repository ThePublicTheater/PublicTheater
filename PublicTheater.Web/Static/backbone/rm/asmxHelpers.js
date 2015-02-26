

/**
Helper functions for consuming ASMX Services
@class AsmxHelpers
@requires underscore
**/

define([

    'underscore'

], function (_) {
    var exports = {};

    /**
    Removes the d root object, converts Pascal Case to Camel
    @method clean
    **/
    var clean = exports.clean = function (response) {
        var raw = response.d;
        parsed = allKeysToCamelCase(raw);
        return parsed;
    };

    function allKeysToCamelCase(obj) {
        var output = {};
        var isArray = _.isArray(obj);

        for (var key in obj) {
            var value = obj[key];
            var camelKey = stringToCamelCase(key);

            if (typeof value === 'object') output[camelKey] = allKeysToCamelCase(value);
            else output[camelKey] = value;
        }

        if (isArray) {
            return _(output).values();
        }

        var output = _(output).omit('__type');

        return output;
    };

    function stringToCamelCase(str) {
        return str.charAt(0).toLowerCase() + str.slice(1);
    };


    return exports;
});