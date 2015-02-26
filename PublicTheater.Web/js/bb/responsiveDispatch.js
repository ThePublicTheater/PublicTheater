(function ($, Events) {

    var ranges = [
        {
            name: "large",
            min: 979,
            max: Infinity
        },
        {
            name: "medium",
            min: 768,
            max: 979
        },
        {
            name: "small",
            min: 0,
            max: 768
        }
    ];

    var entrPrefix = "didEnter:";
    var exitPrefix = "didExit:";

    var currWidth = 0;

    var Range = function (opts) {
        this.name           = opts.name;
        this.min            = opts.min;
        this.max            = opts.max;
        this.hasBeenInRange = false;
    };

    Range.prototype.isInCurrent = function () {
        return isSizeInRange(currWidth, this);
    };

    Range.prototype.fireEntr = function () {
        this.hasBeenInRange = true;
        dispatch.trigger(entrPrefix + this.name);
    };

    Range.prototype.fireExit = function () {
        this.hasBeenInRange = false;
        dispatch.trigger(exitPrefix + this.name);
    };

    var RangeList = function () { };

    RangeList.prototype = [];

    RangeList.prototype.inCurrentRange = function () {
        return _(this).filter(function (range) {
            return range.isInCurrent();
        });
    };

    var dispatch = _(Events).clone();
    dispatch.ranges = new RangeList();

    dispatch.initRanges = function () {
        _(ranges).each(function (opts) {
            dispatch.ranges.push(new Range(opts));
        });
    };

    function didChangeWidth() {
        updateWidth();
        _(dispatch.ranges).each(function (range) {
            var isInCurrent = range.isInCurrent();
            var hasBeenInRange = range.hasBeenInRange;
            if (isInCurrent && !hasBeenInRange) range.fireEntr();
            if (!isInCurrent && hasBeenInRange) range.fireExit();
        });
    };

    function windowLoad() {
        updateWidth();
        _(dispatch.ranges).each(function (range) {
            if (range.isInCurrent()) range.fireEntr();
        });
    };

    function updateWidth() {
        lastWidth = dispatch.currWidth;
        currWidth = $(window).width();
    };

    var isSizeInRange = function (size, range) {
        var aboveMin = (size > range.min);
        var belowMax = (size < range.max);
        if (aboveMin && belowMax) return true;
        else return false;
    };

    dispatch.on('all', function (evtName) {
        console.log("responsiveDispatch triggered : " + evtName);
    });

    (function (responsiveDispatch) {

        responsiveDispatch.initRanges();
        $(window).resize(didChangeWidth);
        window.responsiveDispatch = responsiveDispatch;
        $(window).load(windowLoad);

    })(dispatch);

})(jQuery, Backbone.Events);