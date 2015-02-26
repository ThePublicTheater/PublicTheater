using System;
using PublicTheater.Custom.CoreFeatureSet.Memberships;
using TheaterTemplate.Shared.Common;

namespace PublicTheater.Custom.CoreFeatureSet.Common
{
    public class PublicWebCacheManager : TheaterSharedWebCacheManager
    {
        protected override Adage.Tessitura.Common.WebCacheManager.CacheType GetCacheType(Type currentType)
        {
            var finalCacheType = base.GetCacheType(currentType);

            if (currentType == typeof(MembershipBenefits))
                return CacheType.Context;

            if (currentType == typeof(BenefitRestrictions))
                return CacheType.Context;

            if (currentType == typeof(Memberships.MyBenefits.MyBenefitSummary))
                return CacheType.Context;

            return finalCacheType;
        }
    }
}
