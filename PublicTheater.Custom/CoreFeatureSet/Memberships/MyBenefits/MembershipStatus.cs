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
    public class MembershipStatus
    {
        public virtual string ProgramName { get; protected set; }
        public virtual string ProgramLevel { get; protected set; }
        public virtual DateTime ExpDate { get; protected set; }
        public virtual string Status { get; protected set; }
        public virtual bool AllowRenew { get; protected set; }
        public virtual string RenewalLevel { get; protected set; }
        public virtual DateTime RenewalStartDate { get; protected set; }
        public virtual string ContentMessageId { get; protected set; }
        public virtual bool StatusBar { get; protected set; }

        protected internal virtual void Fill(DataTable table)
        {
            if (table != null && table.Rows.Count > 0)
            {
                var row = table.Rows[0];
                ProgramName = row.GetColumnValue("program_name", string.Empty);
                ProgramLevel = row.GetColumnValue("program_level", string.Empty);
                ExpDate = DataSetExtensions.GetDateTimeWithOffset(row, "exp_date");
                Status = row.GetColumnValue("status", string.Empty);
                AllowRenew = row.GetColumnValue("allow_renew", "N").ToUpper().Equals("Y");
                RenewalLevel = row.GetColumnValue("renewal_level", string.Empty);
                RenewalStartDate = DataSetExtensions.GetDateTimeWithOffset(row, "renewal_start_date");
                ContentMessageId = row.GetColumnValue("content_message_id", string.Empty);
                StatusBar = row.GetColumnValue("status_bar", "N").ToUpper().Equals("Y");
            }
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
