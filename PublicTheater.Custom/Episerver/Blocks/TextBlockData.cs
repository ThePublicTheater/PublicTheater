using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;

namespace PublicTheater.Custom.Episerver.Blocks
{
    [ContentType(GUID = "f7986ba2-aaa1-41a0-ba31-4a3d8381bb81", DisplayName = "Text Block", GroupName = Constants.ContentGroupNames.ContentGroupName)]
    public class TextBlockData : BaseClasses.PublicBaseResponsiveBlockData
    {
        
        [Display(Name = "Text", GroupName = SystemTabNames.Content)]
        public virtual XhtmlString Text { get; set; }

        [Display(Name = "Use Header Style", GroupName = SystemTabNames.Content)]
        public virtual bool HeaderStyle { get; set; }

        [Display(Name = "Css Class", GroupName = SystemTabNames.Content)]
        public virtual string CssClassOverride { get; set; }

        public override void SetDefaultValues(ContentType contentType)
        {
            base.SetDefaultValues(contentType);

            HeaderStyle = false;

        }

    }
}
