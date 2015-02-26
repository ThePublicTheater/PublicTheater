using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using EPiServer.Editor;

namespace PublicTheater.Web.Views.Controls
{
    public class Property : EPiServer.Web.WebControls.Property
    {
        [DefaultValue(false)]
        public override bool DisplayMissingMessage
        {
            get
            {
                return base.DisplayMissingMessage;
            }
            set
            {
                base.DisplayMissingMessage = value;
            }
        }

        /// <summary>
        /// If property has no content or just empty string, don't render the empty property in default page view
        /// </summary>
        protected override void CreateChildControls()
        {
            if (!PageEditing.PageIsInEditMode || !this.Editable)
            {
                //in public view render mode
                if(InnerProperty==null || InnerProperty.Value==null || string.IsNullOrEmpty(InnerProperty.Value.ToString()))
                    return;
            }
            base.CreateChildControls();
        }
    }
}