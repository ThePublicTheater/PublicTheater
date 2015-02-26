using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;

namespace PublicTheater.Custom.Episerver
{
    public static class Enums
    {
        public enum SiteTheme
        {
            [Description("The Public Theater")]
            Default = 0,
            [Description("Joe's Pub")]
            JoesPub = 1,
            [Description("Shakespeare in the Park")]
            Shakespeare = 2,
            [Description("Under the Radar Festival")]
            UnderTheRadar = 3,
            [Description("Public Forum")]
            PublicForum = 4,
            [Description("Emerging Writers Group")]
            EmergingWritersGroup = 5,
            [Description("Here Lies Love")]
            HereLiesLove = 6,
            [Description("Intranet")]
            Intranet = 7,
        }

        public enum BlockLargeView
        {
            Quarter = 0,
            Half = 1,
            ThreeQuarters = 2,
            Full = 3
        }

        public enum BlockMediumView
        {
            Half = 0,
            Full = 1
        }

        public enum BlockHeight
        {
            Single = 0,
            Double = 1,
            Triple = 2,
            Auto = 3,
        }

        public static string GetEnumDescription(Enum value) {
            var fieldInfo = value.GetType().GetField(value.ToString());
            var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes((typeof(DescriptionAttribute)), false);
            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }

        public static IEnumerable<T> EnumToList<T>() {

            var enumType = typeof(T);

            if (enumType.BaseType != typeof(Enum))
                throw new ArgumentException("T must be of type System.Enum");
            
            var enumValArray = Enum.GetValues(enumType);
            var enumValList = new List<T>(enumValArray.Length);
            foreach (int val in enumValArray)
            {
                enumValList.Add((T) Enum.Parse(enumType, val.ToString()));
            }
            return enumValList;
        }

        public static List<string> GetSiteThemeEnumValues()
        {
            return EnumToList<SiteTheme>().Select(theme => GetEnumDescription(theme)).ToList();
        }

        public static string GetBlockResponsiveClass(BlockLargeView largeView, BlockMediumView mediumView, BlockHeight height)
        {
            var responsiveClasses = new List<string>();

            //everything is full width in small
            responsiveClasses.Add("small-12");

            //medium view
            switch (mediumView)
            {
                case BlockMediumView.Full:
                    responsiveClasses.Add("medium-12");
                    break;
                case BlockMediumView.Half:
                    responsiveClasses.Add("medium-6");
                    break;
                default:
                    responsiveClasses.Add("medium-12");
                    break;
            }

            //large view
            switch (largeView)
            {
                case BlockLargeView.Quarter:
                    responsiveClasses.Add("large-3");
                    break;
                case BlockLargeView.Half:
                    responsiveClasses.Add("large-6");
                    break;
                case BlockLargeView.ThreeQuarters:
                    responsiveClasses.Add("large-9");
                    break;
                case BlockLargeView.Full:
                    responsiveClasses.Add("large-12");
                    break;
                default:
                    responsiveClasses.Add("large-6");
                    break;
            }
            
            responsiveClasses.Add("columns");

            //height
            responsiveClasses.Add("height-" + height);

            return string.Join(" ", responsiveClasses);
        }
    

        public enum MediaType
        {
            [Description("Youtube Video")]
            youtube,

            [Description("Audio")]
            audio,

            [Description("Image")]
            image,

            [Description("SoundCloud")]
            soundCloud,
            [Description("Vimeo Video")]
            vimeo,
        }
    
    }
}
