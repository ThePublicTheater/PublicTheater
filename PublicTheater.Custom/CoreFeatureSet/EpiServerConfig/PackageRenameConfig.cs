using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Adage.Tessitura.PerformanceObjects;
using EPiServer.Core;
using TheaterTemplate.Shared.EpiServerConfig;
using TheaterTemplate.Shared.EpiServerProperties;

namespace PublicTheater.Custom.CoreFeatureSet.EpiServerConfig
{
    [Adage.Tessitura.Common.CacheClass]
    public class PackageRenameConfig : EpiServerBaseConfigFactory
    {
        protected Dictionary<int, string> PackageRenames { get; set; }

        public static PackageRenameConfig GetPackageRenameConfig(int configurationId)
        {
            return GetFilledConfig<PackageRenameConfig>(configurationId);
        }

        internal PackageRenameConfig()
        {
            PackageRenames = new Dictionary<int, string>();
        }

        protected override void FillFromLock(PageData configPage)
        {
            if (configPage.Property["ConfigProperty"] != null)
            {
                KeyValueList pairList = configPage.Property["ConfigProperty"] as KeyValueList;
                if (pairList != null)
                {
                    PackageRenames = GetDictionary<int, string>(pairList);
                }
            }

            Filled = true;
        }

        public string GetPackageRename(BasePackage package)
        {
            string packageName;
            if (PackageRenames.TryGetValue(package.PackageId, out packageName))
            {
                return packageName;
            }
            return package.Description;
        }
    }
}
