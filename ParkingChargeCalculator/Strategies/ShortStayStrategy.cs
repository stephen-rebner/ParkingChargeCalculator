using ParkingChargeCalculator.Constants;
using ParkingChargeCalculator.Strategies.Interfaces;
using ParkingChargeCalculator.Utils;
using System;
using System.Globalization;

namespace ParkingChargeCalculator.Strategies
{
    public class ShortStayStrategy : IParkingChargeStrategy
    {
        public string CalculatePrice(DateTime startDateTime, DateTime endDateTime)
        {
            // Seperated out method below into static utils method below
            // since it maybe in future that we need to reuse logic for seperate strategy.
            // Caution should be applied when using static class, however
            // I went for it in this small app since the util class is performing a calculation
            // and holds no state. The calculation logic is unlikely to change.
            var seconds = ChargeableTimeCalculator.CalculateShortStayChargableTimeInSecs(startDateTime, endDateTime);

            var timespan = TimeSpan.FromSeconds(seconds);

            var cost = timespan.TotalHours * AppConstants.ShortStayCharge;

            return cost.ToString("C", CultureInfo.CurrentCulture);
        }
    }
}
