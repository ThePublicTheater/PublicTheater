using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using PublicTheater.Custom.Episerver.Properties;

namespace PublicTheater.Custom.Episerver.BaseClasses
{
    /// <summary>
    /// Base class for all block types on the site
    /// </summary>
    public abstract class PublicBaseResponsiveBlockData : PublicBaseBlockData
    {
        /* BEST PRACTICE TIP
         * Always use your own base class for block types in your projects,
         * instead of having your block types inherit BlockData directly.
         * That way you can easily extend all block types by modifying your base class. */

        [BackingType(typeof(PropertyNumber))]
        [EditorDescriptor(EditorDescriptorType = typeof(EnumEditorDescriptor<Enums.BlockLargeView>))]
        [Display(Order = 91, GroupName = SystemTabNames.Content)]
        public virtual Enums.BlockLargeView LargeView { get; set; }

        [BackingType(typeof(PropertyNumber))]
        [EditorDescriptor(EditorDescriptorType = typeof(EnumEditorDescriptor<Enums.BlockMediumView>))]
        [Display(Order = 92, GroupName = SystemTabNames.Content)]
        public virtual Enums.BlockMediumView MediumView { get; set; }

        [BackingType(typeof(PropertyNumber))]
        [EditorDescriptor(EditorDescriptorType = typeof(EnumEditorDescriptor<Enums.BlockHeight>))]
        [Display(Order = 93, GroupName = SystemTabNames.Content)]
        public virtual Enums.BlockHeight Height { get; set; }

        [BackingType(typeof(PropertyBoolean))]
        [Display(Order = 94, GroupName = SystemTabNames.Content)]
        public virtual bool DisableResponsive { get; set; }

        public override void SetDefaultValues(ContentType contentType)
        {
            base.SetDefaultValues(contentType);

            Height = Enums.BlockHeight.Auto;
            LargeView = Enums.BlockLargeView.Full;
            MediumView = Enums.BlockMediumView.Full;

        }
    }

}