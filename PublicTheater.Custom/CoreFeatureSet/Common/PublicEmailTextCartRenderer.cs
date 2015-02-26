using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EPiServer.Core;
using TheaterTemplate.Shared.Common;

namespace PublicTheater.Custom.CoreFeatureSet.Common
{
    public class PublicEmailTextCartRenderer : TheaterSharedEmailTextCartRenderer
    {
        /// <summary>
        /// create text cart renderer
        /// </summary>
        /// <param name="CMSEmailPage"></param>
        public PublicEmailTextCartRenderer(PageData CMSEmailPage):base(CMSEmailPage)
        {
        }

        protected override void RenderAdaLineItemInfo(Adage.Tessitura.CartLineItem lineItem)
        {
            //don;t append any ada item notes, as the logic in base class is not valid
            //base.RenderAdaLineItemInfo(lineItem);
        }
    }
}
