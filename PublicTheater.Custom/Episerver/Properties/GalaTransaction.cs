using System;
using System.Collections.Generic;
using System.Linq;
using Adage.EpiServer.Library.IteraMultiproperty;
using EPiServer.Core;
using EPiServer.SpecializedProperties;
using EPiServer;
using EPiServer.PlugIn;
using EPiServer.Framework.DataAnnotations;
using PublicTheater.Custom.Episerver.Pages;

namespace PublicTheater.Custom.Episerver.Properties
{
    [Serializable]
    [PropertyDefinitionTypePlugIn(DisplayName = "Gala Level Item")]
    [EditorHint("PropertyMulitBase")]
    public class GalaLevelItem : AdagePropertySingleBase
    {
        protected const string PROPERTY_NAME_TITLE = "Title";
        protected const string PROPERTY_NAME_AMOUNT = "Level Amount";
        protected const string PROPERTY_NAME_CHARITABLE_DONATION = "Charitable";
        protected const string PROPERTY_NAME_DESCRIPTION = "Description";
        protected const string PROPERTY_NAME_CSI_PAGE = "CSI";
        protected const string PROPERTY_NAME_FUND = "Fund no";
        protected const string PROPERTY_NAME_ON_ACCT = "On Account Id";
        protected const string PROPERTY_NAME_DISABLED = "Disabled";

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
                                {PROPERTY_NAME_TITLE , new PropertyString()},
                                {PROPERTY_NAME_AMOUNT , new PropertyNumber()},
                                {PROPERTY_NAME_CHARITABLE_DONATION , new PropertyString()},
                                {PROPERTY_NAME_DESCRIPTION , new PropertyXhtmlString() },
                                {PROPERTY_NAME_CSI_PAGE , new PropertyPageReference()},
                                {PROPERTY_NAME_FUND , new PropertyNumber()},
                                {PROPERTY_NAME_ON_ACCT , new PropertyNumber()},
                                {PROPERTY_NAME_DISABLED , new PropertyBoolean()},
                            };
                }
                return _innerPropertyCollection;
            }
            set { _innerPropertyCollection = value; }
        }

        #region Accessors

        public string Title
        {
            get { return (PropertyCollection[PROPERTY_NAME_TITLE].Value as string) ?? ""; }
            set { PropertyCollection[PROPERTY_NAME_TITLE].Value = value; }
        }
        public decimal LevelAmount
        {
            get
            {
                decimal levelAmount = 0;
                if (!decimal.TryParse(PropertyCollection[PROPERTY_NAME_AMOUNT].Value.ToString(), out levelAmount))
                    levelAmount = 0;

                return levelAmount;
            }
        }
        public string CharitableDonation
        {
            get { return (PropertyCollection[PROPERTY_NAME_CHARITABLE_DONATION].Value as string) ?? ""; }
            set { PropertyCollection[PROPERTY_NAME_CHARITABLE_DONATION].Value = value; }
        }

        public string Description
        {
            get { return ((PropertyCollection[PROPERTY_NAME_DESCRIPTION].Value as XhtmlString) ?? new XhtmlString("")).ToHtmlString(); }
            set { PropertyCollection[PROPERTY_NAME_DESCRIPTION].Value = value; }
        }

        public PageReference CsiPage
        {
            get { return PropertyCollection[PROPERTY_NAME_CSI_PAGE].Value as PageReference; }
            set { PropertyCollection[PROPERTY_NAME_CSI_PAGE].Value = value; }
        }

        public int FundNo
        {
            get
            {
                int fundNo = 0;
                try
                {
                    if (!int.TryParse(PropertyCollection[PROPERTY_NAME_FUND].Value.ToString(), out fundNo))
                        fundNo = 0;
                }
                catch (Exception)
                {

                    return fundNo;
                }

                return fundNo;
               
            }
        }
        public int OnAcctId
        {
            get
            {
                int OnAcctId = 0;
                if (!int.TryParse(PropertyCollection[PROPERTY_NAME_ON_ACCT].Value.ToString(), out OnAcctId))
                    OnAcctId = 0;

                return OnAcctId;
            }
        }

        public bool Disabled
        {
            get { return (PropertyCollection[PROPERTY_NAME_DISABLED].Value != null && ((bool)PropertyCollection[PROPERTY_NAME_DISABLED].Value)); }
            set { PropertyCollection[PROPERTY_NAME_DISABLED].Value = value; }
        }
        #endregion
    }

    [Serializable]
    [PropertyDefinitionTypePlugIn(DisplayName = "Gala Level Group")]
    [EditorHint("PropertyMulitBase")]
    public class GalaLevelGroup : AdagePropertyMulitBase
    {
        protected const string TITLE = "Title";
        protected const string REPEATED_PROPERTY_NAME = "Gala Levels";

        public override AdagePropertyMulitBaseSettings Settings
        {
            get
            {
                AdagePropertyMulitBaseSettings settings = new AdagePropertyMulitBaseSettings(REPEATED_PROPERTY_NAME);
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
                    baseProperties = new PropertyDataCollection
                                         {
                                             { TITLE, new PropertyString() }, 
                                            { REPEATED_PROPERTY_NAME, new GalaLevelItem() }    ,
                                         };
                }
                return baseProperties;
            }
        }

        public string Title
        {
            get { return (PropertyCollection[0].Value as string) ?? ""; }
            set { PropertyCollection[0].Value = value; }
        }

        /// <summary>
        /// Returns the internal collection of GalaLevelItem as a list.
        /// </summary>
        public List<GalaLevelItem> GalaLevelList
        {
            get
            {
                return this.GetTypedCollection<GalaLevelItem>().ToList();
            }
        }
    }

    [Serializable]
    [PropertyDefinitionTypePlugIn(DisplayName = "Gala Level Groups")]
    [EditorHint("PropertyMulitBase")]
    public class GalaLevelGroups : AdagePropertyMulitBase
    {
        protected const string REPEATED_PROPERTY_NAME = "Gala Level Groups";

        public override AdagePropertyMulitBaseSettings Settings
        {
            get
            {
                AdagePropertyMulitBaseSettings settings = new AdagePropertyMulitBaseSettings(REPEATED_PROPERTY_NAME);
                settings.EnableXMLEditFeature = true;
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
                    baseProperties = new PropertyDataCollection { { REPEATED_PROPERTY_NAME, new GalaLevelGroup() } };
                }
                return baseProperties;
            }
        }


        /// <summary>
        /// Returns the internal collection of GalaLevelGroup as a list.
        /// </summary>
        public List<GalaLevelGroup> GalaLevelGroupList
        {
            get
            {
                return this.GetTypedCollection<GalaLevelGroup>().ToList();
            }
        }
    }
}