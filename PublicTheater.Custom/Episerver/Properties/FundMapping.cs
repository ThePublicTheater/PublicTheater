using System;
using System.Collections.Generic;
using System.Linq;
using Adage.EpiServer.Library.IteraMultiproperty;
using EPiServer.Core;
using EPiServer.SpecializedProperties;
using EPiServer;
using EPiServer.PlugIn;
using EPiServer.Framework.DataAnnotations;
using Itera.Examples.Property;

namespace PublicTheater.Custom.Episerver.Properties
{
    [Serializable]
    public class FundMapping : AdagePropertySingleBase
    {
        protected const string PROPERTY_NAME_Text = "Fund ID";
        protected const string PROPERTY_NAME_LINK_URL = "Start Level";

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
                                {PROPERTY_NAME_Text, new PropertyNumber()},
                                {PROPERTY_NAME_LINK_URL , new PropertyNumber()},
                            };
                }
                return _innerPropertyCollection;
            }
            set { _innerPropertyCollection = value; }
        }

        #region Accessors
       

        public int FundID
        {
             get { int max = 0;
             if (PropertyCollection[PROPERTY_NAME_Text].Value != null)
                 int.TryParse(PropertyCollection[PROPERTY_NAME_Text].Value.ToString(), out max);
                return max;
            }
            set { PropertyCollection[PROPERTY_NAME_Text].Value = value; }
        }

        public int StartLevel
        {
            get
            {
                int max = 0;
                if (PropertyCollection[PROPERTY_NAME_LINK_URL].Value != null)
                    int.TryParse(PropertyCollection[PROPERTY_NAME_LINK_URL].Value.ToString(), out max);
                return max;
            }
            set { PropertyCollection[PROPERTY_NAME_LINK_URL].Value = value; }
        }

        #endregion
    }

    [Serializable]
    [PropertyDefinitionTypePlugIn(DisplayName = "Fund Mapping")]
    [EditorHint("PropertyMulitBase")]
    public class FundMappings : AdagePropertyMulitBase
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
                    baseProperties = new PropertyDataCollection { { RepeaterPropertyName, new FundMapping() } };
                }
                return baseProperties;
            }
        }

        /// <summary>
        /// Returns the internal collection of ButtonLink items as a list.
        /// </summary>
        public List<FundMapping> FundMappingList
        {
            get
            {
                return this.GetTypedCollection<FundMapping>().ToList();
            }
        }
    }
}