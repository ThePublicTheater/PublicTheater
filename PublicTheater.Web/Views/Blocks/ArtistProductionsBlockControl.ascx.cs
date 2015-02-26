using System;
using System.Linq;
using System.Web.UI.WebControls;
using Adage.Theater.RelationshipManager;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Web;
using PublicTheater.Custom.Episerver;
using PublicTheater.Custom.Episerver.Properties;
using TheaterTemplate.Shared.EpiServerConfig;

namespace PublicTheater.Web.Views.Blocks
{
    [TemplateDescriptor(Tags = new[] { Constants.RenderingTags.PDP })]
    public partial class ArtistProductionsBlockControl : ContentControlBase<Custom.Episerver.Pages.ArtistPageData>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ltrFeaturedArtistName.Text = CurrentData.PageName;

                var featuredArtistRelatedPDPs = PageRelationship.GetActiveRelatedPageIDs(CurrentData.ContentGuid, PageType.PerformanceID);

                var playsWithFeaturedArtist = PublicPlayDetails.PlayDetails
                    .Where(pdp => featuredArtistRelatedPDPs.Contains(pdp.PageId) && pdp.PageId != CurrentPage.PageLink.ID);

                rptFeaturedArtistPlays.DataSource = playsWithFeaturedArtist;
                rptFeaturedArtistPlays.DataBind();
            }
        }

        private PlayDetailsCollection _publicPlayDetails;
        protected PlayDetailsCollection PublicPlayDetails
        {
            get
            {
                return _publicPlayDetails ?? (_publicPlayDetails = PlayDetailsCollection.GetPlayDetails());
            }

        }
    }
}