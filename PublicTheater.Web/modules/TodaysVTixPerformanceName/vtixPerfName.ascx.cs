using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EPiServer;
using EPiServer.Core;
using EPiServer.DynamicContent;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;
using PublicTheater.Custom.VTIX;
using Adage.Tessitura;
using System.Data;


namespace PublicTheater.Web.modules.TodaysVTixPerformanceName
{
    [DynamicContentPlugIn(DisplayName = "VTixPerfName", ViewUrl = "~/modules/TodaysVTixPerformanceName/vtixPerfName.ascx")]
    public partial class VtixPerfName : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var sqlParameters = new Dictionary<string, object>();

            DataSet perfSet = TessSession.GetSession().ExecuteLocalProcedure("VTixPerfSP", sqlParameters);
            if (perfSet != null && (perfSet.Tables.Count != 0 && perfSet.Tables[0].Rows.Count != 0))
            {
                perfName.Text = (string) perfSet.Tables[0].Rows[0][0];
            }

        }
    }
}