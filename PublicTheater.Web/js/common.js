(function($) {

    $(function () {

        // Promo Code on Login Page
        $('#enterPromoCode').hide();
        $('#havePromo').live("click", function () {
            $(".promo").slideToggle(300);
            $('#enterPromoCode').slideToggle(300);
            return false;
        });
        $('.promoWhatsThisLink').live("click", function () {
            $('.promoWhatsThisContent').slideDown();
        });
        $('.promoWhatsThisContent .close').live("click", function () {
            $('.promoWhatsThisContent').slideUp();
        });

        $(".calendarWrap").hide();

        $("#changePerformance").click(function () {
            $(".calendarWrap").slideDown().parent().css("position", "relative");
            return false;
        });

        $(".syosToggleBtn .on").click(function () {
            $(".calendarWrap").hide().parent().css("position", "static");
        });

        $('.singleTicketWrapper').not(':first').find('.headerLine').remove();


    });
    
})(jQuery);