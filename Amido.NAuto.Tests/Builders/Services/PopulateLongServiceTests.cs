using Amido.NAuto.Builders;
using Amido.NAuto.Builders.Services;
using NUnit.Framework;
using Should;

namespace Amido.NAuto.UnitTests.Builders.Services
{
    [TestFixture]
    public class PopulateLongServiceTests
    {
        private PopulateLongService populateLongService;
        private AutoBuilderConfiguration autoBuilderConfiguration;

        [SetUp]
        public void SetUp()
        {
            autoBuilderConfiguration = new AutoBuilderConfiguration();
            populateLongService = new PopulateLongService();
            populateLongService.SetAutoBuilderConfiguration(autoBuilderConfiguration);
        }

        [TestFixture]
        public class Populate : PopulateLongServiceTests
        {
            [Test]
            public void Should_Return_Passed_In_Value_If_Not_Default()
            {
                // Arrange
                const string propertyName = "CustomerId";
                long currentValue = 1;

                // Act
                var result = populateLongService.Populate(propertyName, currentValue);

                // Assert
                result.ShouldEqual(currentValue);
            }

            [Test]
            public void Should_Return_Convention_Result_If_Convention_Exists()
            {
                // Arrange
                const long testConventionResult = 1234;
                autoBuilderConfiguration.Conventions.Add(new ConventionMap("test", typeof(long), config => testConventionResult));
                const string propertyName = "testname";

                // Act
                var result = populateLongService.Populate(propertyName, 0);

                // Assert
                result.ShouldEqual(testConventionResult);
                result.ShouldBeType<long>();
            }

            [Test]
            public void Should_Return_New_Random_Long_Based_If_Value_Not_Defaulted()
            {
                // Arrange
                const string propertyName = "CustomerGuid";
                long currentValue = 0;

                // Act
                var result = populateLongService.Populate(propertyName, currentValue);

                // Assert
                result.ShouldNotEqual(currentValue);
                result.ShouldBeType<long>();
            }
        }
    }
}