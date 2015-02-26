using System;
using System.Collections.Generic;
using System.Linq;
using Adage.EpiServer.Library.IteraMultiproperty;
using EPiServer.Core;
using EPiServer.SpecializedProperties;
using EPiServer;
using EPiServer.PlugIn;
using EPiServer.Framework.DataAnnotations;
using EPiServer.DataAnnotations;

namespace PublicTheater.Custom.Episerver.Properties
{
    [Serializable]
    [PropertyDefinitionTypePlugIn(DisplayName = "Media Box Item")]
    [EditorHint("PropertySingleBase")]
    public class MediaBoxItem : AdagePropertySingleBase
    {
        protected const string PROPERTY_NAME_IMAGE = "Image";
        protected const string PROPERTY_NAME_LINK_URL = "Link Url";
        protected const string PROPERTY_NAME_CAPTION = "Caption";
        protected const string PROPERTY_NAME_ALBUM = "Album Name";
        

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
                                {PROPERTY_NAME_IMAGE, new PropertyUrl()},
                                {PROPERTY_NAME_LINK_URL , new PropertyUrl()},
                                {PROPERTY_NAME_CAPTION, new PropertyString()},
                                {PROPERTY_NAME_ALBUM, new PropertyString()}
                            };
                }
                return _innerPropertyCollection;
            }
            set { _innerPropertyCollection = value; }
        }

        #region Accessors
        public string ImageUrl
        {
            get { var result = (PropertyCollection[PROPERTY_NAME_IMAGE].Value as Url ?? new Url("")).ToString();
            if (result.Equals(""))
            {
                result = Utility.Utility.getDefaultCoverImage(this.LinkUrl);
                
                PropertyCollection[PROPERTY_NAME_IMAGE].Value = new Url(result);
            }
            return result;
                }
            set { PropertyCollection[PROPERTY_NAME_IMAGE].Value = value; }
        }

        public string LinkUrl
        {
            get { return (PropertyCollection[PROPERTY_NAME_LINK_URL].Value as Url ?? new Url("")).ToString(); }
            set { 
                PropertyCollection[PROPERTY_NAME_LINK_URL].Value = value;
            //if(this.ImageUrl == null){
                //this.ImageUrl = Utility.Utility.getDefaultCoverImage(value);
            }
            
        }
        public string Caption
        {
            get { return (PropertyCollection[PROPERTY_NAME_CAPTION].Value as String); }
            set { PropertyCollection[PROPERTY_NAME_CAPTION].Value = value; }

        }
        public string Album
        {
            get { return (PropertyCollection[PROPERTY_NAME_ALBUM].Value as String); }
            set { PropertyCollection[PROPERTY_NAME_ALBUM].Value = value; }

        }

        #endregion
    }

    [Serializable]
    [PropertyDefinitionTypePlugIn(DisplayName = "Media Box Item List")]
    [EditorHint("PropertyMulitBase")]
    public class MediaBoxItems : AdagePropertyMulitBase
    {
        private const string RepeaterPropertyName = "Item";

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
                    baseProperties = new PropertyDataCollection { { RepeaterPropertyName, new MediaBoxItem() } };
                }
                return baseProperties;
            }
        }

        /// <summary>
        /// Returns the internal collection of ImageBody items as a list.
        /// </summary>
        public List<MediaBoxItem> MediaBoxItemList
        {
            get
            {
                return this.GetTypedCollection<MediaBoxItem>().ToList();
            }
        }
    }
}