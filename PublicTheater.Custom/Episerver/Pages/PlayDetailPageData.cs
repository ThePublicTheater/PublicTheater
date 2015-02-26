using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Adage.Tessitura;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Core;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Web;
using EPiServer;
using PublicTheater.Custom.CoreFeatureSet.Helper;
using PublicTheater.Custom.Episerver.Properties;
using PublicTheater.Custom.Episerver.Utility;
using TheaterTemplate.Shared.EPiServerPageTypes;
using Adage.Theater.RelationshipManager.PlugIn;
using TheaterTemplate.Shared.EpiServerConfig;
using System;


namespace PublicTheater.Custom.Episerver.Pages
{
    [ContentType(GUID = "c01847cc-06f3-41b5-bc23-87f53d9d911a", DisplayName = "[Public Theater] Play Details", GroupName = Constants.ContentGroupNames.SeasonGroupName)]
    public class PlayDetailPageData : PlayDetailsPageType
    {
        #region Epi Properties

        [UIHint(UIHint.Image)]
        [Display(GroupName = SystemTabNames.Content, Name = "Hero Image", Order = 0)]
        public virtual Url HeroImage { get; set; }
        
        [Display(GroupName = SystemTabNames.Content, Name = "Heading Html", Description = "Alternative Editor for heading", Order = 1)]
        public virtual XhtmlString HeadingHtml { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Extra Short Description", Order = 1)]
        public virtual XhtmlString ExtraShortDescription { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Box1 Content", Description = "Additional information go in box 1.", Order = 2)]
        public virtual XhtmlString Box1Content { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Venue Override", Order = 1)]
        public virtual string VenueOverride { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Box2 content", Description = "Additional information go in box 2. (running time and age groups could go here)", Order = 2)]
        public virtual XhtmlString RunTime { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Show Member Price", Description = "Display member price or not.", Order = 2)]
        public virtual bool ShowMemberPrice { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Show Door Price", Description = "Display door price or not.", Order = 2)]
        public virtual bool ShowDoorPrice { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Ticket Not Available Message", Order = 2)]
        public virtual XhtmlString TicketNotAvailableMessage { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Sold Out Message", Order = 2)]
        public virtual XhtmlString SoldOutMessage { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Package Link URL", Order = 3)]
        public virtual string PackageLinkURL { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Package Link Text", Order = 4)]
        public virtual string PackageLinkText { get; set; }


        [Display(GroupName = SystemTabNames.Content, Name = "Footer content", Order = 51)]
        public virtual XhtmlString FooterContent { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Buy Tickets Link Override Text", Order = 51)]
        public virtual string BuyTicketLinkText { get; set; }

        [Display(GroupName = TheaterTabNames.Configuration, Name = "Is Summer Supporter Show", Description = "Is this a summer supporter park show PDP?", Order = 3)]
        public virtual bool IsParkShow { get; set; }

        [Display(GroupName = TheaterTabNames.Configuration, Name = "Hide Buy Tickets From Calendar", Description = "Hide the buy tickets link from the calendar?", Order = 3)]
        public virtual bool HideBuyTicketsFromCalendar { get; set; }

        [Display(Name = "Date Range Override", GroupName = SystemTabNames.Content, Description = "Date range of the play.", Order = 100)]
        public virtual string StartEndDateOverride { get; set; }
        [Display(Name = "Upcoming Performances Name Override", GroupName = SystemTabNames.Content, Description = "The name of the show on the upcoming performances control.", Order = 110)]
        public virtual string upcomingPeromanceName { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Show Promo Code", Order = 1)]
        public virtual Boolean showPromo { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Archived", Order = 1)]
        public virtual Boolean Archived { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Single Ticket Buyer Price Range Override", Order = 1)]
        public virtual string stbPriceOverride { get; set; }
        
        [Display(GroupName = SystemTabNames.Content, Name = "Member Price Range Override", Order = 1)]
        public virtual string memPriceOverride { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Master Price Override", Order = 2)]
        public virtual XhtmlString masterPriceOverride { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Acordion", Order = 10)]
        public virtual Blocks.AccordionBlockData Acordion { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Additional Hidden Text on Package Selection", Order = 100)]
        public virtual XhtmlString AddnlHiddenPackageText { get; set; }
        #endregion


        #region override prop from CFS
        [Editable(false)]
        [ScaffoldColumn(false)]
        public virtual new XhtmlString ProductionSynopsis
        {
            get
            {
                return CalendarSynopsis;
            }
            set
            {
                CalendarSynopsis = value;
            }
        }


        [Editable(false)]
        [ScaffoldColumn(false)]
        public virtual new string StartEndDate
        {
            get
            {
                if (string.IsNullOrWhiteSpace(StartEndDateOverride) == false)
                {
                    return StartEndDateOverride;
                }
                return TessituraProduction != null && TessituraProduction.Performances.Any()
                    ? Utility.Utility.GetPlayDetailsDateDescription(ProductionStartDate, ProductionEndDate)
                    : string.Empty;
            }
        }


        [Editable(false)]
        [ScaffoldColumn(false)]
        public string StartEndDateShort
        {
            get
            {
                if (string.IsNullOrWhiteSpace(StartEndDateOverride) == false)
                {
                    return StartEndDateOverride;
                }
                return TessituraProduction != null && TessituraProduction.Performances.Any()
                    ? Utility.Utility.GetShortDateDescription(ProductionStartDate, ProductionEndDate)
                    : string.Empty;
            }
        }


        public virtual string GetBuyTicketText()
        {
            if (string.IsNullOrWhiteSpace(BuyTicketLinkText) == false)
                return BuyTicketLinkText;
            return IsParkShow ? "Reserve Your Seats" : "Buy Tickets";
        }

        [Editable(false)]
        [ScaffoldColumn(false)]
        public virtual new string Playwright
        {
            get
            {
                return base.Playwright;
            }
            set
            {
                base.Playwright = value;
            }
        }

        [Editable(false)]
        [ScaffoldColumn(false)]
        public virtual new string Director
        {
            get
            {
                return base.Director;
            }
            set
            {
                base.Director = value;
            }
        }


        #endregion

        #region media manager

        [Display(GroupName = Constants.ContentGroupNames.MediaGroupName, Name = "Right column - Credits", Order = 9)]
        public virtual XhtmlString Credits { get; set; }

        //[BackingType(typeof(GenericRelationshipProperty))]
        //[Editable(true)]
        //[Display(Name = "Related Artists", Description = "", GroupName = Constants.ContentGroupNames.MediaGroupName, Order = 10)]
        //public virtual string RelatedArtists { get; set; }
        [Display(GroupName = Constants.ContentGroupNames.MediaGroupName, Name = "Related Media Items", Order = 11)]
        public virtual ContentArea RelatedMedia { get; set; }
        
        [Display(GroupName = Constants.ContentGroupNames.MediaGroupName, Name = "Media Box", Order = 11)]
        public virtual Blocks.MediaBoxBlockData MediaBox { get; set; }

        [Display(GroupName = Constants.ContentGroupNames.MediaGroupName, Name = "Right Column Block List", Order = 11)]
        public virtual ContentArea RightContent { get; set; }


        [Display(GroupName = Constants.ContentGroupNames.MediaGroupName, Name = "Bottom Block List", Order = 11)]
        public virtual ContentArea BottomContent { get; set; }



        #endregion

        #region site theme
        [BackingType(typeof(PropertyNumber))]
        [EditorDescriptor(EditorDescriptorType = typeof(EnumEditorDescriptor<Enums.SiteTheme>))]
        [Display(GroupName = SystemTabNames.Settings, Name = "Site Theme")]
        public virtual Enums.SiteTheme SiteTheme { get; set; }
        #endregion

        #region calculatedProps

        private string _ticketPriceRange;
        public virtual string TicketPriceRange
        {
            get
            {
                if (_ticketPriceRange == null)
                {
                    if (!string.IsNullOrEmpty(stbPriceOverride))
                    {
                        _ticketPriceRange = stbPriceOverride;
                    }
                    else
                    {
                        _ticketPriceRange = PerformanceHelper.GetWebFullPriceRange(TessituraProduction);
                    }
                }
                return _ticketPriceRange;
            }
        }

        private string _memberPriceRange;
        public virtual string MemberPriceRange
        {
            get
            {
                if (_memberPriceRange == null)
                {
                    if (!string.IsNullOrEmpty(memPriceOverride))
                    {
                        _memberPriceRange = memPriceOverride;
                    }
                    else
                    {
                        _memberPriceRange = PerformanceHelper.GetMemberPriceRange(TessituraProduction);
                    }
                }
                return _memberPriceRange;
            }
        }

        private string _doorPriceRange;
        public virtual string DoorPriceRange
        {
            get
            {
                if (_doorPriceRange == null)
                {
                    _doorPriceRange = PerformanceHelper.GetDoorPriceRange(TessituraProduction);
                }
                return _doorPriceRange;
            }
        }

        public string _venueName;
        /// <summary>
        /// Venue Name From Tess
        /// </summary>
        public virtual string VenueName
        {
            get
            {
                if (_venueName == null)
                {
                    if (TessituraProduction == null)
                    {
                        _venueName = string.Empty;
                    }
                    else if (TessituraProduction.Performances.Any() == false)
                    {
                        // no perfs built in tess, use the production's venue name
                        _venueName = TessituraProduction.Venue;
                    }
                    else
                    {
                        //there is perfs, use cms defined user friendly venue display name 
                        var perf = TessituraProduction.Performances.First();
                        _venueName = PublicObjectHelper.GetVenueConfigurations().GetVenueName(perf.VenueId, perf.Venue);
                    }
                }
                return _venueName;
            }
        }

        /// <summary>
        /// Friendly URL
        /// </summary>
        public string FriendlyUrl
        {
            get
            {
                if (PlayDetailLink != null)
                    return PlayDetailLink.ToString();

                return this.GetSiteThemeUrl();
            }
        }

        private Production _tessituraProduction;
        private bool _tessituraProductionLoaded;
        /// <summary>
        /// Tess Production by Tessitura ID
        /// </summary>
        public virtual Production TessituraProduction
        {
            get
            {
                if (_tessituraProductionLoaded == false && TessituraId > 0)
                {
                    _tessituraProductionLoaded = true;
                    try
                    {
                        _tessituraProduction = Production.GetProduction(TessituraId);
                    }
                    catch (System.Exception ex)
                    {
                        Adage.Common.ElmahCustomError.CustomError.LogError(ex, string.Format("bad PDP ProductSeasonID- {0} - {1} - {2}", TessituraId, PageName, ex.StackTrace), null);
                    }

                }
                return _tessituraProduction;
            }
        }

        /// <summary>
        /// Performances with avaiability status, rebuilds if mos updated
        /// </summary>
        public Dictionary<Performance, PerformanceHelper.PerformanceAvailable> TessituraPerformancesWithStatus
        {
            get
            {
                var performancesWithStatus = new Dictionary<Performance, PerformanceHelper.PerformanceAvailable>();
                if (TessituraProduction != null)
                {
                    foreach (Performance performance in TessituraProduction.Performances)
                    {
                        var status = PerformanceHelper.PerformanceAvailableStatus(performance);
                        performancesWithStatus.Add(performance, status);
                    }
                }
                return performancesWithStatus;
            }
        }

        /// <summary>
        /// On sale performances have ticket available
        /// </summary>
        public List<Adage.Tessitura.Performance> AvailablePerformances
        {
            get
            {
                return
                    TessituraPerformancesWithStatus
                    .Where(p => p.Value == PerformanceHelper.PerformanceAvailable.Available)
                    .Select(p => p.Key)
                    .ToList();
            }
        }

        /// <summary>
        /// Sold out
        /// </summary>
        public bool IsSoldOut
        {
            get
            {
                return TessituraPerformancesWithStatus.Any() &&
                       TessituraPerformancesWithStatus.All(p =>
                           p.Value == PerformanceHelper.PerformanceAvailable.NoSectionsAvailableSeats
                           || p.Value == PerformanceHelper.PerformanceAvailable.NoSectionsAvailableSeats);
            }
        }

        /// <summary>
        /// Start Date
        /// </summary>
        public virtual System.DateTime ProductionStartDate
        {
            get { return TessituraProduction == null ? System.DateTime.MinValue : TessituraProduction.FirstPerformance; }
        }

        /// <summary>
        /// End Date
        /// </summary>
        public virtual System.DateTime ProductionEndDate
        {
            get { return TessituraProduction == null ? System.DateTime.MinValue : TessituraProduction.LastPerformance; }
        }
        #endregion

        #region method calls
        /// <summary>
        /// set defaults for the page data
        /// </summary>
        /// <param name="contentType"></param>
        public override void SetDefaultValues(ContentType contentType)
        {
            base.SetDefaultValues(contentType);

            Archived = true;
        }
        #endregion


        #region SEO
        [Display(Name = "Description", Description = "Description of the page", GroupName = "SEO")]
        [UIHint("textarea")]
        public virtual string SEODescription { get; set; }
        [UIHint("textarea")]
        [Display(Name = "SEOKeywords", Description = "Keywords for the page", GroupName = "SEO")]
        public virtual string SEOKeywords { get; set; }
        [UIHint("SEORobotsSource")]
        [Display(Name = "Robots", Description = "How web robots should handle the page", GroupName = "SEO")]
        public virtual string SEORobots { get; set; }
        [Display(Name = "SEO Title", Description = "The title of the pages", GroupName = "SEO")]
        [BackingType(typeof(PropertyString))]
        public virtual string SEOTitle { get; set; }
        #endregion
    }
}
