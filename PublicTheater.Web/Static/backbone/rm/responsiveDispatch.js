define([

    'jquery',
    'backbone'

], function ($, Backbone) {

    var Events = Backbone.Events;

    var entrPrefix = "didEnter:";
    var exitPrefix = "didExit:";

    var currWidth = 0;

    var Range = function (opts) {
        this.name = opts.name;
        this.min = opts.min;
        this.max = opts.max;
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

    dispatch.initRanges = function (ranges) {
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

    function forceEmit() {
        _(dispatch.ranges).each(function (range) {
            range.hasBeenInRange = false;
        });
        didChangeWidth();
    }

    function windowLoad() {
        updateWidth();
        _(dispatch.ranges).each(function (range) {
            if (range.isInCurrent()) range.fireEntr();
        });
    };

    function updateWidth() {
        lastWidth = dispatch.currWidth;
        currWidth = getCurrentWidth();
    };

    function getCurrentWidth() {
        var windowWidth = $(window).width();
        var scrollbarWidth = getScrollbarWidth();
        return windowWidth + scrollbarWidth;
    }

    function getScrollbarWidth() {
        var testElement = document.createElement('div');
        $(testElement).css({ width: '100px', height: '100px', overflow: 'scroll', position: 'absolute', top: '-9999px' });
        document.body.appendChild(testElement);
        var scrollbarWidth = testElement.offsetWidth - testElement.clientWidth;
        document.body.removeChild(testElement);
        return scrollbarWidth;
    }

    var isSizeInRange = function (size, range) {
        var aboveMin = (size > range.min);
        var belowMax = (size < range.max);
        if (aboveMin && belowMax) return true;
        else return false;
    };

    dispatch.on('all', function (evtName) {
        console.log("responsiveDispatch: " + evtName);
    });

    $(window).resize(didChangeWidth);
    $(window).load(windowLoad);

    dispatch.forceEmit = forceEmit;

    return dispatch;

});