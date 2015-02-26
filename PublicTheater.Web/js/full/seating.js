amdShim.add(function () {
    jQuery(document).ready(documentReady);

    function documentReady() {
        jQuery(".theaterSectionSelection .theaterSectionList ul li input:radio").live("click", sectionChanged);
        jQuery(".generalAdminBtn").live("click", showLoadingScreen);
        jQuery(".selectNewSectionContainer .sectionSelection .newSelection ul.tabLinks li").live("click", tabSelected);
        jQuery(".failedPerformanceRow .theaterSectionList ul li input:radio").live("click", newSectionSelected);
        jQuery(".fullUpdateSelection").live("click", updateFullSelection);
    }

    var SELECT_NEW_SECTION_TAB = "SECTION";
    var SELECT_NEW_PACKAGE_TAB = "PACKAGE";

    function sectionChanged() {
        jQuery(".loadingContainer").show();

        var radioButton = jQuery(this);
        var venueId = radioButton.parent().find("input[id*='venueId']").val();
        var sectionId = radioButton.parent().find("input[id*='sectionId']").val();

        var siblingRadios = radioButton.parent().parent().find("input:radio");

        jQuery("#selectedSectionId").val(sectionId);
        jQuery("#selectedVenueId").val(venueId);

        handleRadios(siblingRadios, radioButton);
        jQuery(".sectionChanged").click();

    }

    function newSectionSelected() {
        var selectedCheckbox = jQuery(this);
        var theaterSectionList = selectedCheckbox.parents("div.theaterSectionList");
        var selectedListItem = selectedCheckbox.parents("li");
        var siblingRadios = theaterSectionList.find("input:radio");
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

    function updateFullSelection() {
        showLoadingScreen();
        var sectionId = "";
        var venueId = "";
        var found = false;
        /*
        * Needed a way to update the non-modal section list after updating. Iterate over package selection
        * and update the corresponding section list.
        */
        jQuery(".failedPerformanceRow:first").each(function () {
            var tabSelected = jQuery(this).find("input[id*='tabSelected']").val();
            if (tabSelected == SELECT_NEW_SECTION_TAB && !found) {
                var radio = jQuery(this).find("div.theaterSectionList ul li input:radio:checked");
                if (radio.length > 0) {
                    found = true;
                    sectionId = radio.parent().find("input[id*='sectionId']").val();
                    venueId = radio.parent().find("input[id*='venueId']").val();
                }
            }
        });

        var updated = false;
        if (found) {
            jQuery("#selectSeatingArea .theaterContainer .theaterSectionSelection ul li").each(function () {
                if (!updated) {
                    var sectionIdCheck = jQuery(this).find("input[id*='sectionId']").val();
                    var venueIdCheck = jQuery(this).find("input[id*='venueId']").val();
                    if (sectionId == sectionIdCheck && venueId == venueIdCheck) {
                        var newRadio = jQuery(this).find("input:radio");
                        handleRadios(jQuery(this).parent().find("input:radio"), newRadio);
                        newRadio.prop("checked", "true");
                        update = true;
                    }
                }
            });
        }
    }

    function showLoadingScreen() {
        jQuery(".loadingContainer").show();
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
                tabSelectedField.val(SELECT_NEW_PACKAGE_TAB);
            else
                tabSelectedField.val(SELECT_NEW_SECTION_TAB);

            visibleDiv.hide();
            hiddenDiv.show();
        }
    }

});