using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Adage.EpiServer.Library.IteraMultiproperty;
using EPiServer;
using EPiServer.Core;
using EPiServer.Framework.DataAnnotations;
using EPiServer.PlugIn;
using EPiServer.SpecializedProperties;
using EPiServer.Web;

namespace PublicTheater.Custom.Episerver.Properties
{
    [Serializable]
    public class PodcastItem : AdagePropertySingleBase
    {
    
        protected const string PROPERTY_NAME_IMAGE = "Image";
        protected const string PROPERTY_NAME_TITLE = "Title";
        protected const string PROPERTY_NAME_SUBTITLE = "SubTitle";
        protected const string PROPERTY_NAME_SUMMARY = "Summary";
        protected const string PROPERTY_NAME_MP3 = "MP3";
        protected const string PROPERTY_NAME_PUBDATE = "Publication Date";
        protected const string PROPERTY_NAME_MP3DURATION = "MP3 duration";

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
                                {PROPERTY_NAME_SUBTITLE, new PropertyString()},
                                {PROPERTY_NAME_SUMMARY, new PropertyLongString()},
                                {PROPERTY_NAME_MP3, new PropertyUrl()},
                                {PROPERTY_NAME_PUBDATE, new PropertyString()},
                                {PROPERTY_NAME_MP3DURATION, new PropertyString()},
                            };
                    _innerPropertyCollection[PROPERTY_NAME_PUBDATE].Value = DateTime.Now.ToString("ddd, dd MMM yyyy HH:mm:ss K");
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

        public string Title
        {
            get { return (PropertyCollection[PROPERTY_NAME_TITLE].Value as string) ?? ""; }
            set { PropertyCollection[PROPERTY_NAME_TITLE].Value = value; }
        }
        public string SubTitle
        {
            get { return (PropertyCollection[PROPERTY_NAME_SUBTITLE].Value as string) ?? ""; }
            set { PropertyCollection[PROPERTY_NAME_SUBTITLE].Value = value; }
        }
        [UIHint(UIHint.Textarea)]
        public string Summary
        {
            get { return (PropertyCollection[PROPERTY_NAME_SUMMARY].Value as string) ?? ""; }
            set { PropertyCollection[PROPERTY_NAME_SUMMARY].Value = value; }
        }
        public string Duration
        {
            get { return (PropertyCollection[PROPERTY_NAME_MP3DURATION].Value as string) ?? ""; }
            set { PropertyCollection[PROPERTY_NAME_MP3DURATION].Value = value; }
        }
        public string PublicationDate
        {
            get
            {
                if (string.IsNullOrWhiteSpace((PropertyCollection[PROPERTY_NAME_PUBDATE].Value as string)))
                {
                    PropertyCollection[PROPERTY_NAME_PUBDATE].Value =
                        DateTime.Now.ToString("ddd, dd MMM yyyy HH:mm:ss k");
                }
                
                return (PropertyCollection[PROPERTY_NAME_PUBDATE].Value as string);
             
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    PropertyCollection[PROPERTY_NAME_PUBDATE].Value =
                        DateTime.Now.ToString("ddd, dd MMM yyyy HH:mm:ss k");
                }
                else
                {
                    PropertyCollection[PROPERTY_NAME_PUBDATE].Value = value;
                }
            }
        }
        
        public string Mp3Url
        {
            get
            {
                try
                {
                    return (PropertyCollection[PROPERTY_NAME_MP3].Value as Url ?? new Url("")).ToString();
                }
                catch (Exception)
                {
                    return string.Empty;
                }
            }
            set { PropertyCollection[PROPERTY_NAME_MP3].Value = value; }
        }
        #endregion
    }

    [Serializable]
    [PropertyDefinitionTypePlugIn(DisplayName = "PodcastItem List")]
    [EditorHint("PropertyMulitBase")]
    public class PodcastItems : AdagePropertyMulitBase
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
                    baseProperties = new PropertyDataCollection { { RepeaterPropertyName, new PodcastItem() } };
                    
                }
                return baseProperties;
            }
        }

        /// <summary>
        /// Returns the internal collection of PodcastItem items as a list.
        /// </summary>
        public List<PodcastItem> PodcastItemList
        {
            get
            {
                return this.GetTypedCollection<PodcastItem>().ToList();
            }
        }
    }
}


