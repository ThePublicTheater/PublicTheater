using Adage.Tessitura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Adage.Tessitura.CartObjects;
using PublicTheater.Custom.CoreFeatureSet.Memberships;
using TheaterTemplate.Shared.CartObjects;
using PublicTheater.Custom.CoreFeatureSet.Helper;
using PublicTheater.Custom.Episerver;
using TheaterTemplate.Shared.Common;
using TheaterTemplate.Shared.EpiServerConfig;

namespace PublicTheater.Custom.CoreFeatureSet.CartObjects
{
    public class PublicCart : TheaterSharedCart
    {
        public override double Total
        {
            get
            {
                return base.Total - BenefitsDiscount;
            }
            protected set
            {
                base.Total = value;
            }
        }

        public override double SubTotal
        {
            get
            {
                return base.SubTotal - BenefitsDiscount;
            }
            protected set
            {
                base.SubTotal = value;
            }
        }

        public override double BalanceToCharge
        {
            get
            {
                return base.BalanceToCharge - BenefitsDiscount;
            }
            protected set
            {
                base.BalanceToCharge = value;
            }
        }

        /// <summary>
        /// total partner benefits discount applied to this cart
        /// </summary>
        public virtual Double BenefitsDiscount
        {
            get
            {
                return PerformanceLineItemTickets.Sum(t => t.BenefitDiscount);
            }
        }
        /// <summary>
        /// all performance tickets in the cart
        /// </summary>
        public virtual IEnumerable<PublicTicket> PerformanceLineItemTickets
        {
            get
            {
                return this.CartLineItems.OfType<PerformanceLineItem>().SelectMany(perf => perf.TicketBlocks).SelectMany(t => t).OfType<PublicTicket>();
            }
        }


        /// <summary>
        /// call membership contribution update Sproc to refresh the membership benefits
        /// </summary>
        public virtual void UpdateMembershipContribution()
        {
            var contributions = CartLineItems.OfType<ContributionLineItem>().ToList();
            if (contributions.Any())
            {
                //call sp update
                foreach (var contribution in contributions)
                {
                    var membershipNotes = Memberships.PublicContributionCsiNote.GetFor(contribution);
                    if (membershipNotes == null)
                    {
                        var error = string.Format("Missing membership CSI for contribution -{0} -{1}-{2}", contribution.ItemId, contribution.Notes, contribution.ContributionFundDescription);
                        Adage.Common.ElmahCustomError.CustomError.LogError(new ApplicationException(error), "UpdateMembershipContribution", null);
                    }

                    var spParams = new Dictionary<string, object>
                                       {
                                           {"@customer_no", this.CustomerNumber},
                                           {"@ref_no", contribution.ItemId},
                                           {"@ack_name", membershipNotes == null ? string.Empty : membershipNotes.DonorName},
                                           {"@anonymous", membershipNotes == null || membershipNotes.IsAnonymous ? "Y" : "N"},
                                           {"@decline_benefits",membershipNotes == null || membershipNotes.DeclineBenefits ? "Y" : "N"},
                                           {"@company_match",membershipNotes == null ||string.IsNullOrEmpty(membershipNotes.MatchCompany)? "N": "Y"},
                                           {"@company_match_name",membershipNotes == null ? string.Empty : membershipNotes.MatchCompany},
                                           {"@Option", string.Empty}
                                       };

                    try
                    {
                        TessSession.GetSession().ExecuteLocalProcedure(Common.PublicAppSettings.MembershipUpdateSP, spParams);
                    }
                    catch (Exception ex)
                    {
                        Adage.Common.ElmahCustomError.CustomError.LogError(ex, "UpdateMembershipContribution", null);
                    }

                }
            }
        }

        public override string BuildSectionText(TheaterTemplate.Shared.EpiServerConfig.VenueConfig venueConfig, List<Ticket> tickets, string sectionDescription)
        {
            StringBuilder sectionText = new StringBuilder();
            bool showGeneralAdmissionText = (venueConfig != null && venueConfig.IsGeneralAdmission && !string.IsNullOrEmpty(venueConfig.GeneralAdmissionSectionText));
            bool hideSeatInfo = (venueConfig != null && venueConfig.IsGeneralAdmission && venueConfig.HideGeneralAdmissionSeats);
            if (tickets.Any() && Config.GetValue("hide-seats-for-perfno-" + tickets.First().PerformanceId, "") != "")
            {
                hideSeatInfo = true;
            }
                            

            if (showGeneralAdmissionText)
            {
                sectionText.Append(venueConfig.GeneralAdmissionSectionText);
            }
            else
            {
                //Use price zone description over section description
                var zoneDescription = tickets.Any() ? tickets.First().ZoneDescription : string.Empty;
                if (string.IsNullOrEmpty(zoneDescription) == false)
                {
                    sectionText.Append(zoneDescription);
                }
                else
                {
                    sectionText.Append(sectionDescription);
                }

                //Append seat numbers
                if (hideSeatInfo == false)
                {
                    bool sectionStarting = true;
                    foreach (Ticket ticket in tickets)
                    {
                        
                        bool ticketContainsSeatInfo = !string.IsNullOrEmpty(ticket.SeatRow) && !string.IsNullOrEmpty(ticket.SeatNumber);
                        if (ticketContainsSeatInfo)
                        {

                            Performance currentPerformance = Performance.GetPerformance(ticket.PerformanceId);
                            bool isJoesPub = SiteThemeHelper.GetSiteTheme(currentPerformance).Equals(Enums.SiteTheme.JoesPub);
                            sectionText.Append(FormattedSeatNumber(ticket, sectionDescription, isJoesPub, sectionStarting));
                            sectionStarting = false;
                        }
                    }
                }
            }
            return sectionText.ToString();
        }

        private string FormattedSeatNumber(Ticket ticket, String sectionDescription, bool isJoesPub, bool sectionStarting)
        {
            String seperator = sectionStarting ? ": " : ", ";
            sectionStarting = false;
            String RowOrTable = (isJoesPub ? "Table " : "Row ") + ticket.SeatRow + " ";
            if (IsSeatAtBar(sectionDescription, isJoesPub))
                RowOrTable = "";
            return String.Format("{0}{1}Seat {2}", seperator, RowOrTable, ticket.SeatNumber);
        }

        private bool IsSeatAtBar(String sectionDescription, bool isJoesPub)
        {
            if (isJoesPub && sectionDescription.Contains("Barstool"))
                return true;
            return false;
        }

        /// <summary>
        /// Cart only has GC
        /// </summary>
        /// <returns></returns>
        public virtual bool HasOnlyGiftCards()
        {
            return CartLineItems.Any() && CartLineItems.All(item => item is GiftCertificateLineItem);
        }

        /// <summary>
        /// Cart Has gala sale ticket
        /// </summary>
        /// <returns></returns>
        public virtual bool HasGalaTickets()
        {
            //default to HABO if there is Gala ticket in cart
            foreach (Adage.Tessitura.ContributionLineItem contributionLineItem in CartLineItems.OfType<Adage.Tessitura.ContributionLineItem>())
            {
                var membershipNotes = PublicContributionCsiNote.GetFor(contributionLineItem);
                if (membershipNotes != null && membershipNotes.ContributionType == PublicContributionType.AnnualGala)
                {
                    return true;
                }
            }

            return false;
        }

        public virtual bool HasOnlyContributions()
        {
            return CartLineItems.Any() && CartLineItems.All(item => item is ContributionLineItem);
        }

        /// <summary>
        /// Group fees by description and sum up the amount
        /// </summary>
        public virtual Dictionary<string, double> GetGroupedFeeItems()
        {
            var feeNameAmount = new Dictionary<string, double>();
            var feeTypeConfiguration = IDValueConfig.GetIDValueConfig(Config.GetValue(TheaterSharedAppSettings.FeeTypeConfigId, 0));
            foreach (var itemFee in CartLineItemFees)
            {
                var desc = feeTypeConfiguration.GetValueByID(itemFee.FeeNumber, itemFee.Description);
                if (!feeNameAmount.ContainsKey(desc))
                {
                    feeNameAmount.Add(desc, itemFee.Amount);
                }
                else
                {
                    feeNameAmount[desc] = feeNameAmount[desc] + itemFee.Amount;
                }
            }

            return feeNameAmount;
        }

       
    }
}
