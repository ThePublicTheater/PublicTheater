using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Adage.Tessitura;
using Adage.Tessitura.CartObjects;
using PublicTheater.Custom.CoreFeatureSet.Helper;
using TheaterTemplate.Shared.EpiServerConfig;

namespace PublicTheater.Web.Controls.Cart
{
    public class FlexPackageDisplay : TheaterTemplate.Web.Controls.CartControls.FlexPackageDisplay
    {
        public override void BindDisplay(Adage.Tessitura.CartLineItem lineItem)
        {
            base.BindDisplay(lineItem);
            var flexPackage = FlexPackage.GetFlexPackage(((FlexPackageLineItem)lineItem).PackageId);
            ltrMinimumPerformances.Text = PerformanceHelper.GetFlexPackageRequirementDescription(flexPackage);
        }

    }
}