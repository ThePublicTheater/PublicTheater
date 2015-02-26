using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheaterTemplate.Shared.EpiServerConfig;

namespace PublicTheater.Custom.CoreFeatureSet.EpiServerConfig
{
    public class PublicTicketHistoryConfig : TicketHistoryConfig
    {
        public bool PastPerformanceShowEarliestFirst { get; protected set; }

        protected override void FillFromLock(EPiServer.Core.PageData historyPage)
        {
            base.FillFromLock(historyPage);
            PastPerformanceShowEarliestFirst = GetBoolValue(historyPage, "PastPerformanceShowEarliestFirst");
        }
    }
}
