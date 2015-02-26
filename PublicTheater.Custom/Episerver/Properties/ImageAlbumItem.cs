using System;
using System.Collections.Generic;
using System.Linq;
using Adage.EpiServer.Library.IteraMultiproperty;
using EPiServer.Core;
using EPiServer.SpecializedProperties;
using EPiServer;
using EPiServer.PlugIn;
using EPiServer.Framework.DataAnnotations;

namespace PublicTheater.Custom.Episerver.Properties
{
    [Serializable]
    public class ImageAlbumItem : AdagePropertySingleBase
    {
        protected const string PROPERTY_NAME_IMAGE_LINK = "Image Url";
        protected const string PROPERTY_NAME_TITLE = "Title";
        protected const string PROPERTY_NAME_DESCRIPTION = "Description";


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
                                {PROPERTY_NAME_IMAGE_LINK, new PropertyImageUrl()},
                                {PROPERTY_NAME_TITLE , new PropertyString()},
                                {PROPERTY_NAME_DESCRIPTION, new PropertyXhtmlString()},
                            };
                }
                return _innerPropertyCollection;
            }
            set { _innerPropertyCollection = value; }
        }

        #region Accessors
        public string ImageUrl
        {
            get { return (PropertyCollection[PROPERTY_NAME_IMAGE_LINK].Value as Url ?? new Url("")).ToString(); }
            set { PropertyCollection[PROPERTY_NAME_IMAGE_LINK].Value = value; }
        }

        public XhtmlString Description
        {
            get { return (PropertyCollection[PROPERTY_NAME_DESCRIPTION].Value as XhtmlString) ?? new XhtmlString(); }
            set { PropertyCollection[PROPERTY_NAME_DESCRIPTION].Value = value; }
        }

        public string Title
        {
            get { return (PropertyCollection[PROPERTY_NAME_TITLE].Value as string) ?? ""; }
            set { PropertyCollection[PROPERTY_NAME_TITLE].Value = value; }
        }

        #endregion
    }

    [Serializable]
    [PropertyDefinitionTypePlugIn(DisplayName = "Image Item List")]
    [EditorHint("PropertyMulitBase")]
    public class ImageAlbumItems : AdagePropertyMulitBase
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
                    baseProperties = new PropertyDataCollection { { RepeaterPropertyName, new ImageAlbumItem() } };
                }
                return baseProperties;
            }
        }

        /// <summary>
        /// Returns the internal collection of ImageAlbumItem items as a list.
        /// </summary>
        public List<ImageAlbumItem> ImageAlbumItemList
        {
            get
            {
                return this.GetTypedCollection<ImageAlbumItem>().ToList();
            }
        }
    }
}