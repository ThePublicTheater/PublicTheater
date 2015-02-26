using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Adage.Tessitura;

namespace PublicTheater.Custom.CoreFeatureSet.Memberships.MyBenefits
{
    
    [Serializable]
    [Adage.Tessitura.Common.CacheClass]
    public class BenefitUsage
    {

        public virtual string Benefit { get; protected set; }
        public virtual int Total { get; protected set; }
        public virtual int Used { get; protected set; }
        public virtual int Remaining { get; protected set; }

        protected internal virtual void Fill(DataRow row)
        {
            Benefit = row.GetColumnValue("benefit", string.Empty);
            Total = row.GetColumnValue("total", 0);
            Used = row.GetColumnValue("used", 0);
            Remaining = row.GetColumnValue("remaining", 0);
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
