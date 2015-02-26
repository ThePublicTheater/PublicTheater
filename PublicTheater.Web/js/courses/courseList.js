amdShim.add(function () {

    jQuery(document).ready(documentReady);

    function documentReady() {
        setDefaultValue();
        jQuery(".filter ul li input:checkbox").click(updateFiltering);
        jQuery(".filter ul li input:radio").click(updateFiltering);
    }

    function setDefaultValue() {
        //If default keyword is set, then check that radio or checkbox field
        var defaultVal = jQuery("#defaultKeyword").val();
        if (defaultVal != "") {
            jQuery(".filter ul li").each(function () {
                var lineItem = jQuery(this);
                var value = lineItem.find("span.hiddenArea input").val();
                if (value == defaultVal) {
                    lineItem.find("input:radio").prop("checked", true);
                    lineItem.find("input:checkbox").prop("checked", true);
                }
            });
        }
    }

    function updateFiltering() {
        //Show loading GIF
        jQuery(".loadingContainer").show();

        //Button triggers postback inside update panel
        var postBackButton = jQuery("#btnCourseFilterChanged");
        //Field tracks keyword selected for server side processing
        var selectedKeywordField = jQuery("#selectedKeyword");

        var newValue = getFilterString();

        selectedKeywordField.val(newValue);
        postBackButton.click();
    }

    function getFilterString() {
        var filters = "";
        var prependComma = false;

        jQuery(".filter ul li").each(function () {
            var lineItem = jQuery(this);
            var checkBox = lineItem.find("input:checkbox");
            var radioButton = lineItem.find("input:radio");
            if (checkBox.is(":checked")) {
                var value = lineItem.find("span.hiddenArea input").val();
                if (prependComma) {
                    filters = filters + "," + value;
                }
                else {
                    filters = value;
                    prependComma = true;
                }
            }
            if (radioButton.is(":checked")) {
                var value = lineItem.find("span.hiddenArea input").val();
                if (prependComma) {
                    filters = filters + "," + value;
                }
                else {
                    filters = value;
                    prependComma = true;
                }
            }
        });

        return filters;
    }

});