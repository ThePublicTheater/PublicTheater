using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace PublicTheater.Custom.CoreFeatureSet.Memberships.MyBenefits
{
    [Serializable]
    [Adage.Tessitura.Common.CacheClass]
    public class DTBenefitPriceTypes : List<DTBenefitPriceType>
    {
        public virtual void Fill(DataTable table)
        {
            lock (this)
            {
                this.Clear();
                foreach (DataRow eachRow in table.Rows)
                {
                    var item = new DTBenefitPriceType();
                    item.Fill(eachRow);
                    this.Add(item);
                }
            }
        }
    }
}
