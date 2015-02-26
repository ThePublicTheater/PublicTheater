using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PublicTheater.Web.Controls.Checkout
{
    public class AddressDisplayControl : TheaterTemplate.Web.Controls.CheckoutControls.AddressDisplayControl
    {
        public override void Fill(Adage.Tessitura.User user, Adage.Tessitura.UserObjects.UserAddress address)
        {
            base.Fill(user, address);

            if(address!=null && !string.IsNullOrEmpty(address.StreetAddress2))
            {
                ltrAddress.Text = string.Concat(address.StreetAddress1, @"<br />", address.StreetAddress2);
            }
        }
    }
}