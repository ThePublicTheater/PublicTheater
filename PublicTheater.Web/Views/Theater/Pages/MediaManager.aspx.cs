using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Services;
using Adage.Theater.RelationshipManager;
using EPiServer.Core;
using PublicTheater.Web.Views.Theater.Controls;

namespace PublicTheater.Web.Views.Theater.Pages
{
    public partial class MediaManager: EPiServer.TemplatePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["returnto"] == null) return;
            returnTo.NavigateUrl = Request.QueryString["returnto"];
            returnTo.Visible = true;
        }
        
        [WebMethod]
        public static string AddMedia(string guid, Dictionary<String, object> jsonData)
        {
            
            var mediaGuid = new Guid(guid);
            var media = EPiMedia.GetEPiMediaByMediaGuid(mediaGuid) ?? EPiMedia.AddEPiMedia(mediaGuid);

            var message = Adage.Tessitura.Repository.Get<MediaPropertyManager>().UpdateProperties(media, jsonData);

            return message;
        }

        [WebMethod]
        public static string Add(string guid, string mediaCredit, string mediaDate, string mediaTitle, string mediaDesc, string mediaThumb,
            string RelatedPageId, string artists)
        {
            var mediaGuid = new Guid(guid);
            if (String.IsNullOrEmpty(mediaDate))
                mediaDate = DateTime.Today.ToString(CultureInfo.InvariantCulture);

            DateTime mDate;
            if (!DateTime.TryParse(mediaDate, out mDate)) mDate = DateTime.Now;

            var media = EPiMedia.AddEPiMedia(mediaGuid, mediaCredit, mDate, mediaTitle, mediaDesc, mediaThumb);

            string[] relatedPageId = RelatedPageId.Split("|".ToCharArray(), StringSplitOptions.None);

            if (relatedPageId.Length < 2)
                return "Related pages are not correctly selected.";

            string performancePageId = relatedPageId[0];
            string artistPageId = relatedPageId[1];

            int perfPageId;
            if (int.TryParse(performancePageId, out perfPageId))
            {
                var selectedPerformancePageRef = new PageReference(perfPageId);
                var selectedPerformancePage = EPiServer.DataFactory.Instance.GetPage(selectedPerformancePageRef);

                if (selectedPerformancePage.PageTypeID != PageType.PerformanceID)
                    return "Selected Page for Performances is not a type of performance.";

                media.AddMediaRelation(selectedPerformancePage);
            }

            int artPageId;
            if (int.TryParse(artistPageId, out artPageId))
            {
                var selectedArtistPageId = new PageReference(artPageId);
                var selectedArtistPage = EPiServer.DataFactory.Instance.GetPage(selectedArtistPageId);

                if (selectedArtistPage.PageTypeID != PageType.ArtistID)
                    return "Selected Page for Artists is not a type of artist.";

                media.AddMediaRelation(selectedArtistPage);
            }

            //Check for list of artists for bulk select

            foreach (string aGuid in artists.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
            {
                Guid artistGuid = new Guid(aGuid);

                media.AddMediaRelation(artistGuid, PageType.ArtistID);
            }


            AdageTheaterRelationshipManagerEntities.Current.SaveChanges();

            return "Media Information Updated.";
        }
    }
}