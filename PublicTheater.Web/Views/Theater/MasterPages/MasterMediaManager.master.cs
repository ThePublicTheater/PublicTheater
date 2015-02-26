#region Copyright
// Copyright © EPiServer AB.  All rights reserved.
// 
// This code is released by EPiServer AB under the Source Code File - Specific License Conditions, published August 20, 2007. 
// See http://www.episerver.com/Specific_License_Conditions for details.
#endregion

namespace PublicTheater.Web.Views.Theater.MasterPages
{
    /// <summary>
    /// The masterpage defines the common look and feel and a standard behavior of the website. 
    /// </summary>
    public partial class MasterMediaManager : System.Web.UI.MasterPage
    {
        public string LanguageString { get; private set; }
    }
}
