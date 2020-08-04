namespace ParkingChargeCalculator.Constants
{
    internal class AppConstants
    {
        // for this console app I have created constants for these values
        // so they can be access from one place.
        // However in a real work app, it could be that they are stored
        // somewhere like a DB, especially if the values are likely to change
        // in the future.
        internal const double ShortStayCharge = 1.1;
        internal const double LongStayCharge = 7.5;
        internal const int StartChargableHour = 8;
        internal const int EndChargableHour = 18;
    }
}
