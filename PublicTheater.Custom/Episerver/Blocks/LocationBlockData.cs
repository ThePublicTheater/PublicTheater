using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using PublicTheater.Custom.Episerver.Properties;

namespace PublicTheater.Custom.Episerver.Blocks
{
    [ContentType(GUID = "23f9527c-4ecb-4ab0-b7c6-95f53147ed61", DisplayName = "Location Block", GroupName = Constants.ContentGroupNames.UniversalBlocks)]
    public class LocationBlockData : BaseClasses.PublicBaseResponsiveBlockData
    {
        public virtual PageReference LocationPage { get; set; }

        public Pages.LocationPageData GetLocationPageData()
        {
            return LocationPage != null
                       ? DataFactory.Instance.GetPage(LocationPage) as Pages.LocationPageData
                       : null;
        }
    }

}
