using System;
using System.Web;
using System.Web.UI.HtmlControls;
using Adage.EpiServer.Library.PageTypes.Templates;
using PublicTheater.Custom.Episerver;
using PublicTheater.Custom.Episerver.BaseClasses;
using Adage.Tessitura;

namespace PublicTheater.Web.Views.MasterPages
{
    public partial class NoTessMasterMasterPage : System.Web.UI.MasterPage 
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (RequestedTheme != Enums.SiteTheme.Default)
            //{
            //    secMainContent.Attributes["class"] = secMainContent.Attributes["class"] + " subMainContent";
            //}
            if (!IsPostBack)
            {   
                htmlTag.Attributes["class"] = htmlTag.Attributes["class"] + " " + GetCurrentTemplatePageClassName();
               
            }

            SetBackbonePageType();
        }

        private void SetBackbonePageType()
        {
            //epiPageType.Value = CurrentPage.PageTypeName;
        
            reservePageUrl.Value = "/reserve/index.aspx?performanceNumber=";
        }

        public string GetCurrentTemplatePageClassName()
        {
            var baseType = Page.GetType().BaseType;
            return baseType != null ? baseType.Name.ToLower() : string.Empty;
        }
    }
}