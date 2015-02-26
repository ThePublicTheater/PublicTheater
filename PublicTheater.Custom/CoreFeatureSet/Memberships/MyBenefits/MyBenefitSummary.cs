using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using Adage.Tessitura;

namespace PublicTheater.Custom.CoreFeatureSet.Memberships.MyBenefits
{
    [Serializable]
    [Adage.Tessitura.Common.CacheClass]
    public class MyBenefitSummary
    {
        /// <summary>
        /// general membership status info
        /// </summary>
        public MembershipStatus MembershipStatus { protected set; get; }

        /// <summary>
        /// Downtown total complimentary benefits, only filled for partners program
        /// </summary>
        public BenefitUsages DTBenefits { protected set; get; }

        /// <summary>
        /// Pending Downtown total complimentary benefits, only filled for partners program
        /// </summary>
        public BenefitUsages PendingDTBenefits { protected set; get; }

        /// <summary>
        /// Downtown benefits price types
        /// </summary>
        public DTBenefitPriceTypes DTBenefitPriceTypes { protected set; get; }

        /// <summary>
        /// Pending Downtown benefits price types
        /// </summary>
        public DTBenefitPriceTypes PendingDTBenefitPriceTypes { protected set; get; }

        /// <summary>
        /// Downtown per production price type benefit details
        /// </summary>
        public DTBenefitProductions DTBenefitProductions { protected set; get; }

        /// <summary>
        /// Pending Downtown per production price type benefit details
        /// </summary>
        public DTBenefitProductions PendingDTBenefitProductions { protected set; get; }

        /// <summary>
        /// Shakespeare in the part benefits overview
        /// </summary>
        public BenefitUsages SIPTBenefits { protected set; get; }

        /// <summary>
        /// Pending Shakespeare in the part benefits overview
        /// </summary>
        public BenefitUsages PendingSIPTBenefits { protected set; get; }

        /// <summary>
        /// A list of productions can redeem benefits from
        /// </summary>
        public AvailableProductions AvailableProductions { protected set; get; }

        /// <summary>
        /// Filled from Stored Procedure
        /// </summary>
        protected virtual bool Filled { get; set; }

        /// <summary>
        /// Customer No
        /// </summary>
        public virtual int CustomerNo { get; protected set; }

        /// <summary>
        /// Session key
        /// </summary>
        public virtual string SessionKey { get; protected set; }

        /// <summary>
        /// Has membership benefits
        /// </summary>
        public bool ShowBenefitsMessage
        {
            get
            {
                return MembershipStatus.StatusBar
                       && MembershipStatus.ContentMessageId != ContentMessages.LapsedAndInactive.ToString()
                       && MembershipStatus.ContentMessageId != ContentMessages.BoardAndPC.ToString()
                       && MembershipStatus.ContentMessageId != ContentMessages.SuspendedAndCancelled.ToString()
                       && MembershipStatus.ContentMessageId != ContentMessages.STB.ToString();
            }
        }

        /// <summary>
        /// display membership status bar or not
        /// </summary>
        public bool ShowStatusBar
        {
            get
            {
                return MembershipStatus.StatusBar;
            }
        }

        public virtual void Fill(int customerNo, string sessionKey)
        {
            CustomerNo = customerNo;
            SessionKey = sessionKey;

            lock (this)
            {
                var sqlParameters = new Dictionary<string, object>();
                sqlParameters.Add("@customer_no", customerNo);
                sqlParameters.Add("@sessionkey", sessionKey);

                try
                {
                    DataSet benefits = TessSession.GetSession().ExecuteLocalProcedure(Common.PublicAppSettings.MyBenefitSummarySP, sqlParameters);
                    if (benefits != null && benefits.Tables.Count > 0)
                    {
                        MembershipStatus = Repository.GetNew<MembershipStatus>();
                        MembershipStatus.Fill(benefits.Tables[0]);

                        DTBenefits = Repository.GetNew<BenefitUsages>();
                        DTBenefits.Fill(benefits.Tables[1]);

                        DTBenefitPriceTypes = Repository.GetNew<DTBenefitPriceTypes>();
                        DTBenefitPriceTypes.Fill(benefits.Tables[2]);

                        DTBenefitProductions = Repository.GetNew<DTBenefitProductions>();
                        DTBenefitProductions.Fill(benefits.Tables[3]);

                        SIPTBenefits = Repository.GetNew<BenefitUsages>();
                        SIPTBenefits.Fill(benefits.Tables[4]);

                        AvailableProductions = Repository.GetNew<AvailableProductions>();
                        AvailableProductions.Fill(benefits.Tables[5]);

                        PendingDTBenefits = Repository.GetNew<BenefitUsages>();
                        PendingDTBenefits.Fill(benefits.Tables[6]);

                        PendingDTBenefitPriceTypes = Repository.GetNew<DTBenefitPriceTypes>();
                        PendingDTBenefitPriceTypes.Fill(benefits.Tables[7]);

                        PendingDTBenefitProductions = Repository.GetNew<DTBenefitProductions>();
                        PendingDTBenefitProductions.Fill(benefits.Tables[8]);

                        PendingSIPTBenefits = Repository.GetNew<BenefitUsages>();
                        PendingSIPTBenefits.Fill(benefits.Tables[9]);





                    }
                }
                catch (Exception exception)
                {
                    Adage.Common.ElmahCustomError.CustomError.LogError(exception, "LoadMyBenefitSummary", null);
                }
            }
            Filled = true;
        }

        /// <summary>
        /// Get benefits restriction for customer
        /// </summary>
        /// <param name="customerNo"></param>
        /// <returns></returns>
        public static MyBenefitSummary GetMyBenefitSummary(int customerNo)
        {
            var myBenefitSummary = Repository.Get<MyBenefitSummary>(customerNo);
            var sessionKey = Adage.Tessitura.TessSession.GetSessionKey();
            if (!myBenefitSummary.Filled)
            {
                myBenefitSummary.Fill(customerNo, sessionKey);
            }
            return myBenefitSummary;
        }

        public enum ContentMessages
        {
            [Description("Single Ticket Buyers")]
            STB,

            [Description("Board And Producer’s Circle")]
            BoardAndPC,

            [Description("Lapsed And Inactive")]
            LapsedAndInactive,

            [Description("Suspended And Cancelled")]
            SuspendedAndCancelled,

            [Description("Renewed")]
            Renewed
        }
    }









}
