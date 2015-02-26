amdShim.add(function () {


    jQuery(document).ready(documentReady);

    function documentReady() {
        displayPlayHeaders();
        jQuery(".venueNames .venueHeader h2 a").click(venueHeaderClick);
        jQuery(".packageRow .selectPackageContainer a.selectPackage").click(selectPackage);
        jQuery("#packageDisplay .packageArea .miniCart .remove").live("click", removeFromMiniCart);
        jQuery(".continueBtn a").click(continueToSeating);
        jQuery(".filter ul li input:checkbox").click(updateFiltering);
        jQuery(".filter ul li input:radio").click(updateFiltering);

    }

    function continueToSeating(event) {
        jQuery(".fullError").hide();
        var venueCount = jQuery(".venueNames .venueHeader").length;
        var selectedCount = jQuery(".packageCart tr:visible").length;

        if (selectedCount < venueCount) {
            jQuery(".fullError").show();
            jQuery(".fullError").text("You must select a package from all theaters.");
            event.preventDefault();
            return false;
        }

        return true;
    }

    function scrollToTop() {
        var scrollTop = jQuery(window).scrollTop();
        var topPos = jQuery('#subscriptionBuilder').position().top;
        if (scrollTop > topPos) {
            jQuery('html, body').animate({ scrollTop: topPos }, 600, 'swing');
        }
    }

    function venueHeaderClick() {
        var headerContainer = jQuery(this).parent().parent();
        var venueId = headerContainer.find("input:hidden").val();

        var shownVenuePackages = jQuery(".venues .allPackages:visible");

        var venueArea;
        jQuery(".venues .venueInfo input[id*='venueId']").each(function () {
            var id = jQuery(this).val();
            if (id == venueId) {
                venueArea = jQuery(this).parent().parent().find(".allPackages");
                return;
            }
        });

        shownVenuePackages.hide();
        venueArea.show();
        jQuery("h2.selected").removeClass("selected");
        jQuery(this).parent().addClass("selected");
    }

    function selectPackage(event) {
        jQuery(".fullError").hide();
        var selectButton = jQuery(this);

        if (selectButton.attr('class').indexOf("btnDisabled") == -1) {
            var numberAddedField = jQuery(this).parent().parent().parent().find(".numberAdded input");
            var numberAdded = getNumberAdded(numberAddedField);
            var maxPerVenue = parseInt(jQuery("#maxPerVenue").val());
            var packageRow = selectButton.parent().parent();

            //If there is only one per venue, then selecting a package will override one from that current venue
            var allowOverride = (maxPerVenue == 1);

            if (allowOverride || hasValidNumberAdded(selectButton, numberAdded, maxPerVenue)) {
                addToMiniCart(selectButton, packageRow, numberAddedField, numberAdded, allowOverride);
            }

            scrollToTop();
        }

        event.preventDefault();
        return false;
    }

    function addToMiniCart(selectButton, packageRow, numberAddedField, numberAdded, allowOverride) {
        var venueRow = selectButton.parent().parent().parent().parent();
        var venueId = venueRow.find(".venueInfo input").val();
        var packageId = packageRow.find(".packageInformation input:hidden").val();
        var priceTypeList = jQuery("ul.priceTypes");

        //Used for display in cart
        var priceTypes = getPriceTypeString(priceTypeList);
        //Used for reservation process
        var packagePriceTypes = getPackagePriceType(priceTypeList, packageId);

        //Display fields to move into mini carts
        var packageInfo = packageRow.find(".packageInformation h3");
        var packagePerfs = packageRow.find(".packagePerformances");

        //Mini Cart Container
        var miniCart = jQuery("#packageDisplay .miniCart table");

        //cartRow to display
        var cartRow = getCartRow(allowOverride, miniCart, venueId);

        //Table cells
        var removeButtonCell = cartRow.find("td.removeButton");
        var packageInfoCell = cartRow.find("td.packageInfo");
        var packagePerformancesCell = cartRow.find("td.packagePerformances");
        var priceTypesCell = cartRow.find("td.priceTypes");
        //Hidden Fields
        var venueIdField = removeButtonCell.find("input:hidden");
        var packageIdField = packageInfoCell.find("input:hidden");
        var priceTypeField = priceTypesCell.find("input:hidden");

        //Set field values
        venueIdField.val(venueId);
        packageIdField.val(packageId);

        var priceTypeIdString;
        var prependComma = false;
        for (var index = 0; index < packagePriceTypes.length; index++) {
            if (prependComma) {
                priceTypeIdString = priceTypeIdString + "," + packagePriceTypes[index];
            }
            else {
                priceTypeIdString = packagePriceTypes[index];
            }
        }

        priceTypeField.val(priceTypeIdString);

        //Set Display values
        packageInfoCell.find("h3").html(packageInfo.html());
        packagePerformancesCell.html(packagePerfs.html());
        priceTypesCell.find("span").html(priceTypes);

        cartRow.show();

        numberAdded++;
        numberAddedField.val(numberAdded);
    }

    function getCartRow(allowOverride, miniCart, venueId) {

        var cartRow;
        var found = false;
        if (allowOverride) {
            jQuery(".packageCart tr td.removeButton input:hidden").each(function () {
                var currentId = jQuery(this).val();
                if (currentId == venueId) {
                    found = true;
                    cartRow = jQuery(this).parent().parent();
                }
            });
        }

        if (!found) {
            cartRow = jQuery(".packageCart tr:hidden:first");
        }

        return cartRow;
    }

    function getPriceTypeString(priceTypeList) {
        var priceTypeString = "";
        priceTypeList.find("li").each(function () {
            var typeName = jQuery(this).find("span.priceTypeName").text();
            var amount = jQuery(this).find("select").val();
            if (priceTypeString != "")
                priceTypeString = priceTypeString + " " + amount + " " + typeName;
            else
                priceTypeString = amount + " " + typeName;
        });

        return priceTypeString;
    }

    function getPackagePriceType(priceTypeList) {
        var packagePriceType = new Array();

        priceTypeList.find("li").each(function () {
            var amount = jQuery(this).find("select").val();
            var priceTypeId = jQuery(this).find("input:hidden").val();

            amount = parseInt(amount);
            if (amount > 0) {
                quantPriceType = priceTypeId + "_" + amount;
                packagePriceType.push(quantPriceType);
            }
        });

        return packagePriceType;
    }

    function getNumberAdded(numberAddedField) {
        var numberAdded = 0;
        if (numberAddedField.val() != "")
            numberAdded = parseInt(numberAddedField.val());

        return numberAdded;
    }

    function hasValidNumberAdded(selectButton, numberAdded, maxPerVenue) {
        if (numberAdded >= maxPerVenue) {
            jQuery(".fullError").show();
            jQuery(".fullError").text("You can only add " + maxPerVenue + " package per venue");
            return false;
        }

        return true;
    }

    function removeFromMiniCart() {
        var venueId = jQuery(this).parent().find("input:hidden").val();

        jQuery(".venueInfo input:hidden").each(function () {
            var currentVenueId = jQuery(this).val();
            if (currentVenueId == venueId) {
                var numberAddedField = jQuery(this).parent().parent().find(".numberAdded input:hidden:first");
                var numberAdded = getNumberAdded(numberAddedField);
                numberAdded--;
                numberAddedField.val(numberAdded);
            }
        });

        jQuery(this).closest('tr').hide();
    }

    function updateFiltering() {
        var filterValuesSelected = [];
        $('#filterSelection :checked').each(function (i) {
            filterValuesSelected.push($(this).parent().find("span.hiddenArea input").val());
        });

        jQuery(".packageRow").each(function () {
            var showProd = false;
            jQuery(this).find(".performanceRow li").each(function () {
                var text = jQuery(this).html();

                if (valueMatchesAny(filterValuesSelected, text))
                    showProd = true;

            });

            if (filterValuesSelected.length == 0 || showProd) {
                $(this).show();
            }
            else {
                $(this).hide();
            }


        });

    }

    //Does an exclusive check on the values in the array
    //the value passed in must match all values in the arrary
    function valueMatchesAll(array, value) {
        var index = 0;
        for (index = 0; index < array.length; index++) {
            if (value.toLowerCase().indexOf(array[index].toLowerCase()) == -1) {
                return false;
            }
        }
        return true;
    }

    function valueMatchesAny(array, value) {
        var index = 0;
        for (index = 0; index < array.length; index++) {
            if (value.toLowerCase().indexOf(array[index].toLowerCase()) != -1) {
                return true;
            }
        }
        return false;
    }


    /*
     * BELOW IS INITIAL JQUERY TEMPLATE CODE - WE MAY NOT INTEGRATE UNTIL PHASE 2
     * -AJM 3/9/12
     */

    // Client-side paging variables
    var currentResults;
    var resultsPerPage = 2;
    var currentPage;
    var maxPages;
    var idNum = 1;
    var currentTheatre = 0;
    var maxTheatres = 2;  // Get this from query string somehow

    function getNextId() {
        return ++idNum;
    }

    $(document).ready(function () {
        $('#filterBoxes :checkbox').click(function () {
            callWebService(currentTheatre);
        });
    });

    function nextPage() {
        currentPage++;
        updatePage();
    }

    function prevPage() {
        currentPage--;
        updatePage();
    }

    function showAll() {
        clearResults();
        $("#btnPrev").attr("disabled", true);
        $("#btnNext").attr("disabled", true);

        $("#subscriptionListTemplate").tmpl(currentResults, {
            dataArrayIndex: function (item) {
                return $.inArray(item, currentResults);
            }
        }).appendTo("#subscriptionList");
    }

    function jumpToPage(num) {
        // Paging is in base zero, but passed in parameter in base 1.  Adjust.
        currentPage = num - 1;
        updatePage();
    }

    function uncheckBox(id) {
        $(id).removeAttr("checked");
        callWebService(currentTheatre);
    }

    function displaySelectedPackage(index) {
        // Display selected package in it's theatres "box"
        clearSelectedPackage(currentTheatre);

        var divId = "#subDescription_" + index;
        var subscription = currentResults[index];

        subscription.Performances.forEach(function (perf) {
            $('<li>' + perf.PerformanceDate + '</li>').appendTo("#selectedPackageDates_" + currentTheatre);
            $('<li>' + perf.PerformanceTitle + '</li>').appendTo("#selectedPackageTitles_" + currentTheatre);
        });

        $('#subscriptionList').find(divId).clone().appendTo("#selectedPackageDetails_" + currentTheatre);
        $('<a href="#" onclick="clearSelectedPackage(' + currentTheatre + '); return false;">Remove</a>').appendTo("#removeButton_" + currentTheatre);

        // Move to next tab
        if (currentTheatre != maxTheatres - 1) {
            currentTheatre++;
            selectTab(currentTheatre);
            callWebService(currentTheatre);
        }
        else {
            $("#btnContinue").show();
        }
    }

    function callWebService(venueId) {
        var checkedFilters = $('#filterBoxes :checked').val() != undefined ? [] : [-1];
        if (checkedFilters != [-1]) {
            $('#filterBoxes :checked').each(function (i, selected) {
                checkedFilters[i] = $(selected).val();
            });
        }

        finderData.GetFilteredSubscriptions(checkedFilters, venueId, webServiceSucess, webServiceError, null);
    }
    function webServiceSucess(e) {
        currentResults = e;
        currentPage = 0;
        maxPages = Math.ceil(currentResults.length / resultsPerPage);

        clearPagingNav();
        if (maxPages > 1) {
            var numbers = [];
            var counter = 1;
            for (counter = 1; counter < maxPages + 1; counter++) {
                numbers.push({ Number: counter });
            }
            $("#pagingNavTemplate").tmpl(numbers).appendTo("#pagingNav");
            $('<li><a href="#" onclick="showAll(); return false;">Show All </a></li>').appendTo("#pagingNav");
        }

        updatePage();

    }

    function webServiceError(e) {
        alert(e.message);
    }

    function updatePage() {
        var minResult = currentPage * resultsPerPage;
        var maxResult = ((currentPage + 1) * resultsPerPage);
        var shownResults = currentResults.slice(minResult, maxResult);

        clearResults();
        clearRequirements();

        var reqs = [];

        var checkedFilters = $('#filterBoxes :checked').val() != undefined ? [] : [-1];
        if (checkedFilters != [-1]) {
            $('#filterBoxes :checked').each(function (i, selected) {
                checkedFilters[i] = $(selected).val();
                var assocId = $(this).attr('id');
                var name = $(this).attr('name').toString();
                reqs.push({ Name: name, AssocId: assocId });
            });
        }


        $("#requirementsTemplate").tmpl(reqs).appendTo("#selectedFilters");
        $("#subscriptionListTemplate").tmpl(shownResults, {
            dataArrayIndex: function (item) {
                return $.inArray(item, currentResults);
            }
        }).appendTo("#subscriptionList");

        // Update Paging

        // If no paging needed, hide paging controls and exit.
        if (maxPages <= 1) {
            $("#selectionBar").hide();
            return;
        }

        $("#selectionBar").show();

        if (currentPage <= 0) {
            $("#btnPrev").attr("disabled", true);
        }
        else {
            $("#btnPrev").removeAttr("disabled");
        }

        if (currentResults.length < maxResult + 1) {
            $("#btnNext").attr("disabled", true);
        }
        else {
            $("#btnNext").removeAttr("disabled");
        }
    }

    function clearRequirements() {
        $('#selectedFilters').empty();
    }

    function clearResults() {
        $('#subscriptionList').empty();
    }

    function clearPagingNav() {
        $('#pagingNav').empty();
    }

    function clearSelectedPackage(venueId) {
        $('#selectedPackageDates_' + venueId).empty();
        $('#selectedPackageDetails_' + venueId).empty();
        $('#selectedPackageTitles_' + venueId).empty();
        $('#removeButton_' + venueId).empty();
    }

    // Testing function.  Remove before styling.
    function selectTab(index) {
        currentTheatre = index;
        $("#theatreTabs").children().removeClass('selected');
        $("#theatreTabs a").eq(index).addClass('selected');
    }


    function displayPlayHeaders() {
        var removeButtonVisbible = null;
        console.log({ removeButtonVisbible: removeButtonVisbible })

        var removeButton = $('.perfRow a.removePlayBtn');
        $('.packageCart .headerTr .headerLine').hide();

        $('.selectPackage').on('click', function () {
            $('.packageCart .headerTr .headerLine').show();
            return false;
        })

        $(removeButton).on('click', function () {
            removeButtonVisbible = $('.perfRow .removePlayBtn:visible').length;
            if (removeButtonVisbible <= 1) {
                $('.packageCart .headerTr .headerLine').hide();
            }
        })
    }

});