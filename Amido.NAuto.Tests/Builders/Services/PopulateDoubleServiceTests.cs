using Amido.NAuto.Builders;
using Amido.NAuto.Builders.Services;
using NUnit.Framework;
using Should;

namespace Amido.NAuto.UnitTests.Builders.Services
{
    [TestFixture]
    public class PopulateDoubleServiceTests
    {
        private PopulateDoubleService populateDoubleService;
        private AutoBuilderConfiguration autoBuilderConfiguration;

        [SetUp]
        public void SetUp()
        {
            autoBuilderConfiguration = new AutoBuilderConfiguration();    
            populateDoubleService = new PopulateDoubleService();
            populateDoubleService.SetAutoBuilderConfiguration(autoBuilderConfiguration);
        }

        public class Populate : PopulateDoubleServiceTests
        {
            [Test]
            public void Should_Return_Passed_In_Value_If_Not_Default()
            {
                // Arrange
                const string propertyName = "testname";
                const double currentValue = 1.1234;
                
                // Act
                var result = populateDoubleService.Populate(propertyName, currentValue);

                // Assert
                result.ShouldEqual(currentValue);
            }

            [Test]
            public void Should_Return_Convention_Result_If_Convention_Exists()
            {
                // Arrange
                const double testConventionResult = 1234.1234;
                autoBuilderConfiguration.Conventions.Add(new ConventionMap(ConventionFilterType.Contains, "test", typeof(double), config => testConventionResult));
                const string propertyName = "testname";

                // Act
                var result = populateDoubleService.Populate(propertyName, 0);

                // Assert
                result.ShouldEqual(testConventionResult);
            }

            [Test]
            public void Should_Return_New_Random_String_Based_On_Auto_Builder_Configuration_When_No_Convention_Exists()
            {
                // Arrange
                autoBuilderConfiguration.Conventions.Clear();
                const string propertyName = "testname";

                // Act
                var result = populateDoubleService.Populate(propertyName, 0);

                // Assert
                result.ShouldBeGreaterThanOrEqualTo(autoBuilderConfiguration.DoubleMinimum);
                result.ShouldBeLessThanOrEqualTo(autoBuilderConfiguration.DoubleMaximum);
            }
        }
    }
}
