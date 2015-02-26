using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EPiServer;

namespace PublicTheater.Custom.Episerver.BaseClasses
{
    public class PublicBaseUserControl : UserControlBase
    {
        private Enums.SiteTheme? _requestTheme;
        protected Enums.SiteTheme RequestedTheme
        {
            get
            {
                if (!_requestTheme.HasValue)
                {
                    _requestTheme = Utility.Utility.GetRequestedTheme(CurrentPage);
                }
                return _requestTheme.Value;
            }
        }
    }
}
