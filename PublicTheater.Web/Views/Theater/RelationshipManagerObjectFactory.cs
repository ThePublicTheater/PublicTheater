using System;
using PublicTheater.Web.Views.Theater.Controls;

namespace PublicTheater.Web.Views.Theater
{
    public class RelationshipManagerObjectFactory : PublicTheater.Custom.CoreFeatureSet.Common.PublicObjectFactory
    {
        public override object CreateInstance(Type baseType, Type givenType, string loadFrom, object loadFromArgument)
        {
            if (baseType == typeof(MediaPropertyManager))
            {
                return new MediaPropertyManagerArsht();
            }
            return base.CreateInstance(baseType, givenType, loadFrom, loadFromArgument);
        }
    }
}
