amdShim.add(function () {


    jQuery(document).ready(readyFunction);

    function readyFunction() {
        var OrderNum = jQuery(".currentOrderTotal").text().replace("$", "").replace(",", "");

        if (OrderNum != "") {
            OrderNum = parseFloat(OrderNum);
            var OrderTotal = OrderNum;
            var Results;
            var Formula = jQuery("#RoundUpFormula");

            try {
                eval(Formula.val());
                jQuery(".suggestedDonation").val("$" + Results.toFixed(2));
                var floatResults = parseFloat(Results);
                var floatFinal = OrderNum + floatResults
                jQuery(".donationTotal").text("$" + floatFinal.toFixed(2));
            }
            catch (err) {
                jQuery(".suggestedDonation").val("$10.00");
                var floatFinal = OrderNum + 10;
                jQuery(".donationTotal").text("$" + floatFinal.toFixed(2));
            }
        }
    }

    function RoundTo(numberToRound, roundTo, minumum) {
        var donationAmount = numberToRound + (roundTo - (numberToRound % roundTo))
        donationAmount = donationAmount - numberToRound;
        if (minumum != null) {
            if (donationAmount < minumum) {
                donationAmount += minumum;
            }
        }
        return donationAmount;
    }


});