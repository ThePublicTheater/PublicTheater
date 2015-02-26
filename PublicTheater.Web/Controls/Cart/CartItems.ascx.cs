using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Adage.Tessitura;
using Adage.Tessitura.CartObjects;
using PublicTheater.Custom.CoreFeatureSet.Memberships;

namespace PublicTheater.Web.Controls.Cart
{
    public class CartItems : TheaterTemplate.Web.Controls.CartControls.CartItems
    {
        protected override void RemoveItem(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
        {
            int lineItemId = 0;
            int.TryParse(e.CommandArgument.ToString(), out lineItemId);
            var lineItem = CurrentCart.CartLineItems.FirstOrDefault(item => item.ItemId == lineItemId);

            var contributionLineItem = lineItem as ContributionLineItem;
            if (contributionLineItem != null)
            {
                var membershipNotes = PublicContributionCsiNote.GetFor(contributionLineItem);
                if (membershipNotes != null)
                {
                    if (membershipNotes.ContributionType == PublicContributionType.Membership)
                    {
                        //it's a membership item, remove associated fees
                        foreach (var qty in Enumerable.Range(0, membershipNotes.Quantity))
                        {
                            var associatedFee = CurrentCart.CartLineItemFees.FirstOrDefault(fee => fee.FeeNumber == Custom.CoreFeatureSet.Helper.MembershipHelper.MembershipFeeId);
                            if (associatedFee != null)
                            {
                                associatedFee.Remove();
                            }
                        }
                    }

                    // disable immediate access to benefits for now. will be included in phase 1.5  -WZ @ 02/12/2014
                    //if (membershipNotes.ContributionType == PublicContributionType.PartnersProgram || membershipNotes.ContributionType == PublicContributionType.Membership)
                    //{
                    //    //it's a membership or partner item, remove cart items and update mos to web standard
                    //    var currentSession = Repository.Get<TessSession>();
                    //    if (currentSession.ModeOfSale != currentSession.OriginalMOS)
                    //    {
                    //        Custom.CoreFeatureSet.Helper.MembershipHelper.RemoveCurrentCartItemsWithMosCheck();
                    //        Adage.Tessitura.Cart.Flush();
                    //        base.RemoveItem(source, new RepeaterCommandEventArgs(null, null, new CommandEventArgs(null, 0)));
                    //    }
                    //}
                }

            }

            base.RemoveItem(source, e);
        }

        public override void BindPage()
        {
            base.BindPage();
        }



        private bool hidePerformanceHeader;
        protected override void ChooseAndBindCartItem(RepeaterItemEventArgs e)
        {
            base.ChooseAndBindCartItem(e);

            var cartSingleTicket = e.Item.FindControl("cartSingleTicket") as SingleTicketDisplay;
            if (cartSingleTicket != null && cartSingleTicket.Visible)
            {
                if (hidePerformanceHeader)
                {
                    cartSingleTicket.HideHeader();
                }
                else
                {
                    hidePerformanceHeader = true;
                }
            }
        }
    }
}