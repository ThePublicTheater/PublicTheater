define(function () {

    var cfsConfig = {

        dir: "/backbone/CFS",

        querySuffix: "?q=1337",

        facebook: false,

        reserveControl: {
            target: "#reserveSelectSeats"
            //,customScript: '/js/reserve/customReserveControl.js'
        },

        exchangeControl: {
            target: ""
        }
    };

    cfsConfig.makeCFSUrl = function (part) {
        var url = cfsConfig.dir + part + cfsConfig.querySuffix;
        return url;
    };

    return cfsConfig;
});
    

    
