using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using Adage.Tessitura;
using Adage.Tessitura.CartObjects;
using PublicTheater.Custom.CoreFeatureSet.Helper;
using PublicTheater.Custom.CoreFeatureSet.PerformanceObjects;

namespace PublicTheater.Web.Services
{
    /// <summary>
    /// Summary description for PublicWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class PublicWebService : System.Web.Services.WebService
    {

        public struct CartExpire
        {
            public int ItemsInCart;
            public bool CanExpire;
            public string CartTimer;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public CartExpire GetCartExpiration()
        {
            var cartExpire = new CartExpire();

            try
            {
                var cart = Cart.GetCart(); ;
                cartExpire.ItemsInCart = cart.CartLineItems.Count;
                cartExpire.CanExpire = cart.CartLineItems.Any(i => !(i is GiftCertificateLineItem || i is ContributionLineItem));
                cartExpire.CartTimer = PublicObjectHelper.GetCartTimer();
            }
            catch (Exception ex)
            {
                //cart expired would generate error when call cart object. Need to only catch cart expired exception
                cartExpire.ItemsInCart = 0;
                cartExpire.CanExpire = true;
                cartExpire.CartTimer = PublicObjectHelper.GetCartTimer();
            }
            return cartExpire;
        }

        [WebMethod]
        public void ImportJoesPubContent(int rootPageId, int quantity = 0)
        {
            Custom.DataImportHelper.CreateArchivePages(rootPageId, quantity);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetFlexAvailablePerformances(int productionSeasonNumber, int flexPackageNumber)
        {
            var flexPackage = (PublicFlexPackage)FlexPackage.GetFlexPackage(flexPackageNumber);
            var serializer = new JavaScriptSerializer();
            return serializer.Serialize(flexPackage.GetAvailablePerformances(productionSeasonNumber).Select(perf => perf.PerformanceId));
        }
    }
}
