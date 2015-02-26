using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Core;
using EPiServer.Web;
using EPiServer;
using PublicTheater.Custom.Episerver.BaseClasses;
using TheaterTemplate.Shared.EpiServerProperties;

namespace PublicTheater.Custom.Episerver.Pages
{
    [ContentType(GUID = "893d6cf9-8f67-4b63-be0f-fee63aaa32d7", DisplayName = "[Public Theater] Donate Now", GroupName = Constants.ContentGroupNames.ContributeGroupName)]
    public class DonatePageData : PublicBasePageData
    {
        [Display(Name = "Default Fund", GroupName = SystemTabNames.Content, Description = "")]
        [BackingType(typeof(DonationFundItem))]
        public virtual string Funds { get; set; }

        [Display(Name = "Default Membership", GroupName = SystemTabNames.Content, Description = "Default membership level to show")]
        public virtual PageReference DefaultMembership { get; set; }

        [UIHint(UIHint.Image)]
        [Display(GroupName = SystemTabNames.Content, Name = "Banner Image 1135*215", Order = 0)]
        public virtual Url BannerImage { get; set; }

        [UIHint(UIHint.Textarea)]
        [Display(GroupName = SystemTabNames.Content, Name = "Banner Image Text", Order = 0)]
        public virtual string BannerImageText { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Main Body", Order = 0)]
        public virtual XhtmlString MainBody { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Right Column Block Area")]
        public virtual ContentArea RightContent { get; set; }

        public DonationFundItem DefaultDonationFund
        {
            get
            {
                return Property["Funds"] as DonationFundItem;
            }
        }

        private List<MembershipLevelItemList> _defaultMembershipLevelItemList;
        public List<MembershipLevelItemList> DefaultMembershipLevelItemList
        {
            get
            {
                if (_defaultMembershipLevelItemList == null)
                {
                    _defaultMembershipLevelItemList = new List<MembershipLevelItemList>();
                    if (DefaultMembership != null && DefaultMembership.ID > 0)
                    {
                        var membershipPageData = DataFactory.Instance.GetPage(DefaultMembership);
                        var membershipLevelsListProp = membershipPageData.Property["MembershipLevelItems"] as MembershipLevelItemListList;
                        if (membershipLevelsListProp != null)
                        {
                            _defaultMembershipLevelItemList.AddRange(membershipLevelsListProp.PropertyCollection
                                .Where(property => property.GetType() == typeof(MembershipLevelItemList)).Cast<MembershipLevelItemList>());
                        }
                    }
                }

                return _defaultMembershipLevelItemList;
            }
        }

    }
}