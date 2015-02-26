using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Adage.Tessitura;
using EPiServer;
using EPiServer.Core;
using TheaterTemplate.Shared.EpiServerConfig;

namespace PublicTheater.Custom.CoreFeatureSet.EpiServerConfig
{
    /// <summary>
    /// MembershipConfig - Contains fields populated from EpiServer that drives the membership settings
    /// </summary>
    [Adage.Tessitura.Common.CacheClass]
    public class MembershipConfig : EpiServerBaseConfigFactory
    {
        /// <summary>
        /// Protected constructor - Sets defaults on fields
        /// </summary>
        protected internal MembershipConfig()
        {
        }

        /// <summary>
        /// Based on the configuration ID passed in, populates/returns a  populated MembershipConfig object 
        /// </summary>
        /// <param name="configurationId">Configuration Id</param>
        /// <returns>Populated Calendar Config object</returns>
        public static MembershipConfig GetMembershipConfig(int configurationId)
        {
            return GetFilledConfig<MembershipConfig>(configurationId);
        }

        /// <summary>
        /// Fills within a lock block
        /// </summary>
        protected override void FillFromLock(PageData page)
        {
            if (page != null)
            {
                CartDuplicationCheckEnabled = GetBoolValue(page, "SummerSupportersMemberCheckEnabled");
                CartDuplicationCheckMessage = GetStringValueFromVisitorGroup(page, "SummerSupportersCheckMessage", false);
                CartDuplicationCheckConfirmaButton = GetStringValue(page, "SummerSupportersCheckConfirmaButton");

                MemberMosCheckEnabled = GetBoolValue(page, "MemberMosCheckEnabled");
                MemberMosCheckMessage = GetStringValueFromVisitorGroup(page, "MemberMosCheckMessage", false);
                MemberMosCheckConfirmButton = GetStringValue(page, "MemberMosCheckConfirmButton");

                BenefitRestrictionsEnabled = GetBoolValue(page, "BenefitRestrictionsEnabled");
                BenefitRestrictionsMessage = GetStringValueFromVisitorGroup(page, "BenefitRestrictionsMessage", false);
                BenefitRestrictionsConfirmButton = GetStringValue(page, "BenefitRestrictionsConfirmButton");

                Filled = true;
            }
        }

        public virtual string MemberMosCheckConfirmButton
        {
            get;
            protected set;
        }

        public virtual bool MemberMosCheckEnabled
        {
            get;
            protected set;
        }
        public virtual string MemberMosCheckMessage
        {
            get;
            protected set;
        }

        public virtual bool CartDuplicationCheckEnabled
        {
            get;
            protected set;
        }
        public virtual string CartDuplicationCheckMessage
        {
            get;
            protected set;
        }

        public virtual string CartDuplicationCheckConfirmaButton
        {
            get;
            protected set;
        }

        public virtual bool BenefitRestrictionsEnabled
        {
            get;
            protected set;
        }
        public virtual string BenefitRestrictionsConfirmButton
        {
            get;
            protected set;
        }
        public virtual string BenefitRestrictionsMessage
        {
            get;
            protected set;
        }


    }
}
