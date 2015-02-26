

define([

    'q',
    'friends/app/app',
    'friends/app/context'

], function (Q, FriendsApp, Context) {

    window.Q = Q;

    var context = new Context();

    return new FriendsApp({
        el: $('body'),
        context: context
    });

});