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
    [ContentType(GUID = "a310a08b-3446-40d9-976c-1a463742e1a9", DisplayName = "[Public Theater] Media Album")]
    public class MediaAlbumPageData : PublicBasePageData
    {
        [Display(GroupName = SystemTabNames.Content, Name = "MainBody")]
        public virtual XhtmlString MainBody { get; set; }

        public string CoverImage
        {
            get { return AlbumImages.Any() ? AlbumImages.First().MediaURL.ToString() : string.Empty; }
        }

        private List<MediaItemPageData> _albumImages;
        public List<MediaItemPageData> AlbumImages
        {
            get
            {
                return _albumImages ?? (_albumImages = DataFactory.Instance.GetChildren<MediaItemPageData>(PageLink).Where(p=>p.VisibleInMenu).ToList());
            }
        }

        public virtual string PreviewText
        {
            get
            {
                return AlbumImages.Count > 0 ? string.Format("{0} ({1} Photos)", CalculatedHeading, AlbumImages.Count) : CalculatedHeading;
            }
        }
    }
}