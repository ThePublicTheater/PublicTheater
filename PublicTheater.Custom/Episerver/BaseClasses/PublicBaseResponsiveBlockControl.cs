using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EPiServer.Core;
using EPiServer.Web;

namespace PublicTheater.Custom.Episerver.BaseClasses
{
    public class PublicBaseResponsiveBlockControl<TBlock> : PublicBaseBlockControl<TBlock> where TBlock : PublicBaseResponsiveBlockData
    {
        public string GetResponsiveClass()
        {
            return Enums.GetBlockResponsiveClass(CurrentBlock.LargeView, CurrentBlock.MediumView, CurrentBlock.Height);
        }
        public override void RenderControl(System.Web.UI.HtmlTextWriter writer)
        {
            if (CurrentBlock.DisableResponsive)
            {
                base.RenderControl(writer);
            }
            else
            {
                writer.WriteLine(string.Format("<div class='{0}'>", GetResponsiveClass()));
                base.RenderControl(writer);
                writer.WriteLine("</div>");
            }


        }
    }
}
