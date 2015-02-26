using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Adage.Tessitura;
using EPiServer.DynamicContent;

namespace PublicTheater.Web.modules.VTIXNextPerf
{
    [DynamicContentPlugIn(DisplayName = "VTixNextPerfDate", ViewUrl = "~/modules/VTIXNextPerf/vtixNextPerf.ascx")]
    public partial class vtixNextPerf : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var sqlParameters = new Dictionary<string, object>();
            sqlParameters.Add("@skipTodaysPerf", 1);

            DataSet perfSet = TessSession.GetSession().ExecuteLocalProcedure("NextVTixPerfSP", sqlParameters);
            if (perfSet != null && (perfSet.Tables.Count != 0 && perfSet.Tables[0].Rows.Count != 0))
            {
                ltlVtixNextPerf.Text = ((DateTime)perfSet.Tables[0].Rows[0][1]).ToLongDateString();
            }

        }
    }
}