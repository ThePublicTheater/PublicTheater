// Funds
define([

    "base/view"

], function (BaseView) {

    return BaseView.extend({
        events: {
            // Acknowledgement Section
            "click #cbListedDonation": "didClickListedDonor",
            "click #cbAnonDonor": "didClickAnonymousDonor"
        },

        initialize: function () {
            this.initControls();
        },

        initControls: function () {
            this.$donationAmountTextbox = this.$("[data-target='donationAmount']");
            this.$donorAckNameTextbox = this.$("#txtDonorName");
        },
        
        didClickListedDonor: function (evt) {
            this.$donorAckNameTextbox.val("");
            this.$donorAckNameTextbox.prop("disabled", false);
        },

        didClickAnonymousDonor: function (evt) {
            this.$donorAckNameTextbox.val("Anonymous");
            this.$donorAckNameTextbox.prop("disabled", true);
        },
        
        formatAmountString:function(amount){
            return amount.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
        }
    });
});

