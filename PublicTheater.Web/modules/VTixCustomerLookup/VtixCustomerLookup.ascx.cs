using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Adage.Tessitura;
using EPiServer.DynamicContent;

namespace PublicTheater.Web.modules.VTixCustomerLookup
{
    [DynamicContentPlugIn(DisplayName = "VTixCustomerLookup", ViewUrl = "~/modules/VTixCustomerLookup/VtixCustomerLookup.ascx")]
    public partial class VtixCustomerLookup : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
                tbDate.Text = DateTime.Now.ToShortDateString();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var sqlParameters = new Dictionary<string, object>();
            if(tbCustNo.Text.Length>0)
                sqlParameters.Add("@customerNo", Int32.Parse(tbCustNo.Text));
            if (tbDate.Text.Length > 0)
                sqlParameters.Add("@date", DateTime.Parse(tbDate.Text));
            if (tbEmailAddress.Text.Length > 0)
                sqlParameters.Add("@emailAddress", tbEmailAddress.Text);
            DataSet summarySet = TessSession.GetSession().ExecuteLocalProcedure("VTixCustomerSummarySP", sqlParameters);
            if (summarySet.Tables.Count > 0)
                GridView1.DataSource = summarySet.Tables[0];
            GridView1.DataBind();
        }
    }
}