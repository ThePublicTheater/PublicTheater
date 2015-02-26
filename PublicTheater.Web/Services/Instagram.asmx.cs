using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace PublicTheater.Web.Services
{
    /// <summary>
    /// Summary description for Instagram
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class Instagram : System.Web.Services.WebService
    {
        public class instagramPhotoItem
        {
            public string img;
            public string caption;

        }
        protected static instagramPhotoItem MediaExtractor(JToken media)
                {
                    var img = "";
                    var caption = "";
                    try
                    {
                        img = media["images"]["standard_resolution"]["url"].ToString();
                        caption = media["caption"]["text"].ToString();
                    }
                    catch (Exception)
                    {
                    }
                    return new instagramPhotoItem { img = img, caption = caption };
                }

       [WebMethod(CacheDuration = 300)]
        public static string GetPhotosJSON(int userId)
       {
           return JsonConvert.SerializeObject(GetPhotosArray(userId));

        }
       [WebMethod(CacheDuration = 300)]
       public static instagramPhotoItem[] GetPhotosArray(int userId)
       {
           try
           {
               WebClient client = new WebClient();
               string json =
                   client.DownloadString("https://api.instagram.com/v1/users/" + userId +
                                         "/media/recent/?access_token=1327738706.fa3308e.d551bf0e851d4c46a0ac82cc35132e95&count=15");

               JObject jsonData = JObject.Parse(json);

               var t = from media in jsonData["data"]
                       where media["type"].ToString() == "image"
                       select
                           MediaExtractor(media);
               var y = t.Take(5).ToArray();
               
               return y;
           }
           catch (Exception)
           {

               return new instagramPhotoItem[0];

           }

       }
        [WebMethod(CacheDuration = 300)]
       public static string GetPhotosByUserName(string userName)
        {
            try
            {
                WebClient client = new WebClient();
                string json =
                    client.DownloadString("https://api.instagram.com/v1/users/search?q=" + userName +
                                          "&access_token=1327738706.fa3308e.d551bf0e851d4c46a0ac82cc35132e95");

                JObject jsonData = JObject.Parse(json);

                var t = from users in jsonData["data"]
                    select users["id"].ToString();
                var y = t.ToArray().First();

                return GetPhotosJSON(int.Parse(y));
            }
            catch (Exception)
            {
                return GetPhotosJSON(0);
            }
        }
        [WebMethod(CacheDuration = 300)]
        public static instagramPhotoItem[] GetPhotosByUserNameArray(string userName)
        {
            try
            {
                WebClient client = new WebClient();
                string json =
                    client.DownloadString("https://api.instagram.com/v1/users/search?q=" + userName +
                                          "&access_token=1327738706.fa3308e.d551bf0e851d4c46a0ac82cc35132e95");

                JObject jsonData = JObject.Parse(json);

                var t = from users in jsonData["data"]
                        select users["id"].ToString();
                var y = t.ToArray().First();

                return GetPhotosArray(int.Parse(y));
            }
            catch (Exception)
            {
                return GetPhotosArray(0);
            }
        }
    }
}
