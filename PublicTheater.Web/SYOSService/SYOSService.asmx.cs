using System.Text;
using System.Web.Services;
using Adage.HtmlSyos.EPiServer.Code;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System;
using System.Linq;
using Adage.Tessitura;
using Adage.Tessitura.CartObjects;
using Adage.Tessitura.Constants;
using EPiServer;
using EPiServer.Core;
using TheaterTemplate.Shared.Common;
using TheaterTemplate.Shared.EpiServerConfig;
using TheaterTemplate.Web.SYOSService;
using PublicTheater.Custom.Syos;
using System.Xml.Serialization;

namespace Adage.HtmlSyos.Code
{
    /// <summary>
    /// Summary description for SYOSService
    /// </summary>
    [WebService(Namespace = "http://syos.adagetechnologies.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class SYOSService : TheaterSharedSYOSService
    {
        [WebMethod(EnableSession = true)]
        public override LevelInformation GetLevelInformation(string rootConfigNumber, string itemNumber, string configNumber, string showAll, string syosType)
        {
            var levelInformation = base.GetLevelInformation(rootConfigNumber, itemNumber, configNumber, showAll, syosType);
            return levelInformation;
        }

        [XmlInclude(typeof(PublicSYOSRootData))]
        public override SYOSRootData GetRootData(string dataId)
        {
            return PublicSYOSRootData.GetSYOSRootData(dataId);
        }

        protected override void ReservePerformanceSeats(Tessitura.Performance performance, IEnumerable<IGrouping<string, BaseSYOSService.SeatRequestInfo>> groupedSeats, Tessitura.ReserveRequest seatRequest)
        {
            base.ReservePerformanceSeats(performance, groupedSeats, seatRequest);
        }

        protected override void CheckForAdaRequest(int lineItemId, IEnumerable<IGrouping<string, BaseSYOSService.SeatRequestInfo>> groupedSeats)
        {
            //flush cached cart, since it does not have the cart item yet
            Cart.Flush();
            base.CheckForAdaRequest(lineItemId,groupedSeats);
        }
        
    }
}