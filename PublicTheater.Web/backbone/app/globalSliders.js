

define([

    'base/view'

], function (BaseView) {

    if ($(".pdpRelatedMediaItemsSlider").length > 0) {
        $(".pdpRelatedMediaItemsSlider").bxSlider({
            nextText: "",
            prevText: "",
            infiniteLoop: false,
            minSlides: 3,
            maxSlides: 3,
            slideWidth: 215.25,
            slideMargin: 5,
            hideControlOnEnd: true,
            auto: true
        });
    }
});