using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PublicTheater.Custom.CoreFeatureSet.CartObjects
{
    public class PublicTicket : Adage.Tessitura.CartObjects.Ticket
    {
        private Memberships.MembershipBenefit _currentBenefit;
        protected virtual Memberships.MembershipBenefit CurrentBenefit
        {
            get
            {
                if (_currentBenefit == null)
                {
                    _currentBenefit = Memberships.MembershipBenefits.GetMembershipBenefits(this.PerformanceId).FirstOrDefault(
                            b => b.PriceTypeID == this.PriceTypeId && b.ProductSeasonID == this.ProductionSeasonId);
                }
                return _currentBenefit;
            }
        }

        public override double AmountDue
        {
            get
            {
                if (HasPartnerDiscount)
                {
                    return (double)CurrentBenefit.MaskPriceValue;
                }
                return base.AmountDue;
            }
            protected set
            {
                base.AmountDue = value;
            }
        }

        /// <summary>
        /// Partner Discount Amount
        /// </summary>
        public virtual double BenefitDiscount
        {
            get
            {
                if (HasPartnerDiscount)
                {
                    return base.AmountDue - (double)CurrentBenefit.MaskPriceValue;
                }
                return 0d;
            }
        }

        /// <summary>
        /// Is a partner discounted ticket
        /// </summary>
        public virtual bool HasPartnerDiscount
        {
            get { return CurrentBenefit != null && CurrentBenefit.MaskPrice; }
        }
    }
}
