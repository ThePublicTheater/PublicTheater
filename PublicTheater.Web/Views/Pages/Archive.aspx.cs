using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Adage.Tessitura;
using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.Filters;
using EPiServer.ServiceLocation;
using EPiServer.UI.Report.Reports;
using Microsoft.Ajax.Utilities;
using PublicTheater.Custom.CoreFeatureSet.EpiServerConfig;
using PublicTheater.Custom.CoreFeatureSet.Helper;
using PublicTheater.Custom.Episerver;
using PublicTheater.Custom.Episerver.Pages;
using PublicTheater.Custom.Episerver.Utility;
using PublicTheater.Web.Views.Blocks;
using TheaterTemplate.Shared.EpiServerConfig;
using TheaterTemplate.Shared.Common;
using TheaterTemplate.Web.Views.Controls;


namespace PublicTheater.Web.Views.Pages
{
    public partial class Archive : Custom.Episerver.BaseClasses.PublicBasePage<Custom.Episerver.Pages.ArchivePageData>
    {
        #region Build Archive List

        private const int DefaultPostProductionHours = 24;

        protected virtual int PostProductionHoursConfig
        {
            get
            {
                return CurrentPage.ArchivePostProductionHours > 0
                    ? CurrentPage.ArchivePostProductionHours
                    : DefaultPostProductionHours;
            }
        }

        private List<PublicPlayDetail> _allArchives;


        public List<PublicPlayDetail> AllArchives
        {
            get
            {
                if (_allArchives == null)
                {
                    _allArchives = new List<PublicPlayDetail>();
                    var playDetails = TheaterTemplate.Shared.EpiServerConfig.PlayDetailsCollection.GetPlayDetails();

                    foreach (TheaterTemplate.Shared.EpiServerConfig.PlayDetail playDetail in playDetails.PlayDetails)
                    {
                        
                        var pdp = playDetail as PublicPlayDetail;
                        if (pdp != null && pdp.Archived)
                        {
                            _allArchives.Add(pdp);
                        }
                    }
                    
                    

                    var pdpPageCriteria = new PropertyCriteria()
                    {
                        Name = "PageTypeID",
                        Condition = CompareCondition.Equal,
                        Required = true,
                        Type = PropertyDataType.PageType,
                        Value = Config.GetValue<string>("ARCHIVED_PDP_PAGE_TYPE", "292")
                        //Value = ServiceLocator.Current.GetInstance<PageTypeRepository>().Load(typeof(ArchivedPlayDetailPageData)).ID
                    };


                    var critCollection = new PropertyCriteriaCollection();
                    critCollection.Add(pdpPageCriteria);
                    

                    //var findArchived = DataFactory.Instance.FindPagesWithCriteria(ContentReference.StartPage,critCollection);
                    var findArchived = ContentReference.StartPage.SearchPageByType<ArchivedPlayDetailPageData>();
                    foreach (var playDetailsPage in findArchived)
                    {
                        


                        var pdp = PublicPlayDetail.MakeFromPageData(playDetailsPage);
                        if (pdp != null)
                        {
                            //pdp.Heading = OverrideShowName(pdp);
                            // Need to fix thimbnails
                            _allArchives.Add(pdp);
                        }
                    }
                    

                }
                //most recent first
                _allArchives.Sort((a, b) => b.StartDate.CompareTo(a.StartDate));
                return _allArchives;
            }
        }
        private string OverrideShowName(TheaterTemplate.Shared.EpiServerConfig.PlayDetail playDetail)
        {
            PageData oPage;
            EPiServer.Core.PropertyLongString showNameOverride = EPiServer.Core.PropertyLongString.Parse("");
            oPage = EPiServer.DataFactory.Instance.GetPage(new PageReference(playDetail.PageId));
            showNameOverride = (EPiServer.Core.PropertyLongString)oPage.Property["upcomingPeromanceName"];
            return showNameOverride.ToString();
        }
        private string OverrideThumbnail(TheaterTemplate.Shared.EpiServerConfig.PlayDetail playDetail)
        {
            PageData oPage;
            EPiServer.SpecializedProperties.PropertyUrl ImageOverride;
            oPage = EPiServer.DataFactory.Instance.GetPage(new PageReference(playDetail.PageId));
            ImageOverride = (EPiServer.SpecializedProperties.PropertyUrl)oPage.Property["HeroImage"];
            return ImageOverride.ToString();
        }
        private List<PublicPlayDetail> _filteredArchives;
        protected List<PublicPlayDetail> FilteredArchives
        {
            get
            {
                if (_filteredArchives == null)
                {
                    _filteredArchives = GetFilteredArchives();
                }
                return _filteredArchives;
            }
        }

        /// <summary>
        /// Get Filter Archives based on user filters
        /// </summary>
        /// <returns></returns>
        protected virtual List<PublicPlayDetail> GetFilteredArchives()
        {
            var archivesToReturn = new List<PublicPlayDetail>();

            var criteria = GetPerformanceCriteria();
            var performances = Performances.GetPerformances(criteria);

            var archivePerformances = FilterOnsaleProductions(performances, criteria);

            foreach (Performance performance in archivePerformances)
            {
                var archive = AllArchives.FirstOrDefault(pdp => pdp.TessituraId == performance.ProductionSeasonId);
                if (archive == null)
                {
                    continue;
                }

                if (!archivesToReturn.Contains(archive))
                {
                    archivesToReturn.Add(archive);
                }

                if (archive.StartDate == DateTime.MinValue || performance.PerformanceDate < archive.StartDate)
                {
                    archive.StartDate = performance.PerformanceDate; //start date = min perf date
                }

                if (archive.EndDate == DateTime.MinValue || performance.PerformanceDate > archive.EndDate)
                {
                    archive.EndDate = performance.PerformanceDate; //start date = max perf date
                }
                archive.SiteTheme = SiteThemeHelper.GetSiteTheme(performance);
            }

            //restrict by title or main body text
            if (string.IsNullOrEmpty(FilterPerfInput.Text) == false)
            {
                archivesToReturn = archivesToReturn
                    .Where(item =>
                        item.Heading.ToLower().Contains(FilterPerfInput.Text.ToLower())
                        || (item.MainBody != null && item.MainBody.ToLower().Contains(FilterPerfInput.Text.ToLower())))
                    .ToList();
            }

            foreach (var filteredArchive in archivesToReturn)
            {
                var perfs = archivePerformances.Where(p => p.ProductionSeasonId == filteredArchive.TessituraId);
                filteredArchive.SetDateRange(Utility.GetArchiveDateDescription(perfs.ToList()));
            }

            archivesToReturn = archivesToReturn
                .OrderByDescending(item => !string.IsNullOrEmpty(item.ThumbnailUrl))
                .ThenByDescending(item => item.StartDate).ToList();


            return archivesToReturn;
        }

        /// <summary>
        /// Remove the onsale productions from the archived performances list
        /// </summary>
        /// <param name="performances"></param>
        /// <param name="criteria"></param>
        /// <returns></returns>
        protected virtual List<Performance> FilterOnsaleProductions(Performances performances, PerformancesCriteria criteria)
        {
            var productionCriteria = new ProductionsCriteria
            {
                StartDate = criteria.StartDate,
                EndDate = criteria.EndDate
            };
            //configured post production hours, archive must be no longer on sale after xx hours
            var cutoffDate = DateTime.Now.AddHours(-this.PostProductionHoursConfig);
            var onSaleProductionSeasonIds = Productions.GetProductions(productionCriteria)
                .Where(prod => prod.LastPerformance > cutoffDate)
                .Select(prod => prod.ProductionSeasonNumber);

            return performances.Where(perf => onSaleProductionSeasonIds.Contains(perf.ProductionSeasonId) == false).ToList();
        }

        /// <summary>
        /// get performance search criteria based on filters
        /// </summary>
        /// <returns></returns>
        protected virtual PerformancesCriteria GetPerformanceCriteria()
        {

            //default criteria
            var criteria = new PerformancesCriteria
            {
                StartDate = DateTime.Today.AddYears(-1),
                EndDate = DateTime.Today,
                Keywords = string.Empty
            };

            //restrict year
            int year;
            if (string.IsNullOrEmpty(ddlYear.SelectedValue) == false && int.TryParse(ddlYear.SelectedValue, out year))
            {
                criteria.StartDate = new DateTime(year, 1, 1);
                criteria.EndDate = criteria.StartDate.AddYears(1);
            }

            //restrict month
            if (string.IsNullOrEmpty(ddlMonth.SelectedValue) == false)
            {
                var month = int.Parse(ddlMonth.SelectedValue);
                criteria.StartDate = new DateTime(criteria.StartDate.Year, month, 1);
                criteria.EndDate = criteria.StartDate.AddMonths(1);
            }

            //restrict genre
            if (string.IsNullOrEmpty(ddlGenre.SelectedValue) == false)
            {
                criteria.Keywords = ddlGenre.SelectedValue;
            }
            return criteria;
        }
        #endregion

        protected int GetMaxCountByTheme(Enums.SiteTheme theme)
        {
            switch (theme)
            {
                case Enums.SiteTheme.Default:
                    return CurrentPage.MaxCountPublicTheater > 0 ? CurrentPage.MaxCountPublicTheater : int.MaxValue;

                case Enums.SiteTheme.JoesPub:
                    return CurrentPage.MaxCountJoesPub > 0 ? CurrentPage.MaxCountJoesPub : int.MaxValue;

                case Enums.SiteTheme.Shakespeare:
                    return CurrentPage.MaxCountShakespeare > 0 ? CurrentPage.MaxCountShakespeare : int.MaxValue;

                case Enums.SiteTheme.UnderTheRadar:
                    return CurrentPage.MaxCountUndertheRadar > 0 ? CurrentPage.MaxCountUndertheRadar : int.MaxValue;

                case Enums.SiteTheme.PublicForum:
                    return CurrentPage.MaxCountPublicForum > 0 ? CurrentPage.MaxCountPublicForum : int.MaxValue;

                case Enums.SiteTheme.EmergingWritersGroup:
                    return CurrentPage.MaxCountEmergingWritersGroup > 0 ? CurrentPage.MaxCountEmergingWritersGroup : int.MaxValue;

                default:
                    return int.MaxValue;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindFilters();
                BindArchiveList();
            }
        }

        private void BindFilters()
        {
            var count = DateTime.Now.Year - CurrentPage.ArchiveStartYear + 1;

            ddlYear.DataSource = Enumerable.Range(CurrentPage.ArchiveStartYear, count > 0 ? count : 0).OrderByDescending(y => y);
            ddlYear.DataBind();
            ddlYear.Items.Insert(0, new ListItem("All", "Recent"));

            ddlMonth.Items.Clear();
            var items = Enumerable.Range(1, 12)
                .Select(i => new DateTime(DateTime.Now.Year, i, 1))
                .Select(month => new ListItem(month.ToString("MMMM"), month.Month.ToString())).ToArray();
            ddlMonth.Items.AddRange(items);
            ddlMonth.Items.Insert(0, new ListItem("All", string.Empty));

            ddlGenre.DataSource = CurrentPage.GenraList.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            ddlGenre.DataBind();
            ddlGenre.Items.Insert(0, new ListItem("All", string.Empty));
        }

        protected virtual void BindArchiveList()
        {
            rptVenues.ItemDataBound += new RepeaterItemEventHandler(rptVenues_ItemDataBound);
            rptVenueHeaders.ItemDataBound += rptVenueHeaders_ItemDataBound;

            var availableThemes = FilteredArchives.Select(a => a.SiteTheme).Distinct().OrderBy(s => s).ToList();

            rptVenues.DataSource = availableThemes;
            rptVenues.DataBind();

            rptVenueHeaders.DataSource = availableThemes;
            rptVenueHeaders.DataBind();
        }

        void rptVenueHeaders_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem == null)
                return;
            var currentTheme = (Enums.SiteTheme)e.Item.DataItem;

            var lnkVenueFilter = e.Item.FindControl("lnkVenueFilter") as HyperLink;
            if (lnkVenueFilter != null)
            {
                lnkVenueFilter.Text = Enums.GetEnumDescription(currentTheme);
                lnkVenueFilter.CssClass = currentTheme.ToString();
                lnkVenueFilter.Attributes["href"] = "#" + currentTheme;
            }
        }

        void rptVenues_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            if (e.Item.DataItem == null)
                return;

            var currentTheme = (Enums.SiteTheme)e.Item.DataItem;
            var archives = FilteredArchives
                .Where(a => a.SiteTheme == currentTheme);

            var litThemeName = e.Item.FindControl("litThemeName") as Literal;
            if (litThemeName != null)
            {
                litThemeName.Text = Enums.GetEnumDescription(currentTheme);
            }

            var rptVenueProductions = e.Item.FindControl("rptVenueProductions") as Repeater;
            if (rptVenueProductions != null)
            {
                rptVenueProductions.ItemDataBound += new RepeaterItemEventHandler(rptVenueProductions_ItemDataBound);
                rptVenueProductions.DataSource = archives;
                rptVenueProductions.DataBind();
            }

            var itemPerPage = e.Item.FindControl("itemPerPage") as HiddenField;
            if (itemPerPage != null)
            {
                itemPerPage.Value = GetMaxCountByTheme(currentTheme).ToString();
            }
        }

        void rptVenueProductions_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var pdp = e.Item.DataItem as PublicPlayDetail;
            if (pdp == null)
                return;

            var imgThumbnail = e.Item.FindControl("imgThumbnail") as Image;
            if (string.IsNullOrEmpty(pdp.ThumbnailUrl))
            {
                var thumboverride = OverrideThumbnail(pdp);
                if (string.IsNullOrEmpty(thumboverride))
                {
                    if (imgThumbnail != null)
                    {
                        imgThumbnail.Visible = true;
                    }
                }
                else
                {
                    if (imgThumbnail != null)
                    {
                        imgThumbnail.ImageUrl = thumboverride;
                    }  
                }
        }
            else
            {
                if (imgThumbnail != null)
                {
                    imgThumbnail.ImageUrl = pdp.ThumbnailUrl;
                }
            }

            var lbTitle = e.Item.FindControl("lbTitle") as HyperLink;
            if (lbTitle != null)
            {
                if (!pdp.Heading.IsNullOrWhiteSpace())
                    lbTitle.Text = pdp.Heading;
                else
                    lbTitle.Text = OverrideShowName(pdp);

                lbTitle.NavigateUrl = pdp.PlayDetailLink;
            }

            var lbDate = e.Item.FindControl("lbDate") as Label;
            if (lbDate != null)
            {
                lbDate.Text = pdp.DateRange;
            }
        }

        protected void FilterChangedChanged(object sender, EventArgs e)
        {
            BindArchiveList();
            pnlArchive.Update();
        }
    }
}