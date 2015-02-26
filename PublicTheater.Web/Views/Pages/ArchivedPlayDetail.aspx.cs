using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using PublicTheater.Custom.Episerver.Utility;
using PublicTheater.Custom.WatchAndListen;
using PublicTheater.Custom.CoreFeatureSet.EpiServerConfig;
using PublicTheater.Custom.Episerver.Pages;
using Adage.Theater.RelationshipManager;
using EPiServer;
using EPiServer.Core;
using TheaterTemplate.Shared.EpiServerConfig;

namespace PublicTheater.Web.Views.Pages
{
    public partial class ArchivedPlayDetail : Custom.Episerver.BaseClasses.PublicBasePage<ArchivedPlayDetailPageData>
    {
        protected override void OnInit(EventArgs e)
        {
            /*
            var findArchived = ContentReference.StartPage.SearchPageByType<ArchivedPlayDetailPageData>();

            foreach (var playDetailsPage in findArchived)
            {
                var writablePage = playDetailsPage.CreateWritableClone();
                if (playDetailsPage.Thumbnail150x75.ToString() == "/link/d429aa5fe4874e40a06691d2d2a3db6c.jpg" && playDetailsPage.PageLink.ID == 8298)
                {
                    writablePage["Thumbnail150x75"] = new Url("/Global/Joe's%20Pub/370%20x%20238/JoesPubDefault.jpg");
                    DataFactory.Instance.Save(writablePage, EPiServer.DataAccess.SaveAction.Publish);
                }
            }
              */      

            base.OnInit(e);
            ticketBlock.CurrentData = CurrentPage;
            PDPPackageBlockControl.CurrentData = CurrentPage;

            if (!IsPostBack)
            {
                Custom.CoreFeatureSet.Common.PublicMOSHelper.ClearFlexPackageSourceCode();    
            }
        }

        
        protected void Page_Load(object sender, EventArgs e)
        {
            promoBox.PromoCodeUpdated += new EventHandler(PromoBox_PromoCodeUpdated);
            if (!IsPostBack)
            {
                promoBox.Visible = CurrentPage.showPromo;
                BindRelatedMedia();

                if (CurrentPage.ShowMemberPrice)
                {
                    litMemberPrice.Text = CurrentPage.MemberPriceRange;
                }
                if (CurrentPage.ShowDoorPrice)
                {
                litDoorPrice.Text = CurrentPage.DoorPriceRange;
                }
                if (CurrentPage.IsParkShow)
                {
                    //Adage.Tessitura.TessSession.GetSession().SetReturnURL();
                    litBox1Venue.Text = CurrentPage.VenueName;
                    litBox1Venue.Visible = true;
                    pnlPricing.Visible = false;
                }
            }
        }

        private void BindRelatedMedia()
        {
            if (CurrentPage.RelatedMedia == null || !CurrentPage.RelatedMedia.Contents.Any())
            {
                pnlMedia.Visible = false;
                return;
            }
            rptRelatedMediaItems.DataSource = CurrentPage.RelatedMedia.Contents.Select((media) => {
                var result = new
                {
                    CoverImage = media.Property["CoverImage"] ?? media.GetType().GetProperty("CoverImage").GetValue(media, null),
                    FriendlyURL = media.Property["FriendlyURL"] ?? media.GetType().GetProperty("FriendlyURL").GetValue(media, null)
                };
               
                return result;
            });
            rptRelatedMediaItems.DataBind();
        }

        /// <summary>
        /// reset buy ticket block after promo updated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void PromoBox_PromoCodeUpdated(object sender, EventArgs e)
        {
            ticketBlock.GetPerformanceAvailabilityFromPDP();
        }
    }
}