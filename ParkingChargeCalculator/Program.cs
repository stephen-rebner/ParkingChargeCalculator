using ParkingChargeCalculator.Calculator;
using ParkingChargeCalculator.Strategies;
using ParkingChargeCalculator.Strategies.Interfaces;
using System;
using ParkingChargeCalculator.DI;

namespace ParkingChargeCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            var diResolver = new DIResolver();

            var shortStayStrategy = diResolver.Get<IParkingChargeStrategy, ShortStayStrategy>();
            var longStayStrategy = diResolver.Get<IParkingChargeStrategy, LongStayStrategy>();

            var calculator = diResolver.Get<IParkingCostCalculator>();
            calculator.SetStrategy(shortStayStrategy);

            Console.WriteLine(
                calculator.Calculate(DateTime.Parse("01/08/2020 00:00:00"), DateTime.Parse("02/08/2020 23:59:59"))
                );

            Console.WriteLine(
               calculator.Calculate(DateTime.Parse("07/09/2017 16:50:00"), DateTime.Parse("09/09/2017 19:15:00"))
               );

            calculator.SetStrategy(longStayStrategy); // swap strategy

            Console.WriteLine(
               calculator.Calculate(DateTime.Parse("07/09/2017 07:50:00"), DateTime.Parse("07/09/2017 08:20:00"))
               );

            Console.WriteLine(
               calculator.Calculate(DateTime.Parse("07/09/2017 07:50:00"), DateTime.Parse("09/09/2017 05:20:00"))
               );
        }
    }
}
