using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Script.Serialization;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using EPiServer.Web;
using EPiServer;
using PublicTheater.Custom.Episerver.BaseClasses;
using PublicTheater.Custom.Episerver.Utility;
using PublicTheater.Custom.Models;
using PublicTheater.Custom.Episerver.Blocks;

namespace PublicTheater.Custom.Episerver.Pages
{
    [ContentType(GUID = "ef97e3a8-62d9-42be-acc4-6a81de517f71", DisplayName = "[Public Theater] Watch & Listen",
        GroupName = Constants.ContentGroupNames.MediaGroupName)]
    public class WatchListenPageData : PublicBasePageData
    {
        //config values are not in use

        //[Display(GroupName = SystemTabNames.Content, Name = "Audio Item Height")]
        //public virtual int AudioItemHeight { get; set; }

        //[Display(GroupName = SystemTabNames.Content, Name = "Image Item Height")]
        //public virtual int ImageItemHeight { get; set; }

        //[Display(GroupName = SystemTabNames.Content, Name = "Video Item Height")]
        //public virtual int VideoItemHeight { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "YouTube Channel URL")]
        public virtual Url YouTubeChannel { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Podcast URL")]
        public virtual Url PodcastUrl { get; set; }

        public string GetPlayerData()
        {
            return new JavaScriptSerializer().Serialize(CurrentWatchAndListenData);
        }

        private WatchAndListenModels.WatchAndListenData _currentWatchAndListenData;
        public WatchAndListenModels.WatchAndListenData CurrentWatchAndListenData
        {
            get
            {
                return _currentWatchAndListenData ?? (
                    _currentWatchAndListenData = new WatchAndListenModels.WatchAndListenData
                    {
                        Config = new WatchAndListenModels.PlayerConfig(),
                        MediaItems = GetMediaItemData()
                    });
            }
        }

        public WatchAndListenModels.PlayerConfig GetPlayerConfig()
        {
            return new WatchAndListenModels.PlayerConfig();
        }

        public List<WatchAndListenModels.MediaItem> GetMediaItemData()
        {
            var mediaItemList = new List<WatchAndListenModels.MediaItem>();
            var mediaItems = this.PageLink.GetChildrenByType<MediaItemPageData>(true);
            foreach (var mediaItemPage in mediaItems)
            {
                if(mediaItemPage.VisibleInMenu == false)
                {
                    // if not visible, don't list it on the grid
                    continue;
                }

                var mediaItem = new WatchAndListenModels.MediaItem
                {
                    MediaType = mediaItemPage.MediaType.ToString(),
                    MediaItemTitle = mediaItemPage.MediaTitle ?? string.Empty,
                    MediaItemSubTitle = mediaItemPage.MediaSubtitle ?? string.Empty,
                    MediaDescriptionHTML = mediaItemPage.MediaDescription == null ? string.Empty : mediaItemPage.MediaDescription.ToHtmlString(),
                    MediaURL = mediaItemPage.MediaURL == null ? string.Empty : mediaItemPage.MediaURL.ToString(),
                    MediaId = mediaItemPage.ContentLink.ID,
                    Category = String.Join(" ", mediaItemPage.Category.Select(c => EPiServer.DataAbstraction.Category.Find(c).Name)),
                    VisibleOnGrid = mediaItemPage.VisibleInMenu,
                    WatchAndListenURL = mediaItemPage.FriendlyURL,
                    CreatedDateTime = mediaItemPage.Created
                };

                //if it is a image in a album, only the first image visible
                if (mediaItemPage.MediaType == Enums.MediaType.image && mediaItemPage.ParentLink != null)
                {
                    var parentAlbumPage = ServiceLocator.Current.GetInstance<IContentRepository>().Get<PageData>(mediaItemPage.ParentLink) as MediaAlbumPageData;
                    if (parentAlbumPage != null)
                    {
                        mediaItem.ParentMediaItemId = mediaItemPage.ParentLink.ID;

                        // Title of album should come from the Album page, not the images
                        mediaItem.MediaItemTitle = parentAlbumPage.Heading ?? mediaItem.MediaItemTitle;

                        //not visible if media item itself is not visible
                        //not visible if the album page is not visilbe
                        //not visible if there is already a thumbnail for this album visible
                        mediaItem.VisibleOnGrid = mediaItem.VisibleOnGrid
                            && parentAlbumPage.VisibleInMenu
                            && !mediaItemList.Any(m => m.VisibleOnGrid && m.ParentMediaItemId == mediaItemPage.ParentLink.ID);
                    }
                }
                mediaItemList.Add(mediaItem);
            }
            var contentTypeRepository = ServiceLocator.Current.GetInstance<IContentTypeRepository>();
            var contentRepository = ServiceLocator.Current.GetInstance<IContentRepository>();
            var contentModelUsage = ServiceLocator.Current.GetInstance<IContentModelUsage>();

            var mediaBlockType = contentTypeRepository.Load<MediaItemBlockData>();

            var mediaBlockUsages = contentModelUsage.ListContentOfContentType(mediaBlockType).
                Select<ContentUsage, MediaItemBlockData>((usage) => { 
                    try { return contentRepository.Get<MediaItemBlockData>(usage.ContentLink.CreateReferenceWithoutVersion()); } 
                    catch (Exception) { 
                        return null; } });

            var mediaBlockItems = mediaBlockUsages.Distinct().Where((item)=>{return item != null;});
                        
            foreach (var mediaItemBlock in mediaBlockItems)
            {
                try
                {
                    
                    var mediaItem = new WatchAndListenModels.MediaItem
                    {
                        MediaType = mediaItemBlock.MediaType.ToString(),
                        MediaItemTitle = mediaItemBlock.MediaTitle ?? string.Empty,
                        MediaItemSubTitle = mediaItemBlock.MediaSubtitle ?? string.Empty,
                        MediaDescriptionHTML = mediaItemBlock.MediaDescription == null ? string.Empty : mediaItemBlock.MediaDescription.ToHtmlString(),
                        MediaURL = mediaItemBlock.MediaURL == null ? string.Empty : mediaItemBlock.MediaURL.ToString(),
                        MediaId = (mediaItemBlock as IContent).ContentLink.ID,
                        Category = String.Join(" ", ((EPiServer.Core.ICategorizable)mediaItemBlock).Category.Select(c => EPiServer.DataAbstraction.Category.Find(c).Name)),
                        VisibleOnGrid = mediaItemBlock.WatchAndListenVisible,
                        CoverURL = mediaItemBlock.CoverImage,
                        WatchAndListenURL = mediaItemBlock.FriendlyURL,
                        CreatedDateTime = (DateTime)mediaItemBlock.GetType().GetProperty("Created").GetValue(mediaItemBlock, null)

                    };



                    mediaItemList.Add(mediaItem);
                }
                catch (Exception) { continue; }
            }

            var ImageAlbumBlockType = contentTypeRepository.Load<ImageAlbumBlockData>();

            var ImageAlbumBlockUsages = contentModelUsage.ListContentOfContentType(ImageAlbumBlockType).
                Select<ContentUsage, ImageAlbumBlockData>((usage) =>
                {
                    try { return contentRepository.Get<ImageAlbumBlockData>(usage.ContentLink.CreateReferenceWithoutVersion()); }
                    catch (Exception)
                    {
                        return null;
                    }
                });

            var ImageAlbumBlockItems = ImageAlbumBlockUsages.Distinct().Where((item) => { return item != null; });


            
            int pseudoId = int.MaxValue;
            foreach (var imageAlbumBlock in ImageAlbumBlockItems)
            {

                try
                {
                    bool first = true;
                    int i = 0;
                    foreach (var image in imageAlbumBlock.GetImageLinks())
                    {

                        
                        var mediaItem = new WatchAndListenModels.MediaItem
                        {

                            MediaType = "image",
                            MediaItemTitle = imageAlbumBlock.MediaTitle ?? string.Empty,
                            MediaItemSubTitle = image.Title ?? string.Empty,
                            MediaDescriptionHTML = image.Description == null ? string.Empty : image.Description.ToHtmlString(),
                            MediaURL = image.ImageUrl == null ? string.Empty : image.ImageUrl.ToString(),
                            MediaId = pseudoId--,
                            Category = String.Join(" ", ((EPiServer.Core.ICategorizable)imageAlbumBlock).Category.Select(c => EPiServer.DataAbstraction.Category.Find(c).Name)),
                            VisibleOnGrid = first && imageAlbumBlock.ListedOnWatchAndListen,
                            WatchAndListenURL = imageAlbumBlock.FriendlyURL,
                            ParentMediaItemId = (imageAlbumBlock as IContent).ContentLink.ID,
                            //ugly hack to make image albums stay in sequential order
                            CreatedDateTime = ((DateTime)imageAlbumBlock.GetType().GetProperty("Created").GetValue(imageAlbumBlock, null)).AddSeconds(-(i++))
                        };

                        first = false;

                        mediaItemList.Add(mediaItem);
                        
                    }
                }
                catch (Exception) { continue; }
            }
            // TODO: Rethink how sorting should really work.
            mediaItemList.Sort(new Comparison<WatchAndListenModels.MediaItem>((itemA, itemB)=> {return itemB.CreatedDateTime.CompareTo(itemA.CreatedDateTime);}));
            return mediaItemList;
        }

    }
}