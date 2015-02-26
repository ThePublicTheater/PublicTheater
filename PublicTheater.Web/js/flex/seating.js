amdShim.add(function () {


    jQuery(document).ready(documentReady);

    function documentReady() {
        jQuery("#flexUpdatePanelContainer").on("click", ".sectionContainer .theaterSectionList ul li input:radio", sectionSelected);
        jQuery("#flexUpdatePanelContainer").on("click", ".failedPerformanceRow .theaterSectionList ul li input:radio", newSectionSelected);
        jQuery("#flexUpdatePanelContainer").on("click", ".selectNewSectionContainer .editSectionContainer .theaterSectionList ul li input:radio", editSectionClick);
        jQuery("#flexUpdatePanelContainer").on("click", ".selectNewSectionContainer .sectionSelection .newSelection ul.tabLinks li", tabSelected);
        jQuery("#flexUpdatePanelContainer").on("click", ".flexUpdateSelection", updateFlexSeating);
        jQuery("#flexUpdatePanelContainer").on("click", ".generalAdminBtn", generalAdminUpdate);
        jQuery("#flexUpdatePanelContainer").on("click", ".soldOutMessaging .btn", touchScrollUp);
    }

    var SELECT_NEW_SECTION_TAB = "SECTION";
    var SELECT_NEW_PERF_TAB = "PERFORMANCE";

    function sectionSelected() {

        jQuery(".loadingContainer, .loadingSpinner").show();

        var selectedCheckbox = jQuery(this);
        var theaterSectionList = selectedCheckbox.parents("div.theaterSectionList");
        var selectedListItem = selectedCheckbox.parents("li");

        var venueID = selectedListItem.find("input[id*='venueId']").val();
        var sectionID = selectedListItem.find("input[id*='sectionId']").val();
        var siblingRadios = theaterSectionList.find("input:radio");

        jQuery("#updatedSectionValue").val(sectionID);
        jQuery("#updatedVenueValue").val(venueID);

        handleRadios(siblingRadios, selectedCheckbox);


        jQuery("#btnSectionChanged").click();

    }

    function touchScrollUp() {
        if ($("html").hasClass("touch")) {
            $("html, body").animate({ scrollTop: 100 }, "slow");
        }
    }

    function updateFlexSeating() {
        jQuery(".loadingContainer, .loadingSpinner").show();
    }

    function editSectionClick() {
        var selectedCheckbox = jQuery(this);
        var theaterSectionList = selectedCheckbox.parents("div.theaterSectionList");
        var siblingRadios = theaterSectionList.find("input:radio");
        handleRadios(siblingRadios, selectedCheckbox);
    }

    function newSectionSelected() {
        var selectedCheckbox = jQuery(this);
        var theaterSectionList = selectedCheckbox.parents("div.theaterSectionList");
        var selectedListItem = selectedCheckbox.parents("li");
        var siblingRadios = theaterSectionList.find("input:radio");

        var venueID = selectedListItem.find("input[id*='venueId']").val();
        var sectionID = selectedListItem.find("input[id*='sectionId']").val();

        //Get all inputs with matching venue value
        var mainVenueInputs = jQuery(".sectionContainer").find("input[id*='venueId'][value=" + venueID + "]");
        //Get input with matching section value
        var mainSectionInput = jQuery(".sectionContainer").find("input[id*='sectionId'][value=" + sectionID + "]");

        //uncheck all radios with same venue
        mainVenueInputs.parent().find("input:radio").prop("checked", false);
        //check radio with same section
        mainSectionInput.parent().find("input:radio").prop("checked", true);

        handleRadios(siblingRadios, selectedCheckbox);
    }

    function handleRadios(siblingRadios, selectedCheckbox) {
        siblingRadios.each(function () {
            var currentCheckbox = jQuery(this);
            if (currentCheckbox[0].id != selectedCheckbox[0].id) {
                currentCheckbox.prop("checked", false);
            }
        });
    }

    function tabSelected() {
        var currentTab = jQuery(this);

        if (currentTab.attr("class") != "selected") {
            currentTab.parent().find(".selected").removeClass("selected");
            currentTab.addClass("selected");
            var visibleDiv = currentTab.parent().parent().find("div:visible");
            var hiddenDiv = currentTab.parent().parent().find("div:hidden");
            var tabSelectedField = currentTab.parent().parent().find("input[id*='tabSelected']");

            if (tabSelectedField.val() == SELECT_NEW_SECTION_TAB)
                tabSelectedField.val(SELECT_NEW_PERF_TAB);
            else
                tabSelectedField.val(SELECT_NEW_SECTION_TAB);

            visibleDiv.hide();
            hiddenDiv.show();
        }
    }

    function generalAdminUpdate() {
        jQuery(".loadingContainer, .loadingSpinner").show();
    }
});