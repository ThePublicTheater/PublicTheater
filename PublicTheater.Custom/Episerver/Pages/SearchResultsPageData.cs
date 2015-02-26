using System.ComponentModel.DataAnnotations;
using Adage.Theater.RelationshipManager.PlugIn;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Core;
using EPiServer.UI.Admin;
using EPiServer.Web;
using EPiServer;
using PublicTheater.Custom.Episerver.BaseClasses;

namespace PublicTheater.Custom.Episerver.Pages
{
    [ContentType(GUID = "ffa3219c-1133-4422-b848-6def80c59331", DisplayName = "[Public Theater] Search Results", GroupName=Constants.ContentGroupNames.ContentGroupName)]
    public class SearchResultsPageData : PublicBasePageData
    {
        
    }
}