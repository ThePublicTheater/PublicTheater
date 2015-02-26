using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using PublicTheater.Custom.WatchAndListen;
using PublicTheater.Custom.CoreFeatureSet.EpiServerConfig;
using PublicTheater.Custom.Episerver.Pages;
using Adage.Theater.RelationshipManager;
using EPiServer;
using EPiServer.Core;
using TheaterTemplate.Shared.EpiServerConfig;

namespace PublicTheater.Web.Views.Pages
{
    public partial class PlayDetail : Custom.Episerver.BaseClasses.PublicBasePage<PlayDetailPageData>
    {
        protected override void OnInit(EventArgs e)
        {
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