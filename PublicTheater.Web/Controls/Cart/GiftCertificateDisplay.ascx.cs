using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Adage.Tessitura;
using Adage.Tessitura.CartObjects;
using PublicTheater.Custom.CoreFeatureSet.CartObjects;
using PublicTheater.Custom.CoreFeatureSet.Common;
using PublicTheater.Custom.CoreFeatureSet.EpiServerConfig;

namespace PublicTheater.Web.Controls.Cart
{
    public class GiftCertificateDisplay : TheaterTemplate.Web.Controls.CartControls.GiftCertificateDisplay
    {
        public override void BindDisplay(CartLineItem item)
        {
            base.BindDisplay(item);

            var gcLineItem = item as GiftCertificateLineItem;

            if (gcLineItem != null && PublicPackageVoucher.IsPackageVoucher(gcLineItem))
            {
                var content = (PublicCartContentConfig)CartContentPage;
                //ltrGiftCertificateText.Text = string.Format("{0}{1}", content.PackageVoucherText, gcLineItem.GiftCertificateNumber);
                
                ltrGiftCertificateText.Text = content.PackageVoucherText;//do not display gc number in cart
                ltrHeader.Text = content.PackageVoucherHeader;
                ltrTotalHeader.Text = content.PackageVoucherTotalHeader;
            }
        }
    }
}