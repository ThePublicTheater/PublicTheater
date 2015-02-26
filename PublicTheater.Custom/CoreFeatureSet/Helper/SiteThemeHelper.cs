using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Adage.Tessitura;
using PublicTheater.Custom.CoreFeatureSet.Common;
using PublicTheater.Custom.CoreFeatureSet.EpiServerConfig;
using PublicTheater.Custom.Episerver;
using TheaterTemplate.Shared.EpiServerConfig;

namespace PublicTheater.Custom.CoreFeatureSet.Helper
{
    public static class SiteThemeHelper
    {
        /// <summary>
        /// Get Site theme for a performance
        /// </summary>
        /// <param name="performance"></param>
        /// <returns></returns>
        public static Enums.SiteTheme GetSiteTheme(Performance performance)
        {
            foreach (var theme in Enum.GetValues(typeof(Enums.SiteTheme)))
            {
                if (GetSiteThemePerfTypeIds((Enums.SiteTheme)theme).Contains(performance.PerformanceTypeId))
                {
                    return (Enums.SiteTheme)theme;
                }
            }
            return Enums.SiteTheme.Default;
        }

        /// <summary>
        /// Get site theme perf type ids from app config
        /// </summary>
        /// <param name="siteTheme"></param>
        /// <returns></returns>
        public static List<int> GetSiteThemePerfTypeIds(Enums.SiteTheme siteTheme)
        {
            return Config.GetList<int>(string.Concat("PerfType_", siteTheme), string.Empty);
        }

        /// <summary>
        /// Get site theme perf type ids from app config
        /// </summary>
        /// <param name="siteTheme"></param>
        /// <returns></returns>
        public static List<int> GetSiteThemeContentTypeIds(Enums.SiteTheme siteTheme)
        {
            return Config.GetList<int>(string.Concat("ContentType_", siteTheme), string.Empty);
        }

        /// <summary>
        /// keywords
        /// </summary>
        /// <param name="siteTheme"></param>
        /// <returns></returns>
        public static string GetSiteThemeKeywords(Enums.SiteTheme siteTheme)
        {
            return Config.GetValue(string.Concat("Keywords_", siteTheme), string.Empty);
        }
    }
}
