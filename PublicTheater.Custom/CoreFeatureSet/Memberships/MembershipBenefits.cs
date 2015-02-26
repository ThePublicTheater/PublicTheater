using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Adage.Tessitura;

namespace PublicTheater.Custom.CoreFeatureSet.Memberships
{
    [Serializable]
    [Adage.Tessitura.Common.CacheClass]
    public class MembershipBenefits : List<MembershipBenefit>
    {
        protected virtual bool Filled { get; set; }

        /// <summary>
        /// Performance Id from Tessitura
        /// </summary>
        public virtual int PerformanceId { get; protected set; }

        /// <summary>
        /// MOS the pricing is being retrieved for
        /// </summary>
        public virtual short forMOS { get; protected set; }

        /// <summary>
        /// Session the benefits being loaded for
        /// </summary>
        public virtual string forSession { get; protected set; }

        public virtual void Fill(int performanceId, string sessionKey, short mos)
        {
            PerformanceId = performanceId;
            forMOS = mos;
            forSession = sessionKey;

            if (forMOS == 0)
            {
                forMOS = TessSession.GetSession().ModeOfSale;
            }
            if (string.IsNullOrEmpty(sessionKey))
            {
                forSession = TessSession.GetSession().SessionKey;
            }

            lock (this)
            {
                if (forMOS == Helper.MembershipHelper.MembershipMOS || forMOS == Helper.MembershipHelper.PartnersMOS)
                {
                    var sqlParameters = new Dictionary<string, object>();
                    sqlParameters.Add("@sessionkey", forSession);
                    sqlParameters.Add("@perf_no", performanceId);
                    sqlParameters.Add("@customer_no", 0);
                    sqlParameters.Add("@debug_mode", "N");
                    try
                    {
                        DataSet benefits = TessSession.GetSession().ExecuteLocalProcedure(Common.PublicAppSettings.MembershipBenefitSP, sqlParameters);
                        // add applicable performances
                        if (benefits != null && (benefits.Tables.Count != 0 && benefits.Tables[0].Rows.Count != 0))
                        {
                            foreach (DataRow eachRow in benefits.Tables[0].Rows)
                            {
                                var membershipBenefit = new MembershipBenefit();
                                membershipBenefit.Fill(eachRow);
                                this.Add(membershipBenefit);
                            }
                        }
                    }
                    catch (Exception exception)
                    {
                        Adage.Common.ElmahCustomError.CustomError.LogError(exception, "LoadMembershipBenefits", null);
                    }
                }
            }
            Filled = true;
        }

        public static MembershipBenefits GetMembershipBenefits(int performanceId)
        {
            var sessionKey = Repository.Get<TessSession>().SessionKey;
            var mos = Repository.Get<TessSession>().ModeOfSale;
            var membershipBenefits = Repository.Get<MembershipBenefits>(performanceId, sessionKey, mos);
            if(!membershipBenefits.Filled)
            {
                membershipBenefits.Fill(performanceId,sessionKey,mos);
            }

            return membershipBenefits;
        }
    }
}
