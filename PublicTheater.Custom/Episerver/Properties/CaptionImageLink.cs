using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Adage.EpiServer.Library.IteraMultiproperty;
using EPiServer;
using EPiServer.Core;
using EPiServer.Framework.DataAnnotations;
using EPiServer.PlugIn;
using EPiServer.SpecializedProperties;

namespace PublicTheater.Custom.Episerver.Properties
{
    [Serializable]
    [PropertyDefinitionTypePlugIn(DisplayName = "Captioned Image Link")]
    [EditorHint("PropertyMulitBase")]
    public class CaptionedImageLink : AdagePropertySingleBase
    {
        protected const string PROPERTY_NAME_IMAGE = "Image";
        protected const string PROPERTY_NAME_LINK_URL = "Link Url";
        protected const string PROPERTY_NAME_CAPTION_TITLE = "Caption Title";
        protected const string PROPERTY_NAME_CAPTION_DESCRIPTION = "Caption Description";

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
                                {PROPERTY_NAME_CAPTION_TITLE, new PropertyXhtmlString()},
                                {PROPERTY_NAME_CAPTION_DESCRIPTION, new PropertyXhtmlString()}
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

        public string CaptionTitle
        {
            get { return (PropertyCollection[PROPERTY_NAME_CAPTION_TITLE].Value as XhtmlString ?? new XhtmlString(string.Empty)).ToHtmlString(); }
            set { PropertyCollection[PROPERTY_NAME_CAPTION_TITLE].Value = value; }
        }

        public string CaptionDescription
        {
            get { return (PropertyCollection[PROPERTY_NAME_CAPTION_DESCRIPTION].Value as XhtmlString ?? new XhtmlString(string.Empty)).ToHtmlString(); }
            set { PropertyCollection[PROPERTY_NAME_CAPTION_DESCRIPTION].Value = value; }
        }

        #endregion
    }

    [Serializable]
    [PropertyDefinitionTypePlugIn(DisplayName = "Captioned Image Link List")]
    [EditorHint("PropertyMulitBase")]
    public class CaptionedImageLinks : AdagePropertyMulitBase {

        string RepeatedPropertyName = "Item";

        public override AdagePropertyMulitBaseSettings Settings
        {
            get
            {
                AdagePropertyMulitBaseSettings settings = new AdagePropertyMulitBaseSettings(RepeatedPropertyName);
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
                    baseProperties = new PropertyDataCollection { { RepeatedPropertyName, new CaptionedImageLink() } };
                }
                return baseProperties;
            }
        }

        /// <summary>
        /// Returns the internal collection of ImageBody items as a list.
        /// </summary>
        public List<CaptionedImageLink> CaptionedImageLinkList
        {
            get
            {
                return this.GetTypedCollection<CaptionedImageLink>().ToList();
            }
        }
    }
}
