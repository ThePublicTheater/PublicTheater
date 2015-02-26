using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PublicTheater.Web.Controls.Checkout
{
    public class BillingAddressControl : TheaterTemplate.Web.Controls.CheckoutControls.BillingAddressControl
    {
        protected virtual Custom.CoreFeatureSet.UserObjects.PublicUser CurrentPublicUser
        {
            get
            {
                return base.CurrentUser as PublicTheater.Custom.CoreFeatureSet.UserObjects.PublicUser;
            }
        }
        protected override void BindBillingAddressInformation()
        {
            CurrentUser.GetAccountInfo();
            CurrentUser.GetConstituentInfo();
            CurrentUser.GetAddresses();

            UpdateAddressControl.LoadAddress(CurrentPublicUser.BillingAddress, false);
            ltrOldAddress.Text = CurrentPublicUser.BillingAddress.ToString();
        }

        protected override void UpdateBillingAddress()
        {
            CurrentUser.GetAccountInfo();
            CurrentUser.GetConstituentInfo();
            CurrentUser.GetAddresses();
            CurrentPublicUser.BillingAddress.Primary = false;
            UpdateAddressControl.SaveAddress(CurrentPublicUser.BillingAddress);
        }
    }
}