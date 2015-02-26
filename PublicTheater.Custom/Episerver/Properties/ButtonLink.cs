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
    public class ButtonLink : AdagePropertySingleBase
    {
        protected const string PROPERTY_NAME_Text = "Text";
        protected const string PROPERTY_NAME_LINK_URL = "Link Url";

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
                                {PROPERTY_NAME_Text, new PropertyString()},
                                {PROPERTY_NAME_LINK_URL , new PropertyUrl()},
                            };
                }
                return _innerPropertyCollection;
            }
            set { _innerPropertyCollection = value; }
        }

        #region Accessors
       

        public string LinkUrl
        {
            get { return (PropertyCollection[PROPERTY_NAME_LINK_URL].Value as Url ?? new Url("")).ToString(); }
            set { PropertyCollection[PROPERTY_NAME_LINK_URL].Value = value; }
        }

        public string LinkText
        {
            get { return (PropertyCollection[PROPERTY_NAME_Text].Value as string) ?? ""; }
            set { PropertyCollection[PROPERTY_NAME_Text].Value = value; }
        }

        #endregion
    }

    [Serializable]
    [PropertyDefinitionTypePlugIn(DisplayName = "Link List")]
    [EditorHint("PropertyMulitBase")]
    public class ButtonLinks : AdagePropertyMulitBase
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
                    baseProperties = new PropertyDataCollection { { RepeaterPropertyName, new ButtonLink() } };
                }
                return baseProperties;
            }
        }

        /// <summary>
        /// Returns the internal collection of ButtonLink items as a list.
        /// </summary>
        public List<ButtonLink> ButtonLinkList
        {
            get
            {
                return this.GetTypedCollection<ButtonLink>().ToList();
            }
        }
    }
}