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
    public class AvailableProductions : List<int>
    {
        public virtual void Fill(DataTable table)
        {
            lock (this)
            {
                this.Clear();
                foreach (DataRow eachRow in table.Rows)
                {
                    var item = eachRow.GetColumnValue("prod_season_id", 0);
                    this.Add(item);
                }
            }
        }
    }
}
