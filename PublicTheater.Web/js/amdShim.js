

(function () {
    var shim = {};
    var loadQueue = [];

    shim.add = function (fn) {
        loadQueue.push(fn);
    };


    /* use this definition of add for non-amd sites */
    /*
        shim.add = function (fn) {
            fn.apply(window);
        };    
    */
    


    shim.execute = function () {
        for (var index = 0; index < loadQueue.length; index++) {
            loadQueue[index].call();
        }
    };


    window.amdShim = shim;

}());