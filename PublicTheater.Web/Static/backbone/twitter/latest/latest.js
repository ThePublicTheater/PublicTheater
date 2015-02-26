
define([

    'backbone',
      './templates',
    'moment'

], function (Backbone, Templates, moment) {

    return Backbone.View.extend({

        template: Templates.latest,

        initialize: function (opts) {
            this.tweets = opts.tweets;
            this.context = opts.context;

            this.tweets.on('add remove reset', this.render, this);
            this.context.on('change', this.render, this);
        },

        templateMap: function () {
            var viewBag = {};
            viewBag.context = this.context.toJSON();
            viewBag.tweets = _(this.tweetMap()).first(1);

            return viewBag;
        },

        tweetMap: function () {
            return this.tweets.map(function (tweet) {
                var base = tweet.toJSON();
                base.monthDay = moment(base.createdAt).format("MMM D");
                base.year = moment(base.createdAt).format("YYYY");
                base.time = moment(base.createdAt).format("h:mm a");

                return base;
            });
        },

        urlify: function (text) {
            var urlRegex = /(https?:\/\/[^\s]+)/g;
            return text.replace(urlRegex, function (url) {
                return '<a href="' + url + '">' + url + '</a>';
            })
        },

        render: function () {
            var html = this.template(this.templateMap());
            html = _.unescape(this.urlify(html));
            this.$el.html(html);
        }

    });

});