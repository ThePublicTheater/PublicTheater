using System.Linq;
using Adage.Tessitura;
using PublicTheater.Custom.CoreFeatureSet.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using PublicTheater.Custom.Episerver;
using System.Collections.Generic;

namespace PublicTheater.Custom.Test
{
    
    
    /// <summary>
    ///This is a test class for PerformanceHelperTest and is intended
    ///to contain all PerformanceHelperTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PerformanceHelperTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for GetPerformancesBySiteTheme
        ///</summary>
        [TestMethod()]
        public void GetPerformancesBySiteThemeTest()
        {
            var perfTypes= Performances.GetPerformances(new PerformancesCriteria()).ToArray();
            var stringvaluees =perfTypes.Select(per => per.PerformanceType).Distinct();
        }
    }
}
