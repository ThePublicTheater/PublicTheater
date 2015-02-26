define([
    'jquery',
    'responsiveDispatch',
    'menuAim'

], function ($, responsiveDispatch) {

    var slideshows = {
        slideshowul: $('ul.slideshow'),
        numslideshows: 0,
        faders: [],
        init: function () {
            if (!this.faders.length) {
                this.numslideshows = this.slideshowul.length;
                this.slideshowul.each(function (i) {
                    if ($(this).find('li').length > 1) {
                        slideshows.faders[i] = $(this).bxSlider({
                            mode: 'fade',
                            timeout: 0,
                            speed: 300,
                            controls: !this.classList.contains("noControlsSlideshow"),
                            pager: !this.classList.contains("noControlsSlideshow") && !this.classList.contains("noPagerSlideshow"),
                            nextText: '',
                            prevText: '',
                            auto: !this.classList.contains("manualSlideshow"),
                            adaptiveHeight: this.classList.contains("adaptiveSlideshow")
                        });
                    }
                });
            }
            //This is for the loading GIF
            $('.loading').hide();
            $('.hideTheWrapper').show();
        }
    };

    var CartTimer = {

        syncIntervId: null,

        countdownIntervId: null,

        initialLoad: true,

        ReSyncInterval: 180 * 1000,

        StartSyncTime: function () {
            this.SyncAndCountDown();
            this.syncIntervId = setInterval(this.SyncAndCountDown, this.ReSyncInterval);
        },

        StopSyncTime: function () {
            clearInterval(this.syncIntervId);
        },

        SyncAndCountDown: function () {
            $.ajax({
                type: 'POST',
                url: '/Services/PublicWebService.asmx/GetCartExpiration',
                async: true,
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (data) {
                    var cartItemCount = parseInt(data.d["ItemsInCart"]);
                    if (cartItemCount <= 0) {
                        return;
                    }

                    var cartItem = "(" + cartItemCount + ")";
                    if (CartTimer.initialLoad) {
                        CartTimer.initialLoad = false;
                        $('#lnkCart').append(' ' + cartItem);
                    } else {
                        $('#lnkCart').text($('#lnkCart').text().replace(/\(.*?\)/, cartItem));
                    }


                    var canExpire = Boolean(data.d["CanExpire"]);
                    if (!canExpire) {
                        return;
                    }

                    $('.timeLeft').show();
                    if (!$('.cartExpireSpan').hasClass('hasItems')) {
                        $('.cartExpireSpan').addClass('hasItems').closest('li').addClass('hasItems');
                    }
                    $('#timeLeftInCart').text('Time left to complete purchase:');

                    var startTime = data.d["CartTimer"];

                    if (startTime == "" || startTime == "Expired") {
                        $('.cartExpireSpan').html('Expired');
                        CartTimer.StopSyncTime();
                        return;
                    }
                    CartTimer.countdown($('.cartExpireSpan'), { startTime: startTime, timerEnd: function () { $('.cartExpireSpan').html('Expired'); } });
                }
            });
        },

        countdown: function (elem, userOptions) {

            var options = {
                startTime: "00:01", //format: "mm:ss",
                timerEnd: function () { }
            };
            var pad = "00";

            $.extend(options, userOptions);
            var totalSec = parseInt(options.startTime.substr(0, 2)) * 60 + parseInt(options.startTime.substr(3, 2));
            var counter = 0;

            if (CartTimer.countdownIntervId) {
                clearTimeout(CartTimer.countdownIntervId);
            }
            CartTimer.countdownIntervId = setInterval(function () {
                if (totalSec - counter < 300) {
                    if (!$(elem).hasClass('almostExpired')) {
                        $(elem).addClass('almostExpired');
                    }
                }
                if (counter == totalSec) {
                    options.timerEnd();
                    clearTimeout(CartTimer.countdownIntervId);
                    return;
                }
                else if (counter > totalSec) {
                    clearTimeout(CartTimer.countdownIntervId);
                    return;
                }
                var currentSec = totalSec - counter;

                var min = Math.floor(currentSec / 60) + '';
                var sec = currentSec % 60 + '';
                $(elem).text(pad.substring(0, pad.length - min.length) + min + ':' + pad.substring(0, pad.length - sec.length) + sec);
                counter += 1;
            }, 1000);
        }
    };

    $(document).ready(function () {
        touchNav();
        slideshows.init();
        tabs();
        expandMainNav();
        slideshowHalf();
        homepageSlideShow();
        buildHomepageSlideshowNav();
        fitTextInit();
        initSelectBox();
        tableButton();
        removeBlankImages();
        togglePopUp();
        switchSmallNavs();
        //loginPromoCode(); handled in the CFS common events
        syosFilter();
        CFSCommon();
        cartTableDivision();
        initUniform();
        equalHeights();
        archiveTabWidth();
        highlightGiftCert();
        fixLeftNavPos();
        CartTimer.StartSyncTime();
        adjustCartHeaders();
        externalLinksNewTab();
        stickyThemeLinks();
        smoothScrollAnchorClick();
        mediaBoxSlideshows();
        // on update panel refresh
        window.pageLoad = function () {
            UIsliderTags();
            cartTableDivision();
            initUniform();
        };

        mobileAddtoHomeScreen();
    });


    function mobileAddtoHomeScreen() {
        var pageTypeName = $("#epiPageType").val();
        if (pageTypeName === "HomePageData") {
            require(["/Static/js/lib/addtohomescreen.js"], function () {
                var options = {
                    autostart: false,
                    liftspan: 60
                }
                window.addToHomescreen(options).show();
            });
        }
    }

    function fixLeftNavPos() {
        var navpos = $('.utilityNav').offset();
        if (navpos) {
            console.log(navpos.top);
            $(window).bind('scroll', function () {
                if ($(window).scrollTop() > navpos.top) {
                    $('header').addClass('fixed');
                }
                else {
                    $('header').removeClass('fixed');
                }
            });
        }
    }
    function touchNav() {

        var isTouch = Modernizr.touch;
        var isDesktop = !navigator.userAgent.match(/(iPad)|(iPhone)|(iPod)|(android)|(webOS)/i);

        var itemIndex;
        $('header .mainNav' +
            ' ul li.has-dropdown').children("a").on('click', function (e) {
                var currentRange = responsiveDispatch.ranges.inCurrentRange()[0].name;
                if (currentRange === "medium") {
                    return false;
                }
            });
        if (isTouch && !isDesktop) {
            $('header .mainNav ul li.has-dropdown').children("a").on('touchstart', function (e) {
                var currentRange = responsiveDispatch.ranges.inCurrentRange()[0].name;
                if (currentRange === "medium" || currentRange === "large") {
                    var $itemClicked = $(this).parents("li");
                    var $dropdown = $itemClicked.find(".dropdownMenu");

                    var isVisible = $dropdown.is(":visible");

                    $itemClicked.parents('ul').children("li").find(".dropdownMenu").hide();

                    if (isVisible) {
                        $dropdown.hide();
                    }
                    else {
                        $dropdown.show();
                    }
                    return false;
                }
            });
            $('header .mainNav ul li.has-dropdown').children("a").on('tap', function (e) {
                var currentRange = responsiveDispatch.ranges.inCurrentRange()[0].name;
                if (currentRange === "small") {
                    itemIndex = $(this).parent().index();
                    $(this).parents('ul').children("li:not(:eq(" + itemIndex + "))").find(".dropdown").hide();
                    $(this).next().toggle();
                }
            });

        }
        else {

            $('header').menuAim({
                activate: function (row) {
                    $(row).find('.dropdownMenu').fadeIn("fast");

                    // Position Nav Arrow
                    var targetScrollTop, targetHeight, windowScrollHeight;
                    targetScrollTop = $(row).position().top;
                    targetHeight = $(row).children("a").innerHeight();
                    windowScrollHeight = $(window).scrollTop();
                    calcScroll = targetScrollTop + (targetHeight / 2) - windowScrollHeight;
                    $('.innerArrow').animate({
                        top: calcScroll
                    }, 150);
                },
                deactivate: function (row) {
                    $(row).find('.dropdownMenu').fadeOut("fast");

                },
                exitMenu: function () {
                    //close all dropdown menus
                    $("header").find(".dropdownMenu").fadeOut("fast");
                    return true;
                },
                rowSelector: ".mainNav>ul>li,.utilityNav",
                submenuSelector: ".has-dropdown",
                tolerance: 1,
                delay: 125
            });
        }
    }

    function expandMainNav() {
        $('.toggleSubHomePage a').on('click', function () {
            $('header.top-bar').toggleClass('collapsed');
            $('.subHeader').toggleClass('expanded');
        });
    }

    function tabs() {
        $('.nav-tabs li a').on('click', function () {
            var tabGroup = $(this).closest(".tabWrapper");
            var tabToShow = $(this).parent().index();

            if (tabGroup.hasClass('nested')) {
                tabGroup.find('.nav-tabs li a').removeClass('selected');
                tabGroup.find('.tab-content > div').hide();
                tabGroup.find('.tab-content > div').eq(tabToShow).show();
            }
            else {
                tabGroup.find('> .nav-tabs li a').removeClass('selected');
                tabGroup.find('> .tab-content > div').hide();
                tabGroup.find('> .tab-content > div').eq(tabToShow).show();
            }
            $(this).addClass('selected');

            if ($("#bestAvailableTable").length) {
                cartTableDivision();
            }
            if ($(this).hasClass("linkedtab")) {
                return true;
            }
            // bxSlider doesn't work on hidden slideshows
            $.each(slideshows.faders, function (index, value) {
                value.reloadSlider();
            });
            return false;
        });
    }

    function slideshowHalf() {
        $('.slideshowHalf').bxSlider({
            minSlides: 1,
            maxSlides: 2,
            slideWidth: 540,
            slideMargin: 10,
            prevText: '',
            nextText: '',
            auto: true
        });
    }

    function mediaBoxSlideshows() {
        $('.mediaboxSlideshow').children().children().each(function () {
            var img = $(this).find("img");
            var x = $(img).height() / 100;
            var width = ($(img).width() * (100 / $(img).height()));
            var height = 100;
            $(this).width(width);
            $(img).width(width);
            $(this).height(height);
            $(img).height(height);

        });
        var sliderElm = $('.mediaboxSlideshow').lemmonSlider({
            'infinite': false,
            'loop': false
        });

        if (Modernizr.touch) {
            sliderElm.swipe({
                swipeLeft: function (event, direction, distance, duration, fingerCount) {
                    this.parent().find(".controls .lemmon-next").click();
                },
                swipeRight: function (event, direction, distance, duration, fingerCount) {
                    this.parent().find(".controls .lemmon-prev").click();
                },
                click: function (event, target) {
                    $(target).click();
                }
            });

        }
        // To deal with arrows
        sliderElm.resize();
    }

    function homepageSlideShow() {
        var homeSlideShow = $('.homeSlideShow');
        var numberOfSlides = $('.homeSlideShow li').length;
        window.startingSlide = Math.floor((Math.random() * numberOfSlides) + 1);
        if (numberOfSlides === window.startingSlide) {
            window.startingSlide = 0;
        }
        var $main = $(homeSlideShow[0]).cycle({
            fx: 'fade',
            speed: 1300,
            timeout: 0,
            startingSlide: startingSlide

        });
        var $caption = $(homeSlideShow[1]).cycle({
            fx: 'none',
            speed: 1300,
            timeout: 0,
            startingSlide: startingSlide
        });
    }

    function buildHomepageSlideshowNav() {
        slideshowlength = $('.homeSlideShow li').length;
        for (var i = 0; i < slideshowlength; i++) {
            $('.captionNav').append('<a href="#"></a>');
        }
        $('.captionNav a').eq(window.startingSlide).addClass('selected');
        $('.captionNav a').on('click', function () {
            currentIndex = $(this).index();
            $('.homeSlideShow').cycle(currentIndex);
            $('.captionNav a').removeClass('selected');
            $(this).addClass('selected');
            return false;
        });
    }

    function initSelectBox() {
        $(".feedFormField").each(function () {
            $(this).selectbox({
                effect: "fade"
            });
        });
    }

    function fitTextInit() {
        $(".venues h3").fitText(1.4, { minFontSize: '12px', maxFontSize: '16px' });
        $(".venues h4").fitText(1.4, { minFontSize: '12px', maxFontSize: '14px' });

    }

    function tableButton() {
        $(".OT_TableButton").attr("src", "").attr("type", "submit").attr("value", "Find a Table");
    }

    function switchSmallNavs() {
        $('.switchToPubNav').on('click', function () {
            $('.subHeader').hide();
            $('.publicHeader').show();
            return false;
        });

        $('.switchToSubNav').on('click', function () {
            $('.subHeader').show();
            $('.publicHeader').hide();
            return false;
        });

        $('.toggleMenu').on('click', function () {
            var $subheader = $('.subHeader');
            if ($('.top-bar').hasClass('expanded') && $subheader.length > 0) {
                $('.subHeader').show();
                $('.publicHeader').hide();
            }
        });

    }

    function togglePopUp() {
        var popup = $(".popup");
        popup.addClass("hidePopup");
        $("#chooseSeats").on("click", function () {
            popup.toggleClass("hidePopup");
            return false;
        });
    }

    function loginPromoCode() {

        $('#loginContainer #enterPromoCode').hide();
        $('.havePromoCode a').on('click', function () {
            $('#enterPromoCode').toggle('slow');
            return false;
        });
    }

    function syosFilter() {

        $(".syosWrapper .nav-tabs li a").click(function () {
            $(".syosWrapper .nav-tabs li a").removeClass("selected");
            $(this).addClass("selected");
            return false;
        });
    }

    function CFSCommon() {

        // Promo Code on Login Page
        $('#enterPromoCode').hide();
        $('#havePromo').click(function () {
            $(".promo").slideToggle(300);
            $('#enterPromoCode').slideToggle(300);
            return false;
        });
        $('.promoWhatsThisLink').click(function () {
            $('.promoWhatsThisContent').slideDown();
        });
        $('.promoWhatsThisContent .close').click(function () {
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
    }

    function cartTableDivision() {

        var itemWidth = "auto";
        var numbUL = $("#bestAvailableTable").children('ul').length;
        var numbLi = $("#bestAvailableTable ul li").length / numbUL;

        if (numbLi > 0) {
            itemWidth = (100 / numbLi) + "%";
        }
        $("#bestAvailableTable ul li").css("width", itemWidth);
    }

    function archiveTabWidth() {

        var tabWidth = "auto";
        var tabLi = $(".venueFilters").children('li').length;

        if (tabLi > 0 && $(window).width() >= 650) {
            tabWidth = (100 / tabLi) + "%";
        }
        if ($(window).width() >= 650) {
            $(".venueFilters li").css("width", tabWidth);
        }
    }

    function initUniform() {
        $("input[type='radio'], input[type='checkbox']").uniform();
    }

    function UIsliderTags() {

        var levelsWidth = "auto";
        var numbLevels = $(".donationSliderAmounts").children('li').length;

        if (numbLevels > 0) {
            levelsWidth = (100 / numbLevels) + "%";
        }
        $(".donationSliderAmounts li").css("width", levelsWidth);
    }

    function highlightGiftCert() {

        $('.giftCerts .chooseDesign .thumbnails .thumbnail:first').addClass('giftCertCurrent');

        $('.giftCerts .chooseDesign .thumbnails .thumbnail').click(function () {
            $('.giftCerts .chooseDesign .thumbnails .thumbnail').removeClass('giftCertCurrent');
            $(this).addClass('giftCertCurrent');
        });

    }

    function equalHeights() {
        if ($('.slideshowCaptions.PDP').length > 0) {
            $('.slideshowCaptions.PDP').equalize();
        }

        if ($('#ticketHistoryFooter div').length > 0) {
            $('#ticketHistoryFooter div').equalize();
        }
    }

    function removeBlankImages() {
        $('img[src=""]').hide();
        $('img:not([src=""])').show();
    }

    function adjustCartHeaders() {
        if ($('.emptyCartAlert').length > 0) {
            $('.cartHead').hide();
        }
    }

    function externalLinksNewTab() {
        $("a[href^=http]").each(function () {
            if (this.href.indexOf(location.hostname) == -1) {
                $(this).attr({
                    target: "_blank"
                });
            }
        });
    }

    function smoothScrollAnchorClick() {
        $('a[href*=#]:not([href=#])').click(function () {
            if (location.pathname.replace(/^\//, '') == this.pathname.replace(/^\//, '')
                || location.hostname == this.hostname) {

                var target = $(this.hash);
                target = target.length ? target : $('[name=' + this.hash.slice(1) + ']');
                if (target.length) {
                    $('html,body').animate({
                        scrollTop: target.offset().top
                    }, 500);
                    return false;
                }
            }
        });
    }

    function stickyThemeLinks() {
        //SiteTheme=JoesPub
        var hasSiteTheme = false;
        var siteThemeQueryString = "";
        if (window.location.search != "") {
            var queryString = window.location.search.substring(1);
            var queryStringParts = queryString.split('&');
            for (var i = 0; i < queryStringParts.length; i++) {
                var keyValuePair = queryStringParts[i].split('=');
                if (keyValuePair[0] === "SiteTheme") {
                    siteThemeQueryString = queryStringParts[i];
                    hasSiteTheme = true;
                    break;
                }
            }
        }

        if (hasSiteTheme) {
            $(".mainContent a[href^=http]").each(function () {
                var href = this.href;
                //if it is a internal link and sitetheme not specified already, append current theme to it
                if (href.indexOf(location.hostname) >= 0 && href.indexOf("SiteTheme") == -1) {
                    href += (href.match(/\?/) ? '&' : '?') + siteThemeQueryString;
                    $(this).attr('href', href);
                }
            });

            $(".mainContent a[href^='/']").each(function () {
                var href = this.href;
                //if it is a internal link and sitetheme not specified already, append current theme to it
                if (href.indexOf("SiteTheme") == -1) {
                    href += (href.match(/\?/) ? '&' : '?') + siteThemeQueryString;
                    $(this).attr('href', href);
                }
            });
        }
    }

    $(document).foundation();

})
