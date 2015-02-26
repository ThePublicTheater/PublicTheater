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
    public class ArtistLink : AdagePropertySingleBase
    {
        protected const string PROPERTY_NAME_LINK_URL = "Artist Page";
        protected const string PROPERTY_NAME_LINK_ROLE = "Artist Role";

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
                                {PROPERTY_NAME_LINK_URL , new PropertyPageReference()},
                                {PROPERTY_NAME_LINK_ROLE , new PropertyString()},
                            };
                }
                return _innerPropertyCollection;
            }
            set { _innerPropertyCollection = value; }
        }

        #region Accessors


        public PageReference ArtistPage
        {
            get { return PropertyCollection[PROPERTY_NAME_LINK_URL].Value as PageReference; }
            set { PropertyCollection[PROPERTY_NAME_LINK_URL].Value = value; }
        }

        public string ArtistRole
        {
            get { return (PropertyCollection[PROPERTY_NAME_LINK_ROLE].Value as string) ?? ""; }
            set { PropertyCollection[PROPERTY_NAME_LINK_ROLE].Value = value; }
        }

        private bool _pageDataLoaded = false;
        private Pages.ArtistPageData _artistPageData;
        public Pages.ArtistPageData ArtistPageData
        {
            get
            {
                if (_pageDataLoaded == false)
                {
                    _pageDataLoaded = true;
                    if (ArtistPage != null)
                    {
                        _artistPageData = DataFactory.Instance.GetPage(ArtistPage) as Pages.ArtistPageData;
                    }
                }
                return _artistPageData;
            }
        }
        #endregion
    }

    [Serializable]
    [PropertyDefinitionTypePlugIn(DisplayName = "ArtistLink List")]
    [EditorHint("PropertyMulitBase")]
    public class ArtistLinks : AdagePropertyMulitBase
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
                    baseProperties = new PropertyDataCollection { { RepeaterPropertyName, new ArtistLink() } };
                }
                return baseProperties;
            }
        }

        /// <summary>
        /// Returns the internal collection of ArtistLink items as a list.
        /// </summary>
        public List<ArtistLink> ArtistLinkList
        {
            get
            {
                return this.GetTypedCollection<ArtistLink>().ToList();
            }
        }
    }
}