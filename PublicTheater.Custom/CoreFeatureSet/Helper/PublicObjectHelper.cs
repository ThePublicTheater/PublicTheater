using System;
using Adage.Tessitura;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheaterTemplate.Shared.Common;
using TheaterTemplate.Shared.EpiServerConfig;

namespace PublicTheater.Custom.CoreFeatureSet.Helper
{
    public static class PublicObjectHelper
    {
        public static VenueConfigCollection GetVenueConfigurations()
        {
            return VenueConfigCollection.GetVenues(Config.GetValue(TheaterSharedAppSettings.VENUE_CONFIGURATIONS, 0));
        }

        /// <summary>
        /// Get Cart Timer String
        /// </summary>
        /// <returns></returns>
        public static string GetCartTimer()
        {
            var seatServerTimer = GetSeatServerTimer();
            if (seatServerTimer.IsRunning() == false)
                return string.Empty;
            if (seatServerTimer.RemainingTime.Seconds < 0 || seatServerTimer.RemainingTime.Minutes < 0)
                return "Expired";
            return string.Format("{0:00}:{1:00}", seatServerTimer.RemainingTime.Minutes, seatServerTimer.RemainingTime.Seconds);

        }

        /// <summary>
        /// Get Current Shopping Cart Timer
        /// </summary>
        public static SeatServerTimer GetSeatServerTimer()
        {
            return Repository.Get<SeatServerTimer>();
        }
    }
}
