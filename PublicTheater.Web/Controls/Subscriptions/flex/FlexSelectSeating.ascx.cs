using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Adage.Tessitura;
using Adage.Tessitura.CartObjects;
using PublicTheater.Custom.CoreFeatureSet.Helper;
using TheaterTemplate.Shared.Common;

namespace PublicTheater.Web.Controls.Subscriptions.flex
{
    public class FlexSelectSeating : TheaterTemplate.Web.Controls.Subscriptions.FlexControls.FlexSelectSeating
    {
        protected override void BindDisplayElements()
        {
            base.BindDisplayElements();
            ltrMinPerformances.Text = PerformanceHelper.GetFlexPackageRequirementDescription(CurrentFlexPackage);
        }

        protected override bool ValidateSelections()
        {
            return base.ValidateSelections() && Adage.Tessitura.Cart.GetCart().CartLineItems.OfType<FlexPackageLineItem>().Any();
        }

        protected override void Page_Load(object sender, EventArgs e)
        {

            base.Page_Load(sender, e);
            //JUST WHILE WE ARE IN UTR WORLD
          //  base.btnAddToCart_Click(sender,e);
        }
        protected override void DisplayMissingSectionError()
        {
            pnlError.Visible = true;
            ltrError.Text = Config.GetValue(TheaterSharedAppSettings.FLEX_SELECT_SECTION_ERROR, "One or more of your selections does not have available seats.  You may use the back button to re-build your package or call 212-967-7555 for assistance.”");
        }
    }
}