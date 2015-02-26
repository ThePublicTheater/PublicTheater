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
    public class DTBenefitPriceType
    {
        public virtual int PriceTypeId { get; protected set; }
        public virtual string PriceTypeName { get; protected set; }
        public virtual string Description { get; protected set; }

        protected internal virtual void Fill(DataRow row)
        {
            PriceTypeId = row.GetColumnValue("price_type_id", 0);
            PriceTypeName = row.GetColumnValue("price_type_name", string.Empty);
            Description = row.GetColumnValue("description", string.Empty);
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
