using EPiServer.Editor.TinyMCE;

namespace PublicTheater.Custom.Episerver.TinyMCE
{
    /// <summary>
    /// http://www.frederikvig.com/2010/10/how-to-add-support-for-iframes-and-other-elements-to-tinymce-in-episerver-cms/
    /// </summary>
    [TinyMCEPluginNonVisual(AlwaysEnabled = true, PlugInName = "ExtendedValidElementsPlugin", EditorInitConfigurationOptions = @"
{
    extended_valid_elements:'iframe[src|frameborder=0|alt|title|width|height|align|name], object[data|height|width], a[*], div[*], nav[*]',
    media_strict : false
}")]
    public class ExtendedValidElements
    {

    }


}
