using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Adage.Tessitura.CartObjects;
using com.tessiturasoftware.tessitura;
using PublicTheater.Custom.CoreFeatureSet.CartObjects;
using PublicTheater.Custom.CoreFeatureSet.UserObjects;
using TheaterTemplate.Shared.CartObjects;

namespace PublicTheater.Web.Controls.Checkout
{
    public class ConfirmationOptions : TheaterTemplate.Web.Controls.CheckoutControls.ConfirmationOptions
    {
        protected virtual PublicCart CurrentPublicCart { get { return CurrentCart as PublicCart; } }
        protected virtual PublicUser CurrentPublicUser { get { return CurrentUser as PublicUser; } }
        protected HiddenField hfOrderNo;
        protected Panel GoogleAnalyticsPanel;
        protected HiddenField hfOrganization;
        protected HiddenField hfTotal;
        protected HiddenField hfFees;
        protected HiddenField hfShipping;
        protected HiddenField hfCity;
        protected HiddenField hfState;
        protected HiddenField hfCountry;



        protected override void BindPage()
        {
            base.BindPage();
            billngAddress.Fill(CurrentUser, CurrentPublicUser.BillingAddress);

            if (CurrentCart.ShippingMethodId == TheaterSharedCart.ShippingMethodPostal())
            {
                var shippingAddress = CurrentUser.UserAddresses.FirstOrDefault(addr => addr.AddressId == CurrentCart.ShippingAddressID);

                if (shippingAddress == null)
                    shippingAddress = CurrentUser.PrimaryAddress;

                deliveryAddress.Fill(CurrentUser, shippingAddress);
                shippingAddressHeader.Visible = true;
            }

            if(CurrentPublicCart.HasOnlyGiftCards())
            {
                pnlDeliveryMethod.Visible = false;
            }

            //google analytics panel
            Adage.Tessitura.Cart cart = CurrentCart;
            hfOrderNo.Value = cart.OrderNumber.ToString();
            hfOrganization.Value = "The Public Theater";
            hfTotal.Value = cart.Total.ToString();
            //hfFees.Value = cart.CartLineItemFees.
            hfCity.Value = Adage.Tessitura.User.GetUser().PrimaryAddress.City;
            hfState.Value = Adage.Tessitura.User.GetUser().PrimaryAddress.State;
            hfCountry.Value = Adage.Tessitura.User.GetUser().PrimaryAddress.Country;

            int i = 0;
            foreach (var item in cart.CartLineItems.OfType<PerformanceLineItem>())
            {
                HiddenField hf = new HiddenField();
                hf.Value=cart.OrderNumber.ToString()+";"+item.Performance.PerformanceId.ToString()+";"+
                    item.Performance.Title + ";" + "NA;" + item.PricePerSeat + ";" + item.Quantity.ToString();
                hf.ID = "LineItem" + i.ToString();
                hf.ClientIDMode=System.Web.UI.ClientIDMode.Static;
                GoogleAnalyticsPanel.Controls.Add(hf);
                i++;
            }
        }
    }
}