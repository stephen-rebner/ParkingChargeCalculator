using ParkingChargeCalculator.Constants;
using System;

namespace ParkingChargeCalculator.Utils
{
    public static class ChargeableTimeCalculator
    {
        public static int CalculateShortStayChargableTimeInSecs(DateTime startDateTime, DateTime endDateTime)
        {
            var seconds = 0;
            for (var i = startDateTime; i < endDateTime; i = i.AddSeconds(1))
            {
                if (i.DayOfWeek != DayOfWeek.Saturday && i.DayOfWeek != DayOfWeek.Sunday)
                {
                    if (i.TimeOfDay.Hours >= AppConstants.StartChargableHour && i.TimeOfDay.Hours < AppConstants.EndChargableHour)
                    {
                        seconds++;
                    }
                }
            }

            return seconds;
        }

        public static int CalculateStayInNumberOfDays(DateTime startDateTime, DateTime endDateTime)
        {
            var days = 0;

            for (var i = startDateTime.Date; i <= endDateTime.Date; i = i.AddDays(1))
            {
                days++;
            }

            return days;
        }
    }
}
