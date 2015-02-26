define([

    "mediaItems/mediaItem/mediaItem",
    "mediaItems/ytMediaItem/ytMediaItem",
    "mediaItems/audioMediaItem/audioMediaItem",
    "mediaItems/soundCloudMediaItem/soundCloudMediaItem",
    "mediaItems/imageMediaItem/imageMediaItem",
    "mediaItems/vimeoMedia/vimeoMediaItem"
], function (BaseMediaItem, YoutubeMediaItem, AudioMediaItem, SoundCloudMediaItem, ImageMediaItem, VimeoMediaItem) {

    var mediaItems = {};

    mediaItems.BaseMediaItem = BaseMediaItem;
    mediaItems.YoutubeMediaItem = YoutubeMediaItem;
    mediaItems.AudioMediaItem = AudioMediaItem;
    mediaItems.SoundCloudMediaItem = SoundCloudMediaItem;
    mediaItems.ImageMediaItem = ImageMediaItem;
    mediaItems.VimeoMediaItem = VimeoMediaItem;
    return mediaItems;
});