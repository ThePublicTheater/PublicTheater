if (typeof chrome === "undefined") {
    console = {};
    console.log = function () { return this; };
    console.group = function () { return this; };
    console.groupCollapsed = function () { return this; };
    console.groupEnd = function () { return this; };
    console.time = function () { return this; };
    console.timeEnd = function () { return this; };
    console.trace = function () { return this; };
    console.count = function () { return this; };
    console.info = function () { return this; };
    console.warn = function () { return this; };
    console.dirxml = function () { return this; }
    console.error = function () { return this; };
}