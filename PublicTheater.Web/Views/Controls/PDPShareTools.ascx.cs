using System;
using EPiServer;
using EPiServer.Core;
using PublicTheater.Custom.Episerver.BaseClasses;

namespace PublicTheater.Web.Views.Controls
{
    public partial class PDPShareTools : PublicBaseUserControl
    {
        private bool? _showSocialIcons;
        public bool ShowSocialIcons
        {
            get
            {
                if (_showSocialIcons.HasValue == false)
                {
                    if (CurrentPage.Property["EnableSocialIcons"] != null && CurrentPage.Property["EnableSocialIcons"].Value != null)
                    {
                        _showSocialIcons = (bool)CurrentPage.Property["EnableSocialIcons"].Value;
                    }
                    else
                    {
                        _showSocialIcons = false;
                    }
                }
                
                return _showSocialIcons.Value;
            }
            set { _showSocialIcons = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Visible = ShowSocialIcons;
        }
    }
}