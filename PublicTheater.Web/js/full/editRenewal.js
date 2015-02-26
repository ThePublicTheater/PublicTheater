amdShim.add(function () {

    var MAX_LENGTH = 1000;


    jQuery(document).ready(function () {
        jQuery('textarea').bind('keyup', limitChars);
        jQuery('.viewMore').click(function () {
            jQuery(this).next('.collapsedContent').slideToggle();
            jQuery(this).toggleClass('selected');
            return false;
        });

        jQuery(".packageChoices input:radio").change(updatePriceDifferences);
        jQuery(".packageChoices input:radio").each(function () {
            if (jQuery(this).is(":checked")) {
                jQuery(this).parent().find(".packagePerformances").show();
            }
        });

        jQuery('.renewalQuantity select').change(handleQuantityChanges);
    });

    function updatePriceDifferences() {
        var currentRadio = jQuery(this);
        var container = currentRadio.parents(".venuePanelsDetails");

        container.find(".sectionChoices .priceDifference").each(function () {
            jQuery(this).fadeOut();
        });

        if (currentRadio.is(":checked")) {
            jQuery(this).parent().find(".packagePerformances").slideDown();
        }

        jQuery(".packageChoices input:radio").each(function () {
            if (jQuery(this).is(":checked") == false && jQuery(this).parent().find(".packagePerformances").is(":visible")) {
                jQuery(this).parent().find(".packagePerformances").slideUp();
            }
        });


        //Set a timeout so the fadeout isn't overridden by fade in - AJM
        setTimeout(function () {

            var arrayOfDifferences = getDifferencesArray(currentRadio);

            container.find(".sectionChoices li input:radio").each(function () {
                var sectionId = parseInt(jQuery(this).val());
                var priceDifference = findSectionDifference(arrayOfDifferences, sectionId);
                var sectionDifference = jQuery(this).parent().find(".priceDifference");

                var priceDifferenceString = "";

                if (priceDifference == 0) {
                    //Do nothin as we want to keep it empty
                }
                else if (priceDifference < 0) {
                    priceDifferenceString = "(-$" + Math.abs(priceDifference) + ")";
                }
                else {
                    priceDifferenceString = "($" + priceDifference + ")";
                }

                sectionDifference.text(priceDifferenceString);
            });

            container.find(".sectionChoices .priceDifference").each(function () {
                jQuery(this).fadeIn();
            });
        },
            300);

    }

    function getDifferencesArray(radioButton) {
        var fullArray = new Array();
        var fullList = radioButton.parent().find(".differences input").val();
        var splitList = fullList.split(",");

        for (var index = 0; index < splitList.length; index++) {
            var sectionIdAndPrice = splitList[index].split(":");
            var newArray = [sectionIdAndPrice[0], sectionIdAndPrice[1]];
            fullArray.push(newArray);
        }
        return fullArray;
    }

    function findSectionDifference(arrayOfDifferences, sectionId) {
        for (var index = 0; index < arrayOfDifferences.length; index++) {
            var arrayId = parseInt(arrayOfDifferences[index][0]);
            if (arrayId == sectionId) {
                return parseFloat(arrayOfDifferences[index][1]);
            }
        }

        return 0;
    }

    function limitChars() {
        var val = jQuery(this).val();
        var len = jQuery(this).attr('value').length;
        if (len >= MAX_LENGTH) {
            jQuery(this).val(val.substring(0, MAX_LENGTH));
        }
    }

    function handleQuantityChanges() {
        var parentDiv = jQuery(this).parent();
        var originalQuantity = parentDiv.find('input[type=hidden]').val();
        var currentQuantity = jQuery(this).val();

        parentDiv.find('div').hide();
        if (parseInt(originalQuantity, 10) > parseInt(currentQuantity, 10))
            parentDiv.find('.lowered').show();

        if (parseInt(originalQuantity, 10) < parseInt(currentQuantity, 10))
            parentDiv.find('.increased').show();
    }

});