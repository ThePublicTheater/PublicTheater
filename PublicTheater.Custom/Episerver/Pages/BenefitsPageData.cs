using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Core;
using EPiServer.Web;
using EPiServer;
using PublicTheater.Custom.Episerver.BaseClasses;

namespace PublicTheater.Custom.Episerver.Pages
{
    [ContentType(GUID = "1246cf87-70bd-4b6c-828f-92d5c8c13499", DisplayName = "[Public Theater] My Benefits", GroupName = Constants.ContentGroupNames.HistoryGroupName)]
    public class BenefitsPageData : PublicBasePageData
    {
        #region Properties

        [Display(GroupName = SystemTabNames.Content, Name = "Renewed Message Format")]
        public virtual XhtmlString RenewedMessageFormat { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "No Downtown Usage Message")]
        public virtual XhtmlString NoDowntownUsage { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "No Upcoming Performances Message")]
        public virtual XhtmlString NoUpcomingPerformancesMessage{ get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Program Content Box")]
        public virtual XhtmlString ProgramContentBox { get; set; }

        

        [BackingType(typeof(TheaterTemplate.Shared.EpiServerProperties.KeyPageList))]
        [Editable(true)]
        [Display(GroupName = SystemTabNames.Content, Name = "Program Renewal Links")]
        public virtual string ProgramRenewalLinks { get; set; }

        #region Content messages

        [Display(GroupName = "Content Messages", Name = "STB")]
        public virtual XhtmlString STB { get; set; }

        [Display(GroupName = "Content Messages", Name = "BoardAndPC")]
        public virtual XhtmlString BoardAndPC { get; set; }

        [Display(GroupName = "Content Messages", Name = "LapsedAndInactive")]
        public virtual XhtmlString LapsedAndInactive { get; set; }

        [Display(GroupName = "Content Messages", Name = "SuspendedAndCancelled")]
        public virtual XhtmlString SuspendedAndCancelled { get; set; }

        [Display(GroupName = "Content Messages", Name = "Renewed")]
        public virtual XhtmlString Renewed { get; set; }
        #endregion

        #endregion


        #region Methods

        /// <summary>
        /// Get program name and renewal url pair
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetProgramRenewalLinks()
        {
            var renewalLinks = new Dictionary<string, string>();
            try
            {
                var propProgramRenewalLinks = this.Property["ProgramRenewalLinks"] as TheaterTemplate.Shared.EpiServerProperties.KeyPageList;
                if (propProgramRenewalLinks == null)
                {
                    return renewalLinks;
                }

                var pageList = propProgramRenewalLinks.GetTypedCollection<TheaterTemplate.Shared.EpiServerProperties.KeyPagePair>().ToList();
                foreach (var keyPagePair in pageList)
                {
                    if (keyPagePair.PageId > 0)
                    {
                        var pageUrl = Episerver.Utility.Utility.GetFriendlyUrl(new ContentReference(keyPagePair.PageId));
                        renewalLinks.Add(keyPagePair.Key, pageUrl);
                    }
                }
            }
            catch (Exception ex)
            {
                Adage.Common.ElmahCustomError.CustomError.LogError(ex, "GetProgramRenewalLinks",null);
            }
            
            return renewalLinks;
        }
        #endregion
        
    }
}