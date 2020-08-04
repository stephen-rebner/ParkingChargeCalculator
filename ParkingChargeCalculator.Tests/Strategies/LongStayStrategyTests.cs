using NUnit.Framework;
using ParkingChargeCalculator.Strategies;
using System;
using System.Collections;

namespace ParkingChargeCalculator.Tests.Strategies
{
    public class LongStayStrategyTests
    {
        private LongStayStrategy _longStayStrategy;

        [SetUp]
        public void Setup()
        { 
            _longStayStrategy = new LongStayStrategy();
        }

        [TestCaseSource(typeof(LongStayTestCases), nameof(LongStayTestCases.TestCases))]
        public string CalculatePrice_ShouldChargeDailyRate_ForEachPartDay(string startDateValue, string endDateValue)
        {
            var startDateTime = DateTime.Parse(startDateValue);
            var endDateTime = DateTime.Parse(endDateValue);

            return _longStayStrategy.CalculatePrice(startDateTime, endDateTime);
        }

    }

    public class LongStayTestCases
    {
        public static IEnumerable TestCases
        {
            get
            {
                yield return new TestCaseData("07/09/2017 00:00:00", "07/09/2017 00:00:01").Returns("£7.50"); // 1 second
                yield return new TestCaseData("07/09/2017 00:00:00", "07/09/2017 23:59:59").Returns("£7.50"); // 23 hours, 59 mins, 59 secs
                yield return new TestCaseData("07/09/2017 00:00:00", "08/09/2017 00:00:00").Returns("£15.00"); // 24 hours, going into new day
                yield return new TestCaseData("07/09/2017 07:50:00", "09/09/2017 05:20:00").Returns("£22.50"); // Example given in email
            }
        }
    }
}
