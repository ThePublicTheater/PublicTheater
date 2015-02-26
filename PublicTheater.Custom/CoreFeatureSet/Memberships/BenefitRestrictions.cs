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
    public class BenefitRestrictions : List<BenefitRestriction>
    {
        protected virtual bool Filled { get; set; }

        /// <summary>
        /// Customer No
        /// </summary>
        public virtual int CustomerNo { get; protected set; }


        public virtual void Fill(int customerNo)
        {
            CustomerNo = customerNo;

            lock (this)
            {

                var sqlParameters = new Dictionary<string, object>();
                sqlParameters.Add("@customer_no", customerNo);

                try
                {
                    DataSet benefits = TessSession.GetSession().ExecuteLocalProcedure(Common.PublicAppSettings.BenefitRestrictionSP, sqlParameters);
                    // add applicable performances
                    if (benefits != null && (benefits.Tables.Count != 0 && benefits.Tables[0].Rows.Count != 0))
                    {
                        foreach (DataRow eachRow in benefits.Tables[0].Rows)
                        {
                            var benefitRestriction = new BenefitRestriction();
                            benefitRestriction.Fill(eachRow);
                            this.Add(benefitRestriction);
                        }
                    }
                }
                catch (Exception exception)
                {
                    Adage.Common.ElmahCustomError.CustomError.LogError(exception, "LoadBenefitRestriction", null);
                }

            }
            Filled = true;
        }

        /// <summary>
        /// Get benefits restriction for customer
        /// </summary>
        /// <param name="customerNo"></param>
        /// <returns></returns>
        public static BenefitRestrictions GetBenefitRestrictions(int customerNo)
        {
            var benefitRestrictions = Repository.Get<BenefitRestrictions>(customerNo);
            if (!benefitRestrictions.Filled)
            {
                benefitRestrictions.Fill(customerNo);
            }
            return benefitRestrictions;
        }
    }
}
