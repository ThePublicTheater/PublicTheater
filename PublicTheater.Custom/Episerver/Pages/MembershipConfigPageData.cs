using System.ComponentModel.DataAnnotations;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Core;
using EPiServer.Web;
using EPiServer;
using PublicTheater.Custom.Episerver.BaseClasses;

namespace PublicTheater.Custom.Episerver.Pages
{
    [ContentType(GUID = "2fface8c-758b-4ab1-a029-3bdc3bacb304", DisplayName = "[Public Theater] Membership Dialog Config", GroupName = Constants.ContentGroupNames.ConfigurationGroupName)]
    public class MembershipConfigPageData : PageData
    {

        #region member partner MOS check
        
        [Display(GroupName = "Member MOS Check", Name = "Member Mos Check Enabled")]
        public virtual bool MemberMosCheckEnabled { get; set; }

        [Display(GroupName = "Member MOS Check", Name = "Member Mos Check Message")]
        public virtual XhtmlString MemberMosCheckMessage { get; set; }

        [Display(GroupName = "Member MOS Check", Name = "Member Mos Check Confirm Button")]
        public virtual string MemberMosCheckConfirmButton { get; set; }
        #endregion


        #region Summer Supporter Check

        [Display(GroupName = "Cart Duplication Check", Name = "Cart Duplication Check Enabled")]
        public virtual bool SummerSupportersMemberCheckEnabled{ get; set; }

        [Display(GroupName = "Cart Duplication Check", Name = "Cart Duplication Check Message")]
        public virtual XhtmlString SummerSupportersCheckMessage { get; set; }
        
        [Display(GroupName = "Cart Duplication Check", Name = "Cart Duplication Check Confirm Button")]
        public virtual string SummerSupportersCheckConfirmaButton { get; set; }
        #endregion

        #region Benefit Restrictions
        [Display(GroupName = "Program Cart Restrictions", Name = "Program Cart Restrictions Enabled")]
        public virtual bool BenefitRestrictionsEnabled { get; set; }

        [Display(GroupName = "Program Cart Restrictions", Name = "Program Cart Restrictions Message")]
        public virtual XhtmlString BenefitRestrictionsMessage { get; set; }

        [Display(GroupName = "Program Cart Restrictions", Name = "Program Cart Restrictions Confirm Button")]
        public virtual string BenefitRestrictionsConfirmButton { get; set; }

        #endregion

    }
}