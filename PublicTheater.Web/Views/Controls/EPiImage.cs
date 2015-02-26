using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using EPiServer;

namespace TheaterTemplate.Web.Views.Controls
{
    public class EPiImage : Image
    {
        public String PropertyName { get; set; }

        public override string ImageUrl
        {
            get { return base.ImageUrl; }
            set { throw new InvalidOperationException("Cannot set the Image URL property manually"); }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (String.IsNullOrEmpty(PropertyName))
                throw new InvalidOperationException("Cannot use an EpiImage without setting a PropertyName");

            if (String.IsNullOrEmpty(ImageUrl) && ((PageBase) Page).CurrentPage[PropertyName] != null)
                base.ImageUrl = ((PageBase) Page).CurrentPage[PropertyName].ToString();

            if (String.IsNullOrEmpty(ImageUrl))
                Visible = false;

            if (Visible)
                base.Render(writer);
        }
    }
}