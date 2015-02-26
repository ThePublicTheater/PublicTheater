using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Adage.Tessitura;
using Adage.Tessitura.CartObjects;
using PublicTheater.Custom.CoreFeatureSet.CartObjects;
using TheaterTemplate.Shared.PerformanceObjects;

namespace PublicTheater.Custom.CoreFeatureSet.PerformanceObjects
{
    public class PublicPerformancePricing : TheaterTemplatePerformancePricing
    {
        protected virtual Memberships.MembershipBenefits CurrentBenefits
        {
            get { return Memberships.MembershipBenefits.GetMembershipBenefits(this.PerformanceId); }
        }

        protected virtual bool SessionVerifyNeeded { get; set; }
        protected virtual string forSession { get; set; }


        protected override bool FillNeeded
        {
            get
            {
                var fillNeeded = base.FillNeeded;

                //if base no need to fill, check on sale flag and mos onsale date
                //start filling more frequently when we are close to mos onsale
                if (!fillNeeded && SectionsAvailable && MosStartDate.HasValue && !OnSale)
                {
                    return FillNeededByMosStart();
                }

                return fillNeeded;
            }
        }

        protected override void Fill(System.Data.DataSet dataSet, int performanceId, short MOS, int PromoCode)
        {
            forSession = Adage.Tessitura.TessSession.GetSessionKey();
            base.Fill(dataSet, performanceId, MOS, PromoCode);
        }


        /// <summary>
        /// override price loading to adjust benefits PT and items already in cart, called everytime the pricing is used
        /// </summary>
        /// <param name="performanceId"></param>
        /// <param name="mos"></param>
        /// <param name="promoCode"></param>
        protected override void LoadPricing(int performanceId, short mos, int promoCode)
        {
            if (SessionVerifyNeeded && CurrentBenefits.forSession != this.forSession)
            {
                //reload from tessitura if the benefit session updated
                Filled = false;
            }
            base.LoadPricing(performanceId, mos, promoCode);

            //adjust benefit PT and items already in cart, need to called each time pricing is being called, don't cache at all. 
            AdjustForPartnerBenefitsAndTicketAlreadyInCart();
        }

        protected virtual IEnumerable<PublicTicket> PerfTicketsInCart
        {
            get
            {
                try
                {
                    var cart = Adage.Tessitura.Cart.GetCart() as PublicCart;
                    return cart != null ? cart.PerformanceLineItemTickets.Where(t => t.PerformanceId == this.PerformanceId) : new List<PublicTicket>();
                }
                catch (Exception)
                {
                    //if cart expired, cart object threw exception. perf pricing still need to be loaded. 
                    return new List<PublicTicket>();
                }
            }
        }

        protected override void RestrictByPriceType()
        {
            base.RestrictByPriceType();
            AdjustPerformanceTicketLimits();
        }
        /// <summary>
        /// limit max seats if performance has a web content specify the max ticket per transaction
        /// </summary>
        protected virtual void AdjustPerformanceTicketLimits()
        {
            if (this.PerformanceId <= 0)
            {
                return;
            }

            var currentPerformance = Performance.GetPerformance(this.PerformanceId);

            var restriction = currentPerformance.WebContent.FirstOrDefault(wc => wc.ContentTypeID == Config.GetValue(Common.PublicAppSettings.TicketLimitContentTypeID, 6));

            int ticketLimit;
            if (restriction != null && int.TryParse(restriction.ContentValue, out ticketLimit) && ticketLimit > 0)
            {
                //performance has content type specify the ticket limit, then set it as max seat for each price type
                foreach (var priceType in this.PriceTypes)
                {
                    ((PublicPriceType)priceType).ContentTypeTicketLimit = ticketLimit;
                }
            }
        }


        /// <summary>
        /// Limit max seat for partner benefits PTs based on SP, and also adjust the maxseat for non-partner benefits PTs based on items already in cart. 
        /// </summary>
        public virtual void AdjustForPartnerBenefitsAndTicketAlreadyInCart()
        {
            var ticketsIncart = PerfTicketsInCart.ToList();

            var priceTypeToRemove = new List<int>();
            foreach (var priceType in this.PriceTypes)
            {
                var benefit = CurrentBenefits.FirstOrDefault(b => b.PriceTypeID == priceType.PriceTypeId && b.ProductSeasonID == this.ProductionSeasonId);

                if (benefit != null)
                {
                    //it's a benefit PT, restrict it based on the Sproc returned quantity
                    ((PublicPriceType)priceType).BenefitTicketLimit = benefit.AvailableCount;

                    if (benefit.MaskPrice)
                    {
                        foreach (var section in Sections.Where(s => s.PriceTypeId == benefit.PriceTypeID))
                        {
                            section.SetPrice(benefit.MaskPriceValue);
                        }
                    }
                    if (benefit.AvailableForLevel == false)
                    {
                        //not qualify for current level, need remove the PT
                        priceTypeToRemove.Add(priceType.PriceTypeId);
                    }
                }
                else
                {
                    //it's not a benefit PT, adjust the quantity left by subtracting the number already in cart
                    ((PublicPriceType)priceType).CartItemCount = ticketsIncart.Count(t => t.PriceTypeId == priceType.PriceTypeId);
                }
            }


            if (priceTypeToRemove.Any())
            {
                //remove PTs not avaiable for this benefit level
                this.RemovePriceTypes(priceTypeToRemove);

                //next time perf price call, need verify if session changed, to reload benefits and avilable price types
                SessionVerifyNeeded = true;
            }
        }
    }
}
