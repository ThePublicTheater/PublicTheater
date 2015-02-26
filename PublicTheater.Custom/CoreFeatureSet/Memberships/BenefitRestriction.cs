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
    public class BenefitRestriction
    {
        public virtual int FundId { get; protected set; }
        public virtual double StartLevel { get; protected set; }
        public virtual double EndLevel { get; protected set; }


        public override string ToString()
        {
            return string.Format("Fund Id:{0}, Start Level:{1}, End Level:{2}", FundId, StartLevel, EndLevel);
        }

        protected internal virtual void Fill(DataRow row)
        {
            FundId = row.GetColumnValue("fund_id", 0);
            StartLevel = row.GetColumnValue("start_amt", 0d);
            EndLevel = row.GetColumnValue("end_amt", 0d);
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }


}
