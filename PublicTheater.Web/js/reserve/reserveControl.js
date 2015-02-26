amdShim.add(function () {
    function pageLoad() {
        $(function () {

            if ($('#HfIsSyosEnabled').val() < 1) {
               
                $('#bestAvail').show();
            }

            $('#chooseOwn').click(function () {
                $('#syosOnPage, .syosSubCopy').show();
                $('#bestAvail').hide();
                HtmlSyos.restart();
                $(this).addClass('on').siblings().removeClass('on');
                return false;
            });

            $('#chooseBest').click(function () {
                $('#syosOnPage, .syosSubCopy').hide();
                $('#bestAvail').show();
                $(this).addClass('on').siblings().removeClass('on');
                return false;
            });

            // setupDatepicker();
            $('.PromoPerfDropDown').on('change', function () {
                HtmlSyos.changePerformance($(this).val());
            });

            $('.PromoPerfDropDown').change(promoPerfSelected);
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                PageUpdateEvent();
            });

            $(".bestAvailableButton").click(submitClicked);

            PageUpdateEvent();

            $('#closeDatePicker').live("click", function () {
                closeDatePicker();
            });

            jQuery('#bestAvailableTable td > label').each(assignNumber);
        });
    }

    function submitClicked(evt) {
        if (jQuery("#submitClicked").val().toLowerCase() == "false") {
            jQuery("#submitClicked").val("true");
        }
        else {
            evt.preventDefault();
            return false;
        }
    }

    function PageUpdateEvent() {
        $('#choiceWrapper').toggle($('#HfDataIssue').val() == "");
    }


    function promoPerfSelected() {
        var valueSelected = $(this).find("option:selected").val();
        var eligiblePerf = getPerformanceById(valueSelected);
        selectPerformance(eligiblePerf.Date, eligiblePerf.Time);
    }

    function dateAvailability(date) {
        dmy = date.getFullYear() + "/" + (date.getMonth() + 1) + "/" + date.getDate();
        if ($.inArray(dmy, calendarInfo.PerformanceDates) > -1) {
            return [true, "", "Available"];
        } else {
            return [false, "", "Not Available"];
        }
    }

    function isPerformanceEligible(perfId) {
        return $('.PromoPerfDropDown option[value="' + perfId + '"]').length > 0;
    }

    function dateSelectedHandler(dateText, inst) {
        dateSelected(dateText, '', inst, false);
    }

    function selectPerformance(dateText, timeText) {
        dateSelected(dateText, timeText, '', false);
    }

    function getPerformancesByDate(dateText) {
        return $(calendarInfo.Performances).filter(function () {
            return this.Date == dateText;
        });
    }

    function getPerformanceById(perfId) {
        return $(calendarInfo.Performances).filter(function () {
            return this.ID === parseInt(perfId, 10);
        })[0];
    }

    function dateSelected(dateText, timeText, inst, isFirst) {
        var performances = getPerformancesByDate(dateText);

        var datePickerTimes = $('#changeDateDatepickerTimes').html('Select a peformance time:<br/>');

        performances.each(function () {
            var performanceLink = '<a href="#" data-performance-id="' + this.ID + '" class="performanceLink">' + this.Time + '</a>';
            $(performanceLink).appendTo(datePickerTimes);
        });

        datePickerTimes.find('.performanceLink').click(function () {
            reservePageChangePerformance(this, true);
        });

        var perfLink;
        if (timeText)
            perfLink = datePickerTimes.find('.performanceLink:contains("' + timeText + '")').first();
        else
            perfLink = datePickerTimes.find('.performanceLink').first();
        reservePageChangePerformance(perfLink, isFirst == false);

        
    }

    function reservePageChangePerformance(perfLink, changePerformance) {
        var perfId = perfLink.attr("data-performance-id");
        var perf = getPerformanceById(perfId);
        if (perf) {
            Adage.HtmlSyos.Code.SYOSService.IsPerformanceSyosEnabled(perf.ID,
                function onSuccess(msg) {
                    //if the syos enabled state needs to change from when the page was loaded, reload the page.
                    if (msg != calendarInfo.CurrentPerformance.SyosEnabled) {
                        HtmlSyos.changeLevelPerformance(perf.ID);
                        $('.calendarWrap').hide();
                        return;
                    }

                    var perfDate = (new Date(perf.Date)).format($("#displayDateFormat").val());

                    var performanceEligibility = isPerformanceEligible(perf.ID);

                    var showPromoMessaging = $('#HfShowPromoMessaging').val() > 0;

                    $('.PromoApplicableMessage').toggle(performanceEligibility && showPromoMessaging);
                    $('.PromoNotApplicableMessage').toggle(performanceEligibility == false && showPromoMessaging);


                    $('.dateSelected').text(perfDate + " - " + perf.Time);

                    if (changePerformance) {
                        if (typeof syos !== "undefined") {
                            HtmlSyos.changeLevelPerformance(perf.ID);
                            $('.calendarWrap').hide();
                        }
                        $('#HfSelectedPerformanceId').val(perf.ID);
                        $('.PerformanceChangeButton').click();
                    }
                },
                function onError(msg) {
                    window.location.href = "/reserve/index.aspx?performanceNumber=" + perf.ID;
                    return;
                });
        }
    }

    function setupDatepicker() {
        var firstDate = convertDateToUTC(new Date(calendarInfo.PerformanceDates[0]));
        var lastDate = convertDateToUTC(new Date($(calendarInfo.PerformanceDates).last()[0]));
        var currentDate = convertDateToUTC(new Date(calendarInfo.CurrentPerformance.Date));

        var changeDateDatepicker = $('#changeDateDatepicker').datepicker({
            minDate: firstDate,
            maxDate: lastDate,
            defaultDate: currentDate,
            dateFormat: 'yy/m/d',
            constrainInput: true,
            nextText: '',
            prevText: '',
            beforeShowDay: dateAvailability,
            onSelect: dateSelectedHandler
        }).delay().append('<div id="closeDatePicker" class="closeButton">X</div>');

        dateSelected($.datepicker.formatDate("yy/m/d", changeDateDatepicker.datepicker("getDate")), calendarInfo.CurrentPerformance.Time, changeDateDatepicker, true);

        // Prevent the jumping of the page due to hashchange
        $('.calendarWrap').on('click', 'a', function (event) {
            event.preventDefault();
        });
    }

    function closeDatePicker() {
        $("#calendarWrap").hide();
    }

    function convertDateToUTC(date) {
        return new Date(
            date.getUTCFullYear(),
            date.getUTCMonth(),
            date.getUTCDate(),
            date.getUTCHours(),
            date.getUTCMinutes(),
            date.getUTCSeconds());
    }

    var associativeArray = new Array();
    var arrayPopulated = false;

    function assignNumber() {
        if (arrayPopulated == false) {
            populateArray();
        }

        //$(this).text("[ " + (index + 1) + " ] " + $(this).text());
        var currentSection = $.trim($(this).text());
        var number = associativeArray[currentSection];
        if (number) {
            $(this).text("[ " + number + " ] " + $(this).text());
        }
    }

    function populateArray() {
        associativeArray["Value Balcony A"] = 18;
        associativeArray["Value Balcony B"] = 18;
        associativeArray["Rear Balcony B"] = 17;
        associativeArray["Front Balcony B"] = 16;
        associativeArray["Front Balcony A"] = 15;
        associativeArray["Rear Balcony A"] = 14;
        associativeArray["Front Balcony A"] = 13;
        associativeArray["Value Loge"] = 12;
        associativeArray["Rear Loge"] = 11;
        associativeArray["Front Loge"] = 10;
        associativeArray["Rear Founders Circle"] = 9;
        associativeArray["Founders Circle"] = 8;
        associativeArray["Grand Circle"] = 7;
        associativeArray["Orchestra Wheelchair"] = 6;
        associativeArray["Wheelchair Accessible"] = 6;
        associativeArray["Orchestra Ring"] = 5;
        associativeArray["Front Orchestra Ring"] = 4;
        associativeArray["Main Orchestra"] = 3;
        associativeArray["Center Orchestra"] = 2;
        associativeArray["Premier Orchestra"] = 1;
    }

});