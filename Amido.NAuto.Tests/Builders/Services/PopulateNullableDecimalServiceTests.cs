using Amido.NAuto.Builders;
using Amido.NAuto.Builders.Services;
using NUnit.Framework;
using Should;

namespace Amido.NAuto.UnitTests.Builders.Services
{
    [TestFixture]
    public class PopulateNullableDecimalServiceTests
    {
        private PopulateNullableDecimalService populateNullableDecimalService;
        private AutoBuilderConfiguration autoBuilderConfiguration;

        [SetUp]
        public void SetUp()
        {
            autoBuilderConfiguration = new AutoBuilderConfiguration();
            populateNullableDecimalService = new PopulateNullableDecimalService();
            populateNullableDecimalService.SetAutoBuilderConfiguration(autoBuilderConfiguration);
        }

        public class Populate : PopulateNullableDecimalServiceTests
        {
            [Test]
            public void Should_Return_Passed_In_Value_If_Not_Default()
            {
                // Arrange
                const string propertyName = "testname";
                decimal? currentValue = 1.1234M;
                
                // Act
                var result = populateNullableDecimalService.Populate(propertyName, currentValue);

                // Assert
                result.ShouldEqual(currentValue);
            }

            [Test]
            public void Should_Return_Convention_Result_If_Convention_Exists()
            {
                // Arrange
                decimal? testConventionResult = 1234.1234M;
                autoBuilderConfiguration.Conventions.Add(new ConventionMap("test", typeof(decimal?), config => testConventionResult));
                const string propertyName = "testname";

                // Act
                var result = populateNullableDecimalService.Populate(propertyName, null);

                // Assert
                result.ShouldEqual(testConventionResult);
            }

            [Test]
            public void Should_Return_New_Random_Decimal_Based_On_Auto_Builder_Configuration_When_No_Convention_Exists()
            {
                // Arrange
                autoBuilderConfiguration.Conventions.Clear();
                const string propertyName = "testname";

                // Act
                var result = populateNullableDecimalService.Populate(propertyName, null);

                // Assert
                result.ShouldBeGreaterThanOrEqualTo((decimal?)autoBuilderConfiguration.DoubleMinimum);
                result.ShouldBeLessThanOrEqualTo((decimal?)autoBuilderConfiguration.DoubleMaximum);
            }
        }
    }
}
