amdShim.add(function () {

    $(function () {
        var radios = $(".shippingAddressList").find("input[type=radio]");
        var values = $(".shippingAddressList").find("input[type=hidden]");

        var hfSelectedAddressID = $("#hfSelectedAddressID");

        var selectedHF = $(".shippingAddressList").find('input[type=hidden][value="' + hfSelectedAddressID.val() + '"]');

        selectedHF.siblings('input[type=radio]').prop("checked", true);
        radios.prop("name", "shippingAddressOption");
        radios.click(function () {
            //since we use uniform, radio input renders differently now
            
//            radios.prop("checked", false);
//            $(this).prop("checked", true);
            
            //var selectedValue = $(this).siblings("input[type=hidden]").val();

            var selectedValue = $(this).closest('.radio').siblings("input[type=hidden]").val();
            hfSelectedAddressID.val(selectedValue);
        });
    });

});