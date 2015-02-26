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
using System.Collections.Generic;


namespace PublicTheater.Custom.Episerver.Blocks
{
  
    [ContentType(GUID = "84c55111-5173-468a-aadf-4164ad32b07e", DisplayName = "Image Album Block", GroupName = Constants.ContentGroupNames.MediaGroupName)]
    public class ImageAlbumBlockData : BaseClasses.PublicBaseResponsiveBlockData
    {

        [Display(GroupName = SystemTabNames.Content, Name = "Listed On Watch And Listen")]
        public virtual bool ListedOnWatchAndListen { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Media Title")]
        public virtual string MediaTitle { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Cover Image")]
        public virtual Url PosterImage { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Show Captions")]
        public virtual bool ShowCaptions { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Auto Rotate")]
        public virtual bool AutoRotate { get; set; }
        
        [Display(GroupName = SystemTabNames.Content, Name = "Hide Dots")]
        public virtual bool HideDots { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Images")]
        [BackingType(typeof(ImageAlbumItems))]
        public virtual string ImageItems { get; set; }

        public virtual List<ImageAlbumItem> GetImageLinks()
        {
            var imageLinks = Property["ImageItems"] as ImageAlbumItems;
            return imageLinks != null ? imageLinks.ImageAlbumItemList : new List<ImageAlbumItem>();
        }
        
        
        public virtual string CoverImage
        {
            get
            {
                try
                {
                    if (null != CoverImage)
                        return CoverImage;

                    if (GetImageLinks()[0].ImageUrl != null)
                        return GetImageLinks()[0].ImageUrl;
                }
                catch (Exception) { }

                return PosterImage.ToString();
            }
        }

        private static string _watchListenPageUrl;
        public string FriendlyURL
        {
            get
            {
                if (String.IsNullOrEmpty(_watchListenPageUrl))
                {
                    var wlPageId = Config.GetValue("WatchAndListenPageId", 19);
                    _watchListenPageUrl = Utility.Utility.GetFriendlyUrl(new PageReference(wlPageId));
                }

                                
                return String.Format("{0}#play/{1}", _watchListenPageUrl, (this as IContent).ContentLink.ID);
            }
        }
    }
}