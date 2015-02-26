define([
    "jquery"
], function (jQuery) {

    var queryStringElements = new Array();
    var certificatePreview = jQuery('#imgPreview');

    jQuery(document).ready(function () {
        var designs = jQuery(".thumbnails > li");
        designs.bind('click', function (obj) {
            updateSelectedDesign(jQuery(this));
        });

        jQuery(".previewItem").bind("change", function (obj) {
            updateImagePreview(obj);
        });


        updateSelectedDesign(designs.first());
        function updateSelectedDesign(selectedItem) {
            jQuery(".selectedDesignId:first").val(selectedItem.find('.certificateGraphic').first().attr('src'));
            updateImagePreview();
        }

        function updateImagePreview(event) {
            jQuery('.formSection input').each(appendToQueryString);
            jQuery('.formSection textarea').each(appendToQueryString);

            var imageServiceLocation = jQuery("#imageServicePath").val();
            var serializedQueryString = queryStringElements.join("&");
            certificatePreview.attr('src', imageServiceLocation + "?" + serializedQueryString);
            queryStringElements = [];
        }

        function appendToQueryString() {
            var paramName = jQuery(this).attr('qsparam');
            var paramValue = jQuery(this).val();
            if (paramName !== undefined && paramValue != "") {
                queryStringElements.push(paramName + "=" + paramValue);
            }
        }

        jQuery('.giftCertificateDesigns li').click(function () {
            jQuery('.giftCertificateDesigns li').removeClass('selected');
            jQuery(this).addClass('selected');
        });

    });
});
