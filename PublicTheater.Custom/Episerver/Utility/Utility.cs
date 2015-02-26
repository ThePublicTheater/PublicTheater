using System.IO;
using System.Net;
using System.Web.Routing;
using System.Web.UI;
using Adage.Common.ExtensionMethods.Generic;
using Adage.Tessitura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.Data.Dynamic;
using EPiServer.Filters;
using EPiServer.Personalization.VisitorGroups;
using EPiServer.Security;
using EPiServer.ServiceLocation;
using EPiServer.Web;
using EPiServer.Web.Routing;
using PublicTheater.Custom.CoreFeatureSet.Helper;
using PublicTheater.Custom.Episerver.Pages;
using TheaterTemplate.Shared.Common;
using System.Xml;

namespace PublicTheater.Custom.Episerver.Utility
{
    public static class Utility
    {
        /// <summary>
        /// Recursive find parent page by type id. If not exist then return root page. 
        /// </summary>
        /// <param name="currentPageLink"></param>
        /// <param name="pageTypeID"></param>
        /// <returns></returns>

        public static PageReference FindParentByPageTypeID(PageReference currentPageLink, int pageTypeID)
        {
            PageData currentPage = DataFactory.Instance.GetPage(currentPageLink);
            if (currentPage.PageTypeID.Equals(pageTypeID))
                return currentPage.PageLink;
            if (currentPage.ParentLink != null && currentPage.ParentLink.ID > 0)
                return FindParentByPageTypeID(currentPage.ParentLink, pageTypeID);

            return ContentReference.StartPage;
        }

        /// <summary>
        /// Get All Visitor Groups
        /// </summary>
        /// <returns></returns>
        public static List<VisitorGroup> GetAllVisitorGroups()
        {
            var repository = new VisitorGroupStore(DynamicDataStoreFactory.Instance);
            return repository.List().ToList();
        }

        /// <summary>
        /// Get All Visitor Groups
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> GetAllVisitorGroupsDictionary()
        {
            return GetAllVisitorGroups()
                   .OrderBy(v => v.Name)
                   .ToDictionary(vg => vg.Name, vg => vg.Id.ToString());
        }



        /// <summary>
        /// check if Current User in Visitor Groups
        /// </summary>
        /// <param name="visitorGroups"></param>
        /// <returns></returns>
        public static bool IsCurrentUserInVisitorGroup(IEnumerable<VisitorGroup> visitorGroups)
        {
            var helper = new VisitorGroupHelper();
            return visitorGroups.Any(v => helper.IsPrincipalInGroup(PrincipalInfo.CurrentPrincipal, v.Name));
        }

        /// <summary>
        /// return a intro of description
        /// </summary>
        /// <param name="description"></param>
        /// <param name="limitLength"> </param>
        /// <returns></returns>
        public static string GetAbbrDescription(string description, int limitLength)
        {
            if (String.IsNullOrEmpty(description))
                return String.Empty;

            Regex regex = new Regex("<.*?>");

            int htmlTagLength = regex.Matches(description).Cast<Match>().Sum(match => match.Length);

            int allowedLength = limitLength + htmlTagLength;

            if (description.Length > allowedLength)
                return String.Concat(description.Substring(0, allowedLength), "...");

            return description;
        }

        /// <summary>
        /// Returns a property with the specified property name, looking up the tree if not found.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static PropertyData GetPropertyRecursive(this PageData page, string propertyName)
        {
            PropertyData property = page.Property[propertyName];
            if (property != null)
                return property;

            if (page.ParentLink.ID == 0)
                return null;

            return GetPropertyRecursive(DataFactory.Instance.GetPage(page.ParentLink), propertyName);
        }

        public static T GetPropertyValueRecursive<T>(this PageData page, string propertyName, T defaultValue) where T : class
        {
            var property = GetPropertyRecursive(page, propertyName);
            if (property == null || property.Value == null)
                return defaultValue;

            return property.Value as T;
        }



        /// <summary>
        /// Get Site Theme for a page
        /// </summary>
        /// <param name="contentData"></param>
        /// <returns></returns>
        public static Enums.SiteTheme GetSiteTheme(this ContentData contentData)
        {
            var siteTheme = Enums.SiteTheme.Default;
            var siteThemeProperty = contentData.Property["SiteTheme"];
            if (siteThemeProperty == null)
                return siteTheme;

            Enum.TryParse(siteThemeProperty.ToString(), true, out siteTheme);
            return siteTheme;
        }

        /// <summary>
        /// Get Site Theme URL base on Site Theme Property
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public static string GetSiteThemeUrl(this PageData page)
        {
            var friendlyUrl = GetFriendlyUrl(page.PageLink);

            return GetSiteThemeURL(friendlyUrl, page.GetSiteTheme());
        }

        /// <summary>
        /// Get Site Theme URL base on Site Theme Property
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public static string GetRequestedSiteThemeUrl(this PageData page)
        {

            Enums.SiteTheme requestedTheme;

            if (HttpContext.Current == null
                || !Enum.TryParse(HttpContext.Current.Request.QueryString[Constants.QueryStringParameters.SiteTheme], true, out requestedTheme))
            {
                return page.GetSiteThemeUrl();
            }

            var friendlyUrl = GetFriendlyUrl(page.PageLink);
            var pageSiteTheme = page.GetSiteTheme();
            var finalSiteTheme = pageSiteTheme == Enums.SiteTheme.Default ? requestedTheme : pageSiteTheme;

            return GetSiteThemeURL(friendlyUrl, finalSiteTheme);
        }

        /// <summary>
        /// resolve friendly url for epi pages
        /// </summary>
        /// <param name="pageLink"></param>
        /// <returns></returns>
        public static string GetFriendlyUrl(ContentReference pageLink)
        {
            if (pageLink == null)
                return string.Empty;

            //return ServiceLocator.Current.GetInstance<UrlResolver>().GetVirtualPath(pageLink);

            var requestContext = new RequestContext();
            requestContext.RouteData = new RouteData();
            requestContext.RouteData.DataTokens.Add("contextmode", ContextMode.Default);
            var routeValues = new RouteValueDictionary();
            var urlResolver = new UrlResolver();

            var contextSaved = HttpContext.Current;

            HttpContext.Current = null;
            var url = urlResolver.GetVirtualPath(pageLink, null, routeValues, requestContext);
            HttpContext.Current = contextSaved;

            return url.GetUrl();
        }

        /// <summary>
        /// Get Url with Site Theme query string parameter appended
        /// </summary>
        /// <param name="siteTheme"></param>
        /// <param name="orginalUrl"></param>
        /// <returns></returns>
        public static string GetSiteThemeURL(string orginalUrl, Enums.SiteTheme siteTheme)
        {
            if (siteTheme == Enums.SiteTheme.Default)
                return orginalUrl;

            Uri fullUrl;
            if (Uri.TryCreate(orginalUrl, UriKind.Absolute, out fullUrl) == false)
            {
                //it's not absolute url, convert relative to absolute
                var scheme = HttpContext.Current.Request.Url.Scheme;
                var authority = (HttpContext.Current.Request.Url.IsDefaultPort)
                    ? HttpContext.Current.Request.Url.Host
                    : HttpContext.Current.Request.Url.Authority;

                Uri.TryCreate((scheme + Uri.SchemeDelimiter + authority + orginalUrl).ToLower(), UriKind.Absolute, out fullUrl);
            }

            var urlBuilder = new EPiServer.UrlBuilder(fullUrl);
            var query = System.Web.HttpUtility.ParseQueryString(urlBuilder.Query);
            query[Constants.QueryStringParameters.SiteTheme] = siteTheme.ToString();
            urlBuilder.Query = query.ToString();

            return urlBuilder.ToString();
        }


        public static string GetDateDescription(DateTime startDate, DateTime endDate)
        {
            var formatter = Config.GetValue(TheaterSharedAppSettings.PERFORMANCE_DATE_DISPLAY_FORMAT, "dddd, MMMM d");
            if (endDate == DateTime.MinValue || endDate.Equals(startDate))
                return startDate.ToString(formatter);

            if (endDate.Year.Equals(startDate.Date.Year))
                return string.Format("{0} - {1}", startDate.ToString(formatter), endDate.ToString(formatter));

            return string.Format("{0} - {1}", startDate.ToString(formatter), endDate.ToString(formatter));
        }

        public const string PlayDetailDateFormat = "MMMM d";
        public static string GetPlayDetailsDateDescription(DateTime startDate, DateTime endDate)
        {
            var formatter = PlayDetailDateFormat;
            if (endDate == DateTime.MinValue || endDate.Equals(startDate))
                return startDate.ToString(formatter);

            if (endDate.Year.Equals(startDate.Date.Year))
                return string.Format("{0} - {1}", startDate.ToString(formatter), endDate.ToString(formatter));

            return string.Format("{0} - {1}", startDate.ToString(formatter), endDate.ToString(formatter));
        }

        public const string ArchiveFormatter = "MMMM d, yyyy";
        public const string ArchiveShortFormatter = "MMMM d";
        /// <summary>
        /// Get date description for archive, display year for historical shows
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static string GetArchiveDateDescription(DateTime startDate, DateTime endDate)
        {
            if (endDate == DateTime.MinValue || endDate.Equals(startDate))
                return startDate.ToString(ArchiveFormatter);

            return string.Format("{0} - {1}",
              startDate.Year == endDate.Year ? startDate.ToString(ArchiveShortFormatter) : startDate.ToString(ArchiveFormatter),
              endDate.ToString(ArchiveFormatter));
        }

        /// <summary>
        /// Get archive date desc by performances. For joes pub show, list all perf dates instead of a date range
        /// </summary>
        /// <param name="performances"></param>
        /// <returns></returns>
        public static string GetArchiveDateDescription(List<Performance> performances)
        {
            if (performances.Any() == false)
                return string.Empty;

            if (performances.Count == 1)
            {
                return performances.First().PerformanceDate.ToString(ArchiveFormatter);
            }

            var archiveSingleDatesPerfCount = Config.GetValue("ArchiveSingleDatesPerfCount ", 4);
            
            if (performances.Any(p => SiteThemeHelper.GetSiteTheme(p) == Enums.SiteTheme.JoesPub) 
                && performances.Count <= archiveSingleDatesPerfCount)
            {
                var dateDescription = new StringBuilder();
                var performanceDates = performances.Select(p => p.PerformanceDate.Date).Distinct().OrderBy(d => d).ToList();

                var yearGroups = performanceDates.GroupBy(d => d.Year);

                foreach (IGrouping<int, DateTime> yearGroup in yearGroups)
                {
                    if (dateDescription.Length != 0)
                    {
                        dateDescription.Append(", "); //cross year dates, append spliter
                    }
                    var monthGroups = yearGroup.GroupBy(d => d.Month);
                    var firstInTheYear = true;
                    foreach (IGrouping<int, DateTime> monthGroup in monthGroups)
                    {
                        if (firstInTheYear)
                        {
                            firstInTheYear = false;
                        }
                        else
                        {
                            dateDescription.Append(", ");
                        }

                        var firstInTheMonth = true;
                        var spliter = monthGroup.Count() == 2 ? " and " : ", ";
                        foreach (DateTime perfDate in monthGroup)
                        {
                            if (firstInTheMonth)
                            {
                                dateDescription.Append(perfDate.ToString(ArchiveShortFormatter));
                                firstInTheMonth = false;
                            }
                            else
                            {
                                dateDescription.Append(spliter);
                                dateDescription.Append(perfDate.Day);
                            }
                        }

                        
                    }

                    dateDescription.Append(", " + yearGroup.Key);
                }
                return dateDescription.ToString();
            }

            var startDate = performances.Min(p => p.PerformanceDate);
            var endDate = performances.Max(p => p.PerformanceDate);
            return GetArchiveDateDescription(startDate, endDate);
        }

        private const string ShortDateFormat = "MMM d";
        public static string GetShortDateDescription(DateTime startDate, DateTime endDate)
        {
            if (endDate == System.DateTime.MinValue || endDate.Equals(startDate))
                return startDate.ToString(ShortDateFormat);

            if (endDate.Year.Equals(startDate.Date.Year))
                return string.Format("{0} - {1}", startDate.ToString(ShortDateFormat), endDate.ToString(ShortDateFormat));

            return string.Format("{0} - {1}", startDate.ToString(ShortDateFormat), endDate.ToString(ShortDateFormat));
        }

        public static string GetFormattedePerformanceDate(DateTime performanceDate)
        {
            if (performanceDate.ToString("h:mm tt") == "11:59 PM")
            {
                return string.Format("{0} - Midnight",
                    performanceDate.ToString(Config.GetValue(TheaterSharedAppSettings.PERFORMANCE_DATE_DISPLAY_FORMAT, "dddd, MMMM d")));
            }
            return string.Format("{0} - {1}",
                    performanceDate.ToString(Config.GetValue(TheaterSharedAppSettings.PERFORMANCE_DATE_DISPLAY_FORMAT, "dddd, MMMM d")),
                    performanceDate.ToString(Config.GetValue(TheaterSharedAppSettings.PERFORMANCE_TIME_FORMAT, "h:mm tt")));
        }


        public static string GetHeading(this PageData pageData)
        {
            var siteThemeProperty = pageData.Property["Heading"];
            if (siteThemeProperty == null || string.IsNullOrWhiteSpace(siteThemeProperty.ToString()))
                return pageData.PageName;
            return siteThemeProperty.ToString();
        }


        public static Enums.SiteTheme GetRequestedTheme()
        {
            if (HttpContext.Current == null || string.IsNullOrEmpty(HttpContext.Current.Request.QueryString[Constants.QueryStringParameters.SiteTheme]))
            {

                //return default
                try
                {
                    return Enums.SiteTheme.Default;
                }
                catch (Exception)
                {
                }
            }
            else
            {
                if (
                    !string.IsNullOrEmpty(
                        HttpContext.Current.Request.QueryString[Constants.QueryStringParameters.SiteTheme]))
                {
                    return (Enums.SiteTheme)Enum.Parse(typeof(Enums.SiteTheme), HttpContext.Current.Request.QueryString[Constants.QueryStringParameters.SiteTheme] ?? "", true);
                }
                
            }
            return Enums.SiteTheme.Default;

            // else if theme is requested, set the requested theme 


        }

        public static Enums.SiteTheme GetRequestedTheme(PageData CurrentPage)
        {
            if ((HttpContext.Current == null || string.IsNullOrEmpty(HttpContext.Current.Request.QueryString[Constants.QueryStringParameters.SiteTheme])) && inCartPath(CurrentPage))
            {
                try
                {
                    return (Enums.SiteTheme)HttpContext.Current.Session["requestTheme"];
                }
                catch (Exception)
                {
                }

            }
            else
            {
                if (
                    !string.IsNullOrEmpty(
                        HttpContext.Current.Request.QueryString[Constants.QueryStringParameters.SiteTheme]))
                {
                    return
                        (Enums.SiteTheme)
                            Enum.Parse(typeof (Enums.SiteTheme),
                                HttpContext.Current.Request.QueryString[Constants.QueryStringParameters.SiteTheme] ?? "",
                                true);
                }
                else
                {
                    try
                    {
                        return (Enums.SiteTheme)CurrentPage.Property["SiteTheme"].Value;
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            return Enums.SiteTheme.Default;

        }
        public static string GetTarget(EPiServer.Core.PageData o)
        {
            try
            {
                var result = o.Property["PageTargetFrame"];
                return ((EPiServer.SpecializedProperties.PropertyFrame)result).FrameName;
            }
            catch (Exception e)
            {
                return "";
            }
        }
        private static bool inCartPath(PageData CurrentPage)
        {
            string cartPathPages = Adage.Tessitura.Config.GetValue("cartPathPages");
            string[] arrCartPathStrings = cartPathPages.Split(';');
            return arrCartPathStrings.Contains(Convert.ToString((CurrentPage.PageLink.ID)));

        }
        public static List<T> SearchPageByType<T>(this PageReference pageReference) where T : PageData
        {
            var criterias = new PropertyCriteriaCollection();
            var criteria = new PropertyCriteria();
            criteria.Condition = CompareCondition.Equal;
            criteria.Name = "PageTypeID";
            criteria.Type = PropertyDataType.PageType;
            criteria.Value = ServiceLocator.Current.GetInstance<PageTypeRepository>().Load<T>().ID.ToString();
            criteria.Required = true;
            criterias.Add(criteria);
            return DataFactory.Instance.FindPagesWithCriteria(pageReference, criterias).OfType<T>().ToList();
        }

        public static List<T> GetChildrenByType<T>(this PageReference pageReference, bool recursive) where T : PageData
        {
            var childrenPages = new List<T>();
            foreach (var child in ServiceLocator.Current.GetInstance<IContentRepository>().GetChildren<PageData>(pageReference))
            {
                if (child is T)
                {
                    childrenPages.Add(child as T);
                }
                if (recursive)
                {
                    childrenPages.AddRange(child.PageLink.GetChildrenByType<T>(recursive));
                }
            }
            return childrenPages;
        }

        public static string GetMediaType(string url, out string id)
        {
            Match match = Regex.Match(url, @"^(?:https?:\/\/)?(?:www\.)?(?:youtu\.be\/|youtube\.com\/(?:embed\/|v\/|watch\?v=|watch\?.+&v=))((\w|-){11})(?:\S+)?$");
            if (match.Success)
            {
                id = match.Groups[1].Value;
                return "youtube";
            }
            match = Regex.Match(url, @"vimeo\.com/(?:.*#|.*/videos/)?([0-9]+)");
            if (match.Success)
            {
                id = match.Groups[1].Value;
                return "vimeo";
            }
            match = Regex.Match(url, @"((http:\/\/(soundcloud\.com\/.*|soundcloud\.com\/.*\/.*|soundcloud\.com\/.*\/sets\/.*|soundcloud\.com\/groups\/.*|snd\.sc\/.*))|(https:\/\/(soundcloud\.com\/.*|soundcloud\.com\/.*\/.*|soundcloud\.com\/.*\/sets\/.*|soundcloud\.com\/groups\/.*)))");
            if (match.Success)
            {
                id = "";
                return "soundcloud";
            }
            match = Regex.Match(url, @"^.*\.(aiff|aac|flac|m4a|mp3|ogg|wav)$");
            if (match.Success)
            {
                id = "";
                return "sound";
            }
            match = Regex.Match(url, @"^Album:(.*)");
            if (match.Success)
            {
                id = match.Groups[0].Value;
                return "album";
            }
            id = "";
            return "picture";
        }
        public static string getDefaultCoverImage(string url)
        {
            string id;
            switch (GetMediaType(url, out id))
            {
                case "youtube":
                    return GetYoutubeThumbnail(id, YoutubeThumbnailQuality.HighQuality);
                    break;
                case "vimeo":
                    return GetVimeoThumbnail(url);
                    break;
                case "soundcloud":
                    return GetSoundCloudThumbnail(url);
                    break;
                case "sound":
                    return "SoundDefault";
                    break;

            }

            return url;

        }
        public enum YoutubeThumbnailQuality
        {
            Default, Preview0, Preview1, Preview2, Preview3, HighQuality, MediumQuality, StandardQuality, MaxResolution
        }
        public static string GetYoutubeThumbnail(string videoID, YoutubeThumbnailQuality quality)
        {
            if (quality == YoutubeThumbnailQuality.Preview0)
                return string.Format(@"http://img.youtube.com/vi/{0}/0.jpg", videoID);

            if (quality == YoutubeThumbnailQuality.Preview1)
                return string.Format(@"http://img.youtube.com/vi/{0}/1.jpg", videoID);

            if (quality == YoutubeThumbnailQuality.Preview2)
                return string.Format(@"http://img.youtube.com/vi/{0}/2.jpg", videoID);

            if (quality == YoutubeThumbnailQuality.Preview3)
                return string.Format(@"http://img.youtube.com/vi/{0}/3.jpg", videoID);

            if (quality == YoutubeThumbnailQuality.Default)
                return string.Format(@"http://img.youtube.com/vi/{0}/default.jpg", videoID);

            if (quality == YoutubeThumbnailQuality.HighQuality)
                return string.Format(@"http://img.youtube.com/vi/{0}/hqdefault.jpg", videoID);

            if (quality == YoutubeThumbnailQuality.MediumQuality)
                return string.Format(@"http://img.youtube.com/vi/{0}/mqdefault.jpg", videoID);

            if (quality == YoutubeThumbnailQuality.StandardQuality)
                return string.Format(@"http://img.youtube.com/vi/{0}/sddefault.jpg", videoID);

            if (quality == YoutubeThumbnailQuality.MaxResolution)
                return string.Format(@"http://img.youtube.com/vi/{0}/maxresdefault.jpg", videoID);


            return string.Format(@"http://img.youtube.com/vi/{0}/0.jpg", videoID);
        }
        public static string GetVimeoThumbnail(string vimeoURL)
        {

            try
            {
                string vimeoUrl = System.Web.HttpContext.Current.Server.HtmlEncode(vimeoURL);
                var VimeoRegex = new Regex(@"vimeo\.com/(?:.*#|.*/videos/)?([0-9]+)", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                Match vimeoMatch = VimeoRegex.Match(vimeoUrl);

                XmlDocument doc = new XmlDocument();
                string vimeoID = vimeoMatch.Groups[1].Value;

                doc.Load("http://vimeo.com/api/v2/video/" + vimeoID + ".xml");
                XmlElement root = doc.DocumentElement;
                string vimeoThumb = root.FirstChild.SelectSingleNode("thumbnail_large").ChildNodes[0].Value;
                string imageURL = vimeoThumb;
                return imageURL;
            }
            catch
            {
                //cat with cheese on it's face fail
                return "";
            }
        }

        public static string GetSoundCloudThumbnail(string url)
        {

            try
            {


                WebClient client = new WebClient();
                Stream stream =
                    client.OpenRead("http://api.soundcloud.com/resolve.json?url=" + url + "&client_id=YOUR_CLIENT_ID");
                StreamReader reader = new StreamReader(stream);

                Newtonsoft.Json.Linq.JObject jObject = Newtonsoft.Json.Linq.JObject.Parse(reader.ReadLine());
                string result = (string)jObject["artwork_url"];
                if (result == null)
                {
                    throw new Exception();

                }
                return result;

            }
            catch
            {
                //cat with cheese on it's face fail
                return "SoundCloudDefault";
            }
        }

        public static string AbsoluteUrl(Uri request, string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return "";
            }
            Uri uri = new Uri(path, UriKind.RelativeOrAbsolute);
            if (!uri.IsAbsoluteUri)
            {

                UriBuilder builder = new UriBuilder(request.Scheme, request.Host, request.Port);

                builder.Path = VirtualPathUtility.ToAbsolute(path);
                uri = builder.Uri;
            }
            return Uri.EscapeUriString(uri.ToString());
        }

        public static string escapeXML(string unescaped)
        {
            if (string.IsNullOrEmpty(unescaped))
            {
                return "";
            }
            return unescaped.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;").Replace("'", "&apos;");

        }
    }
}
