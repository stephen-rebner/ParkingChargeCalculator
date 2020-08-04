using System;

namespace ParkingChargeCalculator.Utils
{
    internal static class Validator
    {
        internal static void ThrowIfStartDateGtOrEqEndDate(DateTime startDateTime, DateTime endDateTime)
        {
            if(startDateTime >= endDateTime)
            {
                throw new InvalidOperationException(
                    "The Start Date Time cannot be greater than or equal the End Date Time");
            }
        }
    }
}
