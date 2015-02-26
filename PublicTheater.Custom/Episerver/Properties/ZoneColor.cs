using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Itera.Property;
using EPiServer.Core;
using EPiServer.SpecializedProperties;
using PublicTheater.Custom.Syos;
using EPiServer.PlugIn;
using Adage.EpiServer.Library.IteraMultiproperty;
using EPiServer.Framework.DataAnnotations;

namespace PublicTheater.Custom.Episerver.Properties
{
    [Serializable]
    public class ZoneColor : AdagePropertySingleBase
    {
        protected const string PROPERTY_NAME_ZONE_DESCRIP = "Zone";
        protected const string PROPERTY_NAME_COLOR_HEX = "ZoneColorHex";
        protected const string PROPERTY_NAME_LEGENDTEXT = "LegendText";
        protected const string PROPERTY_NAME_SHOWLEGENDTEXT = "ShowInLegend";

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
                                {PROPERTY_NAME_ZONE_DESCRIP, new PropertyString()},
                                {PROPERTY_NAME_COLOR_HEX, new PropertyString()},
                                {PROPERTY_NAME_LEGENDTEXT, new PropertyString()},
                                {PROPERTY_NAME_SHOWLEGENDTEXT, new PropertyBoolean()}
                            };
                }
                return _innerPropertyCollection;
            }
            set { _innerPropertyCollection = value; }
        }

        public bool ShowInLegend
        {
            get { return (PropertyCollection[PROPERTY_NAME_SHOWLEGENDTEXT].Value == null ? false : ((bool)PropertyCollection[PROPERTY_NAME_SHOWLEGENDTEXT].Value)); }
            set { PropertyCollection[PROPERTY_NAME_SHOWLEGENDTEXT].Value = value; }
        }

        public string Zone
        {
            get { return (PropertyCollection[PROPERTY_NAME_ZONE_DESCRIP].Value as string) ?? ""; }
            set { PropertyCollection[PROPERTY_NAME_ZONE_DESCRIP].Value = value; }
        }

        public string ZoneColorHex
        {
            get { return (PropertyCollection[PROPERTY_NAME_COLOR_HEX].Value as string) ?? ""; }
            set { PropertyCollection[PROPERTY_NAME_COLOR_HEX].Value = value; }
        }

        public string LegendText
        {
            get { return (PropertyCollection[PROPERTY_NAME_LEGENDTEXT].Value as string) ?? ""; }
            set { PropertyCollection[PROPERTY_NAME_LEGENDTEXT].Value = value; }
        }

        public ZoneColorConfig GetZoneColorConfig()
        {
            return new ZoneColorConfig()
            {
                ZoneDescription = Zone,
                ZoneColor = ZoneColorHex,
                ZoneLegendText = LegendText,
                ShowInLegend = ShowInLegend
            };
        }
    }

    [Serializable]
    [PropertyDefinitionTypePlugIn(DisplayName = "Zone Color List")]
    [EditorHint("PropertyMulitBase")]
    public class ZoneColorList : AdagePropertyMulitBase
    {
        protected const string REPEATED_PROPERTY_NAME = "Zone Color";

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
                    baseProperties = new PropertyDataCollection { { REPEATED_PROPERTY_NAME, new ZoneColor() } };
                }
                return baseProperties;
            }
        }

        public List<ZoneColor> ZoneColors
        {
            get
            {
                return this.GetTypedCollection<ZoneColor>().ToList();
            }
        }
    }
}