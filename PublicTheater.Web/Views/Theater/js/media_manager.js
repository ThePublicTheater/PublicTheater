function InitializeMediaManagerControls() {

    $("#btnGoToPage").click(function (event) {

        var enteredInt = parseInt($("#tbPageIndex").val());
        var totalPages = parseInt($("#hfTotalPages").val());
        if (enteredInt > 0 && enteredInt <= totalPages) {
        }
        else {
            alert("Invalid page number. Please try again.");
            return false;
        }
    });

    $("input.addButton").on('click', function (e) {
        var mediaParent = $(this).parents(".mediaType");

        var jsonData = GetJsonFromPropertyManager(mediaParent);
        var mediaManagerURL = $("#HfMediaManagerURL").val();

        var performanceRefresh = mediaParent.find(".performances .refresh_button");
        var artistRefresh = mediaParent.find(".artists .refresh_button");
        $.ajax({
            type: "POST",
            url: mediaManagerURL,
            data: jsonData,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                if (msg.d == "success") {
                    performanceRefresh.click();
                    artistRefresh.click();
                    alert("Media Information Updated.");
                }
                else {
                    alert(msg.d);
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(thrownError);
            }

        });

        e.preventDefault();

    });
}

function LoadImage(img, thumbNailUrl) {
    img.onload = null;
    img.src = thumbNailUrl;
}