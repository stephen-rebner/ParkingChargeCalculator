using NUnit.Framework;
using ParkingChargeCalculator.Utils;
using System;
using System.Collections;

namespace ParkingChargeCalculator.Tests.Utils
{
    public class ChargableTimeCalculatorTests
    {
        [TestCaseSource(typeof(ChargableTimeCalculatorTestCase), nameof(ChargableTimeCalculatorTestCase.CalculateStayInNumberOfDaysTestCases))]
        public int CalculateStayInNumberOfDays_ShouldAdd1ExtraDay_ForEachWholeOrPartDay(string startDateValue, string endDateValue)
        {
            var startDateTime = DateTime.Parse(startDateValue);
            var endDateTime = DateTime.Parse(endDateValue);

            return ChargeableTimeCalculator.CalculateStayInNumberOfDays(startDateTime, endDateTime);
        }

        [TestCaseSource(typeof(ChargableTimeCalculatorTestCase), nameof(ChargableTimeCalculatorTestCase.ChargablePerodTestCases))]
        public int CalculateShortStayChargableTimeInSecs_ShouldCalculateChargableSeconds_ForChargablePeriod(string startDateValue, string endDateValue)
        {
            var startDateTime = DateTime.Parse(startDateValue);
            var endDateTime = DateTime.Parse(endDateValue);

            return ChargeableTimeCalculator.CalculateShortStayChargableTimeInSecs(startDateTime, endDateTime);
        }
    }

    public class ChargableTimeCalculatorTestCase
    {
        public static IEnumerable CalculateStayInNumberOfDaysTestCases
        {
            get
            {
                yield return new TestCaseData("07/09/2017 00:00:00", "07/09/2017 00:00:01").Returns(1);
                yield return new TestCaseData("07/09/2017 00:00:00", "07/09/2017 23:59:59").Returns(1);
                yield return new TestCaseData("07/09/2017 00:00:00", "08/09/2017 00:00:00").Returns(2); 
                yield return new TestCaseData("07/09/2017 07:50:00", "09/09/2017 05:20:00").Returns(3); 
            }
        }

        public static IEnumerable ChargablePerodTestCases
        {
            get
            {
                yield return new TestCaseData("07/09/2017 08:00:00", "07/09/2017 18:00:00").Returns(36000); // 10 hours, all inside chargable period
                yield return new TestCaseData("07/09/2017 00:00:00", "07/09/2017 23:59:59").Returns(36000); // 23 hours, 59 mins, 59 secs. Only 10 hours chargable
                yield return new TestCaseData("07/09/2017 00:00:00", "08/09/2017 18:00:00").Returns(72000); // 42 hours, 20 chargable 
                yield return new TestCaseData("07/09/2017 00:00:00", "08/09/2017 23:59:59").Returns(72000); // 47 hours, 59 mins, 59 secs. Only 10 hours 
                yield return new TestCaseData("31/07/2020 00:00:00", "03/08/2020 07:59:59").Returns(36000); // Friday (Midnight) to Monday morning (1 sec before chargable period)
                yield return new TestCaseData("31/07/2020 00:00:00", "03/08/2020 23:59:59").Returns(72000); // Friday (Midnight) to Monday Night (1 sec before Midnight)
                yield return new TestCaseData("31/07/2020 08:10:00", "31/07/2020 08:11:00").Returns(60); // 1 minute
                yield return new TestCaseData("31/07/2020 08:01:00", "31/07/2020 09:00:00").Returns(3540); // 59 minutes
                yield return new TestCaseData("31/07/2020 08:10:00", "31/07/2020 09:11:00").Returns(3660); // 1 hour 1 minute
                yield return new TestCaseData("31/07/2020 08:10:00", "31/07/2020 08:10:01").Returns(1); // 1 second

            }
        }
    }

}
