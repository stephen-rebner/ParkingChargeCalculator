using ParkingChargeCalculator.Strategies.Interfaces;
using System;

namespace ParkingChargeCalculator.Calculator
{
    public interface IParkingCostCalculator
    {
        void SetStrategy(IParkingChargeStrategy parkingChargeStrategy);

        string Calculate(DateTime startDateTime, DateTime endDateTime);
    }
}
