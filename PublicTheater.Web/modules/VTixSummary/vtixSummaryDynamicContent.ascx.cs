using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Adage.Tessitura;
using EPiServer.DynamicContent;

namespace PublicTheater.Web.modules.VTixSummary
{
    [DynamicContentPlugIn(DisplayName = "VTixSummary", ViewUrl = "~/modules/VTixSummary/vtixSummaryDynamicContent.ascx")]
    public partial class vtixSummaryDynamicContent : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            var sqlParameters = new Dictionary<string, object>();
            //sqlParameters.Add("@skipTodaysPerf", 1);
            DataSet summarySet= TessSession.GetSession().ExecuteLocalProcedure("VTixSummarySP", sqlParameters);
            if (summarySet.Tables.Count > 0)
                GridView1.DataSource = summarySet.Tables[0];
            GridView1.DataBind();
        }
    }
}