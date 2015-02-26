using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Adage.Tessitura;
using EPiServer;
using EPiServer.Core;
using EPiServer.DynamicContent;
using TheaterTemplate.Shared.GiftCertificateObjects;

namespace PublicTheater.Web.modules.GiftCertificateImage
{
    [DynamicContentPlugIn(DisplayName = "GiftCertificateImage", ViewUrl = "~/modules/GiftCertificateImage/GiftCertificateImage.ascx")]
    public partial class GiftCertificateImage : System.Web.UI.UserControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string Certificate_Number = tbGCNum.Text;
            string imageUrl = MapPath("~/gift/images/") + Certificate_Number + ".png";
            string relativeImageUrl = "/gift/images/" + Certificate_Number + ".png";
            try
            {
                var giftCertificateData = GiftCertificateEmail.GetGiftCertificateEmail(Certificate_Number);
                var graphicGenerator = new GiftCertificateGraphicGenerator();
                var certificateImage = graphicGenerator.CreateGiftCertificateGraphic(giftCertificateData);
                certificateImage.Save(imageUrl, ImageFormat.Png);
                var i = new Image { ImageUrl = relativeImageUrl };
                i.ID = "gcImage";
               
                imagePanel.Controls.Add(i);
            }
            catch (Exception exception)
            {
                Adage.Common.ElmahCustomError.CustomError.LogError(exception, imageUrl, null);
                throw;
            }

        }
    }
}