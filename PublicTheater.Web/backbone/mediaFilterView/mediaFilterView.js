define([
    "base/view"
], function (BaseView) {

    return BaseView.extend({
        events: {
            "click li" : "didClickFilterButton"
        },

        filters: [],

        initialize: function (opts) {
            this.context = opts.context;
        },

        didClickFilterButton: function (evt) {
            var $target = $(evt.currentTarget);

            this.toggleFilterClass($target);

            this.buildFilters();
        },

        buildFilters: function () {
            var filterElements = this.$("li a");

            var filters = [];

            _(filterElements).each(function (filterObj) {
                var filterElement = $(filterObj);
                if (filterElement.hasClass("selected")) {
                    filters.push({
                        value: filterElement.data("filter")
                    });
                }
            });

            this.filters = filters;

            this.trigger("updateFilterEvent");
        },

        appliesToFilters: function (mediaItem) {
            var filterMap = _.map(this.filters, function (filter) {
                if (filter.value === "all")
                    return true;

                return (new RegExp(filter.value, "ig")).test(mediaItem.get("Category"));
            }, this);

            if (filterMap.length > 0 && filterMap.indexOf(true) === -1)
                return false;

            return true;
        },

        toggleFilterClass: function ($target) {
            var anchorTag = $target.find("a");

            anchorTag.toggleClass("selected");

            if ($(anchorTag).data("filter") === "all")
                this.$el.find("li:not(:first) a").removeClass("selected");

            if (!this.$el.find("li a").hasClass("selected"))
                $(this.$el.find("li a")[0]).toggleClass("selected");

            if (this.$el.find("li:not(:first):has(.selected)").length)
                $(this.$el.find("li a")[0]).removeClass("selected");
        }
    });

});