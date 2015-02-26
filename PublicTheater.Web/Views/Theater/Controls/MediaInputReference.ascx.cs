using System;
using System.Globalization;
using System.Linq;
using System.Web.UI.WebControls;
using Adage.Theater.RelationshipManager;
using EPiServer;
using EPiServer.Core;
using EPiServer.Filters;
using EPiServer.Web;

namespace PublicTheater.Web.Views.Theater.Controls
{
    public partial class MediaInputReference : UserControlBase
    {
        int GetRelatedPageTypeID()
        {
            if (ViewState["RelatedPageTypeID"] == null)
                throw new Exception("Page Type is missing for the Media Input Reference.");
            return (int)ViewState["RelatedPageTypeID"];
        }
        public int RelatedPageTypeID
        {
            get
            {
                return GetRelatedPageTypeID();
            }
            set { ViewState["RelatedPageTypeID"] = value; }
        }

        public Guid MediaGuid
        {
            get
            {
                //if (ViewState["MediaGuid"] == null)
                //    throw new Exception("Media Guid is missing for Media Input Reference.");
                return (Guid)ViewState["MediaGuid"];
            }
            set { ViewState["MediaGuid"] = value; }
        }

        public string MediaText
        {
            set { lblRelatedPageName.Text = value; }
        }

        public string PerformanceFolder
        {
            get
            {
                if (ViewState["PerformanceFolder"] == null)
                    return String.Empty;
                return (string)ViewState["PerformanceFolder"];
            }
            set { ViewState["PerformanceFolder"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lvMediaRelationship.ItemDataBound += lvMediaRelationship_ItemDataBound;
            lvMediaRelationship.ItemCommand += lvMediaRelationship_ItemCommand;
            lvArtists.ItemDataBound += lvArtists_ItemDataBound;
            Refresh.Click += RefreshClick;
        }

        void RefreshClick(object sender, EventArgs e)
        {
            BindRelatedPages(MediaGuid, RelatedPageTypeID);
        }

        private void lvMediaRelationship_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType != ListViewItemType.DataItem)
                return;

            var relatedPageName = e.Item.FindControl("RelatedPageName") as Label;

            if (relatedPageName == null) return;

            var relationship = e.Item.DataItem as MediaRelationship;

            if (relationship == null) return;

            var hfMediaRelationshipID = e.Item.FindControl("HfMediaRelationshipID") as HiddenField;

            if (hfMediaRelationshipID != null)
                hfMediaRelationshipID.Value = relationship.MediaRelationshipID.ToString(CultureInfo.InvariantCulture);

            var pageReference = PermanentLinkUtility.FindContentReference(relationship.EPiPageGuid);

            var page = EPiServer.DataFactory.Instance.GetPage(pageReference as PageReference);

            relatedPageName.Text = page.PageName;
        }

        private void lvMediaRelationship_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.Item.ItemType != ListViewItemType.DataItem) return;

            if (e.CommandName.ToUpper() != "REMOVE") return;

            var hfMediaRelationshipID = e.Item.FindControl("HfMediaRelationshipID") as HiddenField;

            int mediaRelationshipID;

            if (hfMediaRelationshipID != null && int.TryParse(hfMediaRelationshipID.Value, out mediaRelationshipID))
            {
                MediaRelationship.RemoveByID(mediaRelationshipID);
            }
            AdageTheaterRelationshipManagerEntities.Current.SaveChanges();

            BindRelatedPages(MediaGuid, RelatedPageTypeID);
        }

        internal bool ValidPageType()
        {
            if (bulkSelect.Visible) return true;
            if (ucInputPageReference.PageLink == null) return false;

            if (RelatedPageTypeID == 0)
                throw new Exception("Related Page Type missing for Media Manager.");

            var selectedPage = EPiServer.DataFactory.Instance.GetPage(ucInputPageReference.PageLink);

            if (selectedPage.PageTypeID != RelatedPageTypeID)
                throw new Exception("Selected Page does not meet the page type criteria.");

            return true;
        }
        public void AddMediaRelationshipWithoutUpdatingPanel(EPiMedia media)
        {
            if (ucInputPageReference.PageLink != null)
            {
                var selectedPage = EPiServer.DataFactory.Instance.GetPage(ucInputPageReference.PageLink);

                media.AddMediaRelation(selectedPage);

                ucInputPageReference.PageLink = PageReference.EmptyReference;
            }
        }
        public void AddMediaRelationship(EPiMedia media)
        {
            if (bulkSelect.Visible)
            {
                foreach (var lvi in lvArtists.Items)
                {
                    var cbSelect = lvi.FindControl("cbSelect") as CheckBox;
                    if (cbSelect == null || !cbSelect.Checked) continue;

                    string pageGuid = cbSelect.Attributes["PageGuid"];

                    media.AddMediaRelation(new Guid(pageGuid), PageType.ArtistID);
                }
            }


            if (ucInputPageReference.PageLink != null)
            {
                var selectedPage = EPiServer.DataFactory.Instance.GetPage(ucInputPageReference.PageLink);

                media.AddMediaRelation(selectedPage);

                ucInputPageReference.PageLink = PageReference.EmptyReference;
            }

            BindRelatedPages(MediaGuid, RelatedPageTypeID);
        }

        public void SetMediaProperties(Guid guid, int pageTypeID)
        {
            MediaGuid = guid;
            RelatedPageTypeID = pageTypeID;
            BindRelatedPages(guid, pageTypeID);
        }


        internal void BindRelatedPages(Guid guid, int pageTypeID)
        {

            lvMediaRelationship.DataSource = MediaRelationship.GetMediaRelationshipByGuid(guid, pageTypeID);
            lvMediaRelationship.DataBind();

            if (!String.IsNullOrEmpty(PerformanceFolder))
                LoadArtists(PerformanceFolder);

            upMediaRelationship.Update();

        }

        //Load Artists for the first performance
        public void LoadArtists(string performanceName)
        {
            if (String.IsNullOrEmpty(PerformanceFolder))
                PerformanceFolder = performanceName;
            //Check if there is performance relationship in db
            var performance = MediaRelationship.GetMediaRelationshipByGuid(MediaGuid, PageType.PerformanceID).FirstOrDefault();

            var performancePageGuid = new Guid();

            if (performance != null)
                performancePageGuid = performance.EPiPageGuid;
            else
            {
                //Find default performance
                var pages = GetDefaultPerformance(performanceName);

                if (pages.Count > 0) performancePageGuid = pages[0].PageGuid;
            }
            var relatedArtistPages = MediaRelationship.GetUnAssociatedArtistsForPerformance(MediaGuid, performancePageGuid);

            if (relatedArtistPages == null || relatedArtistPages.Count == 0) return;

            bulkSelect.Visible = true;
            lvArtists.DataSource = relatedArtistPages;
            lvArtists.DataBind();

        }
        public void LoadArtistsWithoutBoundMedia(string performanceName)
        {
            if (String.IsNullOrEmpty(PerformanceFolder))
                PerformanceFolder = performanceName;
            var pages = GetDefaultPerformance(performanceName);
            Guid performancePageGuid = new Guid();
            if (pages.Count > 0) performancePageGuid = pages[0].PageGuid;
            var relatedArtistPages = PageRelationship.GetRelatedPages(performancePageGuid, PageType.PerformanceID,
                                                                      PageType.ArtistID);
            if (relatedArtistPages == null || relatedArtistPages.Count == 0) return;

            bulkSelect.Visible = true;
            lvArtists.DataSource = relatedArtistPages;
            lvArtists.DataBind();
        }
        private void lvArtists_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType != ListViewItemType.DataItem) return;

            var relationshipPage = e.Item.DataItem as PageRelationship;

            var litArtistName = e.Item.FindControl("litArtistName") as Literal;
            var cbSelect = e.Item.FindControl("cbSelect") as CheckBox;
            if (litArtistName != null)
            {
                int artistPageID = relationshipPage.EPiPageTypeID == PageType.ArtistID
                                       ? relationshipPage.EPiPageID
                                       : relationshipPage.EPiTargetPageID;

                PageData artistPage = EPiServer.DataFactory.Instance.GetPage(new PageReference(artistPageID));
                litArtistName.Text = artistPage.PageName;

                cbSelect.Attributes.Add("PageGuid", artistPage.PageGuid.ToString());
            }
        }

        public void SetDefaultPerformance(string performanceName)
        {
            var performance = MediaRelationship.GetMediaRelationshipByGuid(MediaGuid, PageType.PerformanceID);
            if (performance.Count > 0) return;

            var pages = GetDefaultPerformance(performanceName);

            if (pages.Count == 0) return;

            ucInputPageReference.PageLink = pages[0].PageLink;
        }
        public void SetDefaultPerformanceWithoutAttachedMedia(string performanceName)
        {
            var pages = GetDefaultPerformance(performanceName);

            if (pages.Count == 0) return;

            ucInputPageReference.PageLink = pages[0].PageLink;
        }
        private static System.Collections.Generic.List<PageData> GetDefaultPerformance(string performanceName)
        {
            //Check if the performanceName (folder name)  maps to any performance
            var criterias = new PropertyCriteriaCollection
                                {
                                    //Find pages of a specific page type (Performance)
                                    new PropertyCriteria()
                                        {
                                            Name = "PageTypeID",
                                            Condition = CompareCondition.Equal,
                                            Required = true,
                                            Type = PropertyDataType.PageType,
                                            Value = PageType.PerformanceID.ToString(CultureInfo.InvariantCulture)
                                        },
                                };

            // Search pages under the start page
            var pages = DataFactory.Instance.FindPagesWithCriteria(
                ContentReference.StartPage,
                criterias);

            return pages.Where(p => p.PageName.Replace("'", string.Empty).Equals(performanceName)).ToList();
        }

        internal void SetDefaultEventWithoutAttachedMedia(string eventName)
        {
            var pages = GetDefaultEvent(eventName);

            if (pages.Count == 0) return;

            ucInputPageReference.PageLink = pages[0].PageLink;
        }

        internal void SetDefaultSubscriptionWithoutAttachedMedia(string subscriptionName)
        {
            var pages = GetDefaultSubscription(subscriptionName);

            if (pages.Count == 0) return;

            ucInputPageReference.PageLink = pages[0].PageLink;
        }

        internal void SetDefaultEvent(string eventName)
        {
            var eventdetails = MediaRelationship.GetMediaRelationshipByGuid(MediaGuid, PageType.EventsID);
            if (eventdetails.Count > 0) return;

            var pages = GetDefaultEvent(eventName);

            if (pages.Count == 0) return;

            ucInputPageReference.PageLink = pages[0].PageLink;
        }

        internal void SetDefaultSubscription(string subscriptionName)
        {
            var eventdetails = MediaRelationship.GetMediaRelationshipByGuid(MediaGuid, PageType.SubscriptionID);
            if (eventdetails.Count > 0) return;

            var pages = GetDefaultSubscription(subscriptionName);

            if (pages.Count == 0) return;

            ucInputPageReference.PageLink = pages[0].PageLink;
        }

        private static System.Collections.Generic.List<PageData> GetDefaultEvent(string eventName)
        {
            //Check if the performanceName (folder name)  maps to any performance
            var criterias = new PropertyCriteriaCollection
                                {
                                    //Find pages of a specific page type (Performance)
                                    new PropertyCriteria()
                                        {
                                            Name = "PageTypeID",
                                            Condition = CompareCondition.Equal,
                                            Required = true,
                                            Type = PropertyDataType.PageType,
                                            Value = PageType.EventsID.ToString(CultureInfo.InvariantCulture)
                                        },
                                };

            // Search pages under the start page
            var pages = DataFactory.Instance.FindPagesWithCriteria(
                ContentReference.StartPage,
                criterias);

            return pages.Where(p => p.PageName.Replace("'", string.Empty).Equals(eventName)).ToList();
        }

        private static System.Collections.Generic.List<PageData> GetDefaultSubscription(string subscriptionName)
        {

            var criterias = new PropertyCriteriaCollection
                                {
                                    new PropertyCriteria()
                                        {
                                            Name = "PageTypeID",
                                            Condition = CompareCondition.Equal,
                                            Required = true,
                                            Type = PropertyDataType.PageType,
                                            Value = PageType.SubscriptionID.ToString(CultureInfo.InvariantCulture)
                                        },
                                };

            var pages = DataFactory.Instance.FindPagesWithCriteria(ContentReference.StartPage, criterias);
            return pages.Where(p => p.PageName.Replace("'", string.Empty).Equals(subscriptionName)).ToList();
        }
    }
}
