using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Adage.Tessitura;
using Adage.Tessitura.UserObjects;

namespace PublicTheater.Web.Controls.Checkout
{
    public class ShippingAddressControl : TheaterTemplate.Web.Controls.CheckoutControls.ShippingAddressControl
    {
        protected virtual Custom.CoreFeatureSet.UserObjects.PublicUser CurrentPublicUser
        {
            get
            {
                return base.CurrentUser as PublicTheater.Custom.CoreFeatureSet.UserObjects.PublicUser;
            }
        }
        protected override void BindShippingAddressInformation()
        {
            //base.BindShippingAddressInformation();
            CurrentUser.GetAccountInfo();
            CurrentUser.GetConstituentInfo();
            CurrentUser.GetAddresses();


            lvExistingAddresses.DataSource = new List<UserAddress> { CurrentPublicUser.ShippingAddress };
            lvExistingAddresses.DataBind();

            if (string.IsNullOrEmpty(SelectedAddressId))
            {
                int currentShippingAddressID = Adage.Tessitura.Cart.GetCart().ShippingAddressID;
                if (currentShippingAddressID > 0)
                    SelectedAddressId = currentShippingAddressID.ToString();
                else
                    SelectedAddressId = CurrentPublicUser.ShippingAddress.AddressId.ToString();
            }
            
            hfSelectedAddressID.Value = SelectedAddressId;
            UpdateAddressControl.LoadAddress(CurrentPublicUser.ShippingAddress, false);
        }

        protected override void CreateNewShippingAddress()
        {
            //base.CreateNewShippingAddress();
            CurrentUser.GetAccountInfo();
            CurrentUser.GetConstituentInfo();
            CurrentUser.GetAddresses();
            UpdateAddressControl.SaveAddress(CurrentPublicUser.ShippingAddress);
            SelectedAddressId = CurrentPublicUser.ShippingAddress.AddressId.ToString();
            hfSelectedAddressID.Value = SelectedAddressId;
        }
    }
}