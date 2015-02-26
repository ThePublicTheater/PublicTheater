using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Adage.Theater.RelationshipManager;
using EPiServer;
using EPiServer.Core;

namespace PublicTheater.Web.Views.Theater.Controls
{
    [Adage.Tessitura.Common.CacheClass]
    public partial class MediaPropertyManager : UserControlBase
    {
        public virtual void BindProperties(EPiMedia media, String[] parentFolderPath)
        {
            throw new ApplicationException("Must be overriden");
        }
        protected virtual void InitInputRelationshipsForMedia(String[] parentFolderPath)
        {
            throw new ApplicationException("Must be overriden");
        }

        protected virtual void SetRelationshipTypes(String guid)
        {
            throw new ApplicationException("Must be overriden");
        }
        public virtual void UpdateProperties(HiddenField HfMediaGuid)
        {
            throw new ApplicationException("Must be overriden");
        }
        public virtual void BindParentFolderPath(String[] parentFolderPath)
        {
            throw new ApplicationException("Must be overriden");
        }

        protected virtual void UpdatePropertiesFromJsonDictionary(EPiMedia media, Dictionary<string,object> properties)
        {
            foreach(string key in properties.Keys)
            {
                media.SetPropertyData(key, properties[key].ToString());
            }
        }
        protected virtual void UpdateRelationshipsFromJsonDictionary(EPiMedia media, Dictionary<string, object> relationships)
        {
            foreach(string key in relationships.Keys)
            {
                int PageTypeID = PageType.GetPageTypeIDByDescription(key);
                if (PageTypeID == 0)
                    continue;
                UpdateRelationships(media.EPiMediaGuid, relationships[key].ToString(), PageTypeID);
            }

        }
        protected virtual void UpdateRelationships(Guid EPiMediaGuid, String PageIds, int pageType)
        {

            String[] IdArray = PageIds.Split(',');
            foreach (String id in IdArray)
            {
                if (id == string.Empty)
                    continue;
                Guid selectedPageGuid;
                if (Guid.TryParse(id, out selectedPageGuid))
                {
                    MediaRelationship.UpdateRelationship(EPiMediaGuid, selectedPageGuid, pageType);
                }
                else
                {
                    var selectedPageId = new PageReference(id);
                    var selectedPage = EPiServer.DataFactory.Instance.GetPage(selectedPageId);
                    MediaRelationship.UpdateRelationship(EPiMediaGuid, selectedPage.PageGuid, pageType);
                }
            }
        }

        public virtual String UpdateProperties(EPiMedia media, Dictionary<String,Object> jsonData)
        {
            
            try
            {
                var dictJson = jsonData;
                if(dictJson.ContainsKey("Properties")) UpdatePropertiesFromJsonDictionary(media, (Dictionary<string,object>)dictJson["Properties"]);
                if(dictJson.ContainsKey("Relationships")) UpdateRelationshipsFromJsonDictionary(media, (Dictionary<string, object>)dictJson["Relationships"]);

                AdageTheaterRelationshipManagerEntities.Current.SaveChanges();
            }
            catch (Exception e)
            {
                return e.Message;
            }
            
            return "Media Information Updated";
        }


        protected virtual void InitInputRelationshipsWithoutMedia(string[] parentFolderPath)
        {
            throw new NotImplementedException();
        }
    }


}
