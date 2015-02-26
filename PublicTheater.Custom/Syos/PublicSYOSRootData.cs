using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Adage.HtmlSyos.EPiServer.Code;
using PublicTheater.Custom.Episerver.Properties;

namespace PublicTheater.Custom.Syos
{
    [XmlRoot("EPiServerSYOSRootData")]
    public class PublicSYOSRootData : EPiServerSYOSRootData
    {
        public List<ZoneColorConfig> ZoneColors { get; set; }

        protected override void FillSYOSRootData(EPiServer.Core.PageData SYOSPage, EPiServer.Core.PageReference configPageReference)
        {
            base.FillSYOSRootData(SYOSPage, configPageReference);

            var zoneColorsProp = (SYOSPage.Property["ZoneColors"] as ZoneColorList);
            if(zoneColorsProp==null)
            {
                ZoneColors=new List<ZoneColorConfig>();
            }
            else
            {
                ZoneColors = zoneColorsProp
                    .GetTypedCollection<ZoneColor>()
                    .Select(zc => zc.GetZoneColorConfig())
                    .ToList();    
            }
            
        }
    }
}
