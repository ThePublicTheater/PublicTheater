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
    [ContentType(GUID = "84c55191-2173-768a-aade-4764ad32bd7e", DisplayName = "Resource Room Block", GroupName = Constants.ContentGroupNames.MediaGroupName)]
    public class RRBlockData : BaseClasses.PublicBaseBlockData
    {

        [Display(GroupName = SystemTabNames.Content, Name = "Resource Room Show Blocks")]
        public virtual ContentArea RRShows { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Open Box Text")]
        public virtual XhtmlString OpenShowText { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Closed Box Text")]
        public virtual XhtmlString ClosedShowText { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Back To Shows Image")]
        public virtual Url BackToShowImage { get; set; }
       

    }
}