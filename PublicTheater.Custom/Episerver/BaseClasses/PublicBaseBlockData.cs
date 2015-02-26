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
    public abstract class PublicBaseBlockData : EPiServer.Core.BlockData
    {
        /* BEST PRACTICE TIP
         * Always use your own base class for block types in your projects,
         * instead of having your block types inherit BlockData directly.
         * That way you can easily extend all block types by modifying your base class. */
    }

}