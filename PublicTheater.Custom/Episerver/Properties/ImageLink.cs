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
    public class ImageLink : AdagePropertySingleBase
    {
        protected const string PROPERTY_NAME_IMAGE = "Image";
        protected const string PROPERTY_NAME_LINK_URL = "Link Url";
        protected const string PROPERTY_NAME_TITLE = "Title";


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
                                {PROPERTY_NAME_LINK_URL , new PropertyUrl()},
                                {PROPERTY_NAME_TITLE, new PropertyString()},
                            };
                }
                return _innerPropertyCollection;
            }
            set { _innerPropertyCollection = value; }
        }

        #region Accessors
        public string ImageUrl
        {
            get
            {
                try
                {
                    return (PropertyCollection[PROPERTY_NAME_IMAGE].Value as Url ?? new Url("")).ToString();
                }
                catch (Exception)
                {
                    return string.Empty;
                }
            }
            set { PropertyCollection[PROPERTY_NAME_IMAGE].Value = value; }
        }

        public string LinkUrl
        {
            get { return (PropertyCollection[PROPERTY_NAME_LINK_URL].Value as Url ?? new Url("")).ToString(); }
            set { PropertyCollection[PROPERTY_NAME_LINK_URL].Value = value; }
        }

        public string Title
        {
            get { return (PropertyCollection[PROPERTY_NAME_TITLE].Value as string) ?? ""; }
            set { PropertyCollection[PROPERTY_NAME_TITLE].Value = value; }
        }

        #endregion
    }

    [Serializable]
    [PropertyDefinitionTypePlugIn(DisplayName = "Image Link List")]
    [EditorHint("PropertyMulitBase")]
    public class ImageLinks : AdagePropertyMulitBase
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
                    baseProperties = new PropertyDataCollection { { RepeaterPropertyName, new ImageLink() } };
                }
                return baseProperties;
            }
        }

        /// <summary>
        /// Returns the internal collection of ImageBody items as a list.
        /// </summary>
        public List<ImageLink> ImageLinkList
        {
            get
            {
                return this.GetTypedCollection<ImageLink>().ToList();
            }
        }
    }
}