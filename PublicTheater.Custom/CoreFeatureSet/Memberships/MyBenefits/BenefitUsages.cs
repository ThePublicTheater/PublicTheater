using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace PublicTheater.Custom.CoreFeatureSet.Memberships.MyBenefits
{
    [Serializable]
    [Adage.Tessitura.Common.CacheClass]
    public class BenefitUsages : List<BenefitUsage>
    {
        public virtual void Fill(DataTable table)
        {
            lock (this)
            {
                this.Clear();
                foreach (DataRow eachRow in table.Rows)
                {
                    var item = new BenefitUsage();
                    item.Fill(eachRow);
                    this.Add(item);
                }
            }
        }
    }
}
