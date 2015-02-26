using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Web;
using EPiServer.XForms;
using PublicTheater.Custom.Episerver.BaseClasses;

namespace PublicTheater.Custom.Episerver.Pages
{
    [ContentType(GUID = "3158d8e5-599a-44ba-bc2f-2dab4555aef8", DisplayName = "[Public Theater] Blank Util Page", GroupName = Constants.ContentGroupNames.ContentGroupName)]
    public class BlankUtilPageData : PublicBasePageData
    {
        #region Properties

        [Display(GroupName = SystemTabNames.Content, Name = "Main Body")]
        public virtual XhtmlString MainBody { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Misc")]
        [UIHint(UIHint.Textarea)]
        public virtual String Misc { get; set; }

        [Display]
        public virtual XForm xForm { get; set; }

        #endregion
    }
}
