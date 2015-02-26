using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Adage.Tessitura;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Core;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Web;
using EPiServer;
using PublicTheater.Custom.CoreFeatureSet.Helper;
using PublicTheater.Custom.Episerver.Properties;
using PublicTheater.Custom.Episerver.Utility;
using TheaterTemplate.Shared.EPiServerPageTypes;
using Adage.Theater.RelationshipManager.PlugIn;
using TheaterTemplate.Shared.EpiServerConfig;
using System;


namespace PublicTheater.Custom.Episerver.Pages
{
    [ContentType(GUID = "c01847cc-06f3-41b5-bc23-87f51d9d911a", DisplayName = "[Public Theater] Archived Play Details", GroupName = Constants.ContentGroupNames.SeasonGroupName)]
    public class ArchivedPlayDetailPageData : PlayDetailPageData
    {
      
    }
}
