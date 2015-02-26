using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using EPiServer.Framework.Localization;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Shell.ObjectEditing.EditorDescriptors;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace PublicTheater.Custom.Episerver.Properties
{
    public class EnumDescriptionSelectionFactory<TEnum> : ISelectionFactory
    {
        public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
        {
            foreach (var value in Enum.GetValues(typeof(TEnum)))
            {
                yield return new SelectItem
                {
                    Text = GetDisplayName(value),
                    Value = value
                };
            }
        }

        private string GetDisplayName(object value)
        {
            var staticName = Enum.GetName(typeof(TEnum), value);

            var localizationPath = string.Format("/property/enum/{0}/{1}", typeof(TEnum).Name.ToLowerInvariant(), staticName.ToLowerInvariant());

            string localizedName;
            if (LocalizationService.Current.TryGetString(localizationPath, out localizedName))
            {
                return localizedName;
            }

            var attributes = (DescriptionAttribute[])typeof(TEnum).GetField(staticName).GetCustomAttributes((typeof(DescriptionAttribute)), false);
            return attributes.Length > 0 ? attributes[0].Description : staticName;
        }
    }

    public class EnumDescriptionEditorDescriptor<TEnum> : EditorDescriptor
    {
        public override void ModifyMetadata(
            ExtendedMetadata metadata,
            IEnumerable<Attribute> attributes)
        {
            SelectionFactoryType = typeof(EnumDescriptionSelectionFactory<TEnum>);

            ClientEditingClass = "epi.cms.contentediting.editors.SelectionEditor";

            base.ModifyMetadata(metadata, attributes);
        }
    }
}
