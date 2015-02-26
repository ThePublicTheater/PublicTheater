using System;
using Adage.Tessitura;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheaterTemplate.Shared.EpiServerConfig;
using System.Collections.Generic;
using System.Linq;
using PublicTheater.Custom.CoreFeatureSet.EpiServerConfig;


namespace PublicTheater.Custom.Test
{
    [TestClass]
    public class PublicVenueTest : TheaterTemplate.SharedTest.BaseTest
    {
        [TestMethod]
        public void GetVenueConfigs()
        {
            
            //how to get venue config collection?
            var venueConfigs = VenueConfigCollection.GetVenues(Config.GetValue("VENUE_CONFIG_COLLECTION_PAGE_ID", 102));

            var publicVenueConfigs = venueConfigs.VenueCollection;
        }
    }
}
