using System;
using Adage.Tessitura.Common;
using TheaterTemplate.SharedTest;

namespace Public.CoreFeatureSet.Test
{

    public class PublicTestObjectFactory : PublicTheater.Custom.CoreFeatureSet.Common.PublicObjectFactory
    {
        public override object CreateInstance(Type baseType, Type givenType, string loadFrom, object loadFromArgument)
        {
            if (baseType == typeof(Adage.Tessitura.TessituraAPI))
                return new Adage.Tessitura.Tests.MockObjects.MockTessituraAPI();

            if (baseType == typeof(Adage.Tessitura.Config))
                return new Adage.Tessitura.Config();

            if (baseType == typeof(DataSetResourceManager))
                return new TemplateDataSetResourceManager();

            return base.CreateInstance(baseType, givenType, loadFrom, loadFromArgument);
        }
    }
}
