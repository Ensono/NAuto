using Amido.NAuto.Builders;
using Amido.NAuto.Builders.Services;
using NUnit.Framework;
using Should;

namespace Amido.NAuto.UnitTests.Builders.Services
{
    [TestFixture]
    public class PopulateShortServiceTests
    {
        private PopulateShortService populateShortService;
        private AutoBuilderConfiguration autoBuilderConfiguration;

        [SetUp]
        public void SetUp()
        {
            autoBuilderConfiguration = new AutoBuilderConfiguration();
            populateShortService = new PopulateShortService();
            populateShortService.SetAutoBuilderConfiguration(autoBuilderConfiguration);
        }

        public class Populate : PopulateShortServiceTests
        {
            [Test]
            public void Should_Return_Passed_In_Value_If_Not_Default()
            {
                // Arrange
                const string propertyName = "testname";
                const short currentValue = 1;

                // Act
                var result = populateShortService.Populate(propertyName, currentValue);

                // Assert
                result.ShouldEqual(currentValue);
            }

            [Test]
            public void Should_Return_Convention_Result_If_Convention_Exists()
            {
                // Arrange
                const short testConventionResult = 1234;
                autoBuilderConfiguration.Conventions.Add(new ConventionMap("test", typeof(short), config => testConventionResult));
                const string propertyName = "testname";

                // Act
                var result = populateShortService.Populate(propertyName, 0);

                // Assert
                result.ShouldEqual(testConventionResult);
            }

            [Test]
            public void Should_Return_New_Random_Int_Based_On_Auto_Builder_Configuration_When_No_Convention_Exists()
            {
                // Arrange
                autoBuilderConfiguration.Conventions.Clear();
                const string propertyName = "testname";

                // Act
                var result = populateShortService.Populate(propertyName, 0);

                // Assert
                result.ShouldBeGreaterThanOrEqualTo(autoBuilderConfiguration.ShortMinimum);
                result.ShouldBeLessThanOrEqualTo(autoBuilderConfiguration.ShortMaximum);
            }
        }
    }
}
