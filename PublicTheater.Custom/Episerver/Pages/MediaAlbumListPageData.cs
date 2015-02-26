using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.IO;
using EPiServer.Web;
using EPiServer;
using PublicTheater.Custom.Episerver.BaseClasses;

namespace PublicTheater.Custom.Episerver.Pages
{
    [ContentType(GUID = "e267c4f2-de7a-4a6d-adfe-4651fa861771", DisplayName = "[Public Theater] Media Album List")]
    public class MediaAlbumListPageData : PublicBasePageData
    {
        [Display(GroupName = SystemTabNames.Content, Name = "MainBody")]
        public virtual XhtmlString MainBody { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Media Album List", Order = 11)]
        public virtual ContentArea AlbumList { get; set; }

        private List<MediaAlbumPageData> _currentAlbumList;
        public virtual List<MediaAlbumPageData> CurrentAlbumList
        {
            get
            {
                if (_currentAlbumList == null)
                {
                    if (AlbumList != null && AlbumList.Contents.Any())
                    {
                        _currentAlbumList = AlbumList.Contents.OfType<MediaAlbumPageData>().ToList();
                    }
                    else
                    {
                        _currentAlbumList = new List<MediaAlbumPageData>();
                    }
                }
                return _currentAlbumList;
            }
        }

        private string _coverImage;
        public string CoverImage
        {
            get
            {
                if (_coverImage == null)
                {
                    _coverImage = string.Empty;
                    if (CurrentAlbumList.Any())
                    {
                        _coverImage = CurrentAlbumList.First().CoverImage;
                    }
                    else if (ChildAlbumList.Any())
                    {
                        _coverImage = ChildAlbumList.First().CoverImage;
                    }
                }
                return _coverImage;
            }
        }

        public string PreviewText
        {
            get
            {
                return CurrentAlbumList.Count > 0 ? string.Format("{0} ({1} Albums)", CalculatedHeading, CurrentAlbumList.Count) : CalculatedHeading;
            }
        }

        private List<MediaAlbumListPageData> _childAlbumList;
        public List<MediaAlbumListPageData> ChildAlbumList
        {
            get
            {
                return _childAlbumList ?? (_childAlbumList = DataFactory.Instance.GetChildren<MediaAlbumListPageData>(PageLink).ToList());
            }
        }
    }
}