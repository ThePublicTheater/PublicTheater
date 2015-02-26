using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using EPiServer.Filters;
using EPiServer.Web.WebControls;

namespace PublicTheater.Web.Views.Controls
{
    [ParseChildren(true), Designer("EPiServer.Web.WebControls.Design.PageTreeDesigner, EPiServer.Design"), PersistChildren(false)]
    public class SiteMapPageTree : PageTree
    {
        /// <summary>
        /// </summary>
        static readonly string[] FilterPageTypes = new string[0];


        /// <summary>
        /// Custom filter for site map
        /// </summary>
        protected override void CreatePreSortFilters()
        {
            ShowRootPage = true;
            EnableVisibleInMenu = true;
            FilterPagesWithoutTemplate = true;

            foreach (var filterPageType in FilterPageTypes)
            {
                var filterCompareTo = new FilterCompareTo("PageTypeID", ConfigurationManager.AppSettings[filterPageType])
                {
                    Condition = CompareCondition.NotEqual
                };

                this.PageLoader.Filter += filterCompareTo.Filter;
            }

            base.CreatePreSortFilters();
        }
    }
}