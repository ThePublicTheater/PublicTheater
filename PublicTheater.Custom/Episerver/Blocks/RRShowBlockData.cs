using System.Collections.Generic;
using Adage.Tessitura;
using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Core;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Web;
using EPiServer;
using PublicTheater.Custom.Episerver.BaseClasses;
using PublicTheater.Custom.Episerver.Properties;

namespace PublicTheater.Custom.Episerver.Blocks
{
    [ContentType(GUID = "84c55111-5173-468a-aade-4764ad32bd7e", DisplayName = "Resource Room Show Block", GroupName = Constants.ContentGroupNames.MediaGroupName)]
    public class RRShowBlockData : BaseClasses.PublicBaseBlockData
    {

        [Display(GroupName = SystemTabNames.Content, Name = "Items")]
        [BackingType(typeof(ResourceRoomItems))]
        public virtual string Items { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Cover Image")]
        public virtual Url PosterImage { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Show Text")]
        public virtual XhtmlString ShowText { get; set; }

        public virtual List<ResourceRoomItem> GetItems()
        {
            var propertyValue = Property["Items"] as ResourceRoomItems;
            return propertyValue != null ? propertyValue.ResourceRoomItemList : new List<ResourceRoomItem>();
        }

    }
}