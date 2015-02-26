using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Adage.Tessitura;

namespace PublicTheater.Custom.CoreFeatureSet.Memberships
{
    [Serializable]
    //[Adage.Tessitura.Common.CacheClass]
    public class MembershipBenefit
    {
        public virtual int ProductSeasonID { get; protected set; }
        public virtual int PriceTypeID { get; protected set; }
        public virtual int AvailableCount { get; protected set; }
        public virtual int PaymentMethodID { get; protected set; }
        public virtual bool AvailableForLevel { get; protected set; }

        public virtual bool MaskPrice
        {
            get { return PaymentMethodID == Helper.MembershipHelper.PartnersPaymentMethodID; }
        }

        public virtual decimal MaskPriceValue
        {
            get { return 0m; }
        }

        public override string ToString()
        {
            return string.Format("ProductSeasonID:{0}, PriceTypeID:{1}, AvailableCount:{2}, PaymentMethodID:{3}", ProductSeasonID, PriceTypeID, AvailableCount, PaymentMethodID);
        }

        protected internal virtual void Fill(DataRow row)
        {
            ProductSeasonID = row.GetColumnValue("prod_season_no", 0);
            PriceTypeID = row.GetColumnValue("price_type", 0);
            AvailableCount = row.GetColumnValue("avail", 0);
            PaymentMethodID = row.GetColumnValue("pmt_method", 0);
            AvailableForLevel = row.GetColumnValue("avail_for_level", "Y").ToUpper().Equals("Y");
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }


}
