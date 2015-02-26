using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Adage.Tessitura;
using PublicTheater.Custom.CoreFeatureSet.EpiServerConfig;
using PublicTheater.Custom.CoreFeatureSet.PerformanceObjects;
using PublicTheater.Custom.Episerver;
using TheaterTemplate.Shared.EpiServerConfig;

namespace PublicTheater.Custom.CoreFeatureSet.Helper
{
    public class PerformanceHelper
    {
        public enum PerformanceAvailable
        {
            Unknown = 0, Available = 1, NotOnSale = 2, NoPriceTypes = 3, NoSections = 4, NoSectionsAvailable = 5, NoSectionsAvailableSeats = 6
        }
        /// <summary>
        /// Determine if a tess performance is available
        /// </summary>
        /// <param name="performance"></param>
        /// <returns></returns>
        public static PerformanceAvailable PerformanceAvailableStatus(Performance performance)
        {
            if (performance == null)
                return PerformanceAvailable.Unknown;

            if (performance.PerformancePricing.OnSale == false)
            {
                // "performance not on sale";
                return PerformanceAvailable.NotOnSale;
            }
            if (performance.PerformancePricing.PriceTypes.Any() == false)
            {
                //"No price types from tessitura";
                return PerformanceAvailable.NoPriceTypes;
            }

            if (performance.PerformancePricing.Sections.Any() == false)
            {
                // "No sections from tessitura";
                return PerformanceAvailable.NoSections; ;
            }

            if (performance.PerformancePricing.SectionsAvailable == false && performance.PerformancePricing.PriceTypes.Any())
            {
                //"No sections marked as available";
                return PerformanceAvailable.NoSectionsAvailable;
            }

            if (performance.PerformancePricing.SectionsAvailableSeats == false)
            {
                // "No sections have available seats";
                return PerformanceAvailable.NoSectionsAvailableSeats;
            }


            return PerformanceAvailable.Available;
        }



        /// <summary>
        /// Get perfs from now to X days in the future and sort by perf date time
        /// </summary>
        /// <param name="siteTheme"></param>
        /// <param name="daysInTheFuture"></param>
        /// <returns></returns>
        public static IEnumerable<Performance> GetPerformancesBySiteTheme(Enums.SiteTheme siteTheme, int daysInTheFuture)
        {
            var contentTypeIDs = SiteThemeHelper.GetSiteThemeContentTypeIds(siteTheme);


            if (!contentTypeIDs.Any())
                return new List<Performance>();

            //use today, so the tess call is cached during the day
            var startDate = DateTime.Today;
            var endDate = startDate.AddDays(daysInTheFuture);

            var perfCriteria = new PerformancesCriteria
                                   {
                                       StartDate = startDate,
                                       EndDate = endDate,
                                       ContentType = string.Join(",", contentTypeIDs),
                                   };

            var foundPerformance = Performances.GetPerformances(perfCriteria)
                .Where(perf => perf.PerformanceDate >= DateTime.Now && perf.PerformanceDate <= endDate);

            //ensure perf has valid content types 
            //this is not needed if the API returns correct dataset
            //Need Tash to resolve this on the API side then we can take this expensive call out.    WZ - 01/31/2014
            foundPerformance = foundPerformance.Where(p => p.WebContent.Any(wc => contentTypeIDs.Contains(wc.ContentTypeID)));
            return foundPerformance.OrderBy(perf => perf.PerformanceDate).ToList();
        }
        /// <summary>
        /// Get perfs from now to X days in the future and sort by perf date time
        /// </summary>
        /// <param name="siteTheme"></param>
        /// <param name="daysInTheFuture"></param>
        /// <returns></returns>
        public static IEnumerable<Performance> GetPerformancesBySiteThemeKeywords(Enums.SiteTheme siteTheme, int daysInTheFuture)
        {
            var startDate = DateTime.Today;
            var endDate = startDate.AddDays(daysInTheFuture);
            
            var performances = GetPerformancesBySiteThemeKeywords(siteTheme,startDate,endDate);
            
            return performances.Where(perf => perf.PerformanceDate >= DateTime.Now); ;
        }

        /// <summary>
        /// Get perfs from now to X days in the future and sort by perf date time
        /// </summary>
        /// <param name="siteTheme"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static IEnumerable<Performance> GetPerformancesBySiteThemeKeywords(Enums.SiteTheme siteTheme, DateTime startDate, DateTime endDate)
        {
            var keywords = SiteThemeHelper.GetSiteThemeKeywords(siteTheme);
            var perfCriteria = new PerformancesCriteria
            {
                StartDate = startDate,
                EndDate = endDate,
                Keywords = keywords,
            };

            var foundPerformance = Performances.GetPerformances(perfCriteria)
                .Where(perf => perf.PerformanceDate >= startDate && perf.PerformanceDate <= endDate);

            return foundPerformance.OrderBy(perf => perf.PerformanceDate).ToList();
        }

        /// <summary>
        /// Performance is a summer supporters park show by look up venue id
        /// </summary>
        /// <param name="performance"></param>
        /// <returns></returns>
        public static bool IsSummerParkPerformance(Performance performance)
        {
            var summerParkVenueIds = Config.GetList<int>("SummerParkVenueIds", "8");
            return performance != null && summerParkVenueIds.Contains(performance.VenueId);
        }

        /// <summary>
        /// Get Web full Price Range for performance
        /// </summary>
        /// <param name="currentPerformance"></param>
        /// <returns></returns>
        public static string GetWebFullPriceRange(Performance currentPerformance)
        {
            var webFullPriceTypeID = Config.GetValue(Custom.CoreFeatureSet.Common.PublicAppSettings.WebFullPriceTypeID, 119);

            var performancePricings = ((PublicPerformance)currentPerformance).WebStandardPricing.Sections
               .Where(sec => sec.PriceTypeId == webFullPriceTypeID)
               .Select(sec => sec.Price)
               .ToList();

            if (performancePricings.Any() == false)
                return string.Empty;

            var minPrice = performancePricings.Min();
            var maxPrice = performancePricings.Max();

            if (minPrice == maxPrice)
                return string.Format("Ticket Price: {0:c}", minPrice);

            return System.String.Format("Ticket Price: {0:c} - {1:c}", minPrice, maxPrice);
        }

        public static string GetWebFullPriceRange(Production production)
        {
            if (production == null)
                return string.Empty;

            var webFullPriceTypeID = Config.GetValue(CoreFeatureSet.Common.PublicAppSettings.WebFullPriceTypeID, 119);
            var performancePricings = production.Performances.OfType<CoreFeatureSet.PerformanceObjects.PublicPerformance>()
                .Select(perf => perf.WebStandardPricing)
                .SelectMany(pricing => pricing.Sections)
                .Where(sec => sec.PriceTypeId == webFullPriceTypeID)
                .Select(sec => sec.Price)
                .ToList();

            if (performancePricings.Any() == false)
                return string.Empty;

            var minPrice = performancePricings.Min();
            var maxPrice = performancePricings.Max();

            if (minPrice == maxPrice)
                return string.Format("Ticket Price: {0:c}", minPrice);

            return System.String.Format("Ticket Price: {0:c} - {1:c}", minPrice, maxPrice);
        }

        public static string GetMemberPriceRange(Production production)
        {
            if (production == null)
                return string.Empty;

            var memberPriceTypeID = Config.GetValue(CoreFeatureSet.Common.PublicAppSettings.MemberPriceTypeID, 33);
            var performancePricings = production.Performances.OfType<CoreFeatureSet.PerformanceObjects.PublicPerformance>()
                .Select(perf => perf.MembershipPricing)
                .SelectMany(pricing => pricing.Sections)
                .Where(sec => sec.PriceTypeId == memberPriceTypeID)
                .Select(sec => sec.Price)
                .ToList();

            if (performancePricings.Any() == false)
                return string.Empty;

            var minPrice = performancePricings.Min();
            var maxPrice = performancePricings.Max();

            if (minPrice == maxPrice)
                return string.Format("Member Price: {0:c}", minPrice);

            return System.String.Format("Member Price: {0:c} - {1:c}", minPrice, maxPrice);
        }
        public static string GetDoorPriceRange(Production production)
        {
            
            if (production == null)
                return string.Empty;

            var doorPriceTypeID = Config.GetValue(CoreFeatureSet.Common.PublicAppSettings.DoorPriceTypeID, 29);
           
            var sqlParameters = new Dictionary<string, object>();
            sqlParameters.Add("@prod_season_no", production.ProductionSeasonNumber);

            DataSet ptSet = TessSession.GetSession().ExecuteLocalProcedure("GetPriceTypesSP", sqlParameters);
            if (ptSet != null && (ptSet.Tables.Count != 0 && ptSet.Tables[0].Rows.Count != 0))
            {
                DataTable table = ptSet.Tables[0];
                DataRow[] rows = table.Select("price_type=" + doorPriceTypeID);
                int minPrice = Convert.ToInt32(rows[0]["min_price"]);
                int maxPrice = Convert.ToInt32(rows[0]["max_price"]);
                if (minPrice == maxPrice)
                    return string.Format("Door Price: {0:c}", minPrice);
                return System.String.Format("Door Price: {0:c} - {1:c}", minPrice, maxPrice);
            }
            return string.Empty;
        }

        public static string GetFlexPackageRequirementDescription(FlexPackage currentPackage)
        {
            /* Turn back on for ranges
             * if (currentPackage.MinPerformances.HasValue && currentPackage.MaxPerformances.HasValue)
            {
                if (currentPackage.MinPerformances.Value == currentPackage.MaxPerformances.Value)
                {
                    return string.Format("{0} required", currentPackage.MinPerformances.Value);
                }

                return string.Format("{0} - {1} required", currentPackage.MinPerformances.Value, currentPackage.MaxPerformances.Value);
            }
             */
            if (currentPackage.MinPerformances.HasValue)
            {
                return string.Format("{0} minimum", currentPackage.MinPerformances.Value);
            }
            if (currentPackage.MaxPerformances.HasValue)
            {
                return string.Format("{0} maximum", currentPackage.MaxPerformances.Value);
            }
            return string.Empty;
        }
    }

}
