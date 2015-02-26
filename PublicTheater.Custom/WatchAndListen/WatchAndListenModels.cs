using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PublicTheater.Custom.Models
{
    public class WatchAndListenModels
    {
        [Serializable]
        public class PlayerConfig
        {
            // Currently Unused, but could pass globals into media player.
        }

        [Serializable]
        public class MediaItem
        {
            public MediaItem()
            {
                ParentMediaItemId = -1;
            }

            public int MediaId { get; set; }
            public string MediaItemTitle { get; set; }
            public string MediaItemSubTitle { get; set; }
            public string MediaDescriptionHTML { get; set; }

            public string MediaType { get; set; }
            public string MediaURL { get; set; }
            public string WatchAndListenURL { get; set; }

            public int ParentMediaItemId { get; set; }

            public bool VisibleOnGrid { get; set; }
            public string Category { get; set; }

            public List<int> RelatedPlayDetailIDs { get; set; }
            public List<int> RelatedEventDetailIDs { get; set; }

            public string CoverURL { get; set; }

            public DateTime CreatedDateTime { get; set; }
        }

        [Serializable]
        public class WatchAndListenData
        {
            public PlayerConfig Config { get; set; }
            public List<MediaItem> MediaItems { get; set; }
        }

        [Serializable]
        public class PressPhotoData
        {
            public string PageTitle { get; set; }
            public string PageURL { get; set; }
            public string CoverImageURL { get; set; }
            public int ItemListLength { get; set; }
            public string PageType { get; set; }
        }
    }
}
