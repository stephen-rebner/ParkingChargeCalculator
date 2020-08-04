using ParkingChargeCalculator.Strategies.Interfaces;
using ParkingChargeCalculator.Utils;
using System;

namespace ParkingChargeCalculator.Calculator
{
    public class ParkingCostCalculator : IParkingCostCalculator
    {
        private IParkingChargeStrategy _parkingChargeStrategy;

        public void SetStrategy(IParkingChargeStrategy parkingChargeStrategy)
        {
            _parkingChargeStrategy = parkingChargeStrategy;
        }

        public string Calculate(DateTime startDateTime, DateTime endDateTime)
        {
            // In a real world app i.e., a rest API, I would would data annotations
            // to validate objects; perhaprs using the Fluent Validator Nuget Package
            // Since this is a small console app, I have went with a simple Validator class instead
            Validator.ThrowIfStartDateGtOrEqEndDate(startDateTime, endDateTime);

            return _parkingChargeStrategy.CalculatePrice(startDateTime, endDateTime);
        }
    }
}
