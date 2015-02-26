using System;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using Adage.Tessitura;
using Adage.Tessitura.CartObjects;
using Adage.Tessitura.Common;
using Adage.Tessitura.Constants;
using Adage.Tessitura.PerformanceObjects;

namespace PublicTheater.Custom.CoreFeatureSet.PerformanceObjects
{
    /// <summary>
    /// Holds details about a specific Performance
    /// </summary>
    [Serializable, CacheClass]
    [DataContract]
    public class PublicPerformance : Performance
    {
        /// <summary>
        /// Performance's pricing information.
        /// </summary>
        [System.Web.Script.Serialization.ScriptIgnore]
        public virtual PerformancePricing MembershipPricing
        {
            get
            {
                var membershipMos = Helper.MembershipHelper.MembershipMOS;
                var promo = Repository.Get<TessSession>().PromotionCode;
                return PerformancePricing.GetPerformancePricing(PerformanceId, (short)membershipMos, promo);
            }
        }

        /// <summary>
        /// Performance's pricing information.
        /// </summary>
        [System.Web.Script.Serialization.ScriptIgnore]
        public virtual PerformancePricing WebStandardPricing
        {
            get
            {
                var defaultMos = TessSession.DEFAULT_MODE_OF_SALE;
                var promo = Repository.Get<TessSession>().PromotionCode;
                return PerformancePricing.GetPerformancePricing(PerformanceId, defaultMos, promo);
            }
        }

        protected override void SetupPerformancePricing(DataSet performanceDetailsDataSet)
        {
            base.SetupPerformancePricing(performanceDetailsDataSet);
        }
    }
}
