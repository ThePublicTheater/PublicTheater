define([

], function () {

    return {
        formatCurrency: function (amount) {

            var n = amount.toFixed(2).split(".");
            n[0] = n[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");

            if (n[1] === "00")
                return "$" + n[0];
            else
                return "$" + n.join(".");
        }
    }
}
);