using System;
using System.Web.UI.WebControls;
using EPiServer.Core;
using EPiServer.Web.WebControls;

namespace PublicTheater.Web.Views.Theater.Controls
{

    public class EPiServerFileBrowser : WebControl
    {
        /// <summary>
        /// Returns the path to the selected file
        /// </summary>
        public string FilePath
        {
            get
            {
                var textbox = this.FindControl("TextBoxFilePath") as Property;
                if (textbox == null)
                    return string.Empty;
                ((IPropertyControl)textbox.Controls[0]).ApplyChanges();
                return textbox.PropertyValue==null?string.Empty: textbox.PropertyValue.ToString();
            }
            set
            {
                var textbox = FindControl("TextBoxFilePath") as Property;
                if (textbox == null)
                    return;
                textbox.InnerProperty.Value = value;
            }
        }
        /// <summary>
        /// Setup control and include necessary scripts
        /// </summary>
        private void SetupControl()
        {
            var property = new Property(new EPiServer.SpecializedProperties.PropertyImageUrl()) { EditMode = true, ID = "TextBoxFilePath" };

            this.Controls.Add(property);
        }

        protected override void OnInit(EventArgs e)
        {
            SetupControl();
            base.OnInit(e);
        }
    }

}