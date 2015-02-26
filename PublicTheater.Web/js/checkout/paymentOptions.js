amdShim.add(function () {

    $(function () {
        var postOfficeValue = $("#hfShippingAddressTypeID").val();

        radios = $('ul li input[type=radio]');
        postOfficeRadio = $('ul li input[type=radio][value="' + postOfficeValue + '"]');
        shippingAddressPanel = $("#pnlShippingAddress");

        ShowHideShipping(postOfficeRadio, shippingAddressPanel);

        paymentPlanOptIn = $('.optInPaymentPlan input');
        paymentPlanChoice = $('.paymentPlanSelect select');
        paymentPlanAmts = $('#paymentPlansAmts .plans');
        paymentPlanChoice.change(updatePlanAmounts);
        paymentPlanOptIn.change(togglePaymentSelect);

        $('#checkoutBtn .checkout').click(function () {
            $('.loadingContainer').show();
        });


        moveTotalBelowDonation();
        noDonationLink();
        radios.change(function () {
            ShowHideShipping(postOfficeRadio, shippingAddressPanel);
        });
    });

    function noDonationLink() {
        $('#noDonation').click(function (evt) {
            $('.suggestedDonation').hide();
            $('.donationTotalWrap').hide();
            evt.preventDefault();
        });

    }
    function moveTotalBelowDonation() {
        var $donation = $(".donationTotalWrap");
        if (!!$donation && $donation.length > 0) {
            $donation.parent().append($(".total").remove());
        }
    }

    function togglePaymentSelect() {
        if ($(this).is(':checked')) {
            paymentPlanChoice.attr('disabled', false);
        } else {
            paymentPlanChoice.attr('disabled', true).val('');
            paymentPlanAmts.hide();
        }
    }

    function updatePlanAmounts() {
        var planToShow = '#paymentPlansAmts .plan_' + $(this).val();
        paymentPlanAmts.hide();
        $(planToShow).show();
    }

    function ShowHideShipping(postOfficeRadio, shippingAddressPanel) {
        var singleDeliveryMethod = jQuery("#hfSingleShippingMethod").val();
        var postOfficeValue = $("#hfShippingAddressTypeID").val();

        if (postOfficeRadio.prop("checked") || singleDeliveryMethod == postOfficeValue) {
            shippingAddressPanel.show();
        }
        else {
            shippingAddressPanel.hide();
        }
    }

});