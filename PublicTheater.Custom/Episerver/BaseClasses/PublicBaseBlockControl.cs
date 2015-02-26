using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EPiServer.Core;
using EPiServer.Web;

namespace PublicTheater.Custom.Episerver.BaseClasses
{
    public class PublicBaseBlockControl<TBlock> : BlockControlBase<TBlock> where TBlock : PublicBaseBlockData
    {
        
    }
}
