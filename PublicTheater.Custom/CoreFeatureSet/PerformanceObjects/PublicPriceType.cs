using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheaterTemplate.Shared.PerformanceObjects;

namespace PublicTheater.Custom.CoreFeatureSet.PerformanceObjects
{

    public class PublicPriceType : TheaterTemplatePriceType
    {
        /// <summary>
        /// member/partner benefit ticket limit quantity
        /// </summary>
        public virtual int? BenefitTicketLimit { get; set; }

        /// <summary>
        /// performance web content driven ticket limit
        /// </summary>
        public virtual int? ContentTypeTicketLimit { get; set; }

        /// <summary>
        /// already has tickets in cart with this price type and performance
        /// </summary>
        public virtual int? CartItemCount { get; set; }

        /// <summary>
        /// Override Max Seat to leverage the promo offer PT, content type limit, parnter/member benefits, items alrady in cart
        /// </summary>
        public override int MaxSeats
        {
            get
            {
                var returnValue = base.MaxSeats;

                //promo PT with no max seat limit? set it to default
                if (IsPromotion && returnValue <= 0)
                {
                    returnValue = Adage.Tessitura.Config.GetValue(Adage.Tessitura.Common.ConfigKeys.MAX_SEATS, 10);
                }

                //limit ticket by content type
                if (ContentTypeTicketLimit.HasValue && ContentTypeTicketLimit < returnValue)
                {
                    returnValue = ContentTypeTicketLimit.Value;
                }

                //limit ticket by partner/member benefit, which already accounted the items in cart
                if (BenefitTicketLimit.HasValue)
                {
                    if (BenefitTicketLimit < returnValue)
                    {
                        returnValue = BenefitTicketLimit.Value;
                    }
                }
                else if (CartItemCount.HasValue)
                {
                    //if there is no partner/membet limit, need take out the items already in cart
                    returnValue = returnValue - CartItemCount.Value;
                }

                //can't go negative
                return returnValue >= 0 ? returnValue : 0;
            }

            protected set
            {
                base.MaxSeats = value;
            }
        }
    }
}
