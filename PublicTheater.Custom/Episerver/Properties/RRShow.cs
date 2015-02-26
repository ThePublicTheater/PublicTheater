using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Adage.EpiServer.Library.IteraMultiproperty;
using EPiServer;
using EPiServer.Core;
using EPiServer.Framework.DataAnnotations;
using EPiServer.PlugIn;
using EPiServer.SpecializedProperties;
using EPiServer.Web;

namespace PublicTheater.Custom.Episerver.Properties
{
    [Serializable]
    public class ResourceRoomItem : AdagePropertySingleBase
    {

        protected const string PROPERTY_NAME_IMAGE = "Image";
        protected const string PROPERTY_NAME_MEDIA = "Other Media";
        protected const string PROPERTY_NAME_OPTIONS = "Options";
        protected const string PROPERTY_NAME_SCRIPT = "Script";

        PropertyDataCollection _innerPropertyCollection;
        protected override PropertyDataCollection InnerPropertyCollection
        {
            get
            {
                if (_innerPropertyCollection == null)
                {
                    _innerPropertyCollection =
                       new PropertyDataCollection
                            {
                                {PROPERTY_NAME_IMAGE, new PropertyImageUrl()},
                                {PROPERTY_NAME_MEDIA, new PropertyString()},
                                {PROPERTY_NAME_OPTIONS, new PropertyString()},
                                {PROPERTY_NAME_SCRIPT, new PropertyString()},
                                
                            };
                    
                }
                return _innerPropertyCollection;
            }
            set { _innerPropertyCollection = value; }
        }



        #region Accessors
        public string Image
        {

            get { return (PropertyCollection[PROPERTY_NAME_IMAGE].Value as Url ?? new Url("")).ToString(); }
            set { PropertyCollection[PROPERTY_NAME_IMAGE].Value = value; }
        }

        public string Media
        {
            get { return (PropertyCollection[PROPERTY_NAME_MEDIA].Value as string) ?? ""; }
            set { PropertyCollection[PROPERTY_NAME_MEDIA].Value = value; }
        }

        public string Options
        {
            get { return (PropertyCollection[PROPERTY_NAME_OPTIONS].Value as string) ?? ""; }
            set { PropertyCollection[PROPERTY_NAME_OPTIONS].Value = value; }
        }
        public string Script
        {
            get { return (PropertyCollection[PROPERTY_NAME_SCRIPT].Value as string) ?? ""; }
            set { PropertyCollection[PROPERTY_NAME_SCRIPT].Value = value; }
        }
        #endregion
    }

    [Serializable]
    [PropertyDefinitionTypePlugIn(DisplayName = "ResourceRoomItem List")]
    [EditorHint("PropertyMulitBase")]
    public class ResourceRoomItems : AdagePropertyMulitBase
    {
        string RepeaterPropertyName = "Item";

        public override AdagePropertyMulitBaseSettings Settings
        {
            get
            {
                AdagePropertyMulitBaseSettings settings = new AdagePropertyMulitBaseSettings(RepeaterPropertyName);
                settings.EnableXMLEditFeature = false;
                return settings;
            }
        }
        
        PropertyDataCollection baseProperties;
        public override PropertyDataCollection BasePropertys
        {
            get
            {
                if (baseProperties == null)
                {
                    baseProperties = new PropertyDataCollection { { RepeaterPropertyName, new ResourceRoomItem() } };
                    
                }
                return baseProperties;
            }
        }

        /// <summary>
        /// Returns the internal collection of ResourceRoomItem items as a list.
        /// </summary>
        public List<ResourceRoomItem> ResourceRoomItemList
        {
            get
            {
                return this.GetTypedCollection<ResourceRoomItem>().ToList();
            }
        }
    }
}


