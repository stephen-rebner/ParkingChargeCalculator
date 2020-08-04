using System;

namespace ParkingChargeCalculator.Strategies.Interfaces
{
    public interface IParkingChargeStrategy
    {
        string CalculatePrice(DateTime startDateTime, DateTime endDateTime);
    }
}
