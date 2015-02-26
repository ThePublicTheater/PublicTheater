using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Adage.Tessitura;
using Adage.Tessitura.CartObjects;
using Adage.Tessitura.Common;
using Adage.Tessitura.PerformanceObjects;
using Microsoft.Ajax.Utilities;
using PublicTheater.Custom.CoreFeatureSet.Helper;
using PublicTheater.Custom.CoreFeatureSet.Memberships;
using TheaterTemplate.Shared.Common;
using TheaterTemplate.Shared.EpiServerConfig;

namespace PublicTheater.Web.Controls.Reserve
{
    public partial class BestAvailable : TheaterTemplate.Web.Controls.ReserveControls.BestAvailable
    {
      


        #region properties
        private PerformancePricing _currentPerformancePricing;
        /// <summary>
        /// Performance price object
        /// </summary>
        protected virtual PerformancePricing CurrentPerformancePricing
        {
            get
            {
                if (_currentPerformancePricing == null)
                {
                    _currentPerformancePricing = CurrentPerformance.PerformancePricing;
                }
                return _currentPerformancePricing;
            }
        }
        #endregion

        #region CFS overrides
        
        protected override IEnumerable<Section> GetAvailableSections()
        {
            return CurrentPerformancePricing.Sections.Where(section => section.IsAvailable && section.AvailableCount > 0);
        }

        public override void PopulateSectionList()
        {
            var availSections = GetAvailableSections();
            var priceTypes = CurrentPerformancePricing.PriceTypes.Where(pt => availSections.Any(section => section.PriceTypeId == pt.PriceTypeId) && pt.PriceTypeId!=29);
            
            PopulateSectionList(availSections, priceTypes);
            
        }

        public override void PopulateSectionList(IEnumerable<Section> availSections, IEnumerable<PriceType> priceTypes)
        {
            if (PerformanceHelper.IsSummerParkPerformance(CurrentPerformance))
            {
                //mask zones for summer supporter performances, which each price type has their own section available
                //merge the zones into just one row and limit the "N/A" options

                priceTypes = priceTypes.OrderBy(p => p.PriceTypeId == 617)
                    .ThenBy(p => p.PriceTypeId == 530)
                    .ThenBy(p => p.PriceTypeId == 631)
                    .ThenBy(p => p.PriceTypeId==529)
                    .ThenBy(p => p.PriceTypeId==618)
                    .ThenBy(p => p.PriceTypeId == 619)           
                    .ThenBy(p => p.PriceTypeId == 264)
                    .ToList();
                var sectionInfo = new SectionInfo();
                var prices = new List<string>();
                foreach (var priceType in priceTypes)
                {
                    var section = availSections.FirstOrDefault(sec => sec.PriceTypeId == priceType.PriceTypeId);
                    if (section != null)
                    {
                        sectionInfo.SectionId = section.SectionId;
                        sectionInfo.Description = section.Description;
                        prices.Add(section.Price.ToString("C"));
                    }
                    else
                    {
                        prices.Add(Config.GetValue(TheaterSharedAppSettings.BEST_AVAILABLE_PRICETYPE_NO_SECTION, "N/A"));
                    }
                }

                sectionInfo.Prices = prices;

                lvSectionSelection.DataSource = new List<SectionInfo>{ sectionInfo };
                lvSectionSelection.DataBind();

                lvPriceTypeNames.DataSource = GetPriceTypeDescriptions(priceTypes);
                lvPriceTypeNames.DataBind();

                lvPriceTypeQuantitySelection.DataSource = priceTypes;
                lvPriceTypeQuantitySelection.DataBind();
            }
            else
            {
               // base.PopulateSectionList(availSections, priceTypes);
                var groupedSections = availSections.GroupBy(section => section.SectionId);
                var sectionInfoList = new List<SectionInfo>();

                foreach (var sectionGroup in groupedSections)
                {
                    var newSectionInfo = GetSectionInfo(priceTypes, sectionGroup);

                    if (newSectionInfo != null)
                    {
                        if (!newSectionInfo.Prices.TrueForAll((string price) => price.Equals("N/A")))
                        {
                            sectionInfoList.Add(newSectionInfo);
                        }
                    }
                }

                lvSectionSelection.DataSource = sectionInfoList;
                lvPriceTypeQuantitySelection.DataSource = priceTypes;
                lvPriceTypeNames.DataSource = GetPriceTypeDescriptions(priceTypes);

                lvPriceTypeNames.DataBind();
                lvPriceTypeQuantitySelection.DataBind();
                lvSectionSelection.DataBind();
            }


        }


        protected override void ValidateAndReserveSelections()
        {
            if (PerformanceHelper.IsSummerParkPerformance(CurrentPerformance))
            {
                
                pnlError.Visible = false;

                if (IsRequestValid() == false)
                    return;

                var sectionAndPTQuantities = GetSectionAndPtQuantities();

                if (sectionAndPTQuantities.Keys.Count > 1)
                {
                    //try to reserve more than 2 zones?  Warn and ask if continue or not
                    mdlMultiZoneConfirm.Show();
                }
                else
                {
                    ReserveMergedSelections(sectionAndPTQuantities);

                }
            }
            else
            {
                base.ValidateAndReserveSelections();    
            }
            
        }

        /// <summary>
        /// Need tweak it to consider edge case priceType.MaxSeats = 0 for some partner benefits
        /// </summary>
        /// <param name="priceType"></param>
        /// <returns></returns>
        protected override int GetMaxSeats(Adage.Tessitura.PriceType priceType)
        {
            int maxSeats = MaxQuantityPerSection - MinQuantityPerSection + 1;

            //if (priceType.MaxSeats > 0 && priceType.MaxSeats < maxSeats)
            if (priceType.MaxSeats >= 0 && priceType.MaxSeats < maxSeats)
                maxSeats = priceType.MaxSeats - MinQuantityPerSection + 1;

            if (maxSeats < MinQuantityPerSection)
                maxSeats = MinQuantityPerSection;
            return maxSeats;
        }

        protected override void DisplayError(string error)
        {
            base.DisplayError(error);
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "update", "$('.bestAvailableButton').removeClass('disabled');", true);
        }

        protected override ReserveRequest GetReserveRequest(int selectedSectionId, Dictionary<int, int> priceTypeQuantities)
        {
            var reserveRequest = base.GetReserveRequest(selectedSectionId, priceTypeQuantities);
            if (CurrentVenueConfig != null && CurrentVenueConfig.IsGeneralAdmission)
            {
                //do not send special request for venues like Delacorte
                var venueList = Config.GetList<int>("NonSpecialRequestVenueList", "8");
                if (venueList.Contains(CurrentVenueConfig.VenueId))
                {
                    reserveRequest.LeaveSingleSeats = null;
                    reserveRequest.ContiguousSeats = 0;
                }
            }

            return reserveRequest;
        }

        protected override bool ReserveAllSelections(int selectedSectionId, Dictionary<int, int> priceTypeQuantities)
        {
            ReserveRequest seatRequest = GetReserveRequest(selectedSectionId, priceTypeQuantities);

            try
            {
                var results=CurrentPerformance.PerformancePricing.ReserveTickets(seatRequest);
                seatRequest.LineItemId=Repository.Get<TessituraAPI>().LineItemID ?? 0;
                if (Session["BestAvailableLiSeqNo"] == null)
                {
                    Session["BestAvailableLiSeqNo"]=new List<int>();
                    
                }
                ((List<int>)Session["BestAvailableLiSeqNo"]).Add(seatRequest.LineItemId);
            }
            catch (TessituraException ex)
            {
                switch (ex.ExceptionType)
                {
                    case TessituraException.ExceptionTypes.TESSITURA_SEATS_NOT_AVAILABLE_EXCEPTION:
                        DisplayError(Config.GetValue(TheaterSharedAppSettings.BEST_AVAILABLE_RESERVATION_FAILED, "Unable to reserve seats in selected section. Please try again."));
                        break;
                    case TessituraException.ExceptionTypes.TESSITURA_SEAT_LOCKING_EXCEPTION:
                        throw ex;
                    case TessituraException.ExceptionTypes.TESSITURA_INVALID_PRICETYPE_EXCEPTION:
                        DisplayError(Config.GetValue(TheaterSharedAppSettings.BEST_AVAILABLE_RESERVATION_FAILED, "Unable to reserve seats in selected section. Please try again."));
                        Adage.Common.ElmahCustomError.CustomError.LogError(ex, string.Format("Best available price type - {0} - {1}", ex.Message, ex.StackTrace), null);
                        break;
                    default:
                        DisplayError(Config.GetValue(TheaterSharedAppSettings.BEST_AVAILABLE_RESERVATION_FAILED, "Unable to reserve seats in selected section. Please try again."));
                        Adage.Common.ElmahCustomError.CustomError.LogError(ex, string.Format("Best available unknown tessitura error - {0} - {1}", ex.Message, ex.StackTrace), null);
                        break;
                }

                return false;
            }
            catch (Exception ex)
            {
                Adage.Common.ElmahCustomError.CustomError.LogError(ex, string.Format("Best available unknown tessitura error - {0} - {1}", ex.Message, ex.StackTrace), null);
                DisplayError(Config.GetValue(TheaterSharedAppSettings.BEST_AVAILABLE_RESERVATION_FAILED, "Unable to reserve seats in selected section. Please try again."));
                return false;
            }

            return true;
        }

        protected override void Page_Load(object sender, EventArgs e)
        {
            lbOK.Click += new EventHandler(lbOK_Click);
            base.Page_Load(sender, e);
        }

        protected virtual void lbOK_Click(object sender, EventArgs e)
        {
            mdlMultiZoneConfirm.Hide();
            var sectionAndPTQuantities = GetSectionAndPtQuantities();
            ReserveMergedSelections(sectionAndPTQuantities);
        }

        protected override void lvPriceTypeQuantitySelection_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType != ListViewItemType.DataItem)
                return;

            var priceType = e.Item.DataItem as PriceType;
            var ddlPriceTypeQuantity = e.Item.FindControl("ddlPriceTypeQuantity") as DropDownList;
            ddlPriceTypeQuantity.Attributes.Add(ATTRIBUTE_KEY_PRICE_TYPE_ID, priceType.PriceTypeId.ToString());
            if (Config.GetValue("BogoPriceType" + priceType.PriceTypeId.ToString()).IsNullOrWhiteSpace())
            {
                ddlPriceTypeQuantity.DataSource = Enumerable.Range(MinQuantityPerSection, GetMaxSeats(priceType));
            }
            else
            {
                ddlPriceTypeQuantity.DataSource = Enumerable.Range(MinQuantityPerSection, GetMaxSeats(priceType)).Where(
                    x => x%2 == 0);
            }
            ddlPriceTypeQuantity.DataBind();
        }

        #endregion


        protected virtual Dictionary<int, Dictionary<int, int>> GetSectionAndPtQuantities()
        {
            var availableSections = GetAvailableSections();
            var priceTypeQuantities = GetPriceTypesAndQuantities();

            //if summer park show, allow reserve different sections, but group the requests by section
            var sectionAndPTQuantities = new Dictionary<int, Dictionary<int, int>>();
            foreach (var priceTypeQuantity in priceTypeQuantities)
            {
                var sectionId = availableSections.First(sec => sec.PriceTypeId == priceTypeQuantity.Key).SectionId;
                if (sectionAndPTQuantities.ContainsKey(sectionId) == false)
                {
                    sectionAndPTQuantities.Add(sectionId, new Dictionary<int, int>());
                }
                sectionAndPTQuantities[sectionId].Add(priceTypeQuantity.Key, priceTypeQuantity.Value);
            }
            return sectionAndPTQuantities;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sectionAndPTQuantities"></param>
        protected virtual void ReserveMergedSelections(Dictionary<int, Dictionary<int, int>> sectionAndPTQuantities)
        {
            //validate all requests
            foreach (KeyValuePair<int, Dictionary<int, int>> sectionAndPtQuantity in sectionAndPTQuantities)
            {
                if (AreSectionAndPriceTypesValid(sectionAndPtQuantity.Key, sectionAndPtQuantity.Value) == false)
                    return;
            }

            //reserve all requests
            foreach (KeyValuePair<int, Dictionary<int, int>> sectionAndPtQuantity in sectionAndPTQuantities)
            {
                if (ReserveAllSelections(sectionAndPtQuantity.Key, sectionAndPtQuantity.Value) == false)
                    return;
            }

            RedirectToCart();
        }


    }
}