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
    public class AccordionItem : AdagePropertySingleBase
    {
        protected const string PROPERTY_NAME_Heading = "Heading";
        protected const string PROPERTY_NAME_Content = "Content";

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
                                {PROPERTY_NAME_Heading, new PropertyString()},
                                {PROPERTY_NAME_Content , new PropertyXhtmlString()},
                            };
                }
                return _innerPropertyCollection;
            }
            set { _innerPropertyCollection = value; }
        }

        #region Accessors
       

        public string Content
        {
            get { return (PropertyCollection[PROPERTY_NAME_Content].Value as XhtmlString ?? new XhtmlString("")).ToHtmlString(); }
        }

        public string Heading
        {
            get { return (PropertyCollection[PROPERTY_NAME_Heading].Value as string) ?? ""; }
            set { PropertyCollection[PROPERTY_NAME_Heading].Value = value; }
        }

        #endregion
    }

    [Serializable]
    [PropertyDefinitionTypePlugIn(DisplayName = "Accordion Items")]
    [EditorHint("PropertyMulitBase")]
    public class AccordionItems : AdagePropertyMulitBase
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
                    baseProperties = new PropertyDataCollection { { RepeaterPropertyName, new AccordionItem() } };
                }
                return baseProperties;
            }
        }

        /// <summary>
        /// Returns the internal collection of AccordionItem as List.
        /// </summary>
        public List<AccordionItem> AccordionItemList
        {
            get
            {
                return this.GetTypedCollection<AccordionItem>().ToList();
            }
        }
    }
}