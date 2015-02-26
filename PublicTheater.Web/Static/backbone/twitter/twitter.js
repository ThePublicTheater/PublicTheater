

define([

    'backbone',
    './model/context',
    './model/tweetCollection',
    './latest/latest',
    'asmxHelpers'

], function (Backbone, Context, TweetCollection, LatestView, AsmxHelpers) {

    return Backbone.View.extend({

        initialize: function () {
            this.context = new Context();
            this.tweets = new TweetCollection();

            this.initLatestView();

            this.loadTweets();
        },

        initLatestView: function () {
            var targetEl = this.$el.find('[data-bb=twitterLatest]')[0];
            this.latestView = new LatestView({
                el: targetEl,
                tweets: this.tweets,
                context: this.context
            });
            this.latestView.render();
        },

        loadTweets: function () {
            var tweets = this.tweets;
            var twitterOnTheTemplate = this.$el.find('[data-bb=twitterLatest]');
            if (twitterOnTheTemplate.length > 0) {
                var userAccounts = $(this.$el.find('[data-bb=twitterLatest]')[0]).attr('data-bb-username'); ;
                $.ajax({
                    url: "/Services/Twitter.asmx/GetTweetsByUserNames",
                    contentType: "application/json",
                    type: "POST",
                    dataType: 'json',
                    data: JSON.stringify({ "usernames": userAccounts })
                })
            .fail(_(function () {
                this.context.set('error', true);
            }).bind(this))
            .done(_(function (asmxResponse) {
                var response = AsmxHelpers.clean(asmxResponse);
                var statuses = _(response.statuses).values();

                this.tweets.add(statuses);
                console.log("Loaded Tweets:", statuses);
            }).bind(this));
            }

        }

    });

});