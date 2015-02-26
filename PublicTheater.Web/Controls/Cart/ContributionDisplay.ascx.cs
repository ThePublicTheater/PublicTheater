using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Adage.Tessitura;
using Adage.Tessitura.CartObjects;
using PublicTheater.Custom.CoreFeatureSet.Memberships;

namespace PublicTheater.Web.Controls.Cart
{
    public class ContributionDisplay : TheaterTemplate.Web.Controls.CartControls.ContributionDisplay
    {
        public override void BindDisplay(CartLineItem item)
        {
            base.BindDisplay(item);
            var membershipNotes = PublicContributionCsiNote.GetFor(item);
            if (membershipNotes != null && membershipNotes.ContributionType != PublicContributionType.GeneralDonation)
            {
                ltrDonationTotalHeader.Text = membershipNotes.ProgramName;
                ltrDonationThankYouText.Text = membershipNotes.ProgramLevel;
            }
        }


    }
}