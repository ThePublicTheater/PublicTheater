using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PublicTheater.Custom.CoreFeatureSet.UserObjects;
using PublicTheater.Custom.Episerver;

namespace PublicTheater.Custom.CoreFeatureSet.Common
{
    public class PublicEmailObject : TheaterTemplate.Shared.Common.TheaterSharedEmailObject
    {
        protected override string GetCodeProperty(string name)
        {
            var lookupName = name.ToUpper();
            if (lookupName.Equals(Constants.CFSEmailCodeProperty.CUSTOMERNUMBER))
            {
                return _cart.CustomerNumber.ToString();
            }
            if (lookupName.Equals(Constants.CFSEmailCodeProperty.ACCOUNTADDRESS))
            {
                var publicUser = _currentUser as PublicUser;
                if(publicUser!=null)
                {
                    return RenderAddress(publicUser.BillingAddress);
                }
            }
            return base.GetCodeProperty(name);
        }

        /// <summary>
        /// use public customized renderer engines
        /// </summary>
        /// <returns></returns>
        protected override TheaterTemplate.Shared.Common.TheaterSharedEmailCartRenderer GetRenderer()
        {
            if (IsHTML)
                return new PublicEmailHtmlCartRenderer(_CMSPage);
            else
                return new PublicEmailTextCartRenderer(_CMSPage);
        }
    }

    
}
