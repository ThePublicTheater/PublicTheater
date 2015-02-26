using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace PublicTheater.Custom.Episerver.TinyMCE
{
    public class EmptyFileHandler : IHttpHandler
    {
        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
        }
    }
}
