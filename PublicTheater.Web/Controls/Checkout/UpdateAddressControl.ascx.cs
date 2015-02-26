using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Adage.Tessitura;
using PublicTheater.Custom.CoreFeatureSet.UserObjects;
using TheaterTemplate.Shared.Common;

namespace PublicTheater.Web.Controls.Checkout
{
    public class UpdateAddressControl : TheaterTemplate.Web.Controls.CheckoutControls.UpdateAddressControl
    {
        protected global::System.Web.UI.WebControls.Label lbPlaceHolderAddressMessage;

        protected virtual string PlaceholderAddressMessage
        {
            get
            {
                return Adage.Tessitura.Config.GetValue("PlaceholderAddressMessage",
                    "Your account contains a placeholder delivery address. Please update your address on file."
                    );
            }
        }

        protected override void PopulateStates()
        {
            base.PopulateStates();
            regexPostalCode.Enabled = ddlCountry.SelectedValue == Config.GetValue(TheaterSharedAppSettings.COUNTRY_CODE_USA);
        }

        public override void LoadAddress(Adage.Tessitura.UserObjects.UserAddress address, bool isNew)
        {
            base.LoadAddress(address, isNew);

            if (PublicUser.IsPlaceHolderAddress(address))
            {
                lbPlaceHolderAddressMessage.Text = PlaceholderAddressMessage;
                lbPlaceHolderAddressMessage.Visible = true;
            }
            else
            {
                lbPlaceHolderAddressMessage.Visible = false;
            }
        }
    }
}