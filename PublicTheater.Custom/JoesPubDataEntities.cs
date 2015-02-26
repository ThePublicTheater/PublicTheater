using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace PublicTheater.Custom
{
    public partial class JoesPubDataEntities
    {
        private static JoesPubDataEntities _adageEntities = null;
        public static JoesPubDataEntities Current
        {
            get
            {
                if (HttpContext.Current != null)
                {
                    if (HttpContext.Current.Items["JoesPubDataEntities"] == null)
                        HttpContext.Current.Items.Add("JoesPubDataEntities", new JoesPubDataEntities());

                    return (JoesPubDataEntities)HttpContext.Current.Items["JoesPubDataEntities"];
                }

                return _adageEntities ?? (_adageEntities = new JoesPubDataEntities());
            }
        }
    }
}
