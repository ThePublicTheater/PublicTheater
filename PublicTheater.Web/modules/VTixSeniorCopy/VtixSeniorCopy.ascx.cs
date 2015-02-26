using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Adage.Tessitura;
using EPiServer.DynamicContent;
using Sgml;

namespace PublicTheater.Web.modules.VTixSeniorCopy
{
     [DynamicContentPlugIn(DisplayName = "VTixSenioCopy", ViewUrl = "~/modules/VTixSeniorCopy/VtixSeniorCopy.ascx")]
    public partial class VtixSeniorCopy : System.Web.UI.UserControl
    {
        #region Editable Properties

        // Add your editable properties as normal .Net properties.
        // Supported property types are string, int, bool, 
        // EPiServer.Core.PageReference and any class
        //[DynamicContentItemAttribute(PropertyType = typeof(PropertyXhtmlString))]

        //public PropertyXhtmlString Legalese { get; set; }

        public string seniorCopy { get; set; }

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            Adage.Tessitura.User user = Adage.Tessitura.User.GetUser();
            user.GetAccountInfo();
            if (user.LoggedIn && !user.IsAnonymousLogin)
            {
                var sqlParameters = new Dictionary<string, object>();
                sqlParameters.Add("@custNo", user.CustomerNumber);

                DataSet copySet = TessSession.GetSession().ExecuteLocalProcedure("VtixSeniorCopySP", sqlParameters);
                if (copySet != null && (copySet.Tables.Count != 0 && copySet.Tables[0].Rows.Count != 0))
                {
                    if (((string) (copySet.Tables[0].Rows[0][0])).Length > 0 && !String.IsNullOrEmpty(seniorCopy))
                        ltlSenioCopy.Text = seniorCopy;
                    else
                    {
                        ltlSenioCopy.Text = (string)copySet.Tables[0].Rows[0][0];
                    }
                }
            }
        }
    }
}