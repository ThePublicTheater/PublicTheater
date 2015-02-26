using System;
using EPiServer.Web;

namespace PublicTheater.Web.Views.Blocks
{
    public partial class AccordionBlockControl : Custom.Episerver.BaseClasses.PublicBaseBlockControl<Custom.Episerver.Blocks.AccordionBlockData>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                rpt.DataSource = CurrentData.GetAccordionItems();
                rpt.DataBind();
            }
        }
    }
}