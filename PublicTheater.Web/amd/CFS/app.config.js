define(function () {

    var cfsConfig = {

        dir: "../amd/CFS/",   //relative to the main.js file that defines baseUrl

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

        var url = cfsConfig.dir + part;
        return url;
    };

    return cfsConfig;
});
    

    
