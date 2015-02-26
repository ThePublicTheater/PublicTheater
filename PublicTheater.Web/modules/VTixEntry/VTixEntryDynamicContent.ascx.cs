using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Adage.Tessitura;
using EPiServer;
using EPiServer.Core;
using EPiServer.DynamicContent;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;
using PublicTheater.Custom.VTIX;
using PublicTheater.Custom.CoreFeatureSet.Helper;


namespace PublicTheater.Web.modules.VTixEntry
{
    [DynamicContentPlugIn(DisplayName = "VTixEntryDynamicContent", ViewUrl = "~/modules/VTixEntry/VTixEntryDynamicContent.ascx")]
    public partial class VTixEntryDynamicContent : UserControl
    {
        #region Editable Properties

        // Add your editable properties as normal .Net properties.
        // Supported property types are string, int, bool, 
        // EPiServer.Core.PageReference and any class
        //[DynamicContentItemAttribute(PropertyType = typeof(PropertyXhtmlString))]

        //public PropertyXhtmlString Legalese { get; set; }
        
        public string Submit_text { get; set; }
        
        #endregion


        protected void Page_Load(object sender, EventArgs e)
        {
            submitEntry.Text = Submit_text;



            if (!IsPostBack)
            {
                DataBind();
                
            }
        }

        
    }
}