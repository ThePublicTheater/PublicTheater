using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Adage.Tessitura;
using TheaterTemplate.Shared.Common;

namespace PublicTheater.Web.Controls.Contribute
{
    public class PaymentDonationAsk : TheaterTemplate.Web.Controls.ContributeControls.PaymentDonationAsk
    {
        protected override void BindPage()
        {
            base.BindPage();
            descriptionContainer.Visible = false;
        }

        protected override void lbAddNewDonation_Click(object sender, EventArgs e)
        {
            
            base.lbAddNewDonation_Click(sender, e);
            Control c = FindControl("ddlCreditCardType");
            if (c != null)
                Response.Redirect(Config.GetValue(TheaterSharedAppSettings.PAYMENT_PAGE_URL, "~/checkout/payment.aspx"));
        }
    }
}