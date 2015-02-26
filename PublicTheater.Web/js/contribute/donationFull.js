// Funds
define([

    "base/view"

], function (BaseView) {

    return BaseView.extend({
        events: {
            // Acknowledgement Section
            "click #cbListedDonation": "didClickListedDonor",
            "click #cbAnonDonor": "didClickAnonymousDonor",

            // Matching Gift Section
            "click #cbxCompanyMatching": "didClickMatchGift",
            "click .funds li" : "didClickRadioFunds",
            "click [data-target='donationSliderAmountList'] li" : "didClickSliderAmountLabel"
        },

        initialize: function () {
            this.initControls();

            this.initializeSlider();

            $(window).on('resize', _(this.generateSliderLabelWidths).bind(this));
        },

        initControls: function () {
            this.$donationAmountTextbox = this.$("[data-target='donationAmount']");

            this.$shortDonationAmountDescription = this.$("[data-target='shortDonationAmountDescription']");
            this.$longDonationAmountDescription = this.$("[data-target='longDonationAmountDescription']");

            this.$donorAckNameTextbox = this.$("#txtDonorName");
            this.$matchGiftTextbox = this.$("#txtCompanyName");
        },

        didClickRadioFunds: function (evt) {
            var $target = $(evt.currentTarget);

            // Resets
            $("li .radio span", $target.parent()).removeClass("checked");
            $("input[type=radio]", $target.parent()).prop("checked", false);

            // Target Check
            $target.find(".radio span").toggleClass("checked");

            $target.find("input[type=radio]").prop("checked", true);
        },

        didClickSliderAmountLabel: function (evt) {
            var $target = $(evt.currentTarget);

            var numberAmount = parseInt($target.html().replace("$", "").replace(/\,/g, ''));

            var donationLevel = _.indexOf(this.donationAmountOptions, numberAmount);

            if(donationLevel != undefined)
                this.donationSlider.slider("option", "value", donationLevel);
        },

        didClickMatchGift: function (evt) {
            var $target = $(evt.currentTarget);

            if ($target.prop("checked")) {
                this.$matchGiftTextbox.prop("disabled", false);
            } else {
                this.$matchGiftTextbox.prop("disabled", true);
                this.$matchGiftTextbox.val("");
            }
        },

        didClickListedDonor: function (evt) {
            this.$donorAckNameTextbox.val("");
            this.$donorAckNameTextbox.prop("disabled", false);
        },

        didClickAnonymousDonor: function (evt) {
            this.$donorAckNameTextbox.val("Anonymous");
            this.$donorAckNameTextbox.prop("disabled", true);
        },
        

        didChangeDonationAmount: function (evt) {
            var amount = this.$donationAmountTextbox.val();

            this.$shortDonationAmountDescription.html(this.getDonationShortDescByAmount(amount));
            this.$longDonationAmountDescription.html(this.getDonationLongDescByAmount(amount));
        },

        getDonationLongDescByAmount: function (amount) {
            var selector = "[data-target='donationLevel'] > input[type=hidden][value=" + amount + "]";
            var $matchinghiddenAmountArea = this.$(selector);

            return $matchinghiddenAmountArea.parent().find("[data-target='donationDescriptionArea']").html();
        },

        getDonationShortDescByAmount: function (amount) {
            var selector = "[data-target='donationLevel'] > input[type=hidden][value=" + amount + "]";
            var $matchinghiddenAmountArea = this.$(selector);
            var markup = $matchinghiddenAmountArea.parent().find(".levelTitle");
            markup.find('span').html("$" + this.formatAmountString(amount));

            //return $matchinghiddenAmountArea.parent().find(".levelTitle").html();
            return markup.html();
        },

        initializeSlider: function () {
            var $slider = this.$("[data-target='donationSliderArea']");            

            this.donationAmountOptions = this.buildDonationAmountOptions();

            this.displayDonationAmounts(this.donationAmountOptions);

            this.$donationAmountTextbox.val(this.donationAmountOptions[0]);
            this.didChangeDonationAmount();
            this.updateHighlightPrice(0);

            this.updateSliderMargin($slider);

            // Steps represent index in the array of amount options. Change function sets hidden value to value at index in array
            // Using values for actual steps would make for uneven steps and a complicated slide algorithm.
            this.donationSlider = $slider.slider({
                min: 0,
                max: this.donationAmountOptions.length - 1,
                step: 1,
                change: _.bind(function (evt, ui) {
                    this.$donationAmountTextbox.val(this.donationAmountOptions[ui.value]);

                    this.didChangeDonationAmount();
                    this.updateHighlightPrice(ui.value);
                }, this)
            });
        },

        updateHighlightPrice: function (i) {
            i = i + 1;
            var $donationAmountList = $("[data-target='donationSliderAmountList']");
            $donationAmountList.find('li').removeClass('selected');
            $donationAmountList.find('li:nth-child(' + i + ')').addClass('selected', 500, "linear");
        },

        updateSliderMargin: function ($slider) {
            var parentWidth = this.getParentWidth();
            var lastChildWidth = this.getLastChildWidth();

            var sliderRightOffset = lastChildWidth - 1 / this.donationAmountOptions.length * .2 * parentWidth;
            $slider.css({
                'margin-right': sliderRightOffset + "px",
                'margin-left': 100 / this.donationAmountOptions.length * .2 + '%'
            });
        },

        displayDonationAmounts: function () {
            var $donationAmountList = $("[data-target='donationSliderAmountList']");

            _.each(this.donationAmountOptions, function (amount) {

                var liHtml = "<li>$" + this.formatAmountString(amount) + "</li>";

                $donationAmountList.append(liHtml);
            }, this);
        },

        buildDonationAmountOptions: function () {
            var $donationAmountDivs = this.$("[data-target='donationLevel']");

            return _.map($donationAmountDivs, function (donationAmountDiv) {
                var hiddenInputStartValue = $("input[type=hidden]:first", donationAmountDiv).val();
                return parseInt(hiddenInputStartValue);
            });
        },

        didRender: function () {
            this.generateSliderLabelWidths();
        },
        
        generateSliderLabelWidths: function () {
            var levelsWidth = "auto";
            var numbLevels = this.$(".donationSliderAmounts").children('li').length;

            var parentWidth = this.getParentWidth();
            var lastChildWidth = this.getLastChildWidth();
            

            if (numbLevels > 0) {
                //levelsWidth = (100 / numbLevels) + "%";
                levelsWidth = ((parentWidth - lastChildWidth) / (numbLevels - 1)) / parentWidth;
                levelsWidth = levelsWidth * 100 + "%";
            }

            this.$(".donationSliderAmounts li:not(:last)").css("width", levelsWidth);
        },

        getParentWidth: function () {
            return this.$(".donationSliderAmounts").width();
        },

        getLastChildWidth: function () {
            return this.$(".donationSliderAmounts li").last().width();
        },

        formatAmountString:function(amount){
            return amount.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
        }
    });
});


/*
$(".fundRightCol").click(function (evt) {
        var $this = $(this);

        var $fundSpan = $($this.find("span")[0]);

        $fundSpan.toggleClass("checked");
    });

    // Toggle Commemorative Gift Section
    //if (!$("#cbCommemorativeGift").is(":checked")) { $(".commemorativeGiftContainer").hide(); }
    //$("#cbCommemorativeGift").click(function () {
    //    if ($(this).is(":checked")) {
    //        $("#cbCommemorativeGift").attr('checked', false);
    //        $(this).attr('checked', true);
    //    }
    //    if ($("#cbCommemorativeGift").is(":checked")) {
    //        $(".commemorativeGiftContainer").show();
    //    }
    //    else {
    //        $(".commemorativeGiftContainer").hide();
    //    }
    //});

    // Accord
    $('.benefits p.levelTitle').click(function () {
        $('.benefits p.levelTitle').removeClass('ui-state-active');
        $(this).addClass('ui-state-active');
        if ($(this).next().is(':visible')) {
            $(this).removeClass('ui-state-active').next().slideUp();
        }
        else {
            $('.donorInfo:visible').slideUp();
            //$(this).next().slideDown();
        }
        return false;
    });

    $('[data-action="setStartingLevel"]').on('click', function (evt) {
        evt.preventDefault();
        var donationAmount = $(evt.currentTarget).parents('[data-target="donationLevel"]').find('input[type="hidden"]').first().val();
        $('[data-target="donationAmount"]').first().val(donationAmount);
    });

    $(".anonCheckbox input:checkbox").change(function () {
        if ($(this).is(":checked")) {
            $(".donorName input").val("Anonymous");
        }
        else {
            $(".donorName input").val("");
        }
    });

    $(".funds ul li .fundRightCol input:radio").change(function () {
        var currentRadio = $(this);
        currentRadio.attr("checked", "checked")
        $(".funds ul li .fundRightCol input:radio").each(function () {
            var newRadio = $(this);
            if (newRadio[0].id != currentRadio[0].id) {
                newRadio[0].checked = false;
                newRadio.removeAttr("checked");
            }
        });

    });
*/