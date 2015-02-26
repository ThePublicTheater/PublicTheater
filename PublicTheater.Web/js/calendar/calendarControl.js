
amdShim.add(function () {

    jQuery(document).ready(documentReady);

    function documentReady() {
        jQuery(".performanceRow a.toolTipLink").on("click", toolTipClick);
        jQuery(".performanceRow div.performanceToolTip a.closeButton").live("click", closeToolTip);
        jQuery("#calendarTable tr td:nth-child(1n+6)").addClass("fromLeft");
    }

    function toolTipClick() {
        var parentContainer = jQuery(this).parent();
        var toolTip = parentContainer.find(".performanceToolTip");
        $(".performanceToolTip").hide();
        $(toolTip).show();
    }

    function closeToolTip() {
        jQuery(this).parent().hide();
    }


});
