using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace PublicTheater.Custom.Test
{
    [TestClass]
    public class PublicBaseTest : TheaterTemplate.SharedTest.BaseTest
    {
        [AssemblyInitialize]
        public static void Initialize(TestContext context)
        {
            //EPiServerInitializer.Initialize(context);
        }
    }
}
