using NUnit.Framework;
using ParkingChargeCalculator.Strategies;
using System;
using System.Collections;

namespace ParkingChargeCalculator.Tests.Strategies
{
    public class ShortStayStrategyTests
    {
        private ShortStayStrategy _shortStayStrategy;

        [SetUp]
        public void Setup()
        { 
            _shortStayStrategy = new ShortStayStrategy();
        }

        [TestCase("27/07/2020 18:00:00", "28/07/2020 07:59:59")] // Monday Evening / Tuesday Morning
        [TestCase("28/08/2020 00:00:00", "29/07/2020 07:59:59")] // Tuesday Evening / Wednesday Morning
        [TestCase("29/08/2020 00:00:00", "30/07/2020 07:59:59")] // Wednesday Evening / Thursday Morning
        [TestCase("30/08/2020 00:00:00", "31/07/2020 07:59:59")] // Thursday Evening / Friday Morning
        [TestCase("31/08/2020 00:00:00", "01/08/2020 07:59:59")] // Friday Evening / Saturday Morning
        [TestCase("01/08/2020 00:00:00", "03/08/2020 07:59:59")] // Weekend / Monday Morning
        public void CalculatePrice_ShouldEqualZero_WhenTimeRangeOutsideOfChargablePeriod(string startDateValue, string endDateValue)
        {
            var startDateTime = DateTime.Parse(startDateValue);
            var endDateTime = DateTime.Parse(endDateValue);

            var result = _shortStayStrategy.CalculatePrice(startDateTime, endDateTime);

            Assert.AreEqual("£0.00", result);
        }

        [TestCaseSource(typeof(ShortStayTestCases), nameof(ShortStayTestCases.ChargablePerodTestCases))]
        public string CalculatePrice_ShouldChargeCorrectRate_WhenTimeRangeInsideOfChargablePeriod(string startDateValue, string endDateValue)
        {
            var startDateTime = DateTime.Parse(startDateValue);
            var endDateTime = DateTime.Parse(endDateValue);

            return _shortStayStrategy.CalculatePrice(startDateTime, endDateTime);
        }

    }
    public class ShortStayTestCases
    {
        public static IEnumerable ChargablePerodTestCases
        {
            get
            {
                yield return new TestCaseData("07/09/2017 08:00:00", "07/09/2017 18:00:00").Returns("£11.00"); // 10 hours, all inside chargable period
                yield return new TestCaseData("07/09/2017 00:00:00", "07/09/2017 23:59:59").Returns("£11.00"); // 23 hours, 59 mins, 59 secs. Only 10 hours chargable
                yield return new TestCaseData("07/09/2017 00:00:00", "08/09/2017 18:00:00").Returns("£22.00"); // 42 hours, 20 chargable 
                yield return new TestCaseData("07/09/2017 00:00:00", "08/09/2017 23:59:59").Returns("£22.00"); // 47 hours, 59 mins, 59 secs. 20 hours
                yield return new TestCaseData("31/07/2020 00:00:00", "03/08/2020 07:59:59").Returns("£11.00"); // Friday (Midnight) to Monday morning (1 sec before chargable period)
                yield return new TestCaseData("31/07/2020 00:00:00", "03/08/2020 23:59:59").Returns("£22.00"); // Friday (Midnight) to Monday Night (1 sec before Midnight)
                yield return new TestCaseData("31/07/2020 08:10:00", "31/07/2020 08:11:00").Returns("£0.02"); // 1 minute
                yield return new TestCaseData("31/07/2020 08:01:00", "31/07/2020 09:00:00").Returns("£1.08"); // 59 minutes
                yield return new TestCaseData("31/07/2020 08:10:00", "31/07/2020 09:11:00").Returns("£1.12"); // 1 hour 1 minute
                yield return new TestCaseData("07/09/2017 16:50:00", "09/09/2017 19:15:00").Returns("£12.28"); // Example given in the email 
                yield return new TestCaseData("07/09/2017 16:50:00", "11/09/2017 19:15:00").Returns("£23.28"); // Example given in the email + 2 extra days
            }
        }
    }

}
