using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Adage.Tessitura;
using EPiServer;
using EPiServer.Core;
using EPiServer.DataAccess;
using EPiServer.DynamicContent;
using EPiServer.Filters;
using EPiServer.ServiceLocation;
using Microsoft.Ajax.Utilities;
using PublicTheater.Custom.Episerver.Pages;

namespace PublicTheater.Web.modules.PdpFromTess
{
    [DynamicContentPlugIn(DisplayName = "Pdps From Tess", ViewUrl = "~/modules/PdpFromTess/PdpFromTess.ascx")]
    public partial class PdpFromTess : UserControl
    {
        #region Editable Properties

        // Add your editable properties as normal .Net properties.
        // Supported property types are string, int, bool, 
        // EPiServer.Core.PageReference and any class
        // inheriting EPiServer.Core.PropertyData.

        public PageReference RootFolderRef { get; set; }
        public bool Archive { get; set; }
        public bool TryToAlphabetize { get; set; }
        public EPiServer.Core.PropertyDate StartDate { get; set; }
        public EPiServer.Core.PropertyDate EndDate { get; set; }
        public PageReference SyosDefault { get; set; }
        public string Season { get; set; }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EPiServer.Security.PrincipalInfo.HasEditAccess)
            {
                return;
            }
            if (!IsPostBack)
            {

                var crit = new ProductionsCriteria()
                {
                    StartDate = StartDate.Date.Add(new TimeSpan(0,0,DateTime.Now.Millisecond,DateTime.Now.Second)),
                    EndDate = EndDate.Date.Add(new TimeSpan(0, 0, DateTime.Now.Millisecond, DateTime.Now.Second))
                    
                };
                if (!Season.IsNullOrWhiteSpace())
                {
                    crit.SeasonIDs = Season;
                }
                var prods = Productions.GetProductions(crit);

                var pdpPageCriteria = new PropertyCriteria()
                {
                    Name = "PageTypeID",
                    Condition = CompareCondition.Equal,
                    Required = true,
                    Type = PropertyDataType.PageType,
                    Value = Config.GetValue<string>("PDP_PAGE_TYPE", string.Empty)
                };


                foreach (var prod in prods)
                {
                    var cc = new PropertyCriteriaCollection();
                    cc.Add(pdpPageCriteria);
                    cc.Add(new PropertyCriteria()
                    {
                        Name = "TessituraId",
                        Condition = CompareCondition.Equal,
                        Required = true,
                        Type = PropertyDataType.Number,
                        Value = prod.ProductionSeasonNumber.ToString()
                    });
                    var pdpPage = DataFactory.Instance.FindAllPagesWithCriteria(ContentReference.StartPage, cc, "en", new LanguageSelector("en")).FirstOrDefault();

                    if (pdpPage != null)
                    {
                        ProdDiv.InnerHtml += "<div id=\"entry-" + prod.ProductionSeasonNumber + "\"><a href='/epi/Cms/#context=epi.cms.contentdata:///" + pdpPage.ContentLink.ID + "'>" + pdpPage.Name + "</a></div>";
                    }
                    else
                    {

                        ProdDiv.InnerHtml += "<div id=\"entry-" + prod.ProductionSeasonNumber + "\"><span> No pdp Exists: " + prod.Title + "</span><button type='button' onclick=\"document.getElementById('" + createPdpHF.ClientID + "').value = " + prod.ProductionSeasonNumber + ";document.getElementById('" + createButton.ClientID + "').click(); return false;\">Generate a Pdp</button></div>";

                    }

                }

            }
        }

        public void CreatePdp(object sender, EventArgs e)
        {

            int? tess_id;
            createPdpHF.Value.TryParseNullable(out tess_id);
            if (tess_id != null)
            {
                IContentRepository contentRepository = ServiceLocator.Current.GetInstance<IContentRepository>();
                PageReference r =  RootFolderRef;
                
                var prod = Production.GetProduction(tess_id.Value);

                if (TryToAlphabetize)
                {
                    var nameCrit = new PropertyCriteria()
                    {
                        Name = "PageName",
                        Condition = CompareCondition.Equal,
                        IsNull = false,
                        Required = true,
                        Type = PropertyDataType.String,
                        Value = prod.Title.Substring(0, 1)
                    };
                    var critCollection = new PropertyCriteriaCollection();
                    critCollection.Add(nameCrit);
                    var alphabetFolder = DataFactory.Instance.FindPagesWithCriteria(r, critCollection).FirstOrDefault();
                    if (alphabetFolder != null)
                    {

                        r = alphabetFolder.PageLink;
                    }
                }


                var t = DataFactory.Instance.GetDefaultPageData<PlayDetailPageData>(r);
                t.Property["TessituraId"].Value = prod.ProductionSeasonNumber;
                t.Name = prod.Title;
                t.PageName = prod.Title;
                t.Heading = prod.Title;
                t.Archived = Archive;
                t.SYOSRootPage = SyosDefault;
                t.MainBody = new XhtmlString(prod.Synopsis);
                t.CalendarSynopsis = new XhtmlString(prod.Synopsis);
                var newId = contentRepository.Save(t, SaveAction.Save).ID;
                ScriptManager.RegisterStartupScript(UpPanel, UpPanel.GetType(), "newPdp", "document.getElementById('entry-" + tess_id.Value + "').innerHTML = \"<div id='entry-" + prod.ProductionSeasonNumber + "'><a href='/epi/Cms/#context=epi.cms.contentdata:///" + newId + "'>" + prod.Title + "</a></div>\";console.log('hey')", true);


            }
        }

    }
}