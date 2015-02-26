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
    public class HeroImage : AdagePropertySingleBase
    {
        protected const string PROPERTY_NAME_IMAGE = "Image Url";
        protected const string PROPERTY_NAME_TITLE = "Show Title";
        protected const string PROPERTY_NAME_RUN_TIME = "Show Time";
        protected const string PROPERTY_NAME_TICKETS = "Tickets Link";
        protected const string PROPERTY_NAME_DETAILS = "Details Link";

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
                                {PROPERTY_NAME_TITLE, new PropertyString()},
                                {PROPERTY_NAME_RUN_TIME, new PropertyString()},
                                {PROPERTY_NAME_TICKETS, new PropertyUrl()},
                                {PROPERTY_NAME_DETAILS, new PropertyUrl()},
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

        public string ShowTitle
        {
            get { return (PropertyCollection[PROPERTY_NAME_TITLE].Value as string) ?? ""; }
            set { PropertyCollection[PROPERTY_NAME_TITLE].Value = value; }
        }
        
        public string ShowTime
        {
            get { return (PropertyCollection[PROPERTY_NAME_RUN_TIME].Value as string) ?? ""; }
            set { PropertyCollection[PROPERTY_NAME_RUN_TIME].Value = value; }
        }

        public string TicketsLink
        {
            get { return (PropertyCollection[PROPERTY_NAME_TICKETS].Value as Url ?? new Url("")).ToString(); }
            set { PropertyCollection[PROPERTY_NAME_TICKETS].Value = value; }
        }

        public string DetailsLink
        {
            get { return (PropertyCollection[PROPERTY_NAME_DETAILS].Value as Url ?? new Url("")).ToString(); }
            set { PropertyCollection[PROPERTY_NAME_DETAILS].Value = value; }
        }
        #endregion
    }

    [Serializable]
    [PropertyDefinitionTypePlugIn(DisplayName = "Hero Image List")]
    [EditorHint("PropertyMulitBase")]
    public class HeroImages : AdagePropertyMulitBase
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
                    baseProperties = new PropertyDataCollection { { RepeaterPropertyName, new HeroImage() } };
                }
                return baseProperties;
            }
        }

        /// <summary>
        /// Returns the internal collection of HeroImage items as a list.
        /// </summary>
        public List<HeroImage> HeroImageList
        {
            get
            {
                return this.GetTypedCollection<HeroImage>().ToList();
            }
        }
    }
}