//cfsConfig = {
    
//    dir: "/js/app/",

//    facebook: false,

//    reserveControl: {

//        target: ".siteContent",

//        calendarOptions: {
//            dayNamesMin: ["S", "M", "T", "W", "T", "F", "S"],
//            nextText: ">",
//            prevText: "<"
//        }

//    }
//};

cfsConfig = {
    
    dir: "/js/app/",

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

function makeCFSUrl(part) {
    var url = cfsConfig.dir + part + cfsConfig.querySuffix;
    return url;
};

