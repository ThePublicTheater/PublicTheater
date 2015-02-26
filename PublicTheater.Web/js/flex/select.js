amdShim.add(function () {

    jQuery(document).ready(readyFunction);


    function readyFunction() {
        displayPlayHeaders();
        jQuery(".productionRow:not(.added) .productionDisplay .productionButton .selectPlay").on('click', showSelectionArea);
        jQuery(".added .selectPlay").on('click', returnFalse);
        jQuery(".productionRow .productionDisplay .productionButton .addToPackage").click(addToPackage);
        var mustChoosePerfId = $('#autoSelectPerf').val();
        if (mustChoosePerfId != null) {
            var mustChooseSelect = $('.perfDateSelection option[value="' + mustChoosePerfId + '"]');
            
       
            mustChooseSelect.prop('selected', true);
            mustChooseSelect.closest('.productionRow').find('a.addToPackage').click();
        }

        jQuery("#filterSelection").on('change', '.filter ul li input:checkbox', updateProductionFilters);
        jQuery(".productionRow .reserveSelection").on('change', '.perfFilter ul li input:checkbox', updatePerformanceFilters);


        jQuery("#packageDisplay .packageArea .miniCart .remove").on("click", removeFromMiniCart);
        jQuery(".continueBtn a").click(validateSelections);
        jQuery(".filter ul li input:checkbox").click(updateFiltering);
        jQuery(".filter ul li input:radio").click(updateFiltering);
        jQuery(".flexPackagePriceType select").on('change', updateTicketQuantities);
        window.queryStringParams = parseQueryStringParams();
        window.$loadingSpinner = $('.loadingContainer');
        jQuery("#selectedPerformanceIDs").val("");
    }

    function toggleSpinner() {
        window.$loadingSpinner.toggle();
    }

    function parseQueryStringParams() {
        var keyValuePairs = {};
        var assignments = window.location.search.substring(1).split('&');
        for (var i = 0; i < assignments.length; i++) {
            var pair = assignments[i].split('=');
            keyValuePairs[pair[0]] = pair[1];
        }
        return keyValuePairs;
    }

    function updateTicketQuantities() {
        var newQuantityString = getSharedQuantityString();
        jQuery('.packageCart tr.perfRow td:nth-child(5)').text(newQuantityString);
    }

    function validateSelections(event) {
        hideErrorDiv();
        var currentCount = jQuery(".miniCart tr.perfRow:visible").length;
        var minimumRequired = parseInt(jQuery("#minimumPerformances").val());

        if (currentCount < minimumRequired) {
            jQuery("div.flexError").show();
            jQuery("div.flexError").text("You must select at least " + minimumRequired + " performances before proceeding");
            event.preventDefault();
            return false;
        }

    }

    function returnFalse() {
        return false;
    }

    function showSelectionArea(event) {
        event.preventDefault();
        resetAllFilters();
        jQuery(".productionRow .reserveSelection .perfFilter ul li input:checkbox").first().change();
        var productionRow = jQuery(this).closest('.productionRow');

        hideErrorDiv();
        hideAllReserveSections();
        hideAllAddToPackage();
        showAllSelectPlay();

        awaitProductionAvailability(_.bind(populateSelectArea, this), productionRow.find('[data-role=productionSeasonNumber]').val());

        return false;
    }

    function populateSelectArea(availableShows) {
        var reserveSection = jQuery(this).parent().parent().parent().find(".reserveSelection");
        var addToPackageButton = jQuery(this).parent().find(".addToPackage");
        var productionRow = jQuery(this).closest('.productionRow');
        var performanceOptions = productionRow.find('.perfDateSelection option');

        _.each(performanceOptions, function(opt) {
            if (!_.contains(availableShows, Number(opt.value))) {
                opt.remove();
            }
        }, this);

        jQuery('[data-role=description]').show();
        productionRow.find('[data-role=description]').hide();


        reserveSection.show();
        productionRow.addClass('viewing');
        addToPackageButton.show();
        jQuery(this).hide();
    }

    function awaitProductionAvailability(callback, productionSeasonNumber) {
        toggleSpinner();
        return jQuery.ajax({
            url: '/Services/PublicWebService.asmx/GetFlexAvailablePerformances',
            data: {
                productionSeasonNumber: productionSeasonNumber,
                flexPackageNumber: queryStringParams["PackageId"]
            }
        }).done(function(response) {
            callback(jQuery.parseJSON(jQuery(response).text()));
            toggleSpinner();
        });
    }

    function addToPackage(event) {
        hideErrorDiv();

        var currentButton = jQuery(this);
        var productionRow = currentButton.parent().parent().parent();
        var selectDateDropDown = productionRow.find(".reserveSelection .selectionContainer .availablePerformances select.perfDateSelection");
        var reserveSection = productionRow.find(".reserveSelection");
        var selectPlayButton = productionRow.find(".selectPlay");
        var performanceTitle = productionRow.find(".productionInformation h3.flexProdTitle").text();

        var venueName = productionRow.find(".productionInformation [data-role=venueName]").text();
        var quantityList = productionRow.find(".reserveSelection .quantity ul li");
        var performanceSelected = productionRow.find("input[id*='performanceSelected']");

        var totalRequest = 0;
        quantityList.each(function () {
            var item = jQuery(this);
            var quantity = parseInt(item.find("select").val());
            totalRequest += (quantity);
        });

        if (selectDateDropDown.val() == "" || selectDateDropDown.val() == null || totalRequest == 0) {
            jQuery("div.flexError").show();
            jQuery("div.flexError").text("Please select a date and time for your performance.");
        }
        else {
            var selectedOption = selectDateDropDown.find("option:selected");
            var performanceDate = selectedOption.text();
            var performanceID = selectDateDropDown.val();
            var quantityString = getQuantityString(quantityList);
            var priceTypeQuantity = getPriceTypeQuantity(quantityList);

            addToMiniCart(performanceTitle, performanceDate, performanceID, venueName,quantityString, priceTypeQuantity);
            performanceSelected.val(performanceID);

          //  selectedOption.remove();
            reserveSection.slideUp();
            productionRow.addClass('added').find('.productionImage').hide();
            currentButton.hide();
            selectPlayButton.show();
            selectPlayButton.text("Show Added").addClass("disabled").hide().fadeIn();
            updatePlayCount();

            productionRow.find('[data-role=description]').show();
            $('.productionRow.viewing').removeClass('viewing').find('.addToPackage').removeClass('btnStandOut');
        }

        handleItemLimit();

        //We don't want to postback on this button
        //Update if changed to link/image
        event.preventDefault();
        return false;
    }

    function handleItemLimit() {
        var itemLimit = $('#maximumPerformances').val();
        if (itemLimit && Number(itemLimit) == $('.packageCart .perfRow:visible').length) {
            $('.productionRow').not('.added').find('.selectPlay').addClass('disabled');
        }
        else {
            $('.productionRow').not('.added').find('.selectPlay').removeClass('disabled');
        }
    }

    function getQuantityString(quantityList) {
        var quantityString = "";
        var index = 0;
        quantityList.each(function () {
            var item = jQuery(this);
            var priceTypeDesc = item.find("label").text();
            var quantity = item.find("select").val();
            
            if (parseInt(quantity) > 0) {
                var itemString = quantity + " " + priceTypeDesc;

                if (index > 0) {
                    quantityString = quantityString + " : " + itemString;
                }
                else {
                    quantityString = itemString;
                }
            }

            index++;
        });

        return quantityString || getSharedQuantityString();
    }

    function getSharedQuantityString() {
        var $containingElement = jQuery('.flexPackagePriceType li:first');
        if ($containingElement) {
            var pricetTypeName = $containingElement.find('label').text();
            var quantity = $containingElement.find('select').val();
            return quantity + ' ' + pricetTypeName;
        }
    }

    //returns the string with performanceID, price type id, and quanity concatenated together
    function getPriceTypeQuantity(quantityList) {
        var perfPriceTypeID = new Array();
        quantityList.each(function () {
            var item = jQuery(this);
            var quantPriceType = "";
            var quantity = item.find("select").val();
            var priceTypeID = item.find("input:hidden").val();

            quantity = parseInt(quantity);
            if (quantity > 0) {
                quantPriceType = priceTypeID + "_" + quantity;
                perfPriceTypeID.push(quantPriceType);
            }
        });

        return perfPriceTypeID;
    }

    function addToMiniCart(performanceTitle, performanceDate, performanceID, venueName, quantityString, priceTypeQuantity) {
        var miniCartTable = jQuery("#packageDisplay .miniCart table");

        //Select first prebuild repeater LI element
        var nextRow = miniCartTable.find("tr:hidden:first");
        var perfInfo = nextRow.find(".performanceInfo");
        var performanceIDField = nextRow.find("input[id*='performanceID']");
        var priceTypeQuantityField = nextRow.find("input[id*='priceTypeQuantity']");
        var removeLink = nextRow.find("a");

        var performanceInfo = '<td>' + performanceTitle + '</td><td>' + performanceDate + '</td>' +
            //'<td>' + venueName + '</td>' +
            '<td>' + quantityString + '</td>';
        removeLink.parent().after(performanceInfo);

        performanceIDField.val(performanceID);
        removeLink.addClass(performanceID);

        var value = "";
        var prependComma = false;
        for (var index = 0; index < priceTypeQuantity.length; index++) {
            if (prependComma) {
                value += "," + priceTypeQuantity[index];
            }
            else {
                value += priceTypeQuantity[index];
                prependComma = true;
            }
        }
        priceTypeQuantityField.val(value);

        nextRow.fadeIn();

        var selectedPerformanceIDsField = jQuery("#selectedPerformanceIDs");
        var currentValue = selectedPerformanceIDsField.val();
        var newValue = "";
        if (currentValue == "") {
            newValue = performanceID;
        }
        else {
            newValue = currentValue + "," + performanceID;
        }

        selectedPerformanceIDsField.val(newValue);

    }

    function removeFromMiniCart(event) {
        
        var currentRemoveButton = jQuery(this);
        var mustChoosePerfId = $('#autoSelectPerf').val();
        if (mustChoosePerfId != null) {
            if (currentRemoveButton.hasClass(mustChoosePerfId)) {
                alert("Cannot remove this. Required performance.");
                return;
            }
        }
        var currentRemoveClass = currentRemoveButton.attr("class");

        var split = currentRemoveClass.split(' ');
        var performanceID = split[split.length - 1];

        //We need to reset the remove button class here
        currentRemoveClass = jQuery.trim(currentRemoveClass.replace(performanceID, ""));
        currentRemoveButton.attr("class", currentRemoveClass);

        var currentContainer = currentRemoveButton.closest('tr');

        var performanceIDField = currentContainer.find(".miniCart tr input[id*='performanceID']");
        var priceTypeQuantityField = currentContainer.find(".miniCart tr input[id*='priceTypeQuantity']");

        currentContainer.find('td:not(:first)').remove();

        performanceIDField.val("");
        priceTypeQuantityField.val("");
        currentContainer.hide();

        var selectedPerformanceIDsField = jQuery("#selectedPerformanceIDs");
        var currentValue = selectedPerformanceIDsField.val();
        var newValue = currentValue;

        //First try to remove if a comma is before
        newValue = newValue.replace("," + performanceID, "");
        //Then verify that the performance ID has been removed
        newValue = newValue.replace(performanceID, "");

        selectedPerformanceIDsField.val(newValue);

        //Now repopulate the lists with the removed performances
        //Retrieve filter container to get active filters
        var firstFilter = jQuery(".productionRow .reserveSelection .performanceFilters").eq(0);
        var activeFilters = getActiveFilters(firstFilter);

        rebuildPerformanceList();
        //Make similar calls to performance filtering below, except we don't need to reset checkboxes
        if (activeFilters.length > 0) {
            filterDateList(activeFilters);
        }

        var productionRow = jQuery("#allProductions .productionRow input[value='" + performanceID + "']").parent();
        var selectButton = productionRow.find(".selectPlay");
        selectButton.text("Select Show").removeClass("disabled");
        productionRow.removeClass('added').find('.chosen').removeClass('chosen');
        productionRow.find('.productionImage').show();

        updatePlayCount();
        handleItemLimit();
    }

    function updateProductionFilters() {
        var currentFilterCheckbox = jQuery(this);

        var filterContainer = currentFilterCheckbox.parent();
        var filterList = jQuery("#activeFilters .currentSelections ul.filterList");
        var filterText = filterContainer.find("label").text();

        if (currentFilterCheckbox.is(":checked") == false) {
            removeFromFilter(filterText, filterList);
        }
        else {
            addToFilterList(filterText, filterList);
        }

    }

    function updatePerformanceFilters() {
        var currentFilterCheckbox = jQuery(this);
        var allFilterContainer = currentFilterCheckbox.closest('.perfFilter');
        var activeFilters = getActiveFilters(allFilterContainer);

        rebuildPerformanceList();
        filterDateList(activeFilters);


    }

    function rebuildPerformanceList() {
        //TODO: IMPROVE PERFORMANCE, BUT KEEP SORT ORDER - AJM
        jQuery(".reserveSelection .selectionContainer .availablePerformances select.perfDateSelection option").each(function () {
            jQuery(this).remove();
        });

        jQuery(".reserveSelection .selectionContainer .availablePerformances select.perfDateSelectionCopy").each(moveToOriginal);
    }

    function moveToOriginal() {
        var currentDateSelection = jQuery(this).parent().find("select.perfDateSelection");

        jQuery(this).find("option").each(function () {
            var currentOption = jQuery(this);

            if (jQuery("#selectedPerformanceIDs").val().indexOf(currentOption.val()) == -1) {
                currentDateSelection.append(jQuery("<option></option>")
                                              .attr("value", currentOption.val())
                                              .text(currentOption.text()));
            }

        });
    }

    function getActiveFilters(allFilterContainer) {
        var activeFilters = [];
        allFilterContainer.find("input:checkbox").each(function () {
            if (jQuery(this).is(":checked")) {
                var valueToAdd = jQuery(this).closest('[data-role=filterOption]').find("input:hidden").val();
                activeFilters.push(valueToAdd.toLowerCase());
            }
        });
        return activeFilters;
    }

    function filterDateList(activeFilters) {
        if (!activeFilters.length) {
            return;
        }

        jQuery(".productionRow .reserveSelection .selectionContainer .availablePerformances select.perfDateSelection").each(function () {
            jQuery(this).find("option").each(function () {
                var currentOption = jQuery(this);
                //Lower case on filter set in "getActiveFilters"
                if (valueMatchesAny(activeFilters, currentOption.text().toLowerCase()) == false) {
                    currentOption.remove();
                }
            });
        });
    }

    function setActiveFilters(activeFilters) {
        jQuery(".productionRow .reserveSelection .performanceFilters ul li").each(function () {
            var container = jQuery(this);
            var currentValue = container.find("span.hiddenValue input").val().toLowerCase();
            var currentCheckbox = container.find("input:checkbox");

            if (jQuery.inArray(currentValue, activeFilters) != -1) {
                currentCheckbox.attr("checked", "checked");
            }
            else if (currentCheckbox.is(":checked")) {
                currentCheckbox.removeAttr("checked");
            }

        });
    }

    function resetAllFilters() {

        jQuery(".performanceFilters input:checked").removeAttr('checked');
        jQuery(".performanceFilters span.checked").removeClass('checked');
    }

    //Does an exclusive check on the values in the array
    //the value passed in must match all values in the arrary
    function valueMatchesAll(array, value) {
        var index = 0;
        for (index = 0; index < array.length; index++) {
            if (value.indexOf(array[index]) == -1) {
                return false;
            }
        }

        return true;
    }

    function valueMatchesAny(array, value) {
        for (var index = 0; index < array.length; index++) {
            if (value.indexOf(array[index]) >= 0) {
                return true;
            }
        }

        return false;
    }

    //Append/Remove values from display
    //CSS class, based on filter, used to remove from display later
    function addToFilterList(filterText, filterList) {
        filterList.append("<li class='" + filterText.replace(":", "") + "'>" + filterText + "</li>");
    }

    function removeFromFilter(filterText, filterList) {
        var filterToRemove = filterList.find("li." + filterText.replace(":", ""));
        filterToRemove.remove();

    }

    //Hide/Show Utility Functions
    function hideAllReserveSections() {
        $('.productionRow.viewing').removeClass('viewing');
        jQuery(".reserveSelection").each(function () {
            jQuery(this).hide();
        });
    }

    function hideAllAddToPackage() {
        jQuery(".addToPackage").each(function () {
            jQuery(this).hide();
        });
    }

    function hideErrorDiv() {
        jQuery("div.flexError").hide();
        jQuery("div.flexError").text("");
    }

    function showAllSelectPlay() {
        jQuery(".productionRow .productionDisplay .productionButton .selectPlay").show();
    }

    function updatePlayCount() {
        var currentCount = jQuery(".miniCart tr.perfRow:visible").length;
        jQuery("span.plays").text(currentCount);
        if ($("span.plays").text() == $("span.min").text().substring(1,2))
            alert("Hey!");
    }

    function updateFiltering() {
        var filterValuesSelected = [];
        jQuery('#filterSelection :checked').each(function (i) {
            var inputFilterValues = jQuery(this).closest('li').find("span.hiddenArea input").val().split(",");
            jQuery.each(inputFilterValues, function (index, value) {
                filterValuesSelected.push(value);
            });
        });

        $('.productionRow').each(function (i) {
            var showProd = false;

            $(this).find('.filterableValues input[type=hidden]').each(function () {
                var curProdVal = $(this).val();
                if ($.inArray(curProdVal, filterValuesSelected) != -1) {
                    showProd = true;
                }
            });

            // if !prodValues.Contains(Any of filts), hide it.

            if (filterValuesSelected.length == 0 || showProd) {
                $(this).show();
            }
            else {
                $(this).hide();
            }
        });
    }

    function openInNewWindow(url) {
        var newwindow = window.open(url, 'name', 'height=600,width=450');
        if (window.focus) { newwindow.focus(); }
        return false;

    }

    function displayPlayHeaders() {
        var removeButtonVisbible = null;
        console.log({ removeButtonVisbible: removeButtonVisbible })

        var removeButton = $('.perfRow a.removePlayBtn');
        $('.packageCart .headerTr .headerLine').hide();

        $('.addToPackageBtn').on('click', function() {
            $('.packageCart .headerTr .headerLine').show();
            return false;
        });

        $(removeButton).on('click', function() {
            removeButtonVisbible = $('.perfRow .removePlayBtn:visible').length;
            if (removeButtonVisbible <= 1) {
                $('.packageCart .headerTr .headerLine').hide();
            }
        });
    }
});