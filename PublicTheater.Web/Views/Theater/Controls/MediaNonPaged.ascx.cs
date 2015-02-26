using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Adage.Theater.RelationshipManager;
using Adage.Theater.RelationshipManager.NameValueList;
using System.Web.UI.HtmlControls;

namespace PublicTheater.Web.Views.Theater.Controls
{
    public partial class MediaNonPaged : EPiServer.UserControlBase
    {
        public Guid PageGuid { get; set; }


        public int MaxPageItems
        {
            get
            {
                if (ViewState["MaxPageItems"] == null)
                    ViewState["MaxPageItems"] = 8;

                return (int)ViewState["MaxPageItems"];
            }
            set
            {
                ViewState["MaxPageItems"] = value;
            }
        }

        public int CurrentPageMedia
        {
            get
            {
                if (ViewState["CurrentPageMedia"] == null)
                    ViewState["CurrentPageMedia"] = 0;

                return (int)ViewState["CurrentPageMedia"];
            }
            set
            {
                ViewState["CurrentPageMedia"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        internal bool PopulateMedia()
        {
            var medias = GetEPiMedia();
            if (medias.Count == 0) return false;

            //sort by created date desc
            medias.Sort((x, y) => x.SortIndex.CompareTo(y.SortIndex));

            mediaRepeater.ItemDataBound += new RepeaterItemEventHandler(MediaRepeater_ItemDataBound);

            mediaRepeater.DataSource = medias.Take(MaxPageItems).ToList();
            mediaRepeater.DataBind();

            return true;
        }

        private List<MediaRelationship> GetEPiMedia()
        {
            return MediaRelationship.GetMediaRelationshipByPageGuid(PageGuid);
        }

        void MediaRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
                return;

            MediaRelationship relationship = (MediaRelationship)e.Item.DataItem;

            if (relationship == null) return;

            EPiMedia media = relationship.EPiMedia;

            HtmlGenericControl mediaThumbnailItem = (HtmlGenericControl)e.Item.FindControl("mediaThumbnailItem");
            if (mediaThumbnailItem != null)
            {
                mediaThumbnailItem.Attributes.Add("class", GetListItemCSSClassByMedia(media));
            }

            HyperLink thumbImg = (HyperLink)e.Item.FindControl("thumbImage");
            if (thumbImg != null)
            {
                if (string.IsNullOrEmpty(media.EPiMediaThumbPath))
                    thumbImg.ImageUrl = media.EPiMediaPath;
                else
                    thumbImg.ImageUrl = media.EPiMediaThumbPath;

                thumbImg.CssClass = GetLinkCSSClassByMedia(media);
                thumbImg.Attributes.Add("title", media.EPiMediaTitle);
                thumbImg.Attributes.Add("desc", media.EPiMediaDescription);
                thumbImg.NavigateUrl = media.EPiMediaPath;
            }

            Literal mediaDate = (Literal)e.Item.FindControl("date");
            if (mediaDate != null)
                mediaDate.Text = media.CreatedOn.ToShortDateString();

            Literal mediaTitle = (Literal)e.Item.FindControl("title");
            if (mediaTitle != null && !String.IsNullOrEmpty(media.EPiMediaTitle))
                mediaTitle.Text = String.Concat(media.EPiMediaTitle, " : ");

            Literal mediaDescription = (Literal)e.Item.FindControl("description");
            if (mediaDescription != null)
                mediaDescription.Text = media.EpiMediaDescriptionAbbr;

            //thumbImg.NavigateUrl = String.Format("{0}watch-listen/?pageid={1}&mediaguid={2}", Settings.Instance.SiteUrl.ToString(), CurrentPage.PageLink.ID, media.EPiMediaGuid);            

        }

        string GetLinkCSSClassByMedia(EPiMedia media)
        {

            if (media.MediaTypeID == MediaTypeNameValueList.Audio
                || media.MediaTypeID == MediaTypeNameValueList.Video)
                return "mediaLinks";

            return "photoLinks";

        }

        string GetListItemCSSClassByMedia(EPiMedia media)
        {
            if (media.MediaTypeID == MediaTypeNameValueList.Audio)
                return "audio";

            if (media.MediaTypeID == MediaTypeNameValueList.Video)
                return "video";

            return "photo";
        }
    }
}