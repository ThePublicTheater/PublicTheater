using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PublicTheater.Custom.CoreFeatureSet.CartObjects;

namespace PublicTheater.Web.Controls.Cart
{
    public class CartTotals : TheaterTemplate.Web.Controls.CartControls.CartTotals
    {
        public override void BindPage()
        {
            base.BindPage();

            var fees = ((PublicCart)CurrentCart).GetGroupedFeeItems();

            lvOrderFees.DataSource = from fee in fees
                                     select new
                                     {
                                         Description = fee.Key,
                                         Amount = fee.Value.ToString("C2"),
                                     };
            lvOrderFees.DataBind();
        }
    }
}