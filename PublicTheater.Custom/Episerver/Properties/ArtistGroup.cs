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
    public class ArtistGroup : AdagePropertySingleBase
    {
        protected const string PROPERTY_NAME_GROUP_NAME = "Group Name";
        protected const string PROPERTY_NAME_GROUP_LIST = "List";

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
                                
                                {PROPERTY_NAME_GROUP_NAME , new PropertyString()},
                                {PROPERTY_NAME_GROUP_LIST , new ArtistLinks()},
                            };
                }
                return _innerPropertyCollection;
            }
            set { _innerPropertyCollection = value; }
        }

        #region Accessors


        public ArtistLinks ArtistList
        {
            get { return PropertyCollection[PROPERTY_NAME_GROUP_LIST] as ArtistLinks; }
        }

        public string GroupName
        {
            get { return (PropertyCollection[PROPERTY_NAME_GROUP_NAME].Value as string) ?? ""; }
            set { PropertyCollection[PROPERTY_NAME_GROUP_NAME].Value = value; }
        }

        public virtual List<ArtistLink> GetArtistList()
        {
            var artistList = this.ArtistList;
            if (artistList == null)
                return new List<ArtistLink>();

            return artistList.ArtistLinkList.Where(al => al.ArtistPageData != null).ToList();
        }

        #endregion
    }

    [Serializable]
    [PropertyDefinitionTypePlugIn(DisplayName = "ArtistGroup List")]
    [EditorHint("PropertyMulitBase")]
    public class ArtistGroups : AdagePropertyMulitBase
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
                    baseProperties = new PropertyDataCollection { { RepeaterPropertyName, new ArtistGroup() } };
                }
                return baseProperties;
            }
        }

        /// <summary>
        /// Returns the internal collection of ArtistGroup items as a list.
        /// </summary>
        public List<ArtistGroup> ArtistGroupList
        {
            get
            {
                return this.GetTypedCollection<ArtistGroup>().ToList();
            }
        }
    }
}