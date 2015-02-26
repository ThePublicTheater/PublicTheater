using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAccess;
using EPiServer.Security;
using EPiServer.ServiceLocation;
using PublicTheater.Custom.Episerver.Pages;
using PublicTheater.Custom.Episerver.Utility;
using TheaterTemplate.Shared.EPiServerPageTypes;

namespace PublicTheater.Custom
{
    public static class DataImportHelper
    {
        #region Injections

        private static Injected<IContentLoader> _contentLoader;
        private static Injected<PageTypeRepository> _pageTypeLoader;


        private static IContentLoader ContentLoader
        {
            get { return _contentLoader.Service; }
        }

        private static PageTypeRepository PageTypeLoader
        {
            get { return _pageTypeLoader.Service; }
        }

        #endregion



        public static void CreateArchivePages(int rootPageId, int quantity = 0)
        {
            if (quantity <= 0)
            {
                quantity = 50;
            }

            var rootPage = ContentLoader.Get<IContent>(new ContentReference(rootPageId)) as PageData;
            if (rootPage == null)
            {
                throw new ApplicationException("root page id in invalid: " + rootPageId);
            }

            int count = 0;

            foreach (jos_shows josShow in JoesPubDataEntities.Current.jos_shows.Where(s => s.published == 1).OrderByDescending(s => s.id))
            {
                SaveArchivePage(josShow, rootPage.PageLink);
                count++;

                //test one page first
                if (count >= quantity)
                {
                    break;
                }
            }
        }

        public static PageReference SaveArchivePage(jos_shows josShow, PageReference archiveFolder)
        {
            var createdOn = josShow.createdOn;
            var monthFolder = CheckFolder(archiveFolder, createdOn);

            var prod_season_no = josShow.tessi_prod_no.HasValue ? (josShow.tessi_prod_no.Value + 1) : 0;
            PlayDetailPageData newArchivePage = null;

            //try find existing pdp page by prod_season_no
            if (prod_season_no > 0)
            {
                newArchivePage = ContentLoader.GetChildren<PlayDetailPageData>(monthFolder).FirstOrDefault(child => child.TessituraId == prod_season_no);
            }

            //if not found by prod_season_no, find by title
            if (newArchivePage == null)
            {
                newArchivePage = ContentLoader.GetChildren<PlayDetailPageData>(monthFolder)
                    .FirstOrDefault(
                        child =>
                        child.StartPublish.Date.Equals(josShow.createdOn.Date) && child.PageName.Equals(josShow.shortTitle));
            }

            //create new page if not found
            if (newArchivePage == null)
            {
                newArchivePage = DataFactory.Instance.GetDefaultPageData<PlayDetailPageData>(monthFolder);
            }
            else
            {
                newArchivePage = newArchivePage.CreateWritableClone() as PlayDetailPageData;
            }

            newArchivePage.PageName = josShow.shortTitle;
            newArchivePage.Heading = josShow.title;
            newArchivePage.StartPublish = josShow.createdOn;
            newArchivePage.TessituraId = prod_season_no;
            newArchivePage.Property["MainBody"].Value = josShow.description;
            newArchivePage.Property["CalendarSynopsis"].Value = josShow.description;
            newArchivePage.Property["Box1Content"].Value = string.Empty;
            //newArchivePage.Property["RunTime"].Value = josShow.priceInWords;
            newArchivePage.Property["RunTime"].Value = string.Empty;    //don't list price
            newArchivePage.Archived = true;
            newArchivePage.Thumbnail150x75 = @"/Global/Joe's%20Pub/370%20x%20238/JoesPubDefault.jpg";
            if (josShow.showDate.HasValue && josShow.showDate.Value> DateTime.MinValue)
            {
                if (string.IsNullOrEmpty(newArchivePage.StartEndDateOverride))
                {
                    newArchivePage.StartEndDateOverride = josShow.showDate.Value.ToString(Utility.ArchiveFormatter);
                }
                else
                {
                    newArchivePage.StartEndDateOverride = string.Format("{0}, {1}", newArchivePage.StartEndDateOverride,
                        josShow.showDate.Value.ToString(Utility.ArchiveFormatter));
                }
            }

            return DataFactory.Instance.Save(newArchivePage, SaveAction.Publish, AccessLevel.Administer);
        }

        private static void SetThumbnailHeroIamge(jos_shows josShow, PlayDetailPageData newArchivePage)
        {
            string thumbnail;
            string heroImage;
            GetThumbnailAndHero(josShow, out thumbnail, out heroImage);

            if (string.IsNullOrEmpty(thumbnail) == false)
            {
                newArchivePage.Thumbnail150x75 = thumbnail;
            }
            else
            {
                newArchivePage.Thumbnail150x75 = @"/Global/Joe's%20Pub/370%20x%20238/370x238.gif";
            }

            if (string.IsNullOrEmpty(heroImage) == false)
            {
                newArchivePage.HeroImage = heroImage;
            }
        }

        private static PageReference CheckFolder(PageReference archiveFolder, DateTime createdOn)
        {
            var folderName = createdOn.ToString("yyyy-MM");
            var monthFolder = ContentLoader.GetChildren<PageContainerPageType>(archiveFolder)
                .FirstOrDefault(child =>child.PageName.Equals(folderName));

            if (monthFolder == null)
            {
                monthFolder = DataFactory.Instance.GetDefaultPageData<PageContainerPageType>(archiveFolder);
                monthFolder.PageName = folderName;
                DataFactory.Instance.Save(monthFolder, SaveAction.Publish, AccessLevel.Administer);
            }
            return monthFolder.PageLink;
        }

        const decimal ThumbnailRatio = 370m / 238m;
        public static void GetThumbnailAndHero(jos_shows josShow, out string thumbnail, out string heroImage)
        {
            heroImage = thumbnail = string.Empty;

            var showFolder = string.Format(@"\\ftp\FTPClients\publictheater\joesPub\httpsdocs55\httpdocs\images\shows\{0}\photos\", josShow.id);


            var minRatio = ThumbnailRatio;
            var minheroImagewidth = 0;
            foreach (jos_shows_photos file in JoesPubDataEntities.Current.jos_shows_photos.Where(file => file.showID == josShow.id))
            {
                var filePath = string.Concat(showFolder, file.filename);
                try
                {
                    var image = Image.FromFile(filePath);

                    var ratioDifference = Math.Abs(ThumbnailRatio - (decimal)image.Width / (decimal)image.Height);

                    if (ratioDifference < minRatio)
                    {
                        minRatio = ratioDifference;
                        thumbnail = filePath;
                    }

                    if (minheroImagewidth < image.Width)
                    {
                        minheroImagewidth = image.Width;
                        heroImage = filePath;
                    }
                }
                catch (Exception exception)
                {
                    Adage.Common.ElmahCustomError.CustomError.LogError(exception, "Retrieve JP image from old CMS" + filePath, null);
                }
            }

            if (string.IsNullOrEmpty(thumbnail) == false)
            {
                var image = Image.FromFile(thumbnail);
                var newImage = ScaleImage(image, 370, 238);
                var scaledImageName = string.Concat(showFolder, "scaled.png");

                newImage.Save(scaledImageName, ImageFormat.Png);
                thumbnail = scaledImageName;
            }
        }

        public static Image ScaleImage(Image image, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);
            Graphics.FromImage(newImage).DrawImage(image, 0, 0, newWidth, newHeight);
            return newImage;
        }
    }
}
