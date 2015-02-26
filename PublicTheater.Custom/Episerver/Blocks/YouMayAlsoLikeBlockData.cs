using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using PublicTheater.Custom.Episerver.Properties;

namespace PublicTheater.Custom.Episerver.Blocks
{
    [ContentType(GUID = "bf0db6bc-af0a-4213-84aa-bcd5bbdd5850", DisplayName = "You May Also Like", GroupName = Constants.ContentGroupNames.UniversalBlocks)]
    public class YouMayAlsoLikeBlockData : BaseClasses.PublicBaseBlockData
    {

    }

}
