define([

    "base/view"

], function (BaseView) {

    return BaseView.extend({
        donationRadioSelector: ".cboxDonationAmount input[type=radio]",

        events: {
            "click .cboxDonationAmount input[type=radio]": "didClickDonationAmountRadio",
            "click .otherAmountClick": "didClickOtherAmountRadio",
            "click .setAmount": "didClickSetAmount"
        },

        initialize: function () {
            $('.otherAmountSection').hide();
        },

        didClickDonationAmountRadio: function (evt) {
            var $target = $(evt.currentTarget);

            var $otherCboxes = $(this.donationRadioSelector).not($target);
            $otherCboxes.prop('checked', false);
            $otherCboxes.parent().removeClass("checked");

            $('.otherAmountClick input[type=radio]').prop('checked', false);
            $('.otherAmountClick input[type=radio]').parent().removeClass("checked");

            $("#tboxDonationAmount").val($target.parents(".cboxDonationAmount").find("label")[0].innerHTML.replace("$", ""));

            $('.otherAmountSection').hide('fast');
        },

        didClickOtherAmountRadio: function (evt) {
            $(this.donationRadioSelector).prop('checked', false);
            $(this.donationRadioSelector).parent().removeClass("checked");
            $('.otherAmountSection').show('fast');
        }
    });

});



/* Converted


function showOtherAmount() {
    var $donationRadioSelector = ".cboxDonationAmount input[type=radio]";

    $($donationRadioSelector).on('click', function (evt) {
        var $target = $(evt.currentTarget);

        var $otherCboxes = $($donationRadioSelector).not($target);
        $otherCboxes.prop('checked', false);
        $otherCboxes.parent().removeClass("checked");

        $('.otherAmountClick input[type=radio]').prop('checked', false);
        $('.otherAmountClick input[type=radio]').parent().removeClass("checked");
        $('.otherAmountSection').hide('fast');
    });

    $('.otherAmountSection').hide();

    $('.otherAmountClick').click(function (evt) {
        $($donationRadioSelector).prop('checked', false);
        $($donationRadioSelector).parent().removeClass("checked");
        $('.otherAmountSection').show('fast');
    });

    $('.setAmount').click(function (evt) {
        $($donationRadioSelector).prop('checked', false);
        $($donationRadioSelector).parent().removeClass("checked");
        $('.otherAmountSection').hide('fast');
    });

*/