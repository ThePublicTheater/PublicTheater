using System;
using System.Web.UI.WebControls;
using Adage.Theater.RelationshipManager;

namespace PublicTheater.Web.Views.Theater.Controls
{
    [Adage.Tessitura.Common.CacheClass]
    public partial class MediaPropertyManagerArsht : MediaPropertyManager
    {
        public Boolean IsFileUploader { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {


        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (IsFileUploader)
            {
                divThumbnail.Visible = false;
                string toFolder = Page.Request.QueryString["toFolder"];
                if (toFolder == string.Empty)
                    return;
                if (toFolder[toFolder.Length - 1] == '/')
                    toFolder = toFolder.Substring(0, toFolder.Length - 1);
                string[] parentFolderString = toFolder.Split('/');

                InitInputRelationshipsWithoutMedia(parentFolderString);
            }

        }
        public override void BindProperties(EPiMedia media, string[] parentFolderPath)
        {
            var MediaGuid = FindControl("HfImageGuid") as HiddenField;
            if (media == null)
                return;
            if (MediaGuid != null) MediaGuid.Value = media.EPiMediaGuid.ToString();

            mediaTitle.Text = media.GetPropertyData("EPiMediaTitle");
            mediaDescription.Text = media.GetPropertyData("EPiMediaDescription");
            mediaCredits.Text = media.GetPropertyData("EPiMediaCredit");
            mediaDate.Text = media.GetPropertyData("EPiMediaDate");
            //mediaThumbnail.Text = media.GetPropertyData("EPiMediaThumbnail");
            mediaThumbnailFile.FilePath = media.GetPropertyData("EPiMediaThumbnail");

            divThumbnail.Visible = media.MediaTypeID == Adage.Theater.RelationshipManager.NameValueList.MediaTypeNameValueList.Audio;

            InitInputRelationshipsForMedia(parentFolderPath);
        }

        protected override void SetRelationshipTypes(String mediaGuid)
        {
            var guid = new Guid(mediaGuid);

            ucPerformances.SetMediaProperties(guid, PageType.PerformanceID);
            ucArtists.SetMediaProperties(guid, PageType.ArtistID);
            ucEvents.SetMediaProperties(guid, PageType.EventsID);
            ucSubscriptions.SetMediaProperties(guid, PageType.SubscriptionID);
        }
        protected override void InitInputRelationshipsForMedia(string[] parentFolderPath)
        {
            var guid = new Guid(((HiddenField)FindControl("HfImageGuid")).Value);
            ucPerformances.SetMediaProperties(guid, PageType.PerformanceID);
            ucPerformances.SetDefaultPerformance(parentFolderPath[parentFolderPath.Length - 1]);

            ucSubscriptions.SetMediaProperties(guid, PageType.SubscriptionID);
            ucSubscriptions.SetDefaultSubscription(parentFolderPath[parentFolderPath.Length - 1]);

            ucEvents.SetMediaProperties(guid, PageType.EventsID);
            ucEvents.SetDefaultEvent(parentFolderPath[parentFolderPath.Length - 1]);

            String ParentFolder = parentFolderPath[parentFolderPath.Length - 1];
            ucArtists.PerformanceFolder = ParentFolder;
            ucArtists.SetMediaProperties(guid, PageType.ArtistID);
            ucArtists.LoadArtists(ParentFolder);
        }

        protected override void InitInputRelationshipsWithoutMedia(string[] parentFolderPath)
        {
            ucPerformances.SetDefaultPerformanceWithoutAttachedMedia(parentFolderPath[parentFolderPath.Length - 1]);
            
            ucEvents.SetDefaultEventWithoutAttachedMedia(parentFolderPath[parentFolderPath.Length - 1]);

            ucSubscriptions.SetDefaultSubscriptionWithoutAttachedMedia(parentFolderPath[parentFolderPath.Length - 1]);

            String ParentFolder = parentFolderPath[parentFolderPath.Length - 1];
            ucArtists.PerformanceFolder = ParentFolder;
            ucArtists.LoadArtistsWithoutBoundMedia(ParentFolder);
        }

        public override void UpdateProperties(
             HiddenField guid)
        {
            var mediaGuid = new Guid(guid.Value);
            var media = EPiMedia.GetEPiMediaByMediaGuid(mediaGuid);
            if (media == null)
            {
                media = EPiMedia.AddEPiMedia(mediaGuid);
            }

            string title = mediaTitle.Text;
            string description = mediaDescription.Text;
            string credit = mediaCredits.Text;
            string newDate = mediaDate.Text;
            string thumbnail = divThumbnail.Visible && mediaThumbnailFile.FilePath != string.Empty ? mediaThumbnailFile.FilePath : string.Empty;
            //string thumbnail = divThumbnail.Visible ? mediaThumbnail.Text : string.Empty;
            if (title != string.Empty) media.SetPropertyData("EPiMediaTitle", title);
            if (description != string.Empty) media.SetPropertyData("EPiMediaDescription", description);
            if (credit != string.Empty) media.SetPropertyData("EPiMediaCredit", credit);
            if (newDate != string.Empty)
            {
                media.SetPropertyData("EPiMediaDate", newDate);
                DateTime overrideDate;
                if (DateTime.TryParse(newDate, out overrideDate))
                    media.EPiMediaDate = overrideDate;
            }
            if (thumbnail != string.Empty) media.SetPropertyData("EPiMediaThumbnail", thumbnail);

            if (ucPerformances != null) ucPerformances.AddMediaRelationshipWithoutUpdatingPanel(media);
            if (ucArtists != null) ucArtists.AddMediaRelationshipWithoutUpdatingPanel(media);
            if (ucEvents != null) ucEvents.AddMediaRelationshipWithoutUpdatingPanel(media);
            if (ucSubscriptions != null) ucSubscriptions.AddMediaRelationshipWithoutUpdatingPanel(media);

            AdageTheaterRelationshipManagerEntities.Current.SaveChanges();
        }
    }
}