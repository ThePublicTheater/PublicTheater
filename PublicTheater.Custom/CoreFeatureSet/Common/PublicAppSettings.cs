using TheaterTemplate.Shared.Common;

namespace PublicTheater.Custom.CoreFeatureSet.Common
{
    public class PublicAppSettings : TheaterSharedAppSettings
    {

        /// <summary>
        /// Public Theater perf_type id(s)
        /// </summary>
        public const string PerfType_Default = "PerfType_Default";

        /// <summary>
        /// Joes Pub perf_type id(s)
        /// </summary>
        public const string PerfType_JoesPub = "PerfType_JoesPub";

        /// <summary>pricet
        /// Shakespear in the park perf_type id(s)
        /// </summary>
        public const string PerfType_Shakespeare = "PerfType_Shakespeare";

        /// <summary>
        /// Under the Radar perf_type id(s)
        /// </summary>
        public const string PerfType_UnderTheRadar = "PerfType_UnderTheRadar";

        /// <summary>
        /// Default days to show on subhome pages
        /// </summary>
        public const string PERFORMANCE_LIST_DAYS_TO_SHOW = "PERFORMANCE_LIST_DAYS_TO_SHOW";

        /// <summary>
        /// minimum amount qualifies for partners benefits
        /// </summary>
        public const string PartnersMinAmount = "PartnersMinAmount";

        /// <summary>
        /// minimum amount qualifies for young partners benefits
        /// </summary>
        public const string YoungPartnersMinAmount = "YoungPartnersMinAmount";

        /// <summary>
        /// partners benefits MOS
        /// </summary>
        public const string PartnersMOS = "PartnersMOS";

        /// <summary>
        /// membership benefits mos
        /// </summary>
        public const string MembershipMOS = "MembershipMOS";

        /// <summary>
        /// $150 summer ticket benefit no
        /// </summary>
        public const string BenefitNo_150SummerTicket = "BenefitNo_150SummerTicket";

        /// <summary>
        /// member price ticket benefit no
        /// </summary>
        public const string BenefitNo_MemberPrice = "BenefitNo_MemberPrice";

        /// <summary>
        /// 15% off production benefit no
        /// </summary>
        public const string BenefitNo_15OffProduction = "BenefitNo_15OffProduction";

        /// <summary>
        /// memebership item unit price
        /// </summary>
        public const string MembershipUnitPrice = "MembershipUnitPrice";

        /// <summary>
        /// membership item fee unit price
        /// </summary>
        public const string MembershipUnitFee = "MembershipUnitFee";
        
        /// <summary>
        /// membership custom fee id in tessitura
        /// </summary>
        public const string MembershipFeeId = "MembershipFeeId";

        /// <summary>
        /// Stored procedure id to call after purchase to grant immediate membership benefits
        /// </summary>
        public const string MembershipUpdateSP = "MembershipUpdateSP";


        /// <summary>
        /// PartnerTicketCSI Page ID
        /// </summary>
        public const string PartnerTicketCSI = "PartnerTicketCSI";


        /// <summary>
        /// Ticket history page url
        /// </summary>
        public const string Ticket_History_URL = "Ticket_History_URL";

        /// <summary>
        /// Stored procedure to retrieve member benefits status
        /// </summary>
        public const string MembershipBenefitSP = "MembershipBenefitSP";

        /// <summary>
        /// NYMagazine Attribute Keyword Number
        /// </summary>
        public const string NYMagazine_KeyworkNumber = "NYMagazine_KeywordNumber";


        /// <summary>
        /// NYMagazine Attribute Keyword Number
        /// </summary>
        public const string Mail2RESTAPIKey = "Mail2RESTAPIKey";

        /// <summary>
        /// NYMagazine Attribute Keyword Number
        /// </summary>
        public const string Mail2RESTAPIUrl = "Mail2RESTAPIUrl";


        /// <summary>
        /// 
        /// </summary>
        public const string MembershipRankType = "MembershipRankType";

        /// <summary>
        /// 
        /// </summary>
        public const string MembershipStartRank = "MembershipStartRank";

        /// <summary>
        /// 
        /// </summary>
        public const string MembershipEndRank = "MembershipEndRank";

        /// <summary>
        /// Partner rank type id
        /// </summary>
        public const string PartnerRankType = "PartnerRankType";

        /// <summary>
        /// Partner rank start
        /// </summary>
        public const string PartnerStartRank = "PartnerStartRank";

        /// <summary>
        /// Partner rank end
        /// </summary>
        public const string PartnerEndRank = "PartnerEndRank";


        /// <summary>
        /// SummerSupporter rank type id
        /// </summary>
        public const string SummerSupporterRankType = "SummerSupporterRankType";

        /// <summary>
        /// SummerSupporter rank start
        /// </summary>
        public const string SummerSupporterStartRank = "SummerSupporterStartRank";

        /// <summary>
        /// SummerSupporter rank end
        /// </summary>
        public const string SummerSupporterEndRank = "SummerSupporterEndRank";

        /// <summary>
        /// Membership dialog config page id
        /// </summary>
        public const string MembershipConfigPageId = "MembershipConfigPageId";

        /// <summary>
        /// partner tickets payment method id
        /// </summary>
        public const string PartnersPaymentMethodID = "PartnersPaymentMethodID";

        /// <summary>
        /// User Billing Address Type ID in Tessitura
        /// </summary>
        public const string BillingAddressTypeID = "BillingAddressTypeID";

        /// <summary>
        /// ticket limit content type id for performances
        /// </summary>
        public const string TicketLimitContentTypeID = "TicketLimitContentTypeID";

        /// <summary>
        /// Order Notes CSI session key
        /// </summary>
        public const string OrderNotesCSISessionKey = "OrderNotesCSISessionKey";

        /// <summary>
        /// Order Note CSI page ID
        /// </summary>
        public const string OrderNotesCSIPageID = "OrderNotesCSIPageID";

        /// <summary>
        /// Member Price Type ID, to display member price on PDP
        /// </summary>
        public const string MemberPriceTypeID = "MemberPriceTypeID";

        /// <summary>
        /// Door Price Type ID, to display door price on PDP
        /// </summary>
        /// 
        public const string DoorPriceTypeID = "DoorPriceTypeID";

        /// <summary>
        /// Web Full Price Type ID, to display full price on PDP
        /// </summary>
        public const string WebFullPriceTypeID = "WebFullPriceTypeID";

        /// <summary>
        /// Benefit Restriction SP
        /// </summary>
        public const string BenefitRestrictionSP = "BenefitRestrictionSP";

        /// <summary>
        /// My Benefit Summary SP
        /// </summary>
        public const string MyBenefitSummarySP = "MyBenefitSummarySP";

        /// <summary>
        /// Email Lookup Stored Procedure
        /// </summary>
        public const string EmailLookupSP = "EmailLookupSP";

        /// <summary>
        /// Package Voucher PaymentMethodId Stored Procedure
        /// </summary>
        public const string PackageVoucherPaymentMethodSP = "PackageVoucherPaymentMethodSP";

        /// <summary>
        /// ID of EPiServer page containing a key-value list of package renames
        /// </summary>
        public const string PackageRenameConfigId = "PackageRenameConfigId";
    }
}
