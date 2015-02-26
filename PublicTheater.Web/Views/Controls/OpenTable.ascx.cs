using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EPiServer;
using EPiServer.Core;
using PublicTheater.Custom.Episerver.Pages;

namespace PublicTheater.Web.Views.Controls
{
    public partial class OpenTable : PublicTheater.Custom.Episerver.BaseClasses.PublicBaseUserControl
    {
        private PageData _homePage;
        private string _openTableID;
        protected string OpenTableID
        {
            get
            {
                if (_openTableID == null)
                {
                    if (_homePage == null)
                    {
                        _homePage = DataFactory.Instance.GetPage(ContentReference.StartPage);
                    }

                    var homePage = _homePage as HomePageData;
                    _openTableID = homePage != null 
                        ? (homePage).OpenTableID 
                        : _homePage["OpenTableID"] as string;
                }
                return _openTableID;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}