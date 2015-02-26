using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using PublicTheater.Custom.Episerver.BaseClasses;
using PublicTheater.Custom.Episerver.Properties;
using TheaterTemplate.Shared.EPiServerPageTypes;

namespace PublicTheater.Custom.Episerver.Pages
{

    [ContentType(GUID = "907edc09-50e9-4606-8d05-06dece2d8e11", DisplayName = "[Public Theater] Venue", GroupName=Constants.ContentGroupNames.SeatingGroupName)]
    public class VenuePageData : VenuePageType
    {
        #region site theme
        [BackingType(typeof(PropertyNumber))]
        [EditorDescriptor(EditorDescriptorType = typeof(EnumEditorDescriptor<Enums.SiteTheme>))]
        [Display(GroupName = SystemTabNames.Settings, Name = "Site Theme")]
        public virtual Enums.SiteTheme SiteTheme { get; set; }
        
        #endregion
    }
}
