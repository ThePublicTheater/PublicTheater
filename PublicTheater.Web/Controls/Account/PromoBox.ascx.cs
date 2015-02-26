using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Adage.Tessitura;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace PublicTheater.Web.Controls.Account
{
    public class PromoBox : TheaterTemplate.Web.Controls.AccountControls.PromoBox
    {
        protected Panel pnlWrongPromoDisplay;
        protected Label lblWrongPromoCode;

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            lblPromoError.Visible = !string.IsNullOrEmpty(lblPromoError.Text);
            pnlWrongPromoDisplay.Visible = !string.IsNullOrEmpty(lblPromoError.Text);
            lblWrongPromoCode.Text = lblPromoError.Text;
            
        }

        protected override void BindControl()
        {
            base.BindControl();

            if (IsDefaultPromo == false)
            {
                lblPromoCode.Text = CurrentPromoDetails.PromoCodeText;
                lblPromoDescription.Text = string.Empty;
            }
        }

        protected override void btnApplyPromo_Click(object sender, EventArgs e)
        {
            int code;
            if (Int32.TryParse(txtPromoCode.Text.Trim(), out code))
                txtPromoCode.Text = "#" + txtPromoCode.Text.Trim();
            base.btnApplyPromo_Click(sender, e);
            
        }

        protected override void btnRemovePromo_Click(object sender, EventArgs e)
        {
           Adage.Tessitura.Cart.GetCart().Dispose();
           base.btnRemovePromo_Click(sender,e);
        }

        protected override void HandlePromoRequest(int promoCode)
        {
            //removed mos conflict check redirect from CFS

            if (ApplyPromo(promoCode) == false)
            {
                lblPromoError.Text = PromoConfiguration.PromoFailedError;
                pnlWrongPromoDisplay.Visible = true;
            }
            else
            {
                var promoDetails = PromoCodeDetails.Get(promoCode);
                if (DisabledRedirect == false && promoDetails != null && string.IsNullOrWhiteSpace(promoDetails.RedirectURL) == false)
                    Response.Redirect(promoDetails.RedirectURL, true);

                TriggerPromoCodeUpdated();
            }

            BindControl();
        }
    }
}