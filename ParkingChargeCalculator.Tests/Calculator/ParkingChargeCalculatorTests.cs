using Moq;
using NUnit.Framework;
using ParkingChargeCalculator.Calculator;
using ParkingChargeCalculator.Strategies.Interfaces;
using System;

namespace ParkingChargeCalculator.Tests.Calculator
{
    public class ParkingChargeCalculatorTests
    {
        private ParkingCostCalculator _parkingChargeCalculator;
        private Mock<IParkingChargeStrategy> _mockStrategy;

        [SetUp]
        public void Setup()
        {
            _parkingChargeCalculator = new ParkingCostCalculator();
            _mockStrategy = new Mock<IParkingChargeStrategy>(MockBehavior.Strict);
        }

        [TestCase("27/07/2020 18:00:00", "27/07/2020 18:00:00")]
        [TestCase("27/07/2020 18:00:01", "27/07/2020 18:00:00")]
        public void Calculate_ShouldThrowExecption_WhenStartDateGtOrEqEndDate(string startDateValue, string endDateTimeValue)
        {
            var startDateTime = DateTime.Parse(startDateValue);
            var endDateTime = DateTime.Parse(endDateTimeValue);

            var ex = Assert.Throws<InvalidOperationException>(() => _parkingChargeCalculator.Calculate(startDateTime, endDateTime));
            Assert.That(ex.Message, Is.EqualTo("The Start Date Time cannot be greater than or equal the End Date Time"));
        }

        [Test]
        public void Calculate_ShouldExecuteStrategy_WhenProvidedValidDates()
        {
            var startDateTime = DateTime.Parse("27/07/2020 18:00:00");
            var endDateTime = DateTime.Parse("27/07/2020 18:00:01");

            _mockStrategy.Setup(x => x.CalculatePrice(It.Is<DateTime>(startParm => startDateTime == startParm), It.Is<DateTime>(endParm => endDateTime == endParm)))
                .Returns("A Test Value");

            _parkingChargeCalculator.SetStrategy(_mockStrategy.Object);

            _parkingChargeCalculator.Calculate(startDateTime, endDateTime);

            _mockStrategy.Verify(x => x.CalculatePrice(It.Is<DateTime>(startParm => startDateTime == startParm), It.Is<DateTime>(endParm => endDateTime == endParm)), Times.Once);
        }
    }
}
