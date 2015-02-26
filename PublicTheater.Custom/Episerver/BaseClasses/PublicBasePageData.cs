using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Web;
using Adage.EpiServer.Library.PageTypes.Templates;
using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.ServiceLocation;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Web;
using EPiServer.Web.Routing;
using PublicTheater.Custom.Episerver.Properties;

namespace PublicTheater.Custom.Episerver.BaseClasses
{

    /* BEST PRACTICE TIP
     * Always use your own base class for page types in your projects,
     * instead of having your page types inherit PageData directly.
     * That way you can easily extend all page types by modifying your base class. */

    /// <summary>
    /// Base class for all page types
    /// </summary>
    public abstract class PublicBasePageData : AdageBase
    {
        #region site theme

        [BackingType(typeof(PropertyNumber))]
        [EditorDescriptor(EditorDescriptorType = typeof(EnumEditorDescriptor<Enums.SiteTheme>))]
        [Display(GroupName = SystemTabNames.Settings, Name = "Site Theme")]
        public virtual Enums.SiteTheme SiteTheme { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Heading", Description = "Page Heading if different than page name")]
        public virtual string Heading { get; set; }

        [Display(GroupName = SystemTabNames.Settings, Name = "Enable Social Icons", Description = "Enable Social Icons")]
        public virtual bool EnableSocialIcons { get; set; }

        [Display(GroupName = SystemTabNames.Settings, Name = "Enable Mos Check", Description = "Enable MOS check for members and partners")]
        public virtual bool EnableMosCheck { get; set; }


        [Display(GroupName = SystemTabNames.Settings, Name = "Hero Images", Description = "Select hero image from pool")]
        [ClientEditor(
            ClientEditingClass = "epi-cms.widget.FileSelector",
            EditorConfiguration = "{ \"fileBrowserMode\": \"folder\" }"
        )]
        public virtual Url HeroImagePool { get; set; }

        private string _friendlyURL;
        /// <summary>
        /// Theme specific link url
        /// </summary>
        public virtual string FriendlyURL
        {
            get
            {
                return _friendlyURL ?? (_friendlyURL = Utility.Utility.GetFriendlyUrl(this.PageLink));
            }
        }

        public virtual string CalculatedHeading
        {
            get
            {
                if (String.IsNullOrEmpty(Heading))
                {
                    return PageName;
                }
                return Heading;
            }
        }
        #endregion
    }
}