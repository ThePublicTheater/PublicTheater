using System;
using System.Collections.Generic;
using System.Linq;
using Adage.Tessitura;
using Adage.Tessitura.CartObjects;
using Adage.Tessitura.Tests.MockObjects;
using EPiServer;
using EPiServer.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PublicTheater.Custom.CoreFeatureSet.CartObjects;
using TheaterTemplate.Shared.Common;
using TheaterTemplate.Shared.EpiServerConfig;

namespace PublicTheater.Custom.Test
{
    [TestClass]
    public class PublicCartTest
    {
        [TestMethod]
        public void BuildSectionTextForTableSeatTest()
        {
            Performance newPerf = Performance.GetPerformance(24194); //April 2, 2014 Roy Nathanson & Sotto Voce at Joe's Pub
            VenueConfig venue = VenueConfigCollection.GetVenues
                (Config.GetValue(TheaterSharedAppSettings.VENUE_CONFIGURATIONS, 0)).GetVenueConfig(newPerf.VenueId);
            ReserveRequest reserve = new ReserveRequest();
            reserve.NumberOfSeats = 1;
            reserve.PriceTypeId = 119;
            reserve.SectionId = 803; //tables
            Assert.AreEqual(1, newPerf.PerformancePricing.ReserveTickets(reserve));
            PublicCart currentCart = PublicCart.GetCart() as PublicCart;
            PerformanceLineItem perfLineItem = currentCart.CartLineItems.First() as PerformanceLineItem;
            List<Ticket> tickets = perfLineItem.TicketBlocks.First() as List<Ticket>;
            Ticket testTicket = tickets.First() as Ticket;
            String expectedText = String.Format("Tables: Table {0} Seat {1}", testTicket.SeatRow, testTicket.SeatNumber);
            Assert.AreEqual(expectedText, currentCart.BuildSectionText(venue, tickets, "Tables"));
        }

        [TestMethod]
        public void BuildSectionTextForBarstoolSeatTest()
        {
            Performance newPerf = Performance.GetPerformance(24194); //April 2, 2014 Roy Nathanson & Sotto Voce at Joe's Pub
            VenueConfig venue = VenueConfigCollection.GetVenues
                (Config.GetValue(TheaterSharedAppSettings.VENUE_CONFIGURATIONS, 0)).GetVenueConfig(newPerf.VenueId);
            ReserveRequest reserve = new ReserveRequest();
            reserve.NumberOfSeats = 1;
            reserve.PriceTypeId = 119;
            reserve.SectionId = 804; //barstool
            Assert.AreEqual(1, newPerf.PerformancePricing.ReserveTickets(reserve));
            PublicCart currentCart = PublicCart.GetCart() as PublicCart;
            PerformanceLineItem perfLineItem = currentCart.CartLineItems.First() as PerformanceLineItem;
            List<Ticket> tickets = perfLineItem.TicketBlocks.First() as List<Ticket>;
            Ticket testTicket = tickets.First() as Ticket;
            String expectedText = String.Format("Barstool: Seat {1}", testTicket.SeatNumber);
            Assert.AreEqual(expectedText, currentCart.BuildSectionText(venue, tickets, "Tables"));
        }
    }
}
