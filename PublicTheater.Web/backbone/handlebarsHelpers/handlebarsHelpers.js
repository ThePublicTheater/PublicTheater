define([
    "handlebars"
], function (Handlebars) {

    Handlebars.registerHelper('log', function (context, options) {
        console.log(context);
    });


    Handlebars.registerHelper('formattedTime', function (lvalue) {
        if (lvalue)
            var date = moment(lvalue).format("h:mm A");

        return date;
    });

    Handlebars.registerHelper('formattedDate', function (lvalue) {
        if (lvalue)
            var date = moment(lvalue).format("ddd MMM DD");

        return date;
    });


    Handlebars.registerHelper('ifEqual', function (lvalue, rvalue, options) {
        var fnTrue = options.fn, fnFalse = options.inverse;

        if (!!lvalue.match(new RegExp(rvalue, "i")))
            return fnTrue(this);

        return options.inverse(this);
    });


    Handlebars.registerHelper('ifNotEqual', function (lvalue, rvalue, options) {
        var fnTrue = options.fn, fnFalse = options.inverse;

        if (lvalue == rvalue)
            return options.inverse(this);

        return fnTrue(this);
    });
    
    return Handlebars;
});