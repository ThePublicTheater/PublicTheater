using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using PublicTheater.Custom.Episerver.Properties;

namespace PublicTheater.Custom.Episerver.Blocks
{
    [ContentType(GUID = "d993ba4d-9ef6-418e-8f30-d3b9b87c013f", DisplayName = "Accordion Block", GroupName = Constants.ContentGroupNames.UniversalBlocks)]
    public class AccordionBlockData : BaseClasses.PublicBaseBlockData
    {
        [Display(GroupName = SystemTabNames.Content, Name = "Accordion Items")]
        [BackingType(typeof(AccordionItems))]
        public virtual string AccordionItems { get; set; }

        public virtual List<AccordionItem> GetAccordionItems()
        {
            var propertyValue = Property["AccordionItems"] as AccordionItems;
            return propertyValue != null ? propertyValue.AccordionItemList : new List<AccordionItem>();
        }
    }

}
