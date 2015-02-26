using Microsoft.VisualStudio.TestTools.UnitTesting;
using PublicTheater.Custom.CoreFeatureSet.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PublicTheater.Custom.Test.CoreFeatureSet.Common
{
    [TestClass]
    public class PublicGalaTransactionsTest
    {
        [TestMethod]
        public void CanGetGalaTransactions()
        {
            var galaDetails = PublicGalaTransactions.GetGalaDetails();

            Assert.IsTrue(galaDetails.Count > 0);
        }
    }
}
