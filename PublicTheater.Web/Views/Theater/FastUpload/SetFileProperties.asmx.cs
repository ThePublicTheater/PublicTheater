using System;
using System.Web;
using System.Collections.Generic;
using System.Web.Hosting;
using System.Web.Services;
using System.IO;
using System.Linq;
using EPiServer.Security;
using EPiServer.Web.Hosting;
using System.Collections;
using Adage.Theater.RelationshipManager.Utilities;
using System.Web.Script.Serialization;
using Adage.Theater.RelationshipManager;
using PublicTheater.Web.Views.Theater.Pages;

namespace AdageTheaterGroup.Web.FastUpload
{
    /// <summary>
    /// Summary description for SetFileProperities
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class SetFileProperties : System.Web.Services.WebService
    {

        [WebMethod]
        public string UpdateEpiProperties(string fileName, string properties)
        {
            //updateEpiProperities(HttpContext.Current, fileName, properties);
            var currentFile = ((VersioningFile)GetCurrentFile(fileName));
            if (currentFile == null)
                return "Error: File was uploaded but the properties were not set.";
            String guid = currentFile.Guid.ToString();

            JavaScriptSerializer Serializer = new JavaScriptSerializer();
            Dictionary<string, object> dictJson = Serializer.Deserialize<Dictionary<string, object>>(properties);
            var json = dictJson["jsonData"];

            return MediaManager.AddMedia(guid, json as Dictionary<String, object>);
        }

        protected virtual Hashtable ParseProperties(string properties)
        {
            Hashtable results = new Hashtable();
            foreach (string eachValue in properties.Split("&".ToArray(), StringSplitOptions.RemoveEmptyEntries))
            {
                string[] currentValue = eachValue.Split("=".ToArray(), StringSplitOptions.RemoveEmptyEntries);

                if (currentValue == null || currentValue.Length != 2)
                    continue;

                if (results.ContainsKey(currentValue[0]))
                    results[currentValue[0]] = results[currentValue[0]].ToString() + "," + currentValue[1];
                else
                    results.Add(currentValue[0], HttpUtility.UrlDecode(currentValue[1]));
            }

            return results;
        }

        protected virtual void updateEpiProperities(HttpContext context, string fullFilename, string jsonData)
        {
            var request = context.Request;
            var response = context.Response;

            var currentFile = GetCurrentFile(fullFilename);

            // ignore for file systems that can't persist additional info
            if (currentFile.Summary.CanPersist == false)
                return;



            currentFile.Summary.SaveChanges();
        }

        protected virtual void UpdateProperty(UnifiedFile currentFile, DictionaryEntry eachItem)
        {
            switch (eachItem.Key.ToString().ToLower())
            {
                case "title":
                    currentFile.Summary.Title = eachItem.Value.ToString();
                    break;

                case "subject":
                    currentFile.Summary.Subject = eachItem.Value.ToString();
                    break;

                case "keywords":
                    currentFile.Summary.Keywords = eachItem.Value.ToString();
                    break;

                case "author":
                    currentFile.Summary.Author = eachItem.Value.ToString();
                    break;

                case "category":
                    currentFile.Summary.Category = eachItem.Value.ToString();
                    break;

                case "comments":
                    currentFile.Summary.Comments = eachItem.Value.ToString();
                    break;

                default:
                    currentFile.Summary.Dictionary[eachItem.Key] = eachItem.Value.ToString();
                    break;
            }
        }

        protected virtual UnifiedFile GetCurrentFile(string fileName)
        {
            UnifiedFile tempFile = null;
            fileName = fileName.Replace("//", "/");

            if (HostingEnvironment.VirtualPathProvider.FileExists(fileName))
                tempFile = HostingEnvironment.VirtualPathProvider.GetFile(fileName) as UnifiedFile;

            if (tempFile == null)
                throw new ArgumentException("Could not find the file");


            // Checkout file
            IVersioningFile versioningFile;
            if (tempFile.TryAsVersioningFile(out versioningFile))
            {
                if (!versioningFile.IsCheckedOut)
                    versioningFile.CheckOut();
            }

            return tempFile;
        }
    }
}
