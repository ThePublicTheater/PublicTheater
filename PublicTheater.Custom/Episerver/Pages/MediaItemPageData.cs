using Adage.Tessitura;
using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Core;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Web;
using EPiServer;
using PublicTheater.Custom.Episerver.BaseClasses;
using PublicTheater.Custom.Episerver.Properties;

namespace PublicTheater.Custom.Episerver.Pages
{
    [ContentType(GUID = "84c55111-5173-468a-aadf-4164ad33bd7e", DisplayName = "[Public Theater] Media Item", GroupName = Constants.ContentGroupNames.MediaGroupName)]
    public class MediaItemPageData : PublicBasePageData
    {
        [BackingType(typeof(PropertyNumber))]
        [EditorDescriptor(EditorDescriptorType = typeof(EnumDescriptionEditorDescriptor<Enums.MediaType>))]
        [Display(GroupName = SystemTabNames.Content, Name = "Media Type")]
        public virtual Enums.MediaType MediaType { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Media Title")]
        public virtual string MediaTitle { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Media Subtitle")]
        public virtual string MediaSubtitle { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Media Description")]
        public virtual XhtmlString MediaDescription { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Media Url")]
        public virtual Url MediaURL { get; set; }

        public virtual string CoverImage
        {
            get
            {
                if (MediaURL != null && MediaType == Enums.MediaType.image)
                    return MediaURL.ToString();

                if (MediaURL != null && MediaType == Enums.MediaType.youtube)
                {
                    var ub = new UrlBuilder(MediaURL);
                    if (ub.QueryCollection["v"] != null)
                    {
                        return Utility.Utility.GetYoutubeThumbnail(ub.QueryCollection["v"], Utility.Utility.YoutubeThumbnailQuality.HighQuality);
                    }
                }
                return string.Empty;
            }
        }

        private static string _watchListenPageUrl;
        public override string FriendlyURL
        {
            get
            {
                if (String.IsNullOrEmpty(_watchListenPageUrl))
                {
                    var wlPageId = Config.GetValue("WatchAndListenPageId", 19);
                    _watchListenPageUrl = Utility.Utility.GetFriendlyUrl(new PageReference(wlPageId));
                }
                return String.Format("{0}#play/{1}", _watchListenPageUrl, PageLink.ID);
            }
        }
    }
}