using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Adage.Theater.RelationshipManager;
using Adage.Theater.RelationshipManager.PlugIn;
using EPiServer.WebParts.UI.Admin;
using PublicTheater.Custom.Episerver;
using PublicTheater.Custom.Episerver.Pages;
using TheaterTemplate.Shared.EpiServerConfig;

namespace PublicTheater.Custom.CoreFeatureSet.EpiServerConfig
{
    public class PublicPlayDetail : TheaterTemplate.Shared.EpiServerConfig.PlayDetail
    {
        public virtual DateTime StartDate { get; set; }
        public virtual DateTime EndDate { get; set; }

        public virtual bool Archived { get; set; }
        public virtual Guid ContentGuid { get; set; }
        public virtual string BuyTicketLinkText { get; protected set; }
        public virtual bool HideBuyTicketLinkFromCalendar { get; protected set; }
        public virtual Enums.SiteTheme SiteTheme { get; set; }
        protected virtual string StartEndDateOverride { get; set; }
        public virtual string AdditionalHiddenPackageText { get; set; }

        public override string DateRange
        {
            get
            {
                if (!string.IsNullOrEmpty(StartEndDateOverride))
                {
                    return StartEndDateOverride;
                }
                return string.IsNullOrEmpty(base.DateRange) ? Custom.Episerver.Utility.Utility.GetDateDescription(StartDate, EndDate) : base.DateRange;
            }
            protected set
            {
                base.DateRange = value;
            }
        }

        public string ExtraShortDescription { get; protected set; }

        protected override void FillFromLock(EPiServer.Core.PageData playDetailsPage)
        {
            base.FillFromLock(playDetailsPage);

            ExtraShortDescription = GetStringValueFromVisitorGroup(playDetailsPage, "ExtraShortDescription", false);

            ContentGuid = playDetailsPage.ContentGuid;
            var publicPDP = playDetailsPage as Custom.Episerver.Pages.PlayDetailPageData;

            PlayDetailLink = publicPDP == null
                ? Episerver.Utility.Utility.GetFriendlyUrl(playDetailsPage.PageLink)
                : publicPDP.FriendlyUrl;

            BuyTicketLinkText = publicPDP == null
                ? string.Empty
                : publicPDP.GetBuyTicketText();

            AdditionalHiddenPackageText = publicPDP == null
                ? string.Empty
                : (publicPDP.AddnlHiddenPackageText==null
                ? string.Empty
                : publicPDP.AddnlHiddenPackageText.ToHtmlString());


            Archived = publicPDP != null && publicPDP.Archived;
            HideBuyTicketLinkFromCalendar = publicPDP != null && publicPDP.HideBuyTicketsFromCalendar;

            StartEndDateOverride = publicPDP == null ? string.Empty : publicPDP.StartEndDateOverride;
        }

        public static PublicPlayDetail MakeFromPageData(EPiServer.Core.PageData playDetailsPage)
        {
            var detail = new PublicPlayDetail();
            detail.FillFromLock(playDetailsPage);
            return detail;
        }
        public void SetDateRange(string dateRangeString)
        {
            this.DateRange = dateRangeString;
        }
    }
}
