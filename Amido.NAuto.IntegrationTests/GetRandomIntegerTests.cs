using System;
using NUnit.Framework;
using Shouldly;

namespace Amido.NAuto.IntegrationTests
{
    [TestFixture]
    public class GetRandomIntegerTests
    {
        readonly Random random = new Random();

        [Test]
        public void Should_Return_A_Random_Integer_Which_Is_Less_Than_Or_Equal_To_Max()
        {
            // Arrange
            var max = random.Next(123456);

            // Act
            var result1 = NAuto.GetRandomInteger(max);
            var result2 = NAuto.GetRandomInteger(max);

            // Assert
            result1.ShouldNotBe(result2);
            result1.ShouldBeLessThanOrEqualTo(max);
            result2.ShouldBeLessThanOrEqualTo(max);
        }

        [Test]
        public void Should_Return_A_Random_Integer_Which_Is_Between_Min_And_Max()
        {
            // Arrange
            var min = random.Next(100);
            var max = random.Next(101, 123456);

            // Act
            var result1 = NAuto.GetRandomInteger(min, max);
            var result2 = NAuto.GetRandomInteger(min, max);

            // Assert
            result1.ShouldNotBe(result2);
            result1.ShouldBeGreaterThanOrEqualTo(min);
            result1.ShouldBeLessThanOrEqualTo(max);
            result2.ShouldBeGreaterThanOrEqualTo(min);
            result2.ShouldBeLessThanOrEqualTo(max);
        }

        [Test]
        public void Should_Return_A_Random_Double_Which_Is_Between_Min_And_Max()
        {
            // Arrange
            const double min = 123456.123456;
            const double max = 456789.789456;

            // Act
            var result1 = NAuto.GetRandomDouble(min, max);
            var result2 = NAuto.GetRandomDouble(min, max);

            // Assert
            result1.ShouldNotBe(result2);
            result1.ShouldBeGreaterThanOrEqualTo(min);
            result1.ShouldBeLessThanOrEqualTo(max);
            result2.ShouldBeGreaterThanOrEqualTo(min);
            result2.ShouldBeLessThanOrEqualTo(max);
        } 
    }
}