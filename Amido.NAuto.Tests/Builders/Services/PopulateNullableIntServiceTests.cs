using Amido.NAuto.Builders;
using Amido.NAuto.Builders.Services;
using NUnit.Framework;
using Should;

namespace Amido.NAuto.UnitTests.Builders.Services
{
    [TestFixture]
    public class PopulateNullableIntServiceTests
    {
        private PopulateNullableIntService populateNullableIntService;
        private AutoBuilderConfiguration autoBuilderConfiguration;

        [SetUp]
        public void SetUp()
        {
            autoBuilderConfiguration = new AutoBuilderConfiguration();    
            populateNullableIntService = new PopulateNullableIntService();
            populateNullableIntService.SetAutoBuilderConfiguration(autoBuilderConfiguration);
        }

        public class Populate : PopulateNullableIntServiceTests
        {
            [Test]
            public void Should_Return_Passed_In_Value_If_Not_Default()
            {
                // Arrange
                const string propertyName = "testname";
                const int currentValue = 1;
                
                // Act
                var result = populateNullableIntService.Populate(propertyName, currentValue);

                // Assert
                result.ShouldEqual(currentValue);
            }

            [Test]
            public void Should_Return_Convention_Result_If_Convention_Exists()
            {
                // Arrange
                int? testConventionResult = 1234;
                autoBuilderConfiguration.Conventions.Add(new ConventionMap("test", typeof(int?), config => testConventionResult));
                const string propertyName = "testname";

                // Act
                var result = populateNullableIntService.Populate(propertyName, null);

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
                var result = populateNullableIntService.Populate(propertyName, null);

                // Assert
                result.ShouldBeGreaterThanOrEqualTo(autoBuilderConfiguration.IntMinimum);
                result.ShouldBeLessThanOrEqualTo(autoBuilderConfiguration.IntMaximum);
            }
        }
    }
}
