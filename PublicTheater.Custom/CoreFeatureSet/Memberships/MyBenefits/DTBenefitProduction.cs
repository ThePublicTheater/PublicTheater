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
    public class DTBenefitProduction
    {
        public virtual int ProductionSeasonId { get; protected set; }
        public virtual int PriceTypeId{ get; protected set; }
        public virtual int Used{ get; protected set; }
        public virtual int Remaining { get; protected set; }

        protected internal virtual void Fill(DataRow row)
        {
            ProductionSeasonId = row.GetColumnValue("prod_season_id", 0);
            PriceTypeId = row.GetColumnValue("price_type_id", 0);
            Used = row.GetColumnValue("used", -1);
            Remaining = row.GetColumnValue("remaining", -1);
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
