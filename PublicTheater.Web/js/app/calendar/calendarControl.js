(function ($) {

    // #region Closure globals

    var logPrefix = "CFS:CalendarControl ";

    var Errors = {
        cfsUndefined: function () {
            throw logPrefix + "CFS object was not defined...";
        }
    };

    // #endregion

    // #region Pre flight check

    if (typeof CFS === "undefined") Errors.cfsUndefined();

    // #endregion


    // #region CalendarControl

    var CalendarControl = CFS.View.extend({

        initialize: function () {
            $("#calendarTable tr td:nth-child(1n+6)").addClass("fromLeft");
            this.initPerformancePopovers();
            $('body').on('click', _(this.didClickBody).bind(this));
            $(window).on('resize', _(this.didResizeWindow).bind(this));
            
        },

        initPerformancePopovers: function () {
            this.popovers = [];

            _(this.getBBElementAll('performancePopover')).each(function (element) {
                var $element = $(element);
                var $content = $element.find('[data-bb="popoverContent"]');
                var $link = $element.find('[data-bb="popoverLink"]');

                $content.hide();
                var fromLeft = $element.parents('td').first().hasClass('fromLeft');
                var direction = "right";
                if (fromLeft)
                    direction = "left";

                $link.popover({
                    trigger: 'manual',
                    html: true,
                    content: $content.html(),
                    placement: direction
                });

                this.popovers.push($link[0]);
            }, this);
        },

        events: {
            "click .popover": "didClickPopover",
            "click [data-bb='popoverLink']": "didClickPopoverLink"
        },

        didClickPopover: function (evt) {
            evt.stopPropagation();
        },

        didClickPopoverLink: function (evt) {
            evt.stopPropagation();

            var $target = $(evt.currentTarget);

            var popover = $target.data('popover');
            var shown = popover && popover.tip().is(':visible');

            if (shown) {
                $target.popover('hide');
            }
            else {
                this.hideAllPopovers();
                $target.popover('show');
                $target.next('.popover').addClass('calendarPopUp');
            }

            var popupHeight = $target.next('.popover').innerHeight();
            var popupHeightAdjusted = popupHeight / 2;
            $target.next('.popover').css('top', -popupHeightAdjusted);
            
            var popupTop = $target.next('.popover').offset().top,
                popupOffset = popupTop - $(window).scrollTop(),
                headerHeight = $('[data-tag="fixedHeader"]').height(),
                offsetInPixels = 15;

            if (popupOffset < headerHeight ) {
                $('html, body').animate({
                    scrollTop: popupTop - headerHeight - offsetInPixels
            });
            }

        },

        didClickBody: function (evt) {
            this.hideAllPopovers();
        },

        didResizeWindow: function (evt) {
            this.hideAllPopovers();
        },

        hideAllPopovers: function () {
            _(this.popovers).each(function (element) {
                var $element = $(element);

                var popover = $element.data('popover');
                var shown = popover && popover.tip().is(':visible');
                if (shown) {
                    $element.popover('hide');
                }
            }, this);
        }

    });

    // #endregion


    // #region Namespacing

    CFS.CalendarControl = CalendarControl;

    // #endregion


})(jQuery);


