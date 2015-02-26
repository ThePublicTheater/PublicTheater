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
    public class VenueLink : AdagePropertySingleBase
    {
        protected const string PROPERTY_NAME_TITLE = "Venue Name";
        protected const string PROPERTY_NAME_IMAGE = "Image";
        protected const string PROPERTY_NAME_ADDRESS = "Address";
        protected const string PROPERTY_NAME_DIRECTIONS = "Directions";
        protected const string PROPERTY_NAME_WHATS_ON = "Whats On";
        


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
                                {PROPERTY_NAME_TITLE, new PropertyString()},
                                {PROPERTY_NAME_IMAGE, new PropertyImageUrl()},
                                {PROPERTY_NAME_ADDRESS, new PropertyString()},
                                {PROPERTY_NAME_DIRECTIONS, new PropertyUrl()},
                                {PROPERTY_NAME_WHATS_ON, new PropertyUrl()},
                            };
                }
                return _innerPropertyCollection;
            }
            set { _innerPropertyCollection = value; }
        }

        #region Accessors
       

        

        public string VenueName
        {
            get { return (PropertyCollection[PROPERTY_NAME_TITLE].Value as string) ?? ""; }
            set { PropertyCollection[PROPERTY_NAME_TITLE].Value = value; }
        }
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
        public string Address
        {
            get { return (PropertyCollection[PROPERTY_NAME_ADDRESS].Value as string) ?? ""; }
            set { PropertyCollection[PROPERTY_NAME_ADDRESS].Value = value; }
        }
        public string DirectionsUrl
        {
            get { return (PropertyCollection[PROPERTY_NAME_DIRECTIONS].Value as Url ?? new Url("")).ToString(); }
            set { PropertyCollection[PROPERTY_NAME_DIRECTIONS].Value = value; }
        }
        public string WhatsOnUrl
        {
            get { return (PropertyCollection[PROPERTY_NAME_WHATS_ON].Value as Url ?? new Url("")).ToString(); }
            set { PropertyCollection[PROPERTY_NAME_WHATS_ON].Value = value; }
        }
        #endregion
    }

    [Serializable]
    [PropertyDefinitionTypePlugIn(DisplayName = "Venue Link List")]
    [EditorHint("PropertyMulitBase")]
    public class VenueLinks : AdagePropertyMulitBase
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
                    baseProperties = new PropertyDataCollection { { RepeaterPropertyName, new VenueLink() } };
                }
                return baseProperties;
            }
        }

        /// <summary>
        /// Returns the internal collection of VenueBody items as a list.
        /// </summary>
        public List<VenueLink> VenueLinkList
        {
            get
            {
                return this.GetTypedCollection<VenueLink>().ToList();
            }
        }
    }
}