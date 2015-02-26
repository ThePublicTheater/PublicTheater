using System;
using System.Web;
using Adage.Tessitura;
using Adage.Tessitura.CartObjects;
using Adage.Tessitura.Common;
using PublicTheater.Custom.CoreFeatureSet.CartObjects;
using PublicTheater.Custom.CoreFeatureSet.Common;
using PublicTheater.Custom.CoreFeatureSet.PerformanceObjects;
using PublicTheater.Custom.CoreFeatureSet.UserObjects;
using TheaterTemplate.Shared.Common;
using TheaterTemplate.Shared.EpiServerConfig;
using Adage.Tessitura.PerformanceObjects;
using PublicTheater.Custom.CoreFeatureSet.EpiServerConfig;
using Adage.HtmlSyos.EPiServer.Code;
using PublicTheater.Custom.Syos;

namespace PublicTheater.Custom.CoreFeatureSet.Common
{

    /// <summary>
    /// Public object factory - Allows for specific class overrides
    /// </summary>
    public class PublicObjectFactory : TheaterTemplate.Shared.Common.TheaterSharedObjectFactory
    {
        protected override WebCacheManager GetWebCacheManager()
        {
            return new PublicWebCacheManager();
        }
        /// <summary>
        /// Override fore create instance - Allows for Public Specific Classes
        /// </summary>
        public override object CreateInstance(Type baseType, Type givenType, string loadFrom, object loadFromArgument)
        {
            if (baseType == typeof(Adage.Tessitura.Config))
                return new PublicConfig();

            if (baseType == typeof(Adage.Tessitura.WebRefs.TessituraApiMonitor))
                return new PublicApiMonitor();

            if (baseType == typeof(TheaterSharedGiftCertificateEmailObject))
                return new PublicGiftCertificateEmailObject();

            if (baseType == typeof(TheaterSharedGiftCertificateInfo))
                return new PublicPackageVoucher();

            if (baseType == typeof(Adage.Tessitura.User))
                return new PublicUser();

            if (baseType == typeof (FlexPackage))
                return new PublicFlexPackage();

            if (baseType == typeof(TheaterTemplate.Shared.EpiServerConfig.PlayDetail))
                return new PublicPlayDetail();

            if (baseType == typeof(CartContentConfig))
                return new PublicCartContentConfig();

            if (baseType == typeof(Adage.Tessitura.TessSession))
                return new PublicTessSession();

            if (baseType == typeof(Adage.HtmlSyos.Code.SYOSRootData))
                return new PublicSYOSRootData();

            if(baseType == typeof(TheaterTemplate.Shared.Common.TheaterSharedEmailObject))
                return new PublicEmailObject();

            if (baseType == typeof(Adage.Tessitura.Cart))
                return new PublicCart();

            if (baseType == typeof(Memberships.MembershipBenefit))
                return new Memberships.MembershipBenefit();

            if (baseType == typeof(Memberships.MembershipBenefits))
                return new Memberships.MembershipBenefits();

            if (baseType == typeof(Memberships.BenefitRestriction))
                return new Memberships.BenefitRestriction();

            if (baseType == typeof(Memberships.BenefitRestrictions))
                return new Memberships.BenefitRestrictions();

            if (baseType == typeof(Adage.Tessitura.Performance))
                return new PerformanceObjects.PublicPerformance();

            if (baseType == typeof(Adage.Tessitura.Production))
                return new PerformanceObjects.PublicProduction();

            if (baseType == typeof(PerformancePricing))
                return new PerformanceObjects.PublicPerformancePricing();

            if (baseType == typeof(MembershipConfig))
                return new MembershipConfig();

            if (baseType == typeof(Ticket))
                return new PublicTicket();

            if (baseType == typeof(PriceType))
                return new PublicPriceType();

            if (baseType == typeof(Adage.HtmlSyos.Code.LevelInformation))
                return new PublicLevelInformation();

            if (baseType == typeof(Adage.TessituraEpiServer.TessituraVisitorCriterionSelector))
                return new PublicTheater.Custom.Episerver.VisitorGroups.PublicTessituraVisitorCriterionSelector();

            if (baseType == typeof(Memberships.MyBenefits.AvailableProductions))
                return new Memberships.MyBenefits.AvailableProductions();


            if (baseType == typeof(Memberships.MyBenefits.DTBenefitPriceType))
                return new Memberships.MyBenefits.DTBenefitPriceType();

            if (baseType == typeof(Memberships.MyBenefits.DTBenefitPriceTypes))
                return new Memberships.MyBenefits.DTBenefitPriceTypes();

            if (baseType == typeof(Memberships.MyBenefits.DTBenefitProduction))
                return new Memberships.MyBenefits.DTBenefitProduction();

            if (baseType == typeof(Memberships.MyBenefits.DTBenefitProductions))
                return new Memberships.MyBenefits.DTBenefitProductions();

            if (baseType == typeof(Memberships.MyBenefits.MembershipStatus))
                return new Memberships.MyBenefits.MembershipStatus();

            if (baseType == typeof(Memberships.MyBenefits.MyBenefitSummary))
                return new Memberships.MyBenefits.MyBenefitSummary();

            if (baseType == typeof(Memberships.MyBenefits.BenefitUsage))
                return new Memberships.MyBenefits.BenefitUsage();

            if (baseType == typeof(Memberships.MyBenefits.BenefitUsages))
                return new Memberships.MyBenefits.BenefitUsages();

            if (baseType == typeof(TicketHistoryConfig))
                return new PublicTicketHistoryConfig();

            if (baseType == typeof (TheaterSharedMOSHelper))
                return new PublicMOSHelper();

            if (baseType == typeof(PackageDetailConfig))
                return new PublicPackageDetailConfig();

            if (baseType == typeof(PackageListConfig))
                return new PublicPackageListConfig();

            if (baseType == typeof(FlexPackageLineItem))
                return new PublicFlexPackageLineItem();

            if (givenType == typeof (PackageRenameConfig))
                return new PackageRenameConfig();

            if (givenType == typeof(FlexPackage))
                return new PublicFlexPackage();
            
            return base.CreateInstance(baseType, givenType, loadFrom, loadFromArgument);
        }

    }
}
