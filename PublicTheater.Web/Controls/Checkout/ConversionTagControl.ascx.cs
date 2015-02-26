using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Adage.Tessitura;
using EPiServer;
using PublicTheater.Custom.Episerver.BaseClasses;
using EPiServer.Core;

namespace PublicTheater.Web.Controls.Checkout
{
    public partial class ConversionTagControl :  PublicBaseUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string result = "";
            
            var cart = Adage.Tessitura.Cart.GetCart();
            
            List<Tuple<string,string,string>> prodSeasonIds = cart.CartLineItems
                                .OfType<Adage.Tessitura.CartObjects.PerformanceLineItem>()
                                .Select(perfLineItem => new Tuple<string,string,string>( Convert.ToString(perfLineItem.Performance.ProductionSeasonId), Convert.ToString(Convert.ToInt32(perfLineItem.TotalPrice)),  Convert.ToString(perfLineItem.Quantity) ))
                                .ToList();
        
            List<Tuple<string, string>> fundIds = cart.CartLineItems
                               .OfType<ContributionLineItem>()
                               .Select(ContributionLineItem => new Tuple<string, string>(Convert.ToString(ContributionLineItem.ContributionFundId), Convert.ToString(Convert.ToInt32(ContributionLineItem.Amount))))
                               .ToList();


            foreach (Tuple<string,string,string> prodSeasonId in prodSeasonIds)
            {
                string tag=Adage.Tessitura.Config.GetValue("production"+prodSeasonId.Item1 ,string.Empty);
                string grapeSeedTag = Adage.Tessitura.Config.GetValue("grapeseed" + prodSeasonId.Item1, string.Empty);
              
                //grounded tag, not in config page
                if(prodSeasonId.Item1=="27109")
                 result+="<iframe src=\"https://189445.fls.doubleclick.net/activityi;src=189445;type=gabm2698;cat=2015_0;qty="+prodSeasonId.Item3+";cost="+prodSeasonId.Item2+";u6="+DateTime.Today.ToShortDateString()+";u5="+prodSeasonId.Item3+";u4=Grounded;ord="+Convert.ToString(cart.OrderNumber)+"?\" width=\"1\" height=\"1\" frameborder=\"0\" style=\"display:none\"></iframe>";

                if(tag!=string.Empty)
                {
                    string tagId = tag.Split(';')[0];
                    string tagName = tag.Split(';')[1];
                    result += "<iframe src=\"https://secure.img-cdn.mediaplex.com/0/"+tagId+"/universal.html?page_name=" + tagName + "_orderconfirmation&Conversions=1&Revenue=" + prodSeasonId.Item2 + "&Tickets_Sold=" + prodSeasonId.Item3 + "&mpuid=" + Convert.ToString(Adage.Tessitura.User.GetUser().CustomerNumber) + "\" HEIGHT=1 WIDTH=1 FRAMEBORDER=0></iframe>";
                }
                if (grapeSeedTag != string.Empty)
                {
                    result += "<!--Start of Grapeseed Tag: Please do not remove Activity name of this tag: Public Variable Confirmation Pixel--><iframe src=\"https://4220883.fls.doubleclick.net/activityi;src=4220883;type=sales;cat=C9IKX67n;qty=" + prodSeasonId.Item3 + ";cost=" + prodSeasonId.Item2 + ";ord=" + grapeSeedTag + "?\"+ width=\"1\" height=\"1\" frameborder=\"0\" style=\"display:none\"></iframe><!-- End of Grapeseed Tag: Please do not remove -->";
                }
                
            }
            foreach (Tuple<string, string> fundId in fundIds)
            {
                if(fundId.Item1=="15" && Int32.Parse(fundId.Item2)>=2000)
                    continue;//goto next iteration of for loop

                string fundTag = Adage.Tessitura.Config.GetValue("fund" + fundId.Item1, string.Empty);
                if (fundTag!=string.Empty)
                {
                    string tagId = fundTag.Split(';')[0];
                    string tagName = fundTag.Split(';')[1];
                    result += "<iframe src=\"https://secure.img-cdn.mediaplex.com/0/" + tagId + "/universal.html?page_name=" + tagName + @"_orderconfirmation&SITP_SS_Conversion=1&SITP_SS_Revenue=" + fundId.Item2 + @"&mpuid=" + Convert.ToString(Adage.Tessitura.User.GetUser().CustomerNumber) + "\"HEIGHT=1 WIDTH=1 FRAMEBORDER=0></iframe>";
                }
            }

            

            tagHTML.Mode = LiteralMode.PassThrough;
            tagHTML.Text = result;

        }
    }
}