using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Adage.Tessitura.PerformanceObjects;

namespace PublicTheater.Custom.Syos
{
    public class PublicLevelInformation : Adage.HtmlSyos.Code.LevelInformation
    {
        protected override void FillLevelInformation(Adage.HtmlSyos.Code.SYOSRootData rootData, Adage.HtmlSyos.Code.SYOSLevelData levelData, bool _showAll, Adage.Tessitura.PerformanceSeating performanceSeating, Adage.Tessitura.PerformanceObjects.BasePricing pricing)
        {
            foreach (SeatInfo seatInfo in performanceSeating)
            {
                int zoneId = -1;
                if (int.TryParse(seatInfo.ZoneId, out zoneId))
                {
                    var zonePricing = pricing.GetBySectionId(zoneId).FirstOrDefault();
                    if (zonePricing!=null)
                    {
                        seatInfo.SectionDescription = zonePricing.Description;
                    }   
                }
            }
            


            base.FillLevelInformation(rootData, levelData, _showAll, performanceSeating, pricing);

       
        }

       
    }
}
