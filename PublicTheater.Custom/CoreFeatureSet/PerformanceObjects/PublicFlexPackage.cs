using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Adage.Tessitura;
using PublicTheater.Custom.CoreFeatureSet.Common;
using PublicTheater.Custom.CoreFeatureSet.EpiServerConfig;

namespace PublicTheater.Custom.CoreFeatureSet.PerformanceObjects
{
    public class PublicFlexPackage : Adage.Tessitura.FlexPackage
    {
        public List<Performance> GetAvailablePerformances(int productionSeasonNumber)
        {
            var allPerformances = PerformanceGroups.SelectMany(perf => perf.Performances).Where(perf => perf.ProductionSeasonId == productionSeasonNumber);
            return allPerformances.Where(perf => perf.PerformancePricing.SectionsAvailable && perf.PerformancePricing.SectionsAvailableSeats).ToList();
        }

        public override void Fill(System.Data.DataRow packageRow)
        {
            base.Fill(packageRow);

            var configId = Adage.Tessitura.Config.GetValue(PublicAppSettings.PackageRenameConfigId, 0);
            Description = PackageRenameConfig.GetPackageRenameConfig(configId).GetPackageRename(this);
        }
    }
}
