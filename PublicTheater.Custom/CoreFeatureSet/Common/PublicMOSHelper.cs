using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Adage.Tessitura;
using Adage.Tessitura.CartObjects;
using TheaterTemplate.Shared.Common;

namespace PublicTheater.Custom.CoreFeatureSet.Common
{
    public class PublicMOSHelper : TheaterTemplate.Shared.Common.TheaterSharedMOSHelper
    {
        public virtual void TriggerFlexPackageConflict()
        {
            bool cartContainsFlexPackages = Cart.GetCart().CartLineItems
                .OfType<FlexPackageLineItem>()
                .Any();

            if (cartContainsFlexPackages && HttpContext.Current != null)
            {
                HttpContext.Current.Response.Redirect(Config.GetValue(TheaterSharedAppSettings.MOS_CONFLICT_URL, string.Empty));
            }
        }

        /// <summary>
        /// remove flex promocode when returned back to single ticket path
        /// </summary>
        public override void VerifySingleTicketMOS()
        {
            base.VerifySingleTicketMOS();
            
            ClearFlexPackageSourceCode();
        }

        /// <summary>
        /// clear flex package related source code if session is in flex path
        /// </summary>
        public static void ClearFlexPackageSourceCode()
        {
            if (PublicTessSession.IsInFlexPackagePath())
            {
                TessSession.GetSession().UpdateSourceCode(TessSession.DEFAULT_PROMOTION_CODE.ToString());
            }
        }
    }
}
