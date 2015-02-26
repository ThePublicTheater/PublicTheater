using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Adage.Tessitura;

namespace PublicTheater.Custom.CoreFeatureSet.PerformanceObjects
{
    [Serializable, Adage.Tessitura.Common.CacheClass]
    [DataContract]
    public class PublicProduction : Production
    {
        /// <summary>
        /// Production's season no.
        /// </summary>
        [DataMember]
        public virtual int SeasonNo { get; protected set; }

        /// <summary>
        /// Production's season desc.
        /// </summary>
        [DataMember]
        public virtual string SeasonDescription { get; protected set; }

        /// <summary>
        /// Fill from productions object
        /// </summary>
        /// <param name="currentProduction"></param>
        /// <param name="dataSet"></param>
        protected override void FillFromProductions(System.Data.DataRow currentProduction, System.Data.DataSet dataSet)
        {
            base.FillFromProductions(currentProduction, dataSet);

            SeasonNo = currentProduction.GetColumnValue("season_no", 0);

            SeasonDescription = currentProduction.GetColumnValue("season_desc", string.Empty);
             
        }

        /// <summary>
        /// fill from production details object
        /// </summary>
        /// <param name="dataSet"></param>
        protected override void Fill(System.Data.DataSet dataSet)
        {
            base.Fill(dataSet);

            if (_performances.Count > 0)
            {
                var perf = _performances.First();
                SeasonNo = perf.SeasonId;
                SeasonDescription = perf.SeasonDescription;
            }
        }
    }
}
