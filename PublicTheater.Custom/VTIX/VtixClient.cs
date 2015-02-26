using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PublicTheater.Custom.CoreFeatureSet.Common;
using PublicTheater.Custom.VtixService;
using Adage.Tessitura;

namespace PublicTheater.Custom.VTIX
{
    public static class VtixClient
    {
        public static bool insertEntry(string name, string address, int lineType, string email)
        {
            VtixService.VTIXSoap clientSoap = new VTIXSoapClient();
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string sessionKey = TessSession.GetSessionKey();
            string ipAddress = GetIPAddress();
            return clientSoap.InsertVTIXEntry(sessionKey, lineType, ipAddress, name, address, email);

            //req.Body.inputAddress = address;
            //req.Body.inputName = name;
            //req.Body.ipAddress = GetIPAddress();
            //req.Body.sessionKey = GetIPAddress();
            //var response=clientSoap.InsertVTIXEntry(req);
            //return response.Body.InsertVTIXEntryResult;
        }
        private static string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;

            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }
    }
}
