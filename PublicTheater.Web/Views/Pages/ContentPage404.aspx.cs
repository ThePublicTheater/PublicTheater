using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EPiServer;
using PublicTheater.Custom.Episerver.Pages;

namespace PublicTheater.Web.Views.Pages
{
    public partial class ContentPage404 : Custom.Episerver.BaseClasses.PublicBasePage<Custom.Episerver.Pages.PageData404>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var errors = Request.Url.ToString().Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();
            if (errors.Count > 1)
            {
                var url = new Uri(errors[1]).LocalPath.ToLower();
                if (url.Contains("/Tickets/Calendar/PlayDetailsCollection/Joes-Pub/".ToLower()))
                {
                    //is JP pdp page
                    var segments = url.Split("/".ToArray(), StringSplitOptions.RemoveEmptyEntries);
                    if (segments.Length > 1)
                    {
                        var pageName = segments[segments.Length - 1];
                        var jpFolder = segments[segments.Length - 2];
                        if (jpFolder == "joes-pub")
                        {
                            //in direct JP folder
                            var newPath = string.Format("2014/{0}/{1}", pageName[0], pageName);
                            segments[segments.Length - 1] = newPath;
                            var newUrl = "/" + string.Join("/", segments);
                            //re-route
                            Response.Redirect(newUrl);
                        }
                    }
                }
            }
        }
    }
}