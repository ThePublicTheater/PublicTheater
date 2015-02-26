using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Adage.Tessitura;
using Adage.Tessitura.CartObjects;
using PublicTheater.Custom.CoreFeatureSet.Common;

namespace PublicTheater.Custom.CoreFeatureSet.CartObjects
{
    public class PublicFlexPackageLineItem : FlexPackageLineItem
    {
        public override void Remove()
        {
            base.Remove();

            var vouchers = Cart.GetCart().OrderCredits.OfType<GiftCertificateOrderCredit>();
            foreach (GiftCertificateOrderCredit lineItem in vouchers)
            {
                if (PublicPackageVoucher.IsPackageVoucher(lineItem.Description))
                {
                    lineItem.Remove();
                }
            }
        }
    }
}
