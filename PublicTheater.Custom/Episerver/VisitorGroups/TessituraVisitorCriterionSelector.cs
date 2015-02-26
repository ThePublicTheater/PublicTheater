using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Adage.Tessitura;
using System.Data;
using Adage.Tessitura.CartObjects;

namespace PublicTheater.Custom.Episerver.VisitorGroups
{
    public class PublicTessituraVisitorCriterionSelector : Adage.TessituraEpiServer.TessituraVisitorCriterionSelector
    {
        public enum PublicTheaterUserFilterTypes
        {
            CachedStoredProc = 1000,
            CartContainsFlexPackage = 1001
        }

        protected override bool? PassesSessionFilter(FilterCriteria keyValuePair, HttpContextBase httpContext)
        {
            if ((int)keyValuePair.FilterType == (int)PublicTheaterUserFilterTypes.CachedStoredProc)
                return CheckCacheStoredProc(keyValuePair, httpContext);

            if ((int)keyValuePair.FilterType == (int)PublicTheaterUserFilterTypes.CartContainsFlexPackage)
                return CheckCartContainsFlexPackage(keyValuePair, httpContext);
            
            return base.PassesSessionFilter(keyValuePair, httpContext);
        }

        #region CheckCartContainsFlexPackage

        private bool? CheckCartContainsFlexPackage(FilterCriteria keyValuePair, HttpContextBase httpContext)
        {
            return Cart.GetCart().CartLineItems.OfType<FlexPackageLineItem>().Any();
        }

        #endregion

        #region CheckCacheStoredProc

        string CACHE_KEY = "STORED_PROC_RESULTS_{0}";
        string CACHE_TIMEOUT = "STORED_PROC_RESULTS_TIMEOUT_{0}";

        private bool? CheckCacheStoredProc(FilterCriteria keyValuePair, HttpContextBase httpContext)
        {
            int storedProcId = 0;
            if (int.TryParse(keyValuePair.FilterCondition, out storedProcId) == false)
                throw new ApplicationException("Could not parse as the stored proc ID:" + keyValuePair.FilterCondition);

            string cacheKey = string.Format(CACHE_KEY, storedProcId);
            if (httpContext != null)
            {
                bool? cacheValue = (bool?)httpContext.Cache[cacheKey];
                if (cacheValue.HasValue)
                    return cacheValue;
            }

            bool? storedProcValue = getStoredProcValue(storedProcId);

            if (httpContext != null)
            {
                string cacheTimeoutKey = string.Format(CACHE_TIMEOUT, storedProcId);
                int cacheTimeout = Config.GetValue(cacheTimeoutKey, 60);

                httpContext.Cache.Add(cacheKey, storedProcValue, null,
                    DateTime.Now.AddSeconds(cacheTimeout), System.Web.Caching.Cache.NoSlidingExpiration,
                    System.Web.Caching.CacheItemPriority.Normal, null);
            }

            return storedProcValue;
        }

        private bool? getStoredProcValue(int storedProcId)
        {
            DataSet currentResults = TessituraAPI.Get().ExecuteLocalProcedure(TessSession.GetSessionKey(), storedProcId, string.Empty);
            if (currentResults != null && currentResults.Tables.Count > 0)
            {
                // get the value from the results
                if ( currentResults.Tables[0].Rows.Count > 0 && currentResults.Tables[0].Rows[0].IsNull(0) == false)
                {
                    string currentResultValue = currentResults.Tables[0].Rows[0][0].ToString().ToLower();
                    return TRUE_VALUES.Contains(currentResultValue);
                }
            }

            return false;
        }

        string[] TRUE_VALUES = new string[] { "y", "1", "-1", "true", "t" };

        #endregion
    }
}
